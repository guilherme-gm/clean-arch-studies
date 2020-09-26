using System;
using System.Threading.Tasks;
using Controllers.Console.DTO;
using Controllers.Console.DTO.Parameters;
using Domain.Entities;
using Domain.UseCases;

namespace Controllers.Console
{
    public class ViewQuestion : IController
    {
        private QuestionsUseCase QuestionsUseCase;

        public ViewQuestion(QuestionsUseCase useCase)
        {
            this.QuestionsUseCase = useCase;
        }

        public async Task<IControllerResult> Run(ControllerParameters parameters)
        {
            try {
                if (!(parameters is ViewQuestionParameters))
                    throw new Exception("Invalid parameters");

                var param = (ViewQuestionParameters) parameters;
                var question = await QuestionsUseCase.View(param.QuestionId);

                if (question == null)
                    throw new Exception("Question not found");

                return new ViewQuestionResult(true, "Question retrieved", question);
            } catch (Exception error) {
                return new ViewQuestionResult(false, error.Message, null);
            }
        }
    }
}