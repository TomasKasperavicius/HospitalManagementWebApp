﻿using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementWebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
