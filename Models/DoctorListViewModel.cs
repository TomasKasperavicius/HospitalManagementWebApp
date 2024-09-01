namespace HospitalManagementWebApp.Models
{
    public class DoctorListViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public Specialty Specialty { get; set; }

    }
}
