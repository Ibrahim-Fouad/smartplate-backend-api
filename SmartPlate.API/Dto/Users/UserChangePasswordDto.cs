using System.ComponentModel.DataAnnotations;

namespace SmartPlate.API.Dto.Users
{
    public class UserChangePasswordDto
    {
        [Required]
        [StringLength(50)]
        [MinLength(8)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(8)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }
    }
}
