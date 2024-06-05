using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System.IO;

namespace RoomMaster.Misc
{
    public static class DatabaseHelper
    {
        private static string connectionString;

        static DatabaseHelper()
        {
            try
            {
                string dbConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Misc", "dbconfig.json");

                //if (!File.Exists(dbConfigPath))
                var json = File.ReadAllText(dbConfigPath);
                var jObject = JObject.Parse(json);
                connectionString = jObject["ConnectionString"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading dbconfig.json: " + ex.Message);
                connectionString = null;
            }
        }
        public static bool ValidateUser(string username, string password)
        {
            bool isValid = false;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE username=@username AND password_hash=@password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    isValid = result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return isValid;
        }

        public static bool CreateUser(string username, string password)
        {
            bool isCreated = false;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO users (username, password_hash) VALUES (@username, @password)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    int result = cmd.ExecuteNonQuery();
                    isCreated = result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return isCreated;
        }
    }
}
