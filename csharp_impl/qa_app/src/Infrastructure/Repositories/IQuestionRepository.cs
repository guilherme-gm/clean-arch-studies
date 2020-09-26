using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question?> Find(long id);
        Task<Question> Create(Question question);
        Task<List<Question>> Get();
        Task<Question?> FindComplete(long id);
        Task SetCorrectAnswer(long questionId, long anserId);
    }
}