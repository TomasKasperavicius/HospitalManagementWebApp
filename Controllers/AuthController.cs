using HospitalManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace HospitalManagementWebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                // Auth the user
                return RedirectToAction("Index", "Home");
            }
            return View(userLogin);

        }

        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Register the user
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
    }
}
