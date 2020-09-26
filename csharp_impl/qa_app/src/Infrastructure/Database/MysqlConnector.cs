using System;
using System.Threading.Tasks;
using MySqlConnector;

namespace Infrastructure.Database
{
    public class MysqlConnector
    {
        // TODO: Move to configuration
        private const string connectionString = "Server=localhost;User ID=dev;Password=;Database=clean_arch_app";

        public static async Task<MySqlConnection> GetConnection()
        {
            try {
                var con = new MySqlConnection(connectionString);
                System.Console.WriteLine("Connecting");
                await con.OpenAsync();
                System.Console.WriteLine("Connected");
                return con;
            } catch (Exception error)  {
                System.Console.WriteLine(error.Message);
                throw error;
            }
        }

        public static async Task<bool> Test()
        {
            var con = await GetConnection();
            var result = await con.PingAsync();
            await con.CloseAsync();
            
            return result;
        }
    }
}