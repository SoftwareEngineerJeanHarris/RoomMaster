using RoomMaster.Guests;
using RoomMaster.Login;
using RoomMaster.Options;
using RoomMaster.Rooms;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoomMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Room> Rooms { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Welcome, " + AppSettings.Name; 
            
            Rooms = new ObservableCollection<Room>
            {
                new Room { RoomNumber = "Room 101", Status = "Available", Guest = new Guest { Name = "" }, CheckoutDate = null },
                new Room { RoomNumber = "Room 102", Status = "Booked", Guest = new Guest { Name = "John Doe" }, CheckoutDate = DateTime.Now.AddDays(1) },
                new Room { RoomNumber = "Room 103", Status = "Checked-In", Guest = new Guest { Name = "Jane Smith" }, CheckoutDate = DateTime.Now.AddDays(2) },
                new Room { RoomNumber = "Room 104", Status = "Under Repair", Guest = new Guest { Name = "" }, CheckoutDate = null },
                new Room { RoomNumber = "Room 105", Status = "Available", Guest = new Guest { Name = "" }, CheckoutDate = null },
                // Add more rooms as needed, up to 35
            };
            DataContext = this;
        }
    }
}