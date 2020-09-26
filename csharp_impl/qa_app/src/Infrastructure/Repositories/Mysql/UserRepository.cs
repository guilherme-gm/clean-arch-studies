using System;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Database;
using MySqlConnector;

namespace Infrastructure.Repositories.Mysql
{
    public class UserRepository : IUserRepository
    {
        public async Task<User?> Find(string username)
        {
            var con = await MysqlConnector.GetConnection();
            var command = new MySqlCommand("SELECT id, username, password, role FROM users WHERE username = @username LIMIT 0, 1", con);
            if (command == null) {
                throw new Exception("");
            }

            command.Parameters.AddWithValue("@username", username);
            var result = await command.ExecuteReaderAsync();
            if (result == null) {
                throw new Exception("");
            }

            if (!await result.ReadAsync())
                return null;

            return new User(
                result.GetInt64("id"),
                result.GetString("username"),
                result.GetString("password"),
                Role.Parse<Role>(result.GetString("role"))
            );
        }

        public async Task<User?> Find(long id)
        {
            var con = await MysqlConnector.GetConnection();
            var command = new MySqlCommand("SELECT id, username, password, role FROM users WHERE id = @id LIMIT 0, 1", con);
            if (command == null) {
                throw new Exception("");
            }

            command.Parameters.AddWithValue("@id", id);
            var result = await command.ExecuteReaderAsync();
            if (result == null) {
                throw new Exception("");
            }

            if (!await result.ReadAsync())
                return null;

            return new User(
                result.GetInt64("id"),
                result.GetString("username"),
                result.GetString("password"),
                Role.Parse<Role>(result.GetString("role"))
            );
        }
    }
}