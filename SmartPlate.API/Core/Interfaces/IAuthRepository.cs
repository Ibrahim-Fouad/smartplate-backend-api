using System.Threading.Tasks;
using SmartPlate.API.Dto.Users;
using SmartPlate.API.Models.Users;

namespace SmartPlate.API.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserAccessToken> LoginAsync(string userType, string id, string password);

        Task<UserAccessToken> RegisterAsync(string userType, UserForRegisterDto userForRegisterDto);
        Task<IUser> GetUserAsync(string userType, string userId);
        Task<UserForDetailsDto> GetUserMappedAsync(string userType, string userId);
        Task<UserAccessToken> ChangePasswordAsync(string userType, string id, UserChangePasswordDto changePasswordDto);

        Task<UserAccessToken> UpdateUserAsync(string userType, string userId, UserForUpdateDto userForUpdateDto);


        void Seed();
    }
}