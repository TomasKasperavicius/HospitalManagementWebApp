using System.ComponentModel.DataAnnotations;

namespace HospitalManagementWebApp.Models
{
    public class RegisterDoctorViewModel
    {
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
        public Dictionary<Day, TimeSpan> WorkStartTimes { get; set; } = [];
        public Dictionary<Day, TimeSpan> WorkEndTimes { get; set; } = [];
        public Dictionary<Day, TimeSpan> LunchBreakStarts { get; set; } = [];
        public Dictionary<Day, TimeSpan> LunchBreakDurations { get; set; } = [];
        [Required]
        public Specialty Specialty { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        public RegisterDoctorViewModel()
        {
            WorkStartTimes = InitializeDictionary();
            WorkEndTimes = InitializeDictionary();
            LunchBreakStarts = InitializeDictionary();
            LunchBreakDurations = InitializeDictionary();
        }

        private Dictionary<Day, TimeSpan> InitializeDictionary()
        {
            var dict = new Dictionary<Day, TimeSpan>();
            foreach (Day day in Enum.GetValues(typeof(Day)))
            {
                dict[day] = TimeSpan.Zero;
            }
            return dict;
        }
    }
}
