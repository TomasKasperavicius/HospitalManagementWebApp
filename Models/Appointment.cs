namespace HospitalManagementWebApp.Models
{
    public class Appointment
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int DoctorID { get; set; }
        public DateTime Date { get; set; }
        public Address Address { get; set; }
        public Status Status { get; set; }
    }
    public enum Status
    {
        Free,
        Occupied
    }
}
