using MySql.Data.MySqlClient;

namespace OperativeDB
{
    public class Database
    {
        string DbName;
        static string connectionString;
        public Database(string dbName="eagleEyeDB")
        {
            DbName = dbName;
            connectionString = $"Server=Localhost;Port=3306;Database={DbName};User=root;Password='';";
        }

        private MySqlConnection connection;
        public Database Connect()
        {
            var conn = new MySqlConnection(connectionString: connectionString);
            connection = conn;
            try
            {
                conn.Open();
                Console.WriteLine("Connected to the database successfully.");
                conn.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
            return this;
        }

        public MySqlConnection GetConnction()
        {
            try
            {
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
                throw;
            }
        }
    }

}