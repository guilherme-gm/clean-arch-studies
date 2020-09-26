using System.Threading.Tasks;
using Controllers.Console.DTO.Parameters;
using Domain.Entities;

namespace Controllers.Console
{
    public interface IController
    {
        Task<IControllerResult> Run(ControllerParameters parameters);
    }
}