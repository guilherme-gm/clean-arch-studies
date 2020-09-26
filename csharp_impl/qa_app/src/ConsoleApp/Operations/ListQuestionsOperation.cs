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
    public class ListQuestionsOperation
    {
        public static async Task Run(IContainer container, ConsoleUser? user)
        {
            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            using (var scope = container.BeginLifetimeScope())
            {
                var api = scope.Resolve<ConsoleApi>();
                var ctrl = scope.Resolve<ListQuestion>(); //new CreateQuestion();
                var param = new ControllerParameters();
                
                Console.WriteLine("Sending");
                var res = (ListQuestionResult) await api.Run(ctrl, param);
                Console.WriteLine($"> {res.Message}");
                Console.WriteLine($"> {res.Present()}");
                Console.WriteLine("");
            }
        }
    }
}
