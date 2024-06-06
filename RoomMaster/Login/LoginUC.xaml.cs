using RoomMaster.Misc;
using System;
using System.Windows;
using System.Windows.Controls;

namespace RoomMaster.Login
{
    public partial class LoginUC : UserControl
    {
        public event EventHandler LoginSuccessful;

        public LoginUC()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isValid = DatabaseHelper.ValidateUser(username, password);
            if (isValid)
            {
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Login successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
