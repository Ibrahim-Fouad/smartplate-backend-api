using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPlate.API.Dto.StolenCars
{
    public class StolenCarForCreationDto
    {
        [Required]
        public int CarId { get; set; }

        [Required]
        public string LastLocation { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public DateTime DateStoled { get; set; }

        /// <summary>
        /// 0 For car, 1 is plate
        /// </summary>
        [Required]
        public byte CarOrPlateIsStoled { get; set; } // 0 For car, 1 is plate

    }
}
