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


        public static string CheckStoledObject(this byte obj)
        {
            return obj == 0 ? "Car" : "Plate";
        }
    }
}