using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Database;
using MySqlConnector;

namespace Infrastructure.Repositories.Mysql
{
    public class AnswerRepository : IAnswerRepository
    {
        private IUserRepository UserRepository;

        public AnswerRepository(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public async Task<List<Answer>> ByQuestion(long questionId)
        {
            var con = await MysqlConnector.GetConnection();
            var command = new MySqlCommand(
                "SELECT `answers`.`id` as answerId, `message`, `users`.`id` authorId, `users`.`username` as userName, `users`.`role` as userRole "
                + "FROM `answers` "
                + "INNER JOIN `users` ON `answers`.`author` = `users`.`id` "
                + "WHERE `answers`.`question` = @id",
                con
            );
            if (command == null)
            {
                throw new Exception("");
            }

            command.Parameters.AddWithValue("@id", questionId);
            var result = await command.ExecuteReaderAsync();
            if (result == null)
                throw new Exception("");

            var answers = new List<Answer>();
            while (await result.ReadAsync()) {
                var author = new User(
                    result.GetInt64("authorId"),
                    result.GetString("userName"),
                    "",
                    Role.Parse<Role>(result.GetString("userRole"))
                );
                var answer = new Answer(
                    result.GetInt64("answerId"),
                    author,
                    result.GetString("message")
                );
                answers.Add(answer);
            }

            return answers;
        }

        public async Task Create(long questionId, Answer answer)
        {
            var con = await MysqlConnector.GetConnection();
            var command = new MySqlCommand("INSERT INTO `answers`(`author`, `question`, `message`) VALUES (@author, @question, @message)", con);
            if (command == null)
                throw new Exception();

            command.Parameters.AddWithValue("@author", answer.User.Id);
            command.Parameters.AddWithValue("@question", questionId);
            command.Parameters.AddWithValue("@message", answer.Message);

            var result = await command.ExecuteNonQueryAsync();
            if (result < 1)
                throw new Exception("failed to create answer");
        }
    }
}