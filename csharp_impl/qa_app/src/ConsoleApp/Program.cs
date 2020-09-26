using System;
using System.Threading.Tasks;
using Autofac;
using Infrastructure;
using Infrastructure.Console.DTO;
using qa_app.Operations;

namespace qa_app
{
    public class Program
    {

        private static ConsoleUser? Login { get; set; }

#nullable disable
        private static IContainer Container { get; set; }
#nullable restore

        static void Main(string[] args)
        {
            Container = Startup.Init();
            MainAsync().Wait();
        }

        public static async Task MainAsync()
        {
            while (true)
            {
                if (await MainMenu())
                    return;
            }
        }

        public static async Task<bool> MainMenu()
        {
            Console.WriteLine("> Input an Action");
            Console.WriteLine(">> 1. Login");
            Console.WriteLine(">> 2. Create Question");
            Console.WriteLine(">> 3. List Question");
            Console.WriteLine(">> 4. View Question");
            Console.WriteLine(">> 5. Quit.");
            Console.Write("$ ");
            var action = Console.ReadLine();
            int actionCode;

            if (!Int32.TryParse(action, out actionCode))
            {
                Console.WriteLine("Error: Invalid Action");
                return false;
            }

            Console.WriteLine("");

            switch (actionCode)
            {
                case 1:
                    Login = LoginOperation.Run();
                    return false;

                case 2:
                    await CreateQuestionOperation.Run(Container, Login);
                    return false;

                case 3:
                    await ListQuestionsOperation.Run(Container, Login);
                    return false;

                case 4:
                    var questionId = await ViewQuestionOperation.Run(Container, Login);
                    if (questionId == -1)
                        return false;

                    await ShowQuestionMenu(questionId);

                    return false;

                case 5:
                    return true;

                default:
                    Console.WriteLine("Error: Invalid action");
                    return false;
            }
        }

        public static async Task ShowQuestionMenu(long questionId)
        {
            while (true)
            {
                Console.WriteLine("> Input an Action");
                Console.WriteLine(">> 1. Add Answer");
                Console.WriteLine(">> 2. Pick right answer");
                Console.WriteLine(">> 5. Back.");
                Console.Write("$ ");
                var action = Console.ReadLine();
                int actionCode;

                if (!Int32.TryParse(action, out actionCode))
                {
                    Console.WriteLine("Error: Invalid Action");
                    continue;
                }

                Console.WriteLine("");

                switch (actionCode)
                {
                    case 1:
                        Login = LoginOperation.Run();
                        return false;

                    case 2:
                        await CreateQuestionOperation.Run(Container, Login);
                        return false;

                    case 3:
                        await ListQuestionsOperation.Run(Container, Login);
                        return false;

                    case 4:
                        var questionId = await ViewQuestionOperation.Run(Container, Login);
                        if (questionId == -1)
                            return false;

                        await ShowQuestionMenu(questionId);

                        return false;

                    case 5:
                        return true;

                    default:
                        Console.WriteLine("Error: Invalid action");
                        return false;
                }
            }
        }
    }
}
