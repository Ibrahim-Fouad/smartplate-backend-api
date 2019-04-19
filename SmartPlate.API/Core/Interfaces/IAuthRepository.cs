using System.Threading.Tasks;
using SmartPlate.API.Dto.Users;

namespace SmartPlate.API.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserAccessToken> Login<T>(string id, string password) where T : class;
    }
}