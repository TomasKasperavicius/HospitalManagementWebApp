using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Numerics;

namespace HospitalManagementWebApp.Models
{
    public class AppointmentViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Specialty Specialty { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }

        public string Address { get; set; }
    }
}
