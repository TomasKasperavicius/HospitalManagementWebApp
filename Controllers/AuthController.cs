using HospitalManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagementWebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly HospitalManagerDbContext _context;
        public AuthController(HospitalManagerDbContext context)
        {
            _context = context;
        }
        public IActionResult Login(LoginCrediantials login)
        {
            if (ModelState.IsValid)
            {
                var user = _context.patients.FirstOrDefault(p => p.Email == login.Email);
                // Wrong email
                if (user == null)
                {
                    return View(login);
                }
                // Auth the user
                if(login.Password == user.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                // Wrong password
                return View(login);
            }
            return View(login);

        }

        public async Task<IActionResult> Register(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var user = _context.patients.FirstOrDefault(p => p.Email == patient.Email);
                // User already exists with this email
                if(user != null)
                {
                    return View(patient);
                }
                // Register the user
                await _context.patients.AddAsync(patient);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(patient);
        }
    }
}
