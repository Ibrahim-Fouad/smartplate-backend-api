using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SmartPlate.API.Models
{
    public class Traffic
    {
        public Traffic()
        {
            Cars = new Collection<Car>();
        }
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

        public ICollection<Car> Cars { get; set; }
    }
}
