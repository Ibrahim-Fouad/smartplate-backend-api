using System;

namespace SmartPlate.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetAge(this DateTime date)
        {
            return DateTime.Now.Year - date.Year;
        }

        public static DateTime GetCarEndDate(this DateTime date)
        {
            return date.AddYears(10);
        }

        //Check for if license end date is bigger than today.
        public static bool IsVaild(this DateTime date)
        {
            return date > DateTime.Now;
        }
    }
}