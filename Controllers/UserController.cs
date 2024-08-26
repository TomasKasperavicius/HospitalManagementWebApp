using HospitalManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HospitalManagementWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly HospitalManagerDbContext _context;
        public UserController(HospitalManagerDbContext context)
        {
            _context = context;
        }
        public IActionResult  Index(int? doctorID, DateTime? Date)
        {
            //ViewBag.Date = Date ?? DateTime.Today;
            if (doctorID != null)
            {
                var doctor = _context.doctors.Find(doctorID);
                if (doctor != null)
                {
                    DateTime date = Date ?? DateTime.Today;
                    ViewBag.Date = date;
                    var query = from a in _context.appointments
                                join d in _context.doctors on a.DoctorID equals d.ID
                                where a.Date.Year == date.Year &&
                                      a.Date.Month == date.Month &&
                                      a.Date.Day == date.Day
                                select new Appointment
                                {
                                    AddressID = doctor.AddressID,
                                    Date = a.Date,
                                    ID = a.ID,
                                    DoctorID = a.DoctorID,
                                    Status = a.Status,
                                    UserID = a.UserID,
                                };

                    List<Appointment> results = query.ToList();
                    ViewBag.DoctorID = doctorID;
                    return View(results);
                }
            }

            return RedirectToAction("DoctorList","User");
        }
       
            public IActionResult DoctorList()
        {
            List<Doctor> doctors = _context.doctors.ToList();
            var query = from doctor in _context.doctors
                        join address in _context.addresses on doctor.AddressID equals address.ID
                        select new DoctorListViewModel
                        {
                            ID = doctor.ID,
                            Name = doctor.FirstName + " " + doctor.LastName,
                            Email = doctor.Email,
                            Phone = doctor.Phone,
                            Specialty = (Specialty)doctor.Specialty,
                            Address = address.Street + ", " + address.City + ", " + address.State + ", " + address.Country
                        };

            var results = query.ToList();
            return View(results);
        }
        public IActionResult ReserveAppointment(ReserveAppointmentModel reserveAppointmentModel)
        {
            var doctor = _context.doctors.Find(reserveAppointmentModel.DoctorID);
            if (doctor == null) {
                return View("Index");
            }
            var appointment = _context.appointments
                        .Where(a => a.DoctorID == reserveAppointmentModel.DoctorID && a.Date == reserveAppointmentModel.Date)
                        .ToList();
            appointment[0].UserID = reserveAppointmentModel.UserID;
            appointment[0].Status = Status.Occupied;

            _context.appointments.Update(appointment[0]);
            _context.SaveChanges();

            return RedirectToAction("Index", "User", new { doctorID = reserveAppointmentModel.DoctorID, Date = reserveAppointmentModel.Date });
        }
        public IActionResult Appointments(int patientID)
        {
            var query = from patient in _context.patients
                        join appointment in _context.appointments on patient.ID equals appointment.UserID
                        join doctor in _context.doctors on appointment.DoctorID equals doctor.ID
                        join address in _context.addresses on appointment.AddressID equals address.ID
                        where patient.ID == patientID
                        select new AppointmentViewModel
                        {
                            ID = appointment.ID,
                            Name = doctor.FirstName + " " + doctor.LastName,
                            Email = doctor.Email,
                            Phone = doctor.Phone,
                            Specialty = (Specialty)doctor.Specialty,
                            Date = appointment.Date,
                            Address = address.Street + ", " + address.City + ", " + address.State + ", " + address.Country
                        };

            var results = query.ToList();
            return View(results);
        }
    }
}
