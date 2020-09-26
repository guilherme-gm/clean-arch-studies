using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Database;
using MySqlConnector;

namespace Infrastructure.Repositories.Mysql
{
    public class QuestionRepository : IQuestionRepository
    {
        private IUserRepository UserRepository;
        private IAnswerRepository AnswerRepository;

        public QuestionRepository(IUserRepository userRepository, IAnswerRepository answerRepository)
        {
            this.UserRepository = userRepository;
            this.AnswerRepository = answerRepository;
        }

        public async Task<Question> Create(Question question)
        {
            var con = await MysqlConnector.GetConnection();
            var command = new MySqlCommand("INSERT INTO `questions`(`title`,`message`,`asker`) VALUES (@title, @message, @asker)", con);
            if (command == null)
                throw new Exception();

            command.Parameters.AddWithValue("@title", question.Title);
            command.Parameters.AddWithValue("@message", question.Message);
            command.Parameters.AddWithValue("@asker", question.CreatedBy.Id);

            var result = await command.ExecuteNonQueryAsync();
            if (result < 1)
                throw new Exception("failed to create question");

            var createdQestion = await this.Find(command.LastInsertedId);
            if (createdQestion == null)
                throw new Exception("could not retrieve created queston");

            return createdQestion;
        }

        public async Task<Question?> Find(long id)
        {
            var con = await MysqlConnector.GetConnection();
            var command = new MySqlCommand(
                "SELECT `id`, `title`, `message`, `asker` FROM `questions` WHERE `id` = @id LIMIT 0, 1",
                con
            );
            if (command == null)
            {
                throw new Exception("");
            }

            command.Parameters.AddWithValue("@id", id);
            var result = await command.ExecuteReaderAsync();
            if (result == null)
                throw new Exception("");

            if (!await result.ReadAsync())
                return null;

            // Yeah, this could've been an inner join, but let's keep things simple. normally you would have an ORM here too.
            var user = await UserRepository.Find(result.GetInt64("asker"));
            if (user == null)
                throw new Exception("Question creator not found");

            return new Question(
                result.GetInt64("id"),
                result.GetString("title"),
                result.GetString("message"),
                user
            );
        }

        public async Task<List<Question>> Get()
        {
            var con = await MysqlConnector.GetConnection();
            var command = new MySqlCommand(
                "SELECT `id`, `title`, `message`, `asker` FROM `questions`",
                con
            );
            if (command == null)
                throw new Exception("");

            var result = await command.ExecuteReaderAsync();
            if (result == null)
                throw new Exception("");

            List<Question> questions = new List<Question>();

            while (await result.ReadAsync())
            {
                // Yeah, this could've been an inner join, but let's keep things simple. normally you would have an ORM here too.
                var user = await UserRepository.Find(result.GetInt64("asker"));
                if (user == null)
                    throw new Exception("Question creator not found");

                questions.Add(new Question(
                    result.GetInt64("id"),
                    result.GetString("title"),
                    result.GetString("message"),
                    user
                ));
            }

            return questions;
        }

        public async Task<Question?> FindComplete(long id)
        {
            var con = await MysqlConnector.GetConnection();
            var command = new MySqlCommand(
                "SELECT `id`, `title`, `message`, `asker` FROM `questions` WHERE `id` = @id LIMIT 0, 1",
                con
            );
            if (command == null)
            {
                throw new Exception("");
            }

            command.Parameters.AddWithValue("@id", id);
            var result = await command.ExecuteReaderAsync();
            if (result == null)
                throw new Exception("");

            if (!await result.ReadAsync())
                return null;

            // Yeah, this could've been an inner join, but let's keep things simple. normally you would have an ORM here too.
            var user = await UserRepository.Find(result.GetInt64("asker"));
            if (user == null)
                throw new Exception("Question creator not found");

            var question = new Question(
                result.GetInt64("id"),
                result.GetString("title"),
                result.GetString("message"),
                user
            );

            var answers = await AnswerRepository.ByQuestion(question.Id);
            question.Answers = answers;

            return question;
        }

        public async Task SetCorrectAnswer(long questionId, long answerId)
        {
            var con = await MysqlConnector.GetConnection();
            var command = new MySqlCommand(
                "UPDATE `questions` SET correctAnswer = @answer WHERE `id` = @id",
                con
            );
            if (command == null)
            {
                throw new Exception("");
            }

            command.Parameters.AddWithValue("@answer", answerId);
            command.Parameters.AddWithValue("@id", questionId);
            var result = await command.ExecuteNonQueryAsync();
            if (result < 1)
                throw new Exception("Failed to set correct answer");
        }
    }
}