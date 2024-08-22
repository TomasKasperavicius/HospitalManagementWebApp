namespace HospitalManagementWebApp.Models
{
    public class WorkSchedule
    {
        public int ID { get; set; }
        public int DoctorID { get; set; }
        public TimeSpan WorkStartTime { get; set; }
        public TimeSpan WorkEndTime { get; set; }
        public Day Day { get; set; }
        public TimeSpan LunchBreakStart { get; set; }
        public TimeSpan LunchBreakDuration { get; set; }

    }
    public enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

}
