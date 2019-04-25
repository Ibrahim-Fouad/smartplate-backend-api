using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPlate.API.Dto.Users
{
    public class UserForRegisterDto
    {
        [Required] public string Id { get; set; }
        [Required] public string Name { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string ConfirmPassword { get; set; }

        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        [Required] public string BloodType { get; set; }
        [Required] public string EducationalQualification { get; set; }
    }
}