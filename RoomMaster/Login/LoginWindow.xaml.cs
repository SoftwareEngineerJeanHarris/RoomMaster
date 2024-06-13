using RoomMaster.Misc.Controls;
using RoomMaster.Options;
using System.Reflection;
using System.Windows;

namespace RoomMaster.Login
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginUC_LoginSuccessful(object sender, EventArgs e)
        {
            Helper.ShowBanner(LoginUC.banner, "Logging in.", NotificationType.Success);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void CreateAccountUC_AccountCreated(object sender, EventArgs e)
        {
            SwitchToLoginView();
            Helper.ShowBanner(LoginUC.banner, "Account Created.", NotificationType.Success);
        }

        private void SwitchToCreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToCreateAccountView();
        }

        private void SwitchToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToLoginView();
        }

        private void SwitchToCreateAccountView()
        {
            LoginUC.Visibility = Visibility.Collapsed;
            CreateAccountUC.Visibility = Visibility.Visible;
            CreateAccountSwitchToLogin.Visibility = Visibility.Visible;
            LoginSwitchToCreateAccount.Visibility = Visibility.Hidden;
        }

        private void SwitchToLoginView()
        {
            LoginUC.Visibility = Visibility.Visible;
            CreateAccountUC.Visibility = Visibility.Collapsed;
            CreateAccountSwitchToLogin.Visibility = Visibility.Hidden;
            LoginSwitchToCreateAccount.Visibility = Visibility.Visible;
        }
    }
}
