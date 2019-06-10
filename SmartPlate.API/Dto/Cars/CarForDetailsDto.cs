using Newtonsoft.Json;
using SmartPlate.API.Dto.Traffics;
using SmartPlate.API.Dto.Users;
using System;

namespace SmartPlate.API.Dto.Cars
{
    public class CarForDetailsDto
    {
        public int Id { get; set; }

        public string PlateNumber { get; set; }

        public string ShaseehNumber { get; set; }

        public string MotorNumber { get; set; }

        public string Color { get; set; }

        public int ModelYear { get; set; }

        public string ModelMarka { get; set; }

        public string CarModel { get; set; }

        public string VechileType { get; set; }

        public string Fuel { get; set; }

        public int Capacity { get; set; }

        public int Passengers { get; set; }

        public int LoadWeight { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool LicenseIsValid { get; set; }

        public bool ReportedAsStolen { get; set; }

        public UserForDetailsDto User { get; set; }

        public TrafficForDetailsDto Traffic { get; set; }

        [JsonIgnore] public bool Success { get; set; } = true;
        [JsonIgnore] public string ErrorMessage { get; set; }
    }
}