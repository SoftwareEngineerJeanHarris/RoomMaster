using RoomMaster.Misc;
using RoomMaster.Misc.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

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

            bool isValid = DatabaseHelper.ValidateUser(username, password);
            if (isValid)
            {
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Helper.ShowBanner(banner, "Invalid username or password.", NotificationType.Error);
            }
        }
    }
}
