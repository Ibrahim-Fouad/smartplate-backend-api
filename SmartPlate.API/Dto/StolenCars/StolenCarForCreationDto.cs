using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPlate.API.Dto.StolenCars
{
    public class StolenCarForCreationDto
    {

        public int CarId { get; set; }

        public string PlateNumber { get; set; }

        public string LastLocation { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        [Required]
        public DateTime DateStoled { get; set; }

        /// <summary>
        /// 0 For car, 1 is plate,
        /// Ignore it.
        /// </summary>
        
        public byte CarOrPlateIsStoled { get; set; } = 0; // 0 For car, 1 is plate

    }
}
