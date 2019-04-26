using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlate.API.Dto.Traffics
{
    public class TrafficForCreateDto
    {
        [Required] [StringLength(100)] public string Name { get; set; }

        [Required] [StringLength(100)] public string Governorate { get; set; }

        public string PhoneNumber { get; set; }

        [Required] [StringLength(100)] public string Address { get; set; }

        public string Email { get; set; }
    }
}