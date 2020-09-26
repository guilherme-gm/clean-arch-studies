using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Authentication
{
    public interface IPasswordHasher
    {
        Task<string> Hash(string password);
        Task<bool> Compare(string hashedPassword, string password);

    }
}