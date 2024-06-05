using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace RoomMaster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string dbConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Misc", "dbconfig.json");

            if (!File.Exists(dbConfigPath))
            {
                MessageBox.Show("To run this application, you need to request the dbconfig.json file for the connection string, this is to allow for a layer of protection for exposing my connection strings.", "Connection String Required", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
    }

}
