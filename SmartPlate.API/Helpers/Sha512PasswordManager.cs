using System.Security.Cryptography;
using System.Text;

namespace SmartPlate.API.Helpers
{
    public class Sha512PasswordManager
    {
        public void Generate(string password, out byte[] passwordHashed, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHashed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool ValidatePassword(string password, byte[] passwordHashed, byte[] passwordSalt)
        {
            using (var hamc = new HMACSHA512(passwordSalt))
            {
                var passwordGenerated = hamc.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (passwordGenerated.Length != passwordHashed.Length)
                    return false;

                for (var i = 0; i < passwordHashed.Length; i++)
                    if (passwordHashed[i] != passwordGenerated[i])
                        return false;
            }

            return true;
        }
    }
}