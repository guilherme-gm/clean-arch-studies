using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Authentication.PasswordHashers
{
    public class PlainTextHasher : IPasswordHasher
    {
        public Task<string> Hash(string password)
        {
            return Task.Run(() => password);
        }

        public Task<bool> Compare(string hashedPassword, string password)
        {
            return Task.Run(() => hashedPassword.Equals(password));;
        }

    }
}