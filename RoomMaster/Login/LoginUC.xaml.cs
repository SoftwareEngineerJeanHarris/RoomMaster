using RoomMaster.Misc.Controls;
using RoomMaster.Misc;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoomMaster.Options;

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
                Helper.ShowBanner(banner, "Username and password are required.", NotificationType.Error);
                return;
            }

            bool isValid = DatabaseHelper.ValidateUserForLogin(username, password);
            if (isValid)
            {
                Helper.ShowBanner(banner, "Logging in.", NotificationType.Success);
                AppSettings.UserName = username;

                (AppSettings.Name, AppSettings.Permission) = DatabaseHelper.GetProfile(username);

                LoginSuccessful?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Helper.ShowBanner(banner, "Invalid username or password.", NotificationType.Error);
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Helper.ShowBanner(banner, "Please wait while we fetch your profile.", NotificationType.Notification);
                LoginButton_Click(LoginButton, null);
            }
        }
    }
}
