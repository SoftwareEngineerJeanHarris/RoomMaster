using RoomMaster.Misc;
using RoomMaster.Misc.Controls;
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

            if(ValidFields(fullName, email, username, password, confirmPassword))
            {
                AtemptCreatingUser(username, password, fullName, email);
            }
        }

        private bool ValidFields(string fullName, string email, string username, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                Helper.ShowBanner(banner, "All fields are required.", NotificationType.Error);
                return false;
            }

            if (!IsValidEmail(email))
            {
                Helper.ShowBanner(banner, "Invalid email format.", NotificationType.Error);
                return false;
            }

            if (password != confirmPassword)
            {
                Helper.ShowBanner(banner, "Passwords do not match.", NotificationType.Error);
                return false;
            }

            if (!IsStrongPassword(password))
            {
                Helper.ShowBanner(banner, "Password must be at least 8 characters long and contain a mix of letters, numbers, and special characters.", NotificationType.Error);
                return false;
            }

            if (DatabaseHelper.UserExists(username))
            {
                Helper.ShowBanner(banner, "A user with this username already exists.", NotificationType.Error);
                return false;
            }

            return true;
        }

        private void AtemptCreatingUser(string username, string password, string fullName, string email)
        {
            bool isCreated = DatabaseHelper.CreateUser(username, password, fullName, email, 1);
            if (isCreated)
            {
                Helper.ShowBanner(banner, "Account created successfully.", NotificationType.Success);
                ClearFields();
                AccountCreated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Helper.ShowBanner(banner, "Connection error creating account. Please try again.", NotificationType.Notification);
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

        private void ClearFields()
        {
            FullNameTextBox.Clear();
            EmailTextBox.Clear();
            NewUsernameTextBox.Clear();
            NewPasswordBox.Clear();
            ConfirmPasswordBox.Clear();
        }
    }
}
