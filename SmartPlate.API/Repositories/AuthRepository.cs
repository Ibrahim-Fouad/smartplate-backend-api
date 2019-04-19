using System.Threading.Tasks;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Db;
using SmartPlate.API.Dto.Users;
using SmartPlate.API.Helpers;
using SmartPlate.API.Models.Users;

namespace SmartPlate.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly TokenGenerator _tokenGenerator;
        private readonly Sha512PasswordManager _passwordManager;

        public AuthRepository(AppDbContext context,
            TokenGenerator tokenGenerator,
            Sha512PasswordManager passwordManager)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
            _passwordManager = passwordManager;
        }

        public async Task<UserAccessToken> Login<TUser>(string id, string password) where TUser : class
        {
            if (typeof(TUser).GetInterface(nameof(IUser)) != typeof(IUser))
                return new UserAccessToken
                {
                    Success = false,
                    ErrorMessage = "User id or password is incorrect."
                };

            var userInDb = (IUser) await _context.FindAsync<TUser>(id);

            if (userInDb == null ||
                !_passwordManager.ValidatePassword(password, userInDb.PasswordHashed, userInDb.PasswordSalt))
                return new UserAccessToken
                {
                    Success = false,
                    ErrorMessage = "User id or password is incorrect."
                };

            var token = _tokenGenerator.GenerateToken(userInDb);

            return new UserAccessToken
            {
                Success = true,
                AccessToken = token
            };
        }
    }
}