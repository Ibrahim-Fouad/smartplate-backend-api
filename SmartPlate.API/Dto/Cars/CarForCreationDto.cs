using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPlate.API.Dto.Cars
{
    public class CarForCreationDto
    {
        [Required] public string PlateNumber { get; set; }

        [Required] public string ShaseehNumber { get; set; }

        [Required] public string MotorNumber { get; set; }

        [Required] public string Color { get; set; }

        [Required] public int ModelYear { get; set; }

        [Required] public string ModelMarka { get; set; }

        [Required] public string CarModel { get; set; }

        [Required] public string VechileType { get; set; }

        [Required] public string Fuel { get; set; }

        [Required] public int Capacity { get; set; }

        [Required] public int Passengers { get; set; }

        [Required] public int LoadWeight { get; set; }

        [Required] public DateTime StartDate { get; set; }

        [Required] public string UserId { get; set; }
        [Required] public int TrafficId { get; set; }
    }
}