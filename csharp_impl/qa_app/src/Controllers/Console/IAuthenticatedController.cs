using System.Threading.Tasks;
using Controllers.Console.DTO.Parameters;
using Domain.Entities;

namespace Controllers.Console
{
    public interface IAuthenticatedController
    {
        Task<IControllerResult> Run(User user, ControllerParameters parameters);
    }
}