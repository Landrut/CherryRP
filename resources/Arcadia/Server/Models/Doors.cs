using CherryMPServer;
using CherryMPShared;
//;

namespace Arcadia.Server.Models
{
    public class DoorInfo
    {
        public int Hash { get; set; }
        public Vector3 Position { get; set; }
        public int Id { get; set; }

        public bool Locked { get; set; }
        public float State { get; set; }

        public ColShape ColShape { get; set; }
        public ColShape ShortRangeColShape { get; set; }
    }
}
