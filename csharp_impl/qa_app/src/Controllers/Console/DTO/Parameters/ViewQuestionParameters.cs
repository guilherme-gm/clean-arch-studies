using Domain.Entities;

namespace Controllers.Console.DTO.Parameters
{
    public class ViewQuestionParameters : ControllerParameters
    {
        public long QuestionId { get; set; }

        public ViewQuestionParameters(long questionId)
        {
            this.QuestionId = questionId;
        }
    }
}