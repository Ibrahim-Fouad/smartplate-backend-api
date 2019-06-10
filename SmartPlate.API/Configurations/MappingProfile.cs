using System;
using AutoMapper;
using SmartPlate.API.Dto.Cars;
using SmartPlate.API.Dto.StolenCars;
using SmartPlate.API.Dto.Traffics;
using SmartPlate.API.Dto.Users;
using SmartPlate.API.Extensions;
using SmartPlate.API.Models;
using SmartPlate.API.Models.Users;

namespace SmartPlate.API.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Users
            CreateMap<UserForUpdateDto, IUser>()
                .ForMember(member => member.Name, src => src.Condition(u => !string.IsNullOrWhiteSpace(u.Name)))
                .ForMember(member => member.Email, src => src.Condition(u => !string.IsNullOrWhiteSpace(u.Email)))
                .ForMember(member => member.Address, src => src.Condition(u => !string.IsNullOrWhiteSpace(u.Address)))
                .ForMember(member => member.EducationalQualification,
                    src => src.Condition(u => !string.IsNullOrWhiteSpace(u.EducationalQualification)))
                .ForMember(member => member.BloodType,
                    src => src.Condition(u => !string.IsNullOrWhiteSpace(u.BloodType)))
                .ForMember(member => member.PhoneNumber,
                    src => src.Condition(u => !string.IsNullOrWhiteSpace(u.PhoneNumber)));

            CreateMap<IUser, UserForDetailsDto>()
                .ForMember(item => item.Age, src => src.MapFrom(user => user.DateOfBirth.GetAge()));

            CreateMap<UserForRegisterDto, IUser>();
            CreateMap<IUser, User>();
            CreateMap<IUser, Officer>();
            CreateMap<IUser, TrafficUser>();


            //Traffics
            CreateMap<Traffic, TrafficForDetailsDto>()
                .ForMember(member => member.TrafficId, src => src.MapFrom(item => item.Id));
            CreateMap<TrafficForCreateDto, Traffic>();
            CreateMap<TrafficForUpdateDto, Traffic>()
                .ForMember(member => member.Name, src => src.Condition(u => !string.IsNullOrWhiteSpace(u.Name)))
                .ForMember(member => member.Email, src => src.Condition(u => !string.IsNullOrWhiteSpace(u.Email)))
                .ForMember(member => member.Address, src => src.Condition(u => !string.IsNullOrWhiteSpace(u.Address)))
                .ForMember(member => member.Governorate,
                    src => src.Condition(u => !string.IsNullOrWhiteSpace(u.Governorate)))
                .ForMember(member => member.PhoneNumber,
                    src => src.Condition(u => !string.IsNullOrWhiteSpace(u.PhoneNumber)));


            //Cars
            CreateMap<CarForCreationDto, Car>()
                .ForMember(m => m.EndDate, src => src.MapFrom(car => car.StartDate.GetCarEndDate()));
            CreateMap<Car, CarForDetailsDto>()
                .ForMember(member => member.LicenseIsValid, src => src.MapFrom(car => car.EndDate.IsVaild()));
            CreateMap<Car, CarForSimpleDetailsDto>()
                .ForMember(member => member.LicenseIsValid, src => src.MapFrom(car => car.EndDate.IsVaild()));


            CreateMap<CarForUpdateDto, Car>()
                .ForMember(c => c.PlateNumber, src => src.Condition(c => !string.IsNullOrWhiteSpace(c.PlateNumber)))
                .ForMember(c => c.TrafficId, src => src.Condition(c => c.TrafficId > 0))
                .ForMember(c => c.UserId, src => src.Condition(c => !string.IsNullOrWhiteSpace(c.UserId)));

            //Stolen Cars
           CreateMap<StolenCarForCreationDto, StolenCar>()
                .ForMember(m => m.DateCreated, src => src.MapFrom(car => DateTime.Now));
            CreateMap<StolenCar, StolenCarForDetailsDto>()
                .ForMember(m => m.ObjectStoled, src => src.MapFrom(car => car.CarOrPlateIsStoled.CheckStoledObject()))
                .ForMember(m => m.Car, src => src.MapFrom(car => car.Car));
        }
    }
}