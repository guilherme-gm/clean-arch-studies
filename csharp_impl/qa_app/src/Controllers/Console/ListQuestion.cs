using System;
using System.Threading.Tasks;
using Controllers.Console.DTO;
using Controllers.Console.DTO.Parameters;
using Domain.Entities;
using Domain.UseCases;

namespace Controllers.Console
{
    public class ListQuestion : IController
    {
        private QuestionsUseCase QuestionsUseCase;

        public ListQuestion(QuestionsUseCase useCase)
        {
            this.QuestionsUseCase = useCase;
        }

        public async Task<IControllerResult> Run(ControllerParameters parameters)
        {
            try {
                var questions = await QuestionsUseCase.List();

                return new ListQuestionResult(true, "Question list retrieved", questions);
            } catch (Exception error) {
                return new ListQuestionResult(false, error.Message, null);
            }
        }
    }
}