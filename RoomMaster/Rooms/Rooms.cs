using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomMaster.Guests;

namespace RoomMaster.Rooms
{
    class Rooms
    {
        public string RoomNumber { get; set; }
        public string Status { get; set; }
        public  Guest guest { get; set; }
        public DateTime? CheckoutDate { get; set; }
    }
}
