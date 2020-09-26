using Domain.Entities;

namespace Infrastructure.Console.DTO
{
    public class ConsoleUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ConsoleUser(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}