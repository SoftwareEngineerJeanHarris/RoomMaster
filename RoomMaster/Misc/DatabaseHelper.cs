using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using RoomMaster.Login;
using System;
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
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE username=@username AND password_hash=@password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password); // Consider hashing the password
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public static User GetUser(string username)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Name, Username, Email, permission FROM users WHERE username=@username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Name = reader["Name"].ToString(),
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString(),
                                permission = Convert.ToInt32(reader["permission"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }

        public static bool CreateUser(string username, string password, string name, string email, int permission)
        {
            bool isCreated = false;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO users (username, password_hash, name, email, permission) VALUES (@username, @password, @name, @Email, @permission)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password); // Consider hashing the password
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@permission", permission);
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

        public static bool UserExists(string username)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE username=@username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
