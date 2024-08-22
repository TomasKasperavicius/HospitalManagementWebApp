using System.ComponentModel.DataAnnotations;

namespace HospitalManagementWebApp.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
