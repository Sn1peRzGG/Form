using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Login_and_Register_Form
{
    public partial class Form1 : Form
    {
        private DBHelper DBHelper;

        public Form1()
        {
            InitializeComponent();
            DBHelper = new DBHelper();
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
        }
    }

    public class DBHelper
    {
        private string server = "";
        private int port = 0;
        private string username = "";
        private string password = "";
        private string database = "";

        MySqlConnection mySqlConnection = null;

        public DBHelper(string server = "localhost", int port = 3306, string username = "root", string password = "1234", string database = "users")
        {
            mySqlConnection = new MySqlConnection($"server={server};port={port};username={username};password={password};database={database}");
        }

        public MySqlConnection getMySqlConnection()
        {
            return mySqlConnection;
        }

        public bool Login(string usernameOrEmail, string password)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT * FROM users WHERE (username=@username OR email=@email) AND password=@password", mySqlConnection))
            {
                mySqlCommand.Parameters.AddWithValue("@username", usernameOrEmail);
                mySqlCommand.Parameters.AddWithValue("@email", usernameOrEmail);
                mySqlCommand.Parameters.AddWithValue("@password", password);

                mySqlConnection.Open();
                using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        mySqlConnection.Close();
                        return true;
                    }
                }
                mySqlConnection.Close();
                return false;
            }
        }


        public bool Register(string username, string password, string email)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"INSERT INTO users (username, password, email) VALUES (@username, @password, @email)", mySqlConnection))
            {
                mySqlCommand.Parameters.AddWithValue("@username", username);
                mySqlCommand.Parameters.AddWithValue("@password", password);
                mySqlCommand.Parameters.AddWithValue("@email", email);

                mySqlConnection.Open();
                int rowsAffected = mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

                return rowsAffected > 0;
            }
        }

        public UserInfo GetUserInfo(string username)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT * FROM users WHERE username=@username", mySqlConnection))
            {
                mySqlCommand.Parameters.AddWithValue("@username", username);

                mySqlConnection.Open();
                using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UserInfo userInfo = new UserInfo
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = reader["username"].ToString(),
                            Email = reader["email"].ToString(),
                        };

                        mySqlConnection.Close();
                        return userInfo;
                    }
                }
                mySqlConnection.Close();
                return null;
            }
        }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
