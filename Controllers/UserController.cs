using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementWebApp.Controllers
{
    public class UserController(IUserService userService) : Controller
    {
        public IActionResult Index(int? doctorID, DateTime? date)
        {
            if (doctorID != null)
            {
                DateTime newDate = date ?? DateTime.Today;
                ViewBag.Date = newDate;
                List<Appointment> appointments = userService.GetDoctorAppointments(doctorID, newDate);
                ViewBag.DoctorID = doctorID;
                return View(appointments);
            }
            return RedirectToAction("DoctorList", "User");
        }

        public IActionResult DoctorList()
        {
            var doctors = userService.GetDoctors();
            return View(doctors);
        }
        public IActionResult ReserveAppointment(ReserveAppointmentModel reserveAppointmentModel)
        {
            var newReserveAppointmentModel = userService.ReserveAppointment(reserveAppointmentModel);
            if (newReserveAppointmentModel == null)
            {
                return View("Index");
            }
            return RedirectToAction("Index", "User", new { doctorID = newReserveAppointmentModel.DoctorID, Date = newReserveAppointmentModel.Date });
        }
        public IActionResult Appointments(int patientID)
        {
            var appointments = userService.GetPatientAppointments(patientID);
            return View(appointments);
        }
        public IActionResult CancelAppointment(int appointmentID)
        {
            var patientID = userService.CancelAppointment(appointmentID);
            if (patientID != null)
            {
                return RedirectToAction("Appointments", "User", new { patientID = patientID });
            }
            return RedirectToAction("Index", "User");
        }
    }
}
