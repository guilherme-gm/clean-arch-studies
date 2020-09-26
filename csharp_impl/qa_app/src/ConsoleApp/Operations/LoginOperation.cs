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
    public class LoginOperation
    {
        public static ConsoleUser Run()
        {
            Console.Write(">>> Username: ");
            var username = Console.ReadLine();
            Console.Write(">>> Password: ");
            var password = Console.ReadLine();
            var user = new ConsoleUser(username, password);

            Console.WriteLine($"Logged in as {username}\n");

            return user;
        }
    }
}
