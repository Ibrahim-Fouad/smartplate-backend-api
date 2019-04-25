using System;

namespace SmartPlate.API.Dto.Users
{
    public class UserForUpdateDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BloodType { get; set; }
        public string EducationalQualification { get; set; }
    }
}