using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPlate.API.Models.Users
{
    public class Officer : IUser
    {
        public string Id { get; set; }

        [Required] [StringLength(100)] public string Name { get; set; }

        [Required] public DateTime DateOfBirth { get; set; }

        [Required] [StringLength(100)] public string Address { get; set; }
        public string PhoneNumber { get; set; }

        [EmailAddress] public string Email { get; set; }
        public string ImagePath { get; set; }

        [StringLength(5)] [Required] public string BloodType { get; set; }

        [Required] public string EducationalQualification { get; set; }

        [Required] public byte[] PasswordHashed { get; set; }

        [Required] public byte[]  PasswordSalt { get; set; }
    }
}