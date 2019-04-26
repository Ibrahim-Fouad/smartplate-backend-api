using System.ComponentModel.DataAnnotations;

namespace SmartPlate.API.Models
{
    public class Traffic
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Governorate { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public string Email { get; set; }
    }
}
