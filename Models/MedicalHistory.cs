namespace HospitalManagementWebApp.Models
{
    public class MedicalHistory
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string Condition { get; set; }
        public DateTime DateDiagnosed { get; set; }
        public string Treatment { get; set; }
        public string Notes { get; set; }
    }
}
