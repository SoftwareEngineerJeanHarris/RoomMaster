using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace RoomMaster.Misc.Controls
{
    internal class Helper
    {
        public static void ShowBanner(Label bannerLabel, string message, NotificationType type)
        {
            bannerLabel.Content = message;

            switch (type)
            {
                case NotificationType.Success:
                    bannerLabel.Background = new SolidColorBrush(Colors.Green);
                    break;
                case NotificationType.Error:
                    bannerLabel.Background = new SolidColorBrush(Colors.Red);
                    break;
            }

            bannerLabel.Visibility = System.Windows.Visibility.Visible;

            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            timer.Tick += (s, e) =>
            {
                bannerLabel.Visibility = System.Windows.Visibility.Collapsed;
                timer.Stop();
            };
            timer.Start();
        }
    }
}
