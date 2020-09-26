using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IAnswerRepository
    {
        Task<List<Answer>> ByQuestion(long questionId);
        Task Create(long questionId, Answer answer);
    }
}