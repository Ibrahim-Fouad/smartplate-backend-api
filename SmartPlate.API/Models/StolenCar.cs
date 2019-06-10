using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPlate.API.Models
{
    public class StolenCar
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        public string LastLocation { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public DateTime DateStoled { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public bool HasReturenedToOwner { get; set; }

        public DateTime DateReturened { get; set; }

        public byte CarOrPlateIsStoled { get; set; } // 0 => Car, 1 => Plate
    }
}
