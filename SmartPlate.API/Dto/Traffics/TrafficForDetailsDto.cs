using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartPlate.API.Dto.Traffics
{
    public class TrafficForDetailsDto
    {
        public int TrafficId { get; set; }
        [Required] [StringLength(100)] public string Name { get; set; }

        [Required] [StringLength(100)] public string Governorate { get; set; }

        public string PhoneNumber { get; set; }

        [Required] [StringLength(100)] public string Address { get; set; }

        public string Email { get; set; }


        [JsonIgnore] public bool Success { get; set; } = true;


        [JsonIgnore] public string ErrorMessage { get; set; }
    }
}