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
            // Handle post-login actions here (e.g., navigating to the main application window)
        }

        private void CreateAccountUC_AccountCreated(object sender, EventArgs e)
        {
            SwitchToLoginView();
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
