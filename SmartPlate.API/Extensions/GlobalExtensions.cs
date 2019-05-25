using System;
using Microsoft.AspNetCore.Http;

namespace SmartPlate.API.Extensions
{
    public static class GlobalExtensions
    {
        public static void AddApplicationError(this HttpResponse response, string errorMessage)
        {
            response.Headers.Add("Application-Error", errorMessage);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

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