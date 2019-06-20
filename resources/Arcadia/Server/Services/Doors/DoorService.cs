using System.Linq;
using System.Collections.Generic;
//
//
using CherryMPServer;
using CherryMPShared;
//;
using System.Data;
using CherryMPServer.Constant;
using Newtonsoft.Json;
using System;
using Arcadia.Server.Models;
using MySQL;

namespace Arcadia.Server.Services.Doors
{
    class DoorService : Script
    {
        public DoorService()
        {
            API.onEntityEnterColShape += ColShapeTrigger;
        }

        public const ulong SET_STATE_OF_CLOSEST_DOOR_OF_TYPE = 0xF82D8F1926A02C3D;

        private void ColShapeTrigger(ColShape colshape, NetHandle entity)
        {
            var player = API.getPlayerFromHandle(entity);

            if (player == null) return;

            if (colshape != null && colshape.getData("IS_DOOR_TRIGGER") == true)
            {
                var id = colshape.getData("DOOR_ID");
                var info = colshape.getData("DOOR_INFO");

                float heading = 0f;

                if (info.State != null) heading = info.State;

                API.sendNativeToPlayer(player, SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                    info.Hash, info.Position.X, info.Position.Y, info.Position.Z,
                    info.Locked, heading, false);
            }
        }

        public static readonly List<DoorInfo> DoorList = new List<DoorInfo>();

        public static void LoadAllDoorsFromDB()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            DataTable result = Database.ExecutePreparedStatement("SELECT * FROM doors", parameters);
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    DoorInfo door = new DoorInfo
                    {
                        Id = (int)row["Id"],
                        Hash = (int)row["Hash"],
                        Position = JsonConvert.DeserializeObject<Vector3>((string)row["Position"]),
                        Locked = Convert.ToBoolean((int)row["Locked"]),
                        State = (float)row["State"]
                    };

                    ColShape colShape = API.shared.createSphereColShape(door.Position, 35f);
                    colShape.setData("DOOR_INFO", door);
                    colShape.setData("DOOR_ID", door.Id);
                    colShape.setData("IS_DOOR_TRIGGER", true);
                    door.ColShape = colShape;

                    colShape = API.shared.createSphereColShape(door.Position, 4f);
                    colShape.setData("DOOR_INFO", door);
                    colShape.setData("DOOR_ID", door.Id);
                    colShape.setData("IS_DOOR_TRIGGER", true);
                    door.ShortRangeColShape = colShape;
                    DoorList.Add(door);
                }
                API.shared.consoleOutput(LogCat.Info, result.Rows.Count + "Двери загружены!");
            }
            else
            {
                API.shared.consoleOutput(LogCat.Info, "Не удалось загрузить двери!");
            }
        }

        public static void AddNewDoor(int model, Vector3 position)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "@Hash", model.ToString() },
                { "@Position", JsonConvert.SerializeObject(position) },
                { "@Locked", Convert.ToInt32(false).ToString() },
                { "@State", "0" }
            };
            DataTable result = Database.ExecutePreparedStatement("INSERT INTO doors (Hash, Position, Locked, State) " +
                "VALUES (@Hash, @Position, @Locked, @State)", parameters);
        }

        public static void ReloadDoors()
        {
            DoorList.ForEach(door => {
                API.shared.deleteColShape(door.ColShape);
                API.shared.deleteColShape(door.ShortRangeColShape);
            });
            DoorList.Clear();
            LoadAllDoorsFromDB();
        }

        public static bool ToggleDoorState(int doorId)
        {
            DoorInfo door = DoorList.FirstOrDefault(x => x.Id == doorId);
            if (door == null) { return false; }
            SetDoorState(door.Id, !door.Locked, 0);
            return door.Locked;
        }

        public static void SetDoorState(int doorId, bool locked, float heading)
        {
            DoorInfo door = DoorList.FirstOrDefault(x => x.Id == doorId);
            if (door != null)
            {
                door.Locked = locked;
                door.State = heading;

                door.ColShape.setData("DOOR_INFO", door);
                door.ShortRangeColShape.setData("DOOR_INFO", door);

                foreach (var entity in door.ColShape.getAllEntities())
                {
                    var player = API.shared.getPlayerFromHandle(entity);

                    if (player == null) continue;

                    float cH = door.State;

                    API.shared.sendNativeToPlayer(player, SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                        door.Hash, door.Position.X, door.Position.Y, door.Position.Z,
                        door.Locked, cH, false);
                }
            }
        }
    }
}
