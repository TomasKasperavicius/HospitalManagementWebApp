using System.ComponentModel.DataAnnotations;

namespace HospitalManagementWebApp.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        Role Role { get; set; }

    }
    public enum Role { 
        Admin,
        Patient,
        Doctor
    }

}
