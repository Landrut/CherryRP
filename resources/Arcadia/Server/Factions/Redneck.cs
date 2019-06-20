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

public class Redneck : Script
    {
    public Redneck()
    {
        API.onResourceStart += onResourceStart;
    }

    //(2435.457, 4975.898, 46.57142, 0, 0, -133.3672) // RedneckBlipPos
//(2435.457, 4975.898, 46.57142, 0, 0, -133.3672) // RedneckEnterPos
//(2436.197, 4979.421, 46.57142, 0, 0, 46.2193) // RedneckExitOutsidePos
//(2436.646, 4974.792, 46.8106, 0, 0, 43.57576) // RedneckExitPos
//(2436.646, 4974.792, 46.8106, 0, 0, 43.57576) // RedneckEnterInsidePos

    public readonly Vector3 RedneckBlipPos = new Vector3(2435.457f, 4975.898f, 46.57142f);
    public readonly Vector3 RedneckEnterPos = new Vector3(2435.350f, 4975.898f, 46.57142f);
    public readonly Vector3 RedneckExitPos = new Vector3(2436.646f, 4974.792f, 46.8106f);
    public readonly Vector3 RedneckExitOutsidePos = new Vector3(2436.197f, 4979.421f, 46.57142f);
    public readonly Vector3 RedneckEnterInsidePos = new Vector3(2438.187f, 4973.438f, 46.82562f);
    public readonly Vector3 RedneckDutyPos = new Vector3(2428.583f, 4966.292f, 46.93646f);

    public Blip RedneckBlip;

    public ColShape RedneckEnterColshape;
    public ColShape RedneckExitColshape;
    public ColShape RedneckDutyColshape;

    public void onResourceStart()
    {
       // API.requestIpl("des_farmhouse");
        API.requestIpl("farm");
        API.requestIpl("farmint");
        API.requestIpl("farm_props﻿");
        API.removeIpl("farmint_cap");

        RedneckBlip = API.createBlip(RedneckBlipPos);

        API.setBlipSprite(RedneckBlip, 141);
        API.setBlipScale(RedneckBlip, 1.0f);
        API.setBlipColor(RedneckBlip, 5);
        API.setBlipName(RedneckBlip, "Рэднеки");
        API.setBlipShortRange(RedneckBlip, true);

        RedneckEnterColshape = API.createCylinderColShape(RedneckEnterPos, 0.50f, 1f);
        RedneckExitColshape = API.createCylinderColShape(RedneckExitPos, 0.50f, 1f);
        RedneckDutyColshape = API.createCylinderColShape(RedneckDutyPos, 0.50f, 1f);

        API.createMarker(1, RedneckEnterPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.00f, 1.00f, 1.00f), 80, 47, 245, 113); // green
        API.createMarker(1, RedneckExitPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.00f, 1.00f, 1.00f), 80, 47, 245, 113); // green
        API.createMarker(1, RedneckDutyPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.00f, 1.00f, 1.00f), 80, 247, 137, 47); // orange

        List<Vehicle> RedVehicles = new List<Vehicle>();
        API.createVehicle((VehicleHash)(-810318068), new Vector3(2429.198, 4990.554, 45.95653), new Vector3(-2.000701, -0.3500328, -134.9315), 110, 71, 0); // RedneckCar1
        API.createVehicle((VehicleHash)(-1435919434), new Vector3(2432.268, 4993.61, 46.17336), new Vector3(-1.208665, 1.102477, -135.4071), 141, 104, 0); // RedneckCar2
        API.createVehicle((VehicleHash)1753414259, new Vector3(2421.51, 4966.505, 45.60644), new Vector3(0.3422504, -15.00663, 63.31558), 101, 101, 0); // RedneckCar3
        API.createVehicle((VehicleHash)1753414259, new Vector3(2422.425, 4967.467, 45.60852), new Vector3(0.5650553, -14.56588, 62.84566), 101, 101, 0); // RedneckCar4
        API.createVehicle((VehicleHash)1753414259, new Vector3(2423.394, 4968.491, 45.55652), new Vector3(2.600494, -13.57085, 59.39398), 101, 101, 0); // RedneckCar5
        API.createVehicle((VehicleHash)1753414259, new Vector3(2424.363, 4969.536, 45.49768), new Vector3(4.644939, -13.89371, 53.59214), 101, 101, 0); // RedneckCar6

        Vehicle Redveh2;
        foreach (Vehicle Redveh in RedVehicles)
        {
            API.setEntityData(Redveh, "fraction_id", 5);
            API.setVehicleNumberPlate(Redveh, "Redneck");
            API.setVehicleEngineStatus(Redveh, false);

            Redveh2 = Redveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int isRedneckFaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "Redneck")
            {
                if (isRedneckFaction != 5)
                {
                    if (API.getPlayerVehicleSeat(client) == -1)
                    {
                        API.sendChatMessageToPlayer(client, "Вы не состоите в данной фракции!");
                        API.warpPlayerOutOfVehicle(client);
                        return;
                    }
                    else return;
                }
                else return;
            }
            else return;
        };

        RedneckEnterColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else API.setEntityPosition(player, RedneckEnterInsidePos);
        };

        RedneckExitColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else API.setEntityPosition(player, RedneckExitOutsidePos);
        };

        RedneckDutyColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int isredneckfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0; ;
            if (isredneckfaction == 5)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 0)
                {
                    Faction.SetPlayerFactionClothes(player);
                    Player.SetPlayerOnDuty(player);
                    /*
                    foreach (Client client in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(client) == 7 && Player.IsPlayerOnDuty(client) == 1)
                        {
                            API.sendChatMessageToPlayer(client, chatMessageOnDuty);
                        }
                    }
                    API.sendChatMessageToPlayer(player, "~y~Вы вышли на смену как ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                    */
                    API.sendChatMessageToPlayer(player, "~y~Вы в рабочую форму");
                }
                else
                {
                    Player.RemovePlayerOnDuty(player);
                    /*
                    foreach (Client client in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(client) == 7 && Player.IsPlayerOnDuty(client) == 1)
                        {
                            API.sendChatMessageToPlayer(client, chatMessageOffDuty);
                        }
                    }
                    */
                    PlayerFunctions.Player.LoadPlayerClothes(player);
                    API.sendChatMessageToPlayer(player, "~y~Вы переоделись в чистую одежду");
                }
                return;
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции!");
            }
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
        };

    }
    private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (Player.GetFractionId(player) != 5)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции");
                return;
            }
            else return;
        }
    }
}
