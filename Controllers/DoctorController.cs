using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementWebApp.Controllers
{
    public class DoctorController(IDoctorService doctorService, IPatientService patientService) : Controller
    {
        [Authorize]
        public IActionResult Index(int? doctorID, DateTime? date)
        {
            if (doctorID != null)
            {
                DateTime newDate = date ?? DateTime.Today;
                ViewBag.Date = newDate;
                var appointments = doctorService.GetDoctorAppointments(doctorID, newDate);
                ViewBag.DoctorID = doctorID;
                return View(appointments);
            }
            return RedirectToAction("DoctorList", "Doctor");
        }
        [Authorize(Roles = "Doctor")]
        public IActionResult Appointments(int doctorID, DateTime? date)
        {
            DateTime newDate = date ?? DateTime.Today;
            ViewBag.Date = newDate;
            var appointments = doctorService.GetDoctorAppointments(doctorID, newDate);
            List<DoctorAppointmentView> doctorAppointmentViews = new List<DoctorAppointmentView>();
            foreach (var appointment in appointments)
            {
                var patientID = appointment.UserID;
                if (patientID.HasValue)
                {
                    var patient = patientService.GetPatient(patientID.Value);
                    if (patient != null && appointment != null)
                    {

                        doctorAppointmentViews.Add(new DoctorAppointmentView
                        {
                            patientFullName = patient.FirstName + " " + patient.LastName,
                            appointment = appointment
                        });
                    }
                }
            }
            ViewBag.DoctorID = doctorID;
            return View(doctorAppointmentViews);
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
