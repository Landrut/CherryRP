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

public class EMS : Script
{
    public EMS()
    {
        API.onResourceStart += onResourceStart;
        API.onClientEventTrigger += onClientEvent;
        //API.onPlayerEnterVehicle += OnPlayerEnterVehicle;
    }

    public readonly Vector3 EMSEnterPos = new Vector3(307.2335f, -1433.792f, 29.90075f);
    public readonly Vector3 EMSExitPos = new Vector3(275.5407f, -1361.311f, 24.5378f);
    public readonly Vector3 EMSDutyPos = new Vector3(268.7621f, -1365.373f, 24.5378f);
    public readonly Vector3 EMSHealPos = new Vector3(260.4437f, -1358.478f, 24.53779f);

    public Blip EMSBlip;

    public ColShape EMSEnterColshape;
    public ColShape EMSExitColshape;
    public ColShape EMSDutyColshape;
    public ColShape EMSHealColshape;

    private void onResourceStart()
    {
        List<Vehicle> EMSVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)1171614426, new Vector3(304.0121, -1446.078, 29.57556), new Vector3(0.004966584, -0.5116892, -86.61472), 111, 0, 0), // emscar1
            API.createVehicle((VehicleHash)469291905, new Vector3(294.642, -1440.824, 29.42973), new Vector3(0.1124057, -0.02336958, -129.7243), 111, 0, 0), // emscar2
            API.createVehicle((VehicleHash)469291905, new Vector3(289.0313, -1436.217, 29.4304), new Vector3(0.005482068, -0.009562683, -128.8032), 111, 0, 0), // emscar3
            API.createVehicle((VehicleHash)1171614426, new Vector3(331.1278, -1468.397, 29.48061), new Vector3(-1.345117, -0.7575899, -131.5469), 111, 0, 0), // emscar4
            API.createVehicle((VehicleHash)1171614426, new Vector3(328.5711, -1471.584, 29.52323), new Vector3(-0.6989822, -0.7612198, -129.5886), 111, 0, 0), // emscar5
            API.createVehicle((VehicleHash)1171614426, new Vector3(325.3818, -1474.847, 29.57717), new Vector3(0.01555037, -0.136667, -131.0163), 111, 0, 0), // emscar6
            API.createVehicle((VehicleHash)469291905, new Vector3(335.6187, -1479.448, 29.19335), new Vector3(-2.82593, 1.104102, -59.41434), 111, 0, 0), // emscar7
            API.createVehicle((VehicleHash)353883353, new Vector3(299.4937, -1453.769, 46.8985), new Vector3(0.1626782, -0.003988226, -39.80795), 111, 1, 0), // emscar8
            API.createVehicle((VehicleHash)353883353, new Vector3(313.0501, -1465.497, 46.89686), new Vector3(-0.03698704, -0.006741742, -41.51336), 111, 1, 0) // emscar9
        };

        foreach (Vehicle emsveh in EMSVehicles)
        {
            API.setEntityData(emsveh, "fraction_id", 1);
            API.setVehicleNumberPlate(emsveh, "EMS");
            API.setVehicleEngineStatus(emsveh, false);
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int ismedicfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "EMS")
            {
                if (ismedicfaction != 1)
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

        API.requestIpl("coronertrash");
        API.requestIpl("Coroner_Int_on");
        EMSBlip = API.createBlip(new Vector3(307.2335f, -1433.792f, 29.90075f));
        API.setBlipSprite(EMSBlip, 61);
        API.setBlipScale(EMSBlip, 1.0f);
        API.setBlipColor(EMSBlip, 1);
        API.setBlipName(EMSBlip, "EMS");
        API.setBlipShortRange(EMSBlip, true);

        EMSEnterColshape = API.createCylinderColShape(EMSEnterPos, 0.50f, 1f);
        EMSExitColshape = API.createCylinderColShape(EMSExitPos, 0.50f, 1f);
        EMSDutyColshape = API.createCylinderColShape(EMSDutyPos, 0.50f, 1f);
        EMSHealColshape = API.createCylinderColShape(EMSHealPos, 0.50f, 1f);
        API.createMarker(1, EMSEnterPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в больницу", EMSEnterPos, 15f, 0.65f);
        API.createMarker(1, EMSExitPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход из больницы", EMSExitPos, 15f, 0.65f);
        API.createMarker(1, EMSDutyPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        API.createTextLabel("Раздевальная", EMSDutyPos, 15f, 0.65f);
        API.createMarker(1, EMSHealPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Получение препарата по рецепту", EMSHealPos, 15f, 0.65f);

        EMSEnterColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                API.setEntityPosition(player, new Vector3(274.4016f, -1360.317f, 24.5378f));
                API.setEntityDimension(player, 1);
            }
        };

        EMSExitColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            API.setEntityPosition(player, new Vector3(304.9611f, -1433.04f, 29.80411f));
            API.setEntityDimension(player, 0);
        };

        EMSHealColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            API.sendPictureNotificationToPlayer(player, "Мне нужен ваш рецепт", "CHAR_PLANESITE", 0, 3, "Врач", "Препарат по рецепту");
        };


        // выход на смену

        EMSDutyColshape.onEntityEnterColShape += (shape, Entity) =>
    {
        Client player;
        player = API.getPlayerFromHandle(Entity);
        int ismedicfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0; ;
        string groupName = "R";
        var chatMessageOnDuty = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)) + " | " + player.name + "~y~ вышел на смену";
        var chatMessageOffDuty = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)) + " | " + player.name + "~y~ окончил смену";
        if (ismedicfaction == 1)
        {
            int onduty = Player.IsPlayerOnDuty(player);
            if (onduty == 0)
            {
                Faction.SetPlayerFactionClothes(player);
                Player.SetPlayerOnDuty(player);
                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetFractionId(client) == 1 && Player.IsPlayerOnDuty(client) == 1)
                    {
                        API.sendChatMessageToPlayer(client, chatMessageOnDuty);
                    }
                }
                API.sendChatMessageToPlayer(player, "~y~Вы вышли на смену как ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                API.sendPictureNotificationToPlayer(player, "Удачного рабочего дня коллега!", "CHAR_PLANESITE", 0, 3, "Врач", "Сообщение персоналу");
            }
            else
            {
                Player.RemovePlayerOnDuty(player);
                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetFractionId(client) == 1 && Player.IsPlayerOnDuty(client) == 1)
                    {
                        API.sendChatMessageToPlayer(client, chatMessageOffDuty);
                    }
                }
                PlayerFunctions.Player.LoadPlayerClothes(player);
                API.sendPictureNotificationToPlayer(player, "До скорой встречи", "CHAR_PLANESITE", 0, 3, "Врач", "Сообщение персоналу");
            }
            return;
        }
        else
        {
            API.sendPictureNotificationToPlayer(player, "Покиньте служебное помещение!", "CHAR_PLANESITE", 0, 3, "Врач", "Обращение к посетителю");
        }
        if (API.isPlayerInAnyVehicle(player) == true)
        {
            return;
        }
    };
    }

    /*private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (Player.GetFractionId(player) != 1)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции");
                return;
            }
            else return;
        }
    }*/

    [Command("duty")]
    public void dutyCommand(Client sender)
    {
        Faction.SetPlayerFactionClothes(sender);
    }

    int Heal = 300;
    int Health = 20;

    public void onClientEvent(Client player, string EventName, params object[] arguments)
    {

        if (PlayerFunctions.Player.GetMoney(player) < Heal && EMSHealColshape.containsEntity(player))
        {
            API.sendPictureNotificationToPlayer(player, "Простите но этой суммы не хватит на покупку лекарства", "CHAR_PLANESITE", 0, 3, "Врач", "Препарат по рецепту");
            return;
        }
        
        if (API.getPlayerHealth(player) > Health && EMSHealColshape.containsEntity(player))
        {
            API.sendPictureNotificationToPlayer(player, "Возьмите рецепт у врача для начала", "CHAR_PLANESITE", 0, 3, "Врач", "Препарат по рецепту");
            return;
        }
        

        if (EMSHealColshape.containsEntity(player))
        {
            EventName = "Heal";
            PlayerFunctions.Player.ChangeMoney(player, -Heal);
            API.setPlayerHealth(player, 75);
            API.sendPictureNotificationToPlayer(player, "Вот ваше лекарство, поправляйтесь.", "CHAR_PLANESITE", 0, 3, "Врач", "Препарат по рецепту");

        }
    }
}
