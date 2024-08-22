using HospitalManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagementWebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login(LoginCrediantials login)
        {
            if (ModelState.IsValid)
            {
                // Auth the user
                return RedirectToAction("Index", "Home");
            }
            return View(login);

        }

        public IActionResult Register(Patient patient)
        {
            if (ModelState.IsValid)
            {
                // Register the user
                return RedirectToAction("Index", "Home");
            }
            return View(patient);
        }
    }
}
