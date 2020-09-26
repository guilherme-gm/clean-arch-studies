using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> Find(string username);
        Task<User?> Find(long id);
    }
}