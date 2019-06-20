using CherryMPServer;
using CherryMPShared;
using System.Collections.Generic;

namespace Arcadia.Server.Models
{
    public class ShopStorage
    {
             
        public string NameShop { get; set; }
        public Vector3 Position { get; set; }        
        public int eCola { get; set; }
        public int Sprunk { get; set; }
        public int Schokoriegel { get; set; }
        public int Wasser { get; set; }
        public int Chips { get; set; }
        public int Repair_Kit { get; set; }
        public int Bottle { get; set; }
        public int Flashlight { get; set; }
        public int Beer { get; set; }
        public int Caffe { get; set; }
        public int Benzin_Kanister { get; set; }
        public int Tabak { get; set; }
        public int Gamburger { get; set; }
        public int Viske { get; set; }

        public ColShape ColShape { get; set; }
    }
}
