using System.Threading.Tasks;
using SmartPlate.API.Dto.Users;
using SmartPlate.API.Models.Users;

namespace SmartPlate.API.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserAccessToken> Login(string userType, string id, string password);

        Task<UserAccessToken> Register(string userType, UserForRegisterDto userForRegisterDto);
        Task<IUser> GetUser(string userType, string userId);
        Task<UserForDetailsDto> GetUserMapped(string userType, string userId);
        Task<UserAccessToken> ChangePassword(string userType, string id, UserChangePasswordDto changePasswordDto);

        Task<UserAccessToken> UpdateUser(string userType, string userId, UserForUpdateDto userForUpdateDto);


        void Seed();
    }
}