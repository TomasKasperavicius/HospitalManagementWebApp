using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Models.Dto;
using HospitalManagementWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagementWebApp.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {
        [AllowAnonymous]
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
                    var jwt = authService.GenerateJwt(new JwtDTO
                    {
                        ID = user.ID,
                        Email = user.Email,
                        Role = user.Role,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    });
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddHours(1),
                        SameSite = SameSiteMode.None,
                        Path = "/"
                    };
                    Response.Cookies.Append("JwtToken", jwt, cookieOptions);
                    return RedirectToAction("Index", "Home");
                }
                // Wrong password
                return View(login);
            }
            return View(login);
        }
        [AllowAnonymous]
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
                var jwt = authService.GenerateJwt(new JwtDTO
                {
                    ID = patient.ID,
                    Email = patient.Email,
                    Role = patient.Role,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                });
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                Response.Cookies.Append("JwtToken", jwt, cookieOptions);
                return RedirectToAction("Index", "Home");
            }
            return View(patient);
        }
        [Authorize(Roles = "Admin")]
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