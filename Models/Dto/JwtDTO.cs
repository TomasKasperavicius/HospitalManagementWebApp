
namespace HospitalManagementWebApp.Models.Dto
{
    public class JwtDTO
    {
        public int ID {  get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
