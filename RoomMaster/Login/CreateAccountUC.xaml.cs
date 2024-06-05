using RoomMaster.Misc;
using System.Windows;
using System.Windows.Controls;

namespace RoomMaster.Login
{
    /// <summary>
    /// Interaction logic for CreateAccountUC.xaml
    /// </summary>
    public partial class CreateAccountUC : UserControl
    {
        public event EventHandler<User> AccountCreated;

        public CreateAccountUC()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            string username = NewUsernameTextBox.Text;
            string password = NewPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            if (DatabaseHelper.CreateUser(username, password))
            {
                User user = new User { Username = username, Password = password };
                AccountCreated?.Invoke(this, user);
            }
            else
            {
                MessageBox.Show("Account creation failed. Try a different username.");
            }
        }
    }
}
