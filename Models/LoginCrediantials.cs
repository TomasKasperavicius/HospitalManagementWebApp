﻿using System.ComponentModel.DataAnnotations;

namespace HospitalManagementWebApp.Models
{
    public class LoginCrediantials
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
