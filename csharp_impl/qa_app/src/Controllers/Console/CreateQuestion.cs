using System;
using System.Threading.Tasks;
using Controllers.Console.DTO;
using Controllers.Console.DTO.Parameters;
using Domain.Entities;
using Domain.UseCases;

namespace Controllers.Console
{
    public class CreateQuestion : IAuthenticatedController
    {
        private QuestionsUseCase QuestionsUseCase;

        public CreateQuestion(QuestionsUseCase useCase)
        {
            this.QuestionsUseCase = useCase;
        }

        public async Task<IControllerResult> Run(User user, ControllerParameters parameters)
        {
            try {
                if (!(parameters is CreateQuestionParameters))
                    throw new Exception("Invalid parameters");
                
                var param = (CreateQuestionParameters) parameters;

                Question question = new Question(param.Title, param.Message, user);
                var createdQuestion = await QuestionsUseCase.Create(question);

                if (createdQuestion == null)
                    return new CreateQuestionResult(false, "Something went wrong while creating your question.", null);

                return new CreateQuestionResult(true, "Question created", createdQuestion);
            } catch (Exception error) {
                return new CreateQuestionResult(false, error.Message, null);
            }
        }
    }
}