using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalManagementWebApp.Controllers
{
    public class UserController(IUserService userService) : Controller
    {
        [Authorize]
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
        [AllowAnonymous]
        public IActionResult DoctorList()
        {
            var doctors = userService.GetDoctors();
            return View(doctors);
        }
        [Authorize]
        public IActionResult ReserveAppointment(ReserveAppointmentModel reserveAppointmentModel)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 

            if (id != reserveAppointmentModel.UserID.ToString())
            {
                return Unauthorized();
            }
            
            var newReserveAppointmentModel = userService.ReserveAppointment(reserveAppointmentModel);
            if (newReserveAppointmentModel == null)
            {
                return View("Index");
            }
            return RedirectToAction("Index", "User", new { doctorID = newReserveAppointmentModel.DoctorID, Date = newReserveAppointmentModel.Date });
        }
        [Authorize]
        public IActionResult Appointments(int patientID)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (id != patientID.ToString())
            {
                return Unauthorized();
            }
            var appointments = userService.GetPatientAppointments(patientID);
            return View(appointments);
        }
        [Authorize]
        public IActionResult CancelAppointment(int appointmentID)
        {
            var patientID = userService.CancelAppointment(appointmentID);
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id != patientID.ToString())
            {
                return Unauthorized();
            }
            if (patientID != null)
            {
                return RedirectToAction("Appointments", "User", new { patientID = patientID });
            }
            return RedirectToAction("Index", "User");
        }
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("JwtToken"); 
            return RedirectToAction("Login", "Auth");
        }
    }
}
