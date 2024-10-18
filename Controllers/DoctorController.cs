using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementWebApp.Controllers
{
    public class DoctorController(IDoctorService doctorService) : Controller
    {
        [Authorize]
        public IActionResult Index(int? doctorID, DateTime? date)
        {
            if (doctorID != null)
            {
                DateTime newDate = date ?? DateTime.Today;
                ViewBag.Date = newDate;
                List<Appointment> appointments = doctorService.GetDoctorAppointments(doctorID, newDate);
                ViewBag.DoctorID = doctorID;
                return View(appointments);
            }
            return RedirectToAction("DoctorList", "Doctor");
        }
        [AllowAnonymous]
        public IActionResult DoctorList()
        {
            var doctors = doctorService.GetDoctors();
            return View(doctors);
        }
        [AllowAnonymous]
        public IActionResult DoctorProfile(int doctorID)
        {
            var doctor = doctorService.GetDoctor(doctorID);
            return View(doctor);
        }
    }
}
