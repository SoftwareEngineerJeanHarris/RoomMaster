using RoomMaster.Misc;
using System.Windows;
using System.Windows.Controls;

namespace RoomMaster.Login
{
    /// <summary>
    /// Interaction logic for LoginUC.xaml
    /// </summary>
    public partial class LoginUC : UserControl
    {
        public event EventHandler<User> LoginSuccessful;

        public LoginUC()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (DatabaseHelper.ValidateUser(username, password))
            {
                User user = new User { Username = username, Password = password };
                LoginSuccessful?.Invoke(this, user);
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}
