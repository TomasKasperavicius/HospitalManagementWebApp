using HospitalManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HospitalManagementWebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly HospitalManagerDbContext _context;
        public AuthController(HospitalManagerDbContext context)
        {
            _context = context;
        }
        public IActionResult Login(LoginCrediantials login)
        {
            if (ModelState.IsValid)
            {
                var user = _context.patients.FirstOrDefault(p => p.Email == login.Email);
                // Wrong email
                if (user == null)
                {
                    return View(login);
                }
                // Auth the user
                else if(login.Password == user.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                var doctor = _context.doctors.FirstOrDefault(p => p.Email == login.Email);
                // Wrong email
                if (doctor == null)
                {
                    return View(login);
                }
                // Auth the user
                else if(login.Password == doctor.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                // Wrong password
                return View(login);
            }
            return View(login);

        }

        public async Task<IActionResult> Register(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var user = _context.patients.FirstOrDefault(p => p.Email == patient.Email);
                // User already exists with this email
                if(user != null)
                {
                    return View(patient);
                }
                // Register the user
                await _context.patients.AddAsync(patient);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(patient);
        }
       
        public async Task<IActionResult> RegisterDoctor(RegisterDoctorViewModel registerDoctorViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.doctors.FirstOrDefaultAsync(p => p.Email == registerDoctorViewModel.Email);
                // User already exists with this email
                if (user != null)
                {
                    return View(registerDoctorViewModel);
                }
                var newAddress = new Address
                {
                    City = registerDoctorViewModel.City,
                    State = registerDoctorViewModel.State,
                    Country = registerDoctorViewModel.Country,
                    Street = registerDoctorViewModel.Street,
                };
                // Register address
                await _context.addresses.AddAsync(newAddress);
                await _context.SaveChangesAsync();
                
                // Register the user
                var newDoctor = new Doctor
                {
                    AddressID = newAddress.ID,
                    Email = registerDoctorViewModel.Email,
                    FirstName = registerDoctorViewModel.FirstName,
                    LastName = registerDoctorViewModel.LastName,
                    Password = registerDoctorViewModel.Password,
                    Phone = registerDoctorViewModel.Phone,
                    Role = Role.Doctor,
                    Specialty = registerDoctorViewModel.Specialty,
                };
                await _context.doctors.AddAsync(newDoctor);
                await _context.SaveChangesAsync();
                // Create work schedule
                List<WorkSchedule> workSchedule = new();
                foreach (Day day in Enum.GetValues(typeof(Day)))
                {
                    workSchedule.Add(new WorkSchedule
                    {
                        Day = day,
                        DoctorID = newDoctor.ID,
                        WorkStartTime = registerDoctorViewModel.WorkStartTimes[day],
                        WorkEndTime = registerDoctorViewModel.WorkEndTimes[day],
                        LunchBreakStart = registerDoctorViewModel.LunchBreakStarts[day],
                        LunchBreakDuration = registerDoctorViewModel.LunchBreakDurations[day]
                    });

                }
                await _context.workSchedules.AddRangeAsync(workSchedule);
                await _context.SaveChangesAsync();
                List<Appointment> appointments = new List<Appointment>();
                // Create appointments based on work schedule

                // Get the current date and determine the end date (3 months later)
                DateTime currentDate = DateTime.Today;
                DateTime endDate = currentDate.AddMonths(3);

                while (currentDate < endDate)
                {
                    Day today = Utils.ConvertToDay(currentDate.DayOfWeek);
                    // Find the work schedule for the current day
                    TimeSpan todayWorkStart = registerDoctorViewModel.WorkStartTimes[today];
                    TimeSpan todayWorkEnd = registerDoctorViewModel.WorkEndTimes[today];
                    TimeSpan todayLunchBreakStart = registerDoctorViewModel.LunchBreakStarts[today];
                    TimeSpan todayLunchBreakDuration = registerDoctorViewModel.LunchBreakDurations[today];

                    // Create appointments based on the work schedule
                    TimeSpan currentTime = todayWorkStart;
                    TimeSpan todaLunchBreakEnd = todayLunchBreakStart + todayLunchBreakDuration;

                    while (currentTime < todayWorkEnd)
                    {
                        // Check if the current time is within the lunch break
                        if (currentTime < todayLunchBreakStart || currentTime >= todaLunchBreakEnd)
                        {
                            appointments.Add(new Appointment
                            {
                                AddressID = newAddress.ID,
                                Date = currentDate.Add(currentTime),
                                DoctorID = newDoctor.ID,
                                Status = Status.Free,
                                UserID = null
                            });
                        }
                        // Increment the time by 30 minutes
                        currentTime = currentTime.Add(TimeSpan.FromMinutes(30));
                    }
                    // Move to the next day
                    currentDate = currentDate.AddDays(1);
                }
                await _context.appointments.AddRangeAsync(appointments);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
           
            return View(registerDoctorViewModel);
        }
    }
}
