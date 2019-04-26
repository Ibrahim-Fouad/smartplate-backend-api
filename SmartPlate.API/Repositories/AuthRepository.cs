using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthRepository(AppDbContext context,
            TokenGenerator tokenGenerator,
            Sha512PasswordManager passwordManager,
            IMapper mapper)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
            _passwordManager = passwordManager;
            _mapper = mapper;
        }


        public async Task<UserAccessToken> RegisterAsync(string userType, UserForRegisterDto userForRegisterDto)
        {
            if (userForRegisterDto.Password != userForRegisterDto.ConfirmPassword)
                return new UserAccessToken
                {
                    Success = false,
                    ErrorMessage = "Password and confirm password must be equal."
                };


            var isIdInDb = await GetUserAsync(userType, userForRegisterDto.Id);
            if (isIdInDb != null)
                return new UserAccessToken
                {
                    Success = false,
                    ErrorMessage = "User Id is already exists."
                };

            var user = _mapper.Map<UserForRegisterDto, IUser>(userForRegisterDto);

            _passwordManager.Generate(userForRegisterDto.Password, out var passwordHashed, out var passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHashed = passwordHashed;
            switch (userType.ToLower())
            {
                case "user":
                    await _context.AddAsync(_mapper.Map<IUser, User>(user));
                    break;
                case "officer":
                    await _context.AddAsync(_mapper.Map<IUser, Officer>(user));
                    break;
                case "traffic":
                    await _context.AddAsync(_mapper.Map<IUser, TrafficUser>(user));
                    break;
                default:
                    return new UserAccessToken
                    {
                        Success = false,
                        ErrorMessage = "User type is not defined successfully."
                    };
            }

            if (await _context.SaveChangesAsync() > 0)
                return new UserAccessToken
                {
                    Success = true,
                    AccessToken = _tokenGenerator.GenerateToken(user),
                    User = _mapper.Map<UserForDetailsDto>(user)
                };

            return new UserAccessToken
            {
                Success = false,
                ErrorMessage = "Unexpected error happened."
            };
        }

        public async Task<IUser> GetUserAsync(string userType, string userId)
        {
            switch (userType.ToLower())
            {
                case "user":
                    return await _context.FindAsync<User>(userId);
                case "officer":
                    return await _context.FindAsync<Officer>(userId);
                case "traffic":
                    return await _context.FindAsync<TrafficUser>(userId);
                default:
                    return null;
            }
        }

        public async Task<UserForDetailsDto> GetUserMappedAsync(string userType, string userId)
        {
            var userInDb = await GetUserAsync(userType, userId);
            if (userInDb == null)
                return new UserForDetailsDto
                {
                    Success = false,
                    ErrorMessage = "User is not found, please login again."
                };
            return _mapper.Map<UserForDetailsDto>(userInDb);
        }

        public async Task<UserAccessToken> LoginAsync(string userType, string id, string password)
        {
            var userInDb = await GetUserAsync(userType, id);

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
                AccessToken = token,
                User = _mapper.Map<UserForDetailsDto>(userInDb)
            };
        }

        public async Task<UserAccessToken> ChangePasswordAsync(string userType, string id,
            UserChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                return new UserAccessToken
                {
                    Success = false,
                    ErrorMessage = "New password and confirm password must be equals."
                };

            var userInDb = await GetUserAsync(userType, id);

            if (userInDb == null ||
                !_passwordManager.ValidatePassword(changePasswordDto.OldPassword, userInDb.PasswordHashed,
                    userInDb.PasswordSalt))
                return new UserAccessToken
                {
                    Success = false,
                    ErrorMessage = "password is incorrect."
                };

            _passwordManager.Generate(changePasswordDto.NewPassword, out var passwordHashed, out var passwordSalt);
            userInDb.PasswordHashed = passwordHashed;
            userInDb.PasswordSalt = passwordSalt;

            //_context.Update(userInDb);
            var count = await _context.SaveChangesAsync();

            if (count > 0)
                return new UserAccessToken
                {
                    Success = true,
                    AccessToken = _tokenGenerator.GenerateToken(userInDb),
                    User = _mapper.Map<UserForDetailsDto>(userInDb)
                };

            return new UserAccessToken
            {
                Success = false,
                ErrorMessage = "No data changed."
            };
        }

        public async Task<UserAccessToken> UpdateUserAsync(string userType, string userId, UserForUpdateDto userForUpdateDto)
        {
            var user = await GetUserAsync(userType, userId);
            if (user == null)
                return new UserAccessToken
                {
                    Success = false,
                    ErrorMessage = "User is not found."
                };

            _mapper.Map(userForUpdateDto, user);

            var count = await _context.SaveChangesAsync();
            if (count > 0)
                return new UserAccessToken
                {
                    Success = true,
                    AccessToken = _tokenGenerator.GenerateToken(user),
                    User = _mapper.Map<UserForDetailsDto>(user)
                };

            return new UserAccessToken
            {
                Success = false,
                ErrorMessage = "No data changed."
            };
        }

        public void Seed()
        {
            if (_context.Users.Any())
                return;

            var user = new User
            {
                Id = "123456789",
                Name = "User 1",
                Address = "Cairo",
                BloodType = "A+",
                EducationalQualification = "BCS",
                DateOfBirth = DateTime.Now.AddYears(-35)
            };

            _passwordManager.Generate("password", out var passwordHashed, out var passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHashed = passwordHashed;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}