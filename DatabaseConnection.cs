using MySqlConnector;
using System;
using System.Threading.Tasks;

namespace LoginSystemNet8
{
    public static class DatabaseConnection
    {
        // String de conexão
        private static readonly string connectionString =
            "Server=devreis.mysql.dbaas.com.br;" +
            "Database=devreis;" +
            "User=devreis;" +
            "Password=a@Hvg132gyiu47;";

        /// <summary>
        /// Retorna uma nova conexão MySQL
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Testa se a conexão está funcionando
        /// </summary>
        public static async Task<bool> TestConnectionAsync()
        {
            try
            {
                using var connection = GetConnection();
                await connection.OpenAsync();
                return connection.State == System.Data.ConnectionState.Open;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na conexão: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Verifica se um usuário existe e a senha está correta
        /// </summary>
        public static async Task<(bool success, string username, string email)> ValidateUserAsync(string username, string password)
        {
            try
            {
                using var connection = GetConnection();
                await connection.OpenAsync();

                string query = @"
                    SELECT * 
                    FROM usu_register 
                    WHERE username = @username 
                    AND password = @password 
                    LIMIT 1";

                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    string user = reader["username"].ToString()!;
                    string email = reader["email"].ToString()!;
                    return (true, user, email);
                }

                return (false, string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na validação: {ex.Message}");
                return (false, string.Empty, string.Empty);
            }
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        public static async Task<bool> RegisterUserAsync(string username, string email, string password)
        {
            try
            {
                using var connection = GetConnection();
                await connection.OpenAsync();

                // Verifica se o usuário já existe
                string checkQuery = "SELECT * FROM usu_register WHERE username = @username OR email = @email";
                using var checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@username", username);
                checkCommand.Parameters.AddWithValue("@email", email);

                var count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                if (count > 0)
                {
                    return false; // Usuário ou email já existe
                }

                // Insere o novo usuário
                string insertQuery = @"
                    INSERT INTO usu_register (username, email, password, data_created) 
                    VALUES (@username, @email, @password, CURRENT_TIMESTAMP)";

                using var insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@username", username);
                insertCommand.Parameters.AddWithValue("@email", email);
                insertCommand.Parameters.AddWithValue("@password", password);

                int rowsAffected = await insertCommand.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao registrar usuário: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Obtém informações do usuário
        /// </summary>
        public static async Task<(bool found, int id, string username, string email, DateTime dataCreated)> GetUserInfoAsync(string username)
        {
            try
            {
                using var connection = GetConnection();
                await connection.OpenAsync();

                string query = @"
                    SELECT * 
                    FROM usu_register 
                    WHERE username = @username 
                    LIMIT 1";

                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string user = reader["username"].ToString()!;
                    string email = reader["email"].ToString()!;
                    DateTime dataCreated = Convert.ToDateTime(reader["data_created"]);

                    return (true, id, user, email, dataCreated);
                }

                return (false, 0, string.Empty, string.Empty, DateTime.MinValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter informações do usuário: {ex.Message}");
                return (false, 0, string.Empty, string.Empty, DateTime.MinValue);
            }
        }
    }
}