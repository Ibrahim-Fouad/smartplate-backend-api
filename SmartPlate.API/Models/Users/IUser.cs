using System;

namespace SmartPlate.API.Models.Users
{
    public interface IUser
    {
        string Id { get; set; }

        string Name { get; set; }

        DateTime DateOfBirth { get; set; }

        string Address { get; set; }

        string PhoneNumber { get; set; }

        string Email { get; set; }

        string ImagePath { get; set; }

        string BloodType { get; set; }

        string EducationalQualification { get; set; }
    }
}