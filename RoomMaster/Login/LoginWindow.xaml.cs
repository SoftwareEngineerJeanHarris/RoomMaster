using System.Windows;
using System.Windows.Controls;

namespace RoomMaster.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginUC_LoginSuccessful(object sender, User user)
        {
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }

        private void CreateAccountUC_AccountCreated(object sender, User user)
        {
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }

        private void SwitchToCreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginUC.Visibility == Visibility.Visible)
            {
                LoginUC.Visibility = Visibility.Collapsed;
                CreateAccountUC.Visibility = Visibility.Visible;
                ((Button)sender).Content = "Switch to Login";
            }
            else
            {
                LoginUC.Visibility = Visibility.Visible;
                CreateAccountUC.Visibility = Visibility.Collapsed;
                ((Button)sender).Content = "Switch to Create Account";
            }
        }
    }
}
