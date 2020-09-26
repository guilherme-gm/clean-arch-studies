using System;
using System.Threading.Tasks;
using Autofac;
using Controllers.Console;
using Controllers.Console.DTO.Parameters;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Console;
using Infrastructure.Console.DTO;

namespace qa_app.Operations
{
    public class ViewQuestionOperation
    {
        public static async Task<long> Run(IContainer container, ConsoleUser? user)
        {
            Console.WriteLine("Enter the question number.");
            Console.Write("$ ");
            var input = Console.ReadLine();
            long questionId;
            if (!Int64.TryParse(input, out questionId)) {
                Console.WriteLine("Invalid question id.");
                return -1;
            }

            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            using (var scope = container.BeginLifetimeScope())
            {
                var api = scope.Resolve<ConsoleApi>();
                var ctrl = scope.Resolve<ViewQuestion>(); //new CreateQuestion();
                
                // var params = new CreateQuestionParameters(question);
                var param = new ViewQuestionParameters(questionId);
                var res = (ViewQuestionResult) await api.Run(ctrl, param);
                Console.WriteLine($"> {res.Message}");
                Console.WriteLine($"> {res.Present()}");
                return questionId;
            }
        }
    }
}
