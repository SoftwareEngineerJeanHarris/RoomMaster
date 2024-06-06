using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using RoomMaster.Login;
using System;
using System.IO;
using System.Security.Cryptography;

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
                    string query = "SELECT password_hash, salt FROM users WHERE username=@username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPasswordHash = reader["password_hash"].ToString();
                            byte[] salt = Convert.FromBase64String(reader["salt"].ToString());
                            string hashedPassword = HashPassword(password, salt);
                            return storedPasswordHash == hashedPassword;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
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

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static bool CreateUser(string username, string password, string name, string email, int permission)
        {
            byte[] salt = GenerateSalt();
            string hashedPassword = HashPassword(password, salt);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO users (username, password_hash, name, email, permission, salt) VALUES (@username, @password, @name, @Email, @permission, @salt)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@permission", permission);
                    cmd.Parameters.AddWithValue("@salt", Convert.ToBase64String(salt));
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
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

        private static string HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20); // 20 bytes = 160 bits
                byte[] hashBytes = new byte[36]; // 20 bytes for the hash, 16 bytes for the salt
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
