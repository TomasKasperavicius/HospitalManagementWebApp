using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;


namespace HospitalManagementWebApp.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {
        public IActionResult Login(LoginCrediantials login)
        {
            if (ModelState.IsValid)
            {
                var user = authService.Login(login);
                // Wrong email
                if (user == null)
                {
                    return View(login);
                }
                // Auth the user
                else if (login.Password == user.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                // Wrong password
                return View(login);
            }
            return View(login);
        }

        public IActionResult RegisterPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var existingPatient = authService.RegisterPatient(patient);
                // Patient already exists with this email
                if (existingPatient != null)
                {
                    return View(patient);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(patient);
        }

        public IActionResult RegisterDoctor(RegisterDoctorViewModel registerDoctorViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingDoctor = authService.RegisterDoctor(registerDoctorViewModel);
                // Doctor already exists with this email
                if (existingDoctor != null)
                {
                    return View(registerDoctorViewModel);
                }
                return RedirectToAction("Index", "Home");
            }

            return View(registerDoctorViewModel);
        }
        
    }
}