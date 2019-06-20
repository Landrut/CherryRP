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
        API.createVehicle((VehicleHash)1069929536, new Vector3(2529.07, 4965.882, 44.15203), new Vector3(1.433007, -2.222221, 86.67546), 97, 97, 0); // redneck1
        API.createVehicle((VehicleHash)1069929536, new Vector3(2529.081, 4970.596, 44.26245), new Vector3(-0.2470856, -3.314916, 92.73549), 97, 97, 0); // redneck2
        API.createVehicle((VehicleHash)(-1477580979), new Vector3(2463.57, 4962.282, 44.71824), new Vector3(-0.06886934, 2.730978, 135.5581), 97, 97, 0); // redneck3
        API.createVehicle((VehicleHash)(-1477580979), new Vector3(2457.591, 4956.298, 44.71935), new Vector3(-0.1329874, 1.960458, 134.5567), 97, 97, 0); // redneck4
        API.createVehicle((VehicleHash)(-1189015600), new Vector3(2423.783, 4958.287, 46.00284), new Vector3(1.463413, -0.9057194, 43.65947), 97, 97, 0); // redneck5
        API.createVehicle((VehicleHash)(-16948145), new Vector3(2422.723, 4969.979, 45.65261), new Vector3(-0.6451266, 0.7371159, -42.65478), 97, 97, 0); // redneck6
        API.createVehicle((VehicleHash)(-16948145), new Vector3(2429.922, 4989.672, 45.71655), new Vector3(0.05432734, -3.746885, 134.7644), 97, 97, 0); // redneck7
        API.createVehicle((VehicleHash)(-1453280962), new Vector3(2434.446, 4982.456, 45.47869), new Vector3(-0.7089658, -14.07642, 7.506919), 97, 97, 0); // redneck8
        API.createVehicle((VehicleHash)(-1453280962), new Vector3(2437.244, 4984.815, 45.49623), new Vector3(0.4598258, -14.71106, 16.87983), 97, 97, 0); // redneck9
        API.createVehicle((VehicleHash)(-1453280962), new Vector3(2435.92, 4983.697, 45.46737), new Vector3(0.8983734, -15.80152, 7.609585), 97, 97, 0); // redneck10
        API.createVehicle((VehicleHash)729783779, new Vector3(2451.458, 4997.02, 45.64552), new Vector3(0.2138452, -0.8681133, 44.7455), 97, 97, 0); // redneck11
        API.createVehicle((VehicleHash)65402552, new Vector3(2409.331, 4994.915, 45.93899), new Vector3(-5.047274, -0.2833859, 177.1147), 97, 97, 0); // redneck12
        API.createVehicle((VehicleHash)65402552, new Vector3(2405.795, 4992.981, 45.91076), new Vector3(-2.953881, -0.9416638, -176.491), 97, 97, 0); // redneck13
        API.createVehicle((VehicleHash)1645267888, new Vector3(2429.943, 5038.423, 45.83714), new Vector3(-0.7316478, 0.2128453, 135.4525), 97, 97, 0); // redneck14
        API.createVehicle((VehicleHash)1645267888, new Vector3(2427.335, 5040.841, 45.84429), new Vector3(-2.586392, -1.357558, 134.8855), 97, 97, 0); // redneck15
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
