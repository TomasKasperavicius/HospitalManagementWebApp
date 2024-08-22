using HospitalManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly HospitalManagerDbContext _context;
        public UserController(HospitalManagerDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DoctorList()
        {
            List<Doctor> doctors = _context.doctors.ToList();
            var query = from doctor in _context.doctors
                        join address in _context.addresses on doctor.AddressID equals address.ID
                        select new DoctorListViewModel
                        {
                            ID = doctor.ID,
                            Name = doctor.FirstName + " " + doctor.LastName,
                            Email = doctor.Email,
                            Phone = doctor.Phone,
                            Specialty = (Specialty)doctor.Specialty,
                            Address = address.Street + ", " + address.City + ", " + address.State + ", " + address.Country
                        };

            var results = query.ToList();
            return View(results);
        }
    }
}
