using AutoMapper;
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
        }
    }
}