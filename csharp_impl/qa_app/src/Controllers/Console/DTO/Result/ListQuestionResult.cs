using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Controllers.Console.DTO.Parameters
{
    public class ListQuestionResult : IControllerResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<Question> Questions { get; set; }

        public ListQuestionResult(bool isSuccess, string message, List<Question> questions)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Questions = questions;
        }
        
        public string Present() {
            StringBuilder sb = new StringBuilder();

            this.Questions.ForEach((question) => {
                sb.Append(
                    $"Question ID: {question.Id}\n"
                    + $"Question: {question.Title}\n"
                    + $"Message: \n"
                    + $"{question.Message}\n\n"
                );
            });

            return sb.ToString();
        }
    }
}
