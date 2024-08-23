using HospitalManagementWebApp.Models;
using System.ComponentModel;

namespace HospitalManagementWebApp
{
    public class Utils
    {
        public static string GetEnumDescription(Enum value)
        {
            var descriptionAttribute = (DescriptionAttribute)value
                .GetType()
                .GetMember(value.ToString())[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault();

            return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
        }
        public static Day ConvertToDay(DayOfWeek day)
        {
            return (Day)((int)day == 0 ? 6 : (int)day - 1);
        }

        public static DayOfWeek ConvertToDayOfWeek(Day customDay)
        {
            return (DayOfWeek)((int)customDay == 6 ? 0 : (int)customDay + 1);
        }
    }
}
