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

public class LostMC : Script
{
    public LostMC()
    {
        API.onResourceStart += onResourceStart;
    }

    public readonly Vector3 LostMCBlippPos = new Vector3(1005.78f, -114.4409f, 73.97013f);
    public readonly Vector3 LostMCEnterHousePos = new Vector3(1005.78f, -114.4409f, 73.97013f);

    public readonly Vector3 LostMCExitHousePos = new Vector3(1065.997f, -3183.441f, -39.16351f);



    public Blip LostMCBlip;


    public ColShape LostMCEnterHouseColshape;

    public ColShape LostMCExitHouseColshape;



    public void onResourceStart()
    {
        API.requestIpl("bkr_biker_interior_placement_interior_3_biker_dlc_int_ware02_milo ");
        API.requestIpl("bkr_bi_hw1_13_int");

        List<Vehicle> LostMCVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)2053223216, new Vector3(950.1777, -122.9707, 74.34542), new Vector3(0.1709283, -0.3566383, -138.3452), 1, 1, 0), // LostCar
            API.createVehicle((VehicleHash)1491277511, new Vector3(971.0103, -112.6652, 73.82076), new Vector3(-1.904216, -9.757932, -159.0383), 22, 94, 0), // LostCar1
            API.createVehicle((VehicleHash)1873600305, new Vector3(969.7512, -113.8621, 73.81989), new Vector3(1.225129, -5.524207, -156.7677), 1, 1, 0), // LostCar2
            API.createVehicle((VehicleHash)1873600305, new Vector3(968.9027, -114.5672, 73.81473), new Vector3(1.389329, -9.997059, -159.4203), 1, 1, 0), // LostCar3
            API.createVehicle((VehicleHash)(-1606187161), new Vector3(966.9993, -116.8898, 73.8759), new Vector3(-1.404195, -9.740121, -155.0462), 1, 1, 0), // LostCar4
            API.createVehicle((VehicleHash)(-159126838), new Vector3(971.935, -111.9902, 73.78563), new Vector3(0.2434836, -5.537365, -160.2308), 1, 1, 0), // LostCar5
            API.createVehicle((VehicleHash)(-2115793025), new Vector3(965.8952, -117.7498, 73.82553), new Vector3(-1.403961, -2.768543, -154.6968), 1, 1, 0), // LostCar6
            API.createVehicle((VehicleHash)(-2115793025), new Vector3(964.9878, -118.3564, 73.81953), new Vector3(-1.751259, -7.712617, -158.623), 1, 1, 0), // LostCar7
            API.createVehicle((VehicleHash)(-2115793025), new Vector3(964.1639, -118.9805, 73.82057), new Vector3(-1.75811, -6.505342, -160.0638), 1, 1, 0), // LostCar8
            API.createVehicle((VehicleHash)(-2115793025), new Vector3(963.0764, -119.7229, 73.81986), new Vector3(-1.720882, -7.753591, -162.842), 1, 1, 0), // LostCar9
            API.createVehicle((VehicleHash)(-589178377), new Vector3(981.3338, -129.045, 72.79763), new Vector3(8.903959, 0.02923875, 58.1654), 1, 1, 0), // LostCar10
            API.createVehicle((VehicleHash)833469436, new Vector3(978.0262, -133.362, 73.32454), new Vector3(7.406063, 0.05749185, 58.58335), 1, 1, 0), // LostCar11
            API.createVehicle((VehicleHash)1180875963, new Vector3(985.5464, -138.4357, 72.6734), new Vector3(0.07104804, -0.02645477, 57.05006), 1, 1, 0), // LostCar12
            API.createVehicle((VehicleHash)(-1745203402), new Vector3(966.7733, -140.9158, 74.24145), new Vector3(0.2514894, 0.2624993, 58.6483), 8, 70, 0), // LostCar13
            API.createVehicle((VehicleHash)(-1745203402), new Vector3(955.8264, -133.9279, 74.25818), new Vector3(-0.2377292, -0.7588211, -121.1004), 100, 126, 0)
        };

        Vehicle LostMCveh2;
        foreach (Vehicle LostMCveh in LostMCVehicles)
        {
            API.setEntityData(LostMCveh, "gang_id", 4);
            API.setVehicleNumberPlate(LostMCveh, "LostMC");
            API.setVehicleEngineStatus(LostMCveh, false);

            LostMCveh2 = LostMCveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int isLostMCGang = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "LostMC")
            {
                if (isLostMCGang != 4)
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

        LostMCBlip = API.createBlip(LostMCBlippPos);
        API.setBlipSprite(LostMCBlip, 226);
        API.setBlipScale(LostMCBlip, 1.0f);
        API.setBlipColor(LostMCBlip, 1);
        API.setBlipName(LostMCBlip, "LostMC");
        API.setBlipShortRange(LostMCBlip, true);

        LostMCEnterHouseColshape = API.createCylinderColShape(LostMCEnterHousePos, 0.50f, 1f);
        LostMCExitHouseColshape = API.createCylinderColShape(LostMCExitHousePos, 0.50f, 1f);

        LostMCExitHouseColshape = API.createCylinderColShape(LostMCExitHousePos, 0.50f, 1f);

        API.createMarker(1, LostMCEnterHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в склад LostMC", LostMCEnterHousePos, 15f, 0.65f);

        API.createMarker(1, LostMCExitHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход со склада", LostMCExitHousePos, 15f, 0.65f);

        LostMCEnterHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                int lsLostMCfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
                if (lsLostMCfaction == 4)
                {
                    API.setEntityPosition(player, new Vector3(1064.922f, -3183.589f, -39.1636f));
                    API.setEntityDimension(player, 4);
                    API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
                }
                else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
            }
        };


        LostMCExitHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsLostMCfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsLostMCfaction == 4)
            {
                API.setEntityPosition(player, new Vector3(1004.828f, -113.317f, 73.96364f));
                API.setEntityDimension(player, 0);
                API.sendPictureNotificationToPlayer(player, "Ave LostMC!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
        };

    }

    private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (Player.GetFractionId(player) != 4)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной банде");
                return;
            }
            else return;
        }
    }
}
