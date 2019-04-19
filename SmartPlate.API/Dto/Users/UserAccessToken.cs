using Newtonsoft.Json;

namespace SmartPlate.API.Dto.Users
{
    public class UserAccessToken
    {
        public string AccessToken { get; set; }

        [JsonIgnore] public bool Success { get; set; }

        [JsonIgnore] public string ErrorMessage { get; set; }
    }
}