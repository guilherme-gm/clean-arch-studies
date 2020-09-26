using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Authentication
{
    public interface IAuthenticator
    {
        Task<User?> Authenticate();
    }
}