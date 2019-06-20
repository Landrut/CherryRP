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

public class Gover : Script
{
    public Gover()
    {
        API.onResourceStart += onResourceStart;
    }

    public readonly Vector3 GoverBlippPos = new Vector3(238.1217f, -412.0628f, 48.11195f);
    public readonly Vector3 GoverEnterPos = new Vector3(238.1217f, -412.0628f, 48.11195f);
    public readonly Vector3 GoverExitPos = new Vector3(-1395.93f, -480.6679f, 72.04211f);
    public readonly Vector3 GoverDutyPos = new Vector3(-127.5882f, -633.9233f, 168.8205f);
    public readonly Vector3 GoverArmoryPos = new Vector3(-127.6263f, -632.317f, 168.8205f);


    public Blip GoverBlip;

    public ColShape GoverEnterColshape;
    public ColShape GoverExitColshape;
    public ColShape GoverDutyColshape;
    public ColShape GoverArmoryColshape;

    public void onResourceStart()
    {
        API.requestIpl("ex_sm_15_office_03a");

        List<Vehicle> GoverVehicles = new List<Vehicle>();
        GoverVehicles.Add(API.createVehicle((VehicleHash)1922255844, new Vector3(252.8146, -376.0839, 44.03017), new Vector3(1.224683, 1.318173, -108.9697), 111, 111, 0)); // Gov1
        GoverVehicles.Add(API.createVehicle((VehicleHash)(-1961627517), new Vector3(259.1023, -378.563, 44.27523), new Vector3(1.229927, 1.000891, -110.2085), 111, 111, 0)); // Gov2
        GoverVehicles.Add(API.createVehicle((VehicleHash)666166960, new Vector3(265.7706, -380.8165, 44.62334), new Vector3(1.005567, -0.2588988, -105.3974), 111, 111, 0)); // Gov3
        GoverVehicles.Add(API.createVehicle((VehicleHash)(-888242983), new Vector3(241.3348, -372.3481, 43.85332), new Vector3(1.061412, 1.332742, -108.3704), 111, 111, 0)); // Gov4
        GoverVehicles.Add(API.createVehicle((VehicleHash)704435172, new Vector3(235.9972, -370.5287, 43.74553), new Vector3(0.5981793, 1.137748, -109.527), 111, 111, 0)); // Gov5
        GoverVehicles.Add(API.createVehicle((VehicleHash)666166960, new Vector3(230.8293, -368.6938, 44.08332), new Vector3(0.7242315, 1.13114, -109.026), 111, 111, 0)); // Gov6

        Vehicle Goverveh2;
        foreach (Vehicle Goverveh in GoverVehicles)
        {
            API.setEntityData(Goverveh, "fraction_id", 7);
            API.setVehicleNumberPlate(Goverveh, "Gover");
            API.setVehicleEngineStatus(Goverveh, false);

            Goverveh2 = Goverveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int lsGoverfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "Gover")
            {
                if (lsGoverfaction != 7)
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

        GoverBlip = API.createBlip(GoverBlippPos);
        API.setBlipSprite(GoverBlip, 419);
        API.setBlipScale(GoverBlip, 1.0f);
        API.setBlipColor(GoverBlip, 1);
        API.setBlipName(GoverBlip, "Gover");
        API.setBlipShortRange(GoverBlip, true);

        GoverEnterColshape = API.createCylinderColShape(GoverEnterPos, 0.50f, 1f);
        GoverExitColshape = API.createCylinderColShape(GoverExitPos, 0.50f, 1f);
        GoverDutyColshape = API.createCylinderColShape(GoverDutyPos, 0.50f, 1f);
        GoverArmoryColshape = API.createCylinderColShape(GoverArmoryPos, 0.50f, 1f);
        API.createMarker(1, GoverEnterPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в мерию", GoverEnterPos, 15f, 0.65f);
        API.createMarker(1, GoverExitPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход на улицу", GoverExitPos, 15f, 0.65f);
        API.createMarker(23, GoverDutyPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        API.createTextLabel("Получение формы", GoverDutyPos, 15f, 0.65f);
        API.createMarker(23, GoverArmoryPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        API.createTextLabel("Получение снаряжения", GoverArmoryPos, 15f, 0.65f);

        GoverEnterColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                API.setEntityPosition(player, new Vector3(-1392.563f, -480.549f, 72.0421f));
                API.setEntityDimension(player, 1);
                API.sendPictureNotificationToPlayer(player, "Добро пожаловать в здание мерии", "CHAR_ANTONIA", 0, 3, "Секретарь", "Обращение к человеку");
            }
        };
        GoverExitColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            API.setEntityPosition(player, new Vector3(233.2751f, -410.4284f, 48.11195f));
            API.setEntityDimension(player, 0);
            API.sendPictureNotificationToPlayer(player, "Всего доброго, до новых встреч", "CHAR_ANTONIA", 0, 3, "Секретарь", "Обращение к человеку");
        };
bool weaponcare = false;

        GoverArmoryColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsGoverfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0; ;
            if (lsGoverfaction == 7)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 1)
                {
                    if (weaponcare == false)
                    {
                        API.sendChatMessageToPlayer(player, "~y~Вы взяли оружие и снаряжение для ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                        API.sendPictureNotificationToPlayer(player, "Будь аккуратней с этим", "CHAR_ANTONIA", 0, 3, "Секретарь", "Обращение к сотруднику" );
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
                        API.sendPictureNotificationToPlayer(player, "Всего хорошего, я поставлю запись в журнале", "CHAR_ANTONIA", 0, 3, "Секретарь", "Обращение к сотруднику");
                        weaponcare = false;
                        return;
                    }
                }
                else
                {                    
                    API.sendPictureNotificationToPlayer(player, "Ты не забыл отметится в журнале?", "CHAR_ANTONIA", 0, 3, "Секретарь", "Обращение к сотруднику");
                }
                return;
            }
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                API.sendPictureNotificationToPlayer(player, "Покиньте служебное помещение!", "CHAR_ANTONIA", 0, 3, "Секретарь", "Обращение к посетителю");
            }
        };

        GoverDutyColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsGoverfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0; ;
            if (lsGoverfaction == 7)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 0)
                {
                    Faction.SetPlayerFactionClothes(player);
                    Player.SetPlayerOnDuty(player);                   
                    API.sendChatMessageToPlayer(player,"~y~Вы переоделись в форму: ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                    API.sendPictureNotificationToPlayer(player, "Удачного дня! Запись в журнал уже сделала", "CHAR_ANTONIA", 0, 3, "Секретарь", "Обращение к сотруднику");
                }
                else
                {
                    Player.RemovePlayerOnDuty(player);
                   
                    PlayerFunctions.Player.LoadPlayerClothes(player);
                    API.sendPictureNotificationToPlayer(player, "Пока пока, запишу в журнале когда ты ушел", "CHAR_ANTONIA", 0, 3, "Секретарь", "Обращение к сотруднику");
                }
                return;
            }
            else
            {
                API.sendPictureNotificationToPlayer(player, "Покиньте служебное помещение!", "CHAR_ANTONIA", 0, 3, "Секретарь", "Обращение к посетителю");
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
            if (Player.GetFractionId(player) != 7)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции");
                return;
            }
            else return;
        }
    }
}
