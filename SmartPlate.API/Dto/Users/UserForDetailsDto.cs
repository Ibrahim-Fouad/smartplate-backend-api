using Newtonsoft.Json;

namespace SmartPlate.API.Dto.Users
{
    public class UserForDetailsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string BloodType { get; set; }
        public string EducationalQualification { get; set; }

        [JsonIgnore] public bool Success { get; set; } = true;
        [JsonIgnore] public string ErrorMessage { get; set; }
    }
}