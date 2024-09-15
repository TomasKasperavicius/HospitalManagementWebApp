using System.ComponentModel.DataAnnotations;

namespace HospitalManagementWebApp.Models
{
    public interface IUser
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
        public Role Role { get; set; }
    }
}
