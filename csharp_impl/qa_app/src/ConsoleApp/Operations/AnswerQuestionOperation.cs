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
    public class AnswerQuestionOperation
    {
        public static async Task Run(IContainer container, ConsoleUser? user)
        {
            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            using (var scope = container.BeginLifetimeScope())
            {
                var api = scope.Resolve<ConsoleApi>();
                var ctrl = scope.Resolve<CreateQuestion>(); //new CreateQuestion();
                // var params = new CreateQuestionParameters(question);
                var param = new CreateQuestionParameters("test", "hello world");
                
                Console.WriteLine("Sending");
                var res = (CreateQuestionResult) await api.Run(ctrl, param, user);
                Console.WriteLine($"> {res.Message}");
                Console.WriteLine($"> {res.Present()}");
            }
        }
    }
}
