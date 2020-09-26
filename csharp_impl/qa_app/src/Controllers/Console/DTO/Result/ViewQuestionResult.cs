using System.Text;
using Domain.Entities;

namespace Controllers.Console.DTO.Parameters
{
    public class ViewQuestionResult : IControllerResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Question? Question { get; set; }

        public ViewQuestionResult(bool isSuccess, string message, Question? question)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Question = question;
        }
        
        public string Present() {
            if (Question == null)
                return "No question data..";

            var result = new StringBuilder();
            result.Append(
                $"Question ID: {Question.Id}\n"
                + $"Question: {Question.Title}\n"
                + $"Message: \n"
                + $"{Question.Message}\n\n"
                + "Answers:\n"
            );

            if (this.Question.Answers != null) {
                this.Question.Answers.ForEach((answer) => {
                    result.Append(
                        $"> Answer ID: {answer.AnswerId}\n"
                        + $">> By: {answer.User.Username}\n"
                        + $">> Message: {answer.Message}\n\n"
                    );
                });
            }

            return result.ToString();
        }
    }
}
