using System.Windows;
using System.Windows.Controls;

namespace RoomMaster.Misc.Controls
{
    public partial class NotificationBanner : UserControl
    {
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(NotificationBanner), new PropertyMetadata(""));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public NotificationBanner()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
