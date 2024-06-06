using RoomMaster.Misc;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace RoomMaster.Login
{
    public partial class CreateAccountUC : UserControl
    {
        public event EventHandler AccountCreated;

        public CreateAccountUC()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            string email = EmailTextBox.Text;
            string username = NewUsernameTextBox.Text;
            string password = NewPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsStrongPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters long and contain a mix of letters, numbers, and special characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DatabaseHelper.UserExists(username))
            {
                MessageBox.Show("A user with this username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isCreated = DatabaseHelper.CreateUser(username, password, fullName, email, 1);
            if (isCreated)
            {
                AccountCreated?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Account created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error creating account. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsStrongPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUpperChar = false, hasLowerChar = false, hasDigit = false, hasSpecialChar = false;
            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpperChar = true;
                else if (char.IsLower(c)) hasLowerChar = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else hasSpecialChar = true;
            }

            return hasUpperChar && hasLowerChar && hasDigit && hasSpecialChar;
        }
    }
}
