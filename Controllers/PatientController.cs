using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalManagementWebApp.Controllers
{
    public class PatientController(IPatientService userService) : Controller
    {
        
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
            return RedirectToAction("Index", "Doctor", new { doctorID = newReserveAppointmentModel.DoctorID, Date = newReserveAppointmentModel.Date });
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
            return RedirectToAction("Index", "Doctor");
        }
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("JwtToken"); 
            return RedirectToAction("Login", "Auth");
        }
    }
}
