using Newtonsoft.Json;
using SmartPlate.API.Dto.Cars;
using System;

namespace SmartPlate.API.Dto.StolenCars
{
    public class StolenCarForDetailsDto
    {
        public int Id { get; set; }

        public CarForSimpleDetailsDto Car { get; set; }

        public string LastLocation { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public DateTime DateStoled { get; set; }

        public DateTime DateCreated { get; set; }

        public string ObjectStoled { get; set; }

        public bool HasReturenedToOwner { get; set; }

        public DateTime DateReturened { get; set; }

        [JsonIgnore] public bool Success { get; set; } = true;

        [JsonIgnore] public string ErrorMessage { get; set; }
    }
}