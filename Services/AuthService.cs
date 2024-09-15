using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;
using HospitalManagementWebApp.Services.Interfaces;

namespace HospitalManagementWebApp.Services
{
    public class AuthService(IPatientRepository patientRepository, IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository, IAddressRepository addressRepository, IWorkScheduleRepository workScheduleRepository,IWebHostEnvironment hostEnvironment) : IAuthService
    {
        
        public IUser? Login(LoginCrediantials loginCrediantials)
        {
            var patient = patientRepository.Find(p => p.Email == loginCrediantials.Email).FirstOrDefault();
            if (patient == null)
            {
                var doctor = doctorRepository.Find(d => d.Email == loginCrediantials.Email).FirstOrDefault();
                return doctor;
            }
            return patient;
        }

        public Patient? RegisterPatient(Patient patient)
        {
            var existingPatient = patientRepository.Find(p => p.Email == patient.Email).FirstOrDefault();
            if (existingPatient == null)
            {
                patientRepository.Add(patient);
                return patient;
            }
            return existingPatient;
        }

        public Doctor? RegisterDoctor(RegisterDoctorViewModel registerDoctorViewModel)
        {
            var existingDoctor = doctorRepository.Find(d => d.Email == registerDoctorViewModel.Email).FirstOrDefault();
            if (existingDoctor == null)
            {
                var newAddress  = CreateAddress(registerDoctorViewModel);
                var newDoctor = CreateDoctor(registerDoctorViewModel, newAddress.ID);
                if (newDoctor != null)
                {
                    CreateWorkSchedule(registerDoctorViewModel, newDoctor.ID);
                    CreateAppointments(registerDoctorViewModel, newDoctor.ID, newAddress.ID);
                    return newDoctor;
                }
                return null;
            }
            return existingDoctor;
        }
        private Address CreateAddress(RegisterDoctorViewModel registerDoctorViewModel)
        {
            var newAddress = new Address
            {
                City = registerDoctorViewModel.City,
                State = registerDoctorViewModel.State,
                Country = registerDoctorViewModel.Country,
                Street = registerDoctorViewModel.Street,
            };
            addressRepository.Add(newAddress);
            return newAddress;
        }
        private Doctor? CreateDoctor(RegisterDoctorViewModel registerDoctorViewModel, int addressID)
        {
            string? image = SaveImage(registerDoctorViewModel.Image);
            if (string.IsNullOrEmpty(image))
            {
                return null;
            }
            var newDoctor = new Doctor
            {
                AddressID = addressID,
                Email = registerDoctorViewModel.Email,
                FirstName = registerDoctorViewModel.FirstName,
                LastName = registerDoctorViewModel.LastName,
                Password = registerDoctorViewModel.Password,
                Phone = registerDoctorViewModel.Phone,
                Image = image,
                Role = Role.Doctor,
                Specialty = registerDoctorViewModel.Specialty,
            };
            doctorRepository.Add(newDoctor);
            return newDoctor;
        }
        private void CreateWorkSchedule(RegisterDoctorViewModel registerDoctorViewModel, int doctorID)
        {
            List<WorkSchedule> workSchedule = new();
            foreach (Day day in Enum.GetValues(typeof(Day)))
            {
                workSchedule.Add(new WorkSchedule
                {
                    Day = day,
                    DoctorID = doctorID,
                    WorkStartTime = registerDoctorViewModel.WorkStartTimes[day],
                    WorkEndTime = registerDoctorViewModel.WorkEndTimes[day],
                    LunchBreakStart = registerDoctorViewModel.LunchBreakStarts[day],
                    LunchBreakDuration = registerDoctorViewModel.LunchBreakDurations[day]
                });
            }
            workScheduleRepository.AddRange(workSchedule);
        }
        private void CreateAppointments(RegisterDoctorViewModel registerDoctorViewModel, int doctorID, int addressID)
        {
            // Create appointments based on work schedule
            // Get the current date and determine the end date (3 months later)
            List<Appointment> appointments = new List<Appointment>();
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
                            AddressID = addressID,
                            Date = currentDate.Add(currentTime),
                            DoctorID = doctorID,
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
            appointmentRepository.AddRange(appointments);
        }
        private string? SaveImage(IFormFile image)
        {
            try
            {
                if (image != null && image.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".jfif", ".svg" };

                    var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(extension))
                    {
                        return null;
                    }

                    var uploadDir = "Uploads/Images";
                    var uploadPath = Path.Combine(hostEnvironment.WebRootPath, uploadDir).Replace("\\", "/");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                    var filePath = Path.Combine(uploadPath, uniqueFileName).Replace("\\", "/");

                    using var stream = new FileStream(filePath, FileMode.Create);
                    image.CopyTo(stream);
                    return Path.Combine(uploadDir, uniqueFileName).Replace("\\", "/");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
