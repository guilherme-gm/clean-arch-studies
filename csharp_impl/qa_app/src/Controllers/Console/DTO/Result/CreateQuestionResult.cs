using Domain.Entities;

namespace Controllers.Console.DTO.Parameters
{
    public class CreateQuestionResult : IControllerResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Question? Question { get; set; }

        public CreateQuestionResult(bool isSuccess, string message, Question? question)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Question = question;
        }
        
        public string Present() {
            if (Question == null)
                return "No question data..";

            return ""
                + $"Question ID: {Question.Id}\n"
                + $"Question: {Question.Title}\n"
                + $"Message: \n"
                + $"{Question.Message}\n\n";
        }
    }
}
