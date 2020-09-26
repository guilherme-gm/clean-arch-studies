using Domain.Entities;

namespace Controllers.Console.DTO.Parameters
{
    public class CreateQuestionParameters : ControllerParameters
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public CreateQuestionParameters(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }
    }
}