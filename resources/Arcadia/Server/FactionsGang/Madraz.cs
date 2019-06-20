using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//
//
using CherryMPServer;
using CherryMPServer.Constant;
using CherryMPShared;
//;

using MySQL;
using PlayerFunctions;
using CustomSkin;
using System.Collections.Generic;

public class Madraz : Script
{
    public Madraz()
    {
        API.onResourceStart += onResourceStart;
    }

    public readonly Vector3 MadrazBlippPos = new Vector3(1394.461f, 1141.83f, 114.6081f);
    public readonly Vector3 MadrazEnterHousePos = new Vector3(1394.461f, 1141.83f, 114.6081f);
    public readonly Vector3 MadrazExitHousePos = new Vector3(1396.745f, 1141.505f, 114.3336f);


    public Blip MadrazBlip;


    public ColShape MadrazEnterHouseColshape;
    public ColShape MadrazExitHouseColshape;


    public void onResourceStart()
    {
        List<Vehicle> MadrazVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)1348744438, new Vector3(1387.103, 1116.838, 114.5502), new Vector3(-1.166459, -0.6314517, 60.16597), 11, 88, 0), // madrazzo
            API.createVehicle((VehicleHash)1348744438, new Vector3(1393.025, 1116.891, 114.6264), new Vector3(-0.2354501, -0.2194151, 61.59468), 11, 88, 0), // madrazzo1
            API.createVehicle((VehicleHash)1348744438, new Vector3(1381.394, 1117.568, 114.3944), new Vector3(-1.94663, -0.5017591, 59.08216), 11, 88, 0), // madrazzo2
            API.createVehicle((VehicleHash)142944341, new Vector3(1367.859, 1156.354, 113.6675), new Vector3(-0.6997388, 0.01852753, 48.76898), 11, 88, 0), // madrazzo3
            API.createVehicle((VehicleHash)(-808831384), new Vector3(1353.53, 1155.635, 113.737), new Vector3(-0.4100908, -0.0205409, 132.1038), 11, 88, 0), // madrazzo4
            API.createVehicle((VehicleHash)(-808831384), new Vector3(1356.865, 1137.559, 113.7381), new Vector3(-0.2524501, -0.06745677, -115.8443), 11, 88, 0), // madrazzo5
            API.createVehicle((VehicleHash)(-391594584), new Vector3(1360.723, 1178.573, 112.2381), new Vector3(-1.819827, -2.07155, -0.5275105), 11, 88, 0), // madrazzo6
            API.createVehicle((VehicleHash)(-89291282), new Vector3(1360.715, 1172.191, 112.6412), new Vector3(-5.174406, -1.073234, 0.3002387), 11, 88, 0), // madrazzo7
            API.createVehicle((VehicleHash)(-391594584), new Vector3(1360.844, 1165.549, 113.223), new Vector3(-4.079458, -0.3851952, 0.9705957), 11, 88, 0), // madrazzo8
            API.createVehicle((VehicleHash)(-1743316013), new Vector3(1401.142, 1117.145, 114.6533), new Vector3(-0.02484969, 0.02618872, -0.7618424), 11, 88, 0), // madrazzo1
            API.createVehicle((VehicleHash)(-1743316013), new Vector3(1407.038, 1117.21, 114.6333), new Vector3(-0.6025849, -0.64552, -0.2441373), 11, 88, 0), // madrazzo2
            API.createVehicle((VehicleHash)1348744438, new Vector3(1374.472, 1121.036, 114.0968), new Vector3(-2.640113, -0.004402803, 44.2356), 11, 88, 0) // madrazzo3
    };

        Vehicle Madrazveh2;
        foreach (Vehicle Madrazveh in MadrazVehicles)
        {
            API.setEntityData(Madrazveh, "gang_id", 6);
            API.setVehicleNumberPlate(Madrazveh, "Madraz");
            API.setVehicleEngineStatus(Madrazveh, false);

            Madrazveh2 = Madrazveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int isMadrazGang = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "Madraz")
            {
                if (isMadrazGang != 6)
                {
                    if (API.getPlayerVehicleSeat(client) == -1)
                    {
                        API.sendChatMessageToPlayer(client, "Вы не состоите в данной банде!");
                        API.warpPlayerOutOfVehicle(client);
                        return;
                    }
                    else return;
                }
                else return;
            }
            else return;
        };

        MadrazBlip = API.createBlip(MadrazBlippPos);
        API.setBlipSprite(MadrazBlip, 543);
        API.setBlipScale(MadrazBlip, 1.0f);
        API.setBlipColor(MadrazBlip, 59);
        API.setBlipName(MadrazBlip, "Madraz");
        API.setBlipShortRange(MadrazBlip, true);

        MadrazEnterHouseColshape = API.createCylinderColShape(MadrazEnterHousePos, 0.50f, 1f);
        MadrazExitHouseColshape = API.createCylinderColShape(MadrazExitHousePos, 0.50f, 1f);




        API.createMarker(1, MadrazEnterHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в дом Madraz", MadrazEnterHousePos, 15f, 0.65f);
        API.createMarker(1, MadrazExitHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход с дома", MadrazExitHousePos, 15f, 0.65f);

        MadrazEnterHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                int lsMadrazfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
                if (lsMadrazfaction == 6)
                {
                    API.setEntityPosition(player, new Vector3(1397.109f, 1142.683f, 114.3336f));
                    API.setEntityDimension(player, 6);
                    API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
                }
                else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
            }
        };


        MadrazExitHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsMadrazfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsMadrazfaction == 6)
            {
                API.setEntityPosition(player, new Vector3(1393.39f, 1141.809f, 114.4433f));
                API.setEntityDimension(player, 0);
                API.sendPictureNotificationToPlayer(player, "Ave Madraz!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
        };
    }

    private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (Player.GetFractionId(player) != 6)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной банде");
                return;
            }
            else return;
        }
    }
}
