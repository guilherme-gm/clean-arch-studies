using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Authentication.Authenticators
{
    public class BasicAuth : IAuthenticator
    {
        private string? ReceivedUsername;

        private string? ReceivedPassword;

        private IUserRepository UserRepository;

        private IPasswordHasher PasswordHasher;

        public BasicAuth(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            this.UserRepository = userRepository;
            this.PasswordHasher = passwordHasher;
        }

        public void SetCredentials(string username, string password)
        {
            this.ReceivedUsername = username;
            this.ReceivedPassword = password;
        }

        public async Task<User?> Authenticate()
        {
            if (this.ReceivedUsername == null || this.ReceivedPassword == null)
                return null;

            var user = await UserRepository.Find(this.ReceivedUsername);
            if (user == null)
                return null;
            
            if (!await this.PasswordHasher.Compare(user.Password, this.ReceivedPassword))
                return null;

            return user;
        }
    }
}