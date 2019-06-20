//
using System.Collections.Generic;
using System.Linq;
using Arcadia.Server.XMLDatabase;
using CherryMPServer;
using CherryMPShared;

namespace Arcadia.Server.Managers
{
    public class BlipManager : Script
    {

        public static List<CherryMPServer.Blip> BlipsOnMap = new List<CherryMPServer.Blip>();
        public BlipManager()
        {

            db_Blips dbBlips = new XMLDatabase.db_Blips();
            dbBlips.GetAll();

            foreach (var item in db_Blips.currentBlips.Items)
            {
                BlipsOnMap.Add(API.createBlip(item.Position, item.Range, item.Dimension));
                BlipsOnMap.LastOrDefault().color = item.Color;
                BlipsOnMap.LastOrDefault().name = item.Name;
                BlipsOnMap.LastOrDefault().sprite = item.ModelId;
                BlipsOnMap.LastOrDefault().shortRange = item.ShortRange;
            }
        }
    }
}
