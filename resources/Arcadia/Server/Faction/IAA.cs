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

public class IAA : Script
{
    public IAA()
    {
        API.onResourceStart += onResourceStart;
    }

    public readonly Vector3 IAABlippPos = new Vector3(112.2704f, -629.2507f, 44.22956f);
    public readonly Vector3 IAAEnterPos = new Vector3(112.2704f, -629.2507f, 44.22956f);
    public readonly Vector3 IAAExitPos = new Vector3(2155.136f, 2921.113f, -61.90244f);
    public readonly Vector3 IAADutyPos = new Vector3(2106.055f, 2947.804f, -61.90189f);
    public readonly Vector3 IAAArmoryPos = new Vector3(2117.457f, 2944.275f, -61.9019f);

    public Blip IAABlip;

    public ColShape IAAEnterColshape;
    public ColShape IAAExitColshape;
    public ColShape IAADutyColshape;
    public ColShape IAAArmoryColshape;

    public void onResourceStart()
    {
        List<Vehicle> IAAVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)1777363799, new Vector3(180.7228, -695.4959, 32.65022), new Vector3(0.0448116, 0.02781741, 68.04761), 0, 0, 0), // IAA3
            API.createVehicle((VehicleHash)1777363799, new Vector3(179.4471, -699.0682, 32.65387), new Vector3(-0.04408736, 0.0232578, 69.96588), 0, 0, 0), // IAA4
            API.createVehicle((VehicleHash)1777363799, new Vector3(176.6556, -706.7001, 32.65643), new Vector3(-0.04583715, 0.01724948, 71.07574), 0, 0, 0), // IAA5
            API.createVehicle((VehicleHash)1777363799, new Vector3(175.3861, -710.3008, 32.65231), new Vector3(0.1519537, -0.09569856, 71.30284), 0, 0, 0), // IAA6
            API.createVehicle((VehicleHash)80636076, new Vector3(176.9267, -687.4487, 32.41441), new Vector3(0.01605958, 0.01524466, 158.1595), 0, 0, 0), // IAA7
            API.createVehicle((VehicleHash)80636076, new Vector3(173.3816, -686.1784, 32.41327), new Vector3(0.08822118, 0.015526, 160.6771), 0, 0, 0), // IAA8
            API.createVehicle((VehicleHash)80636076, new Vector3(169.949, -684.9019, 32.41516), new Vector3(0.06634739, -0.01591411, 158.6185), 0, 0, 0), // IAA9
            API.createVehicle((VehicleHash)1127131465, new Vector3(163.9526, -682.7216, 32.77159), new Vector3(0.03492415, -0.07807794, 158.4769), 0, 0, 0), // IAA10
            API.createVehicle((VehicleHash)1127131465, new Vector3(156.5536, -680.0181, 32.77141), new Vector3(0.07767295, -0.004634133, 159.8455), 0, 0, 0), // IAA12
            API.createVehicle((VehicleHash)1127131465, new Vector3(160.2556, -681.3813, 32.77168), new Vector3(0.04791036, -0.01453344, 159.5819), 111, 111, 0), // IAA12
            API.createVehicle((VehicleHash)(-1647941228), new Vector3(144.8737, -695.3228, 32.74929), new Vector3(0.03091615, 0.100222, -112.3799), 111, 111, 0), // IAA13
            API.createVehicle((VehicleHash)(-1647941228), new Vector3(143.687, -698.4905, 32.74855), new Vector3(0.04075204, -0.01167959, -110.049), 111, 111, 0), // IAA14
            API.createVehicle((VehicleHash)(-1647941228), new Vector3(148.9858, -683.85, 32.7524), new Vector3(-0.004184504, -0.06868739, -109.2714), 0, 0, 0), // IAA1
            API.createVehicle((VehicleHash)(-1647941228), new Vector3(147.6422, -687.5766, 32.75058), new Vector3(-0.0121687, -0.05119393, -109.288), 0, 0, 0) // IAA2
        };

        Vehicle IAAveh2;
        foreach (Vehicle IAAveh in IAAVehicles)
        {
            API.setEntityData(IAAveh, "fraction_id", 4);
            API.setVehicleNumberPlate(IAAveh, "IAA");
            API.setVehicleEngineStatus(IAAveh, false);

            IAAveh2 = IAAveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int lsIaafactio = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "IAA")
            {
                if (lsIaafactio != 4)
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

        IAABlip = API.createBlip(IAABlippPos);
        API.setBlipSprite(IAABlip, 590);
        API.setBlipScale(IAABlip, 1.0f);
        API.setBlipColor(IAABlip, 1);
        API.setBlipName(IAABlip, "IAA");
        API.setBlipShortRange(IAABlip, true);

        IAAEnterColshape = API.createCylinderColShape(IAAEnterPos, 0.50f, 1f);
        IAAExitColshape = API.createCylinderColShape(IAAExitPos, 0.50f, 1f);
        IAADutyColshape = API.createCylinderColShape(IAADutyPos, 0.50f, 1f);
        IAAArmoryColshape = API.createCylinderColShape(IAAArmoryPos, 0.50f, 1f);
        API.createMarker(1, IAAEnterPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в офис IAA", IAAEnterPos, 15f, 0.65f);
        API.createMarker(1, IAAExitPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход с офиса", IAAExitPos, 15f, 0.65f);
        API.createMarker(23, IAADutyPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        API.createTextLabel("Получение формы", IAADutyPos, 15f, 0.65f);
        API.createMarker(23, IAAArmoryPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        API.createTextLabel("Получение снаряжения", IAAArmoryPos, 15f, 0.65f);

        IAAEnterColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                API.setEntityPosition(player, new Vector3(2153.026f, 2920.876f, -61.89701f));
                API.setEntityDimension(player, 1);
                API.sendPictureNotificationToPlayer(player, "Приветствуем вас в офисе IAA", "CHAR_STEVE", 0, 3, "Служба безопасности", "Обращение к посетителю");
            }
        };
        IAAExitColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            API.setEntityPosition(player, new Vector3(111.7202f, -630.9254f, 44.22955f));
            API.setEntityDimension(player, 0);
            API.sendPictureNotificationToPlayer(player, "Все что вы узнали должно остаться в офисе IAA", "CHAR_STEVE", 0, 3, "Служба безопасности", "Обращение к посетителю");
        };
bool weaponcare = false;

        IAAArmoryColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsIaafaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (lsIaafaction == 4)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 1)
                {
                    if (weaponcare == false)
                    {
                        API.sendChatMessageToPlayer(player, "~y~Вы взяли оружие и снаряжение для ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                        API.sendPictureNotificationToPlayer(player, "Проверь все, бывали случаи...", "CHAR_STEVE", 0, 3, "Служба безопасности", "Обращение к сотруднику");
                        API.givePlayerWeapon(player, WeaponHash.AssaultSMG, 300, false, true);
                        API.givePlayerWeapon(player, WeaponHash.HeavyPistol, 150, false, true);
                        API.givePlayerWeapon(player, WeaponHash.BullpupShotgun, 35, false, true);
                        API.givePlayerWeapon(player, WeaponHash.Parachute, 1, false, true);
                        API.setPlayerArmor(player, 100);
                        weaponcare = true;
                        return;
                    }
                    if (weaponcare == true)
                    {
                        API.removePlayerWeapon(player, WeaponHash.AssaultSMG);
                        API.removePlayerWeapon(player, WeaponHash.HeavyPistol);
                        API.removePlayerWeapon(player, WeaponHash.BullpupShotgun);
                        API.removePlayerWeapon(player, WeaponHash.Parachute);
                        API.setPlayerArmor(player, 0);
                        API.sendPictureNotificationToPlayer(player, "Я передам на пост чтобы почистили твоё оружие", "CHAR_STEVE", 0, 3, "Служба безопасности", "Обращение к сотруднику");
                        weaponcare = false;
                        return;
                    }
                }
                else
                {
                    API.sendPictureNotificationToPlayer(player, "Что то я не вижу записи о том что ты на смене", "CHAR_STEVE", 0, 3, "Служба безопасности", "Обращение к сотруднику");
                }
                return;
            }
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                API.sendPictureNotificationToPlayer(player, "Тебе нельзя тут находиться!", "CHAR_STEVE", 0, 3, "Служба безопасности", "Обращение к посетителю");
            }
        };

        IAADutyColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsIaafaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0; ;
            if (lsIaafaction == 4)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 0)
                {
                    Faction.SetPlayerFactionClothes(player);
                    Player.SetPlayerOnDuty(player);                   
                    API.sendChatMessageToPlayer(player,"~y~Вы переоделись в форму: ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                    API.sendPictureNotificationToPlayer(player, "Удачного рабочего дня", "CHAR_STEVE", 0, 3, "Служба безопасности", "Обращение к сотруднику");
                }
                else
                {
                    Player.RemovePlayerOnDuty(player);
                   
                    PlayerFunctions.Player.LoadPlayerClothes(player);
                    API.sendPictureNotificationToPlayer(player, "Доложу о том что ты закончил, удачи!", "CHAR_STEVE", 0, 3, "Служба безопасности", "Обращение к сотруднику");
                }
                return;
            }
            else
            {
                API.sendPictureNotificationToPlayer(player, "Тебе нельзя находится в служебных помещениях!", "CHAR_STEVE", 0, 3, "Служба безопасности", "Обращение к посетителю");
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
            if (Player.GetFractionId(player) != 4)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции");
                return;
            }
            else return;
        }
    }
}
