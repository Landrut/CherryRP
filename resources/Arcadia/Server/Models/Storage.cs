using CherryMPServer;
using CherryMPShared;
using System.Collections.Generic;

namespace Arcadia.Server.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public int IdFraction { get; set; }
        public string NameFaction { get; set; }
        public Vector3 Position { get; set; }
        public bool Locked { get; set; }
        public int Pistols { get; set; }
        public int Rifle { get; set; }
        public int SMG { get; set; }
        public int Shotgun { get; set; }
        public int Bullets { get; set; }
        public int Melee { get; set; }
        public int Armor { get; set; }
        public int Medicals { get; set; }
        public int Patrols { get; set; }
        public int Deportament { get; set; }
        public int Trening { get; set; }
        public int SWAT { get; set; }
        public int NoMarker { get; set; }
        public int Detectiv { get; set; }
        public int Narko { get; set; }

        public ColShape ColShape { get; set; }
    }
}