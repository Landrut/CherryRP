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
using MenuManagement;

public class Families : Script
{
    public Families()
    {
        API.onResourceStart += onResourceStart;
    }

    public readonly Vector3 FamiliesPos = new Vector3(1100.834f, -3101.895f, -38.99995f);
    public readonly Vector3 FamiliesColshaedPos = new Vector3(1100.834f, -3101.895f, -38.99995f);
    public readonly Vector3 FamiliesBlippPos = new Vector3(-45.59018f, -1532.383f, 31.55332f);/*
    public readonly Vector3 FamiliesEnterHousePos = new Vector3(114.1165f, -1961.005f, 21.33419f);
    public readonly Vector3 FamiliesEnterHouse2Pos = new Vector3(113.4786f, -1974.115f, 21.32017f);    
    public readonly Vector3 FamiliesExitHousePos = new Vector3(-14.41827f, -1427.617f, 31.10148f);*/
    public readonly Vector3 FamiliesExitSkladPos = new Vector3(1087.883f, -3099.252f, -38.99994f);
    public readonly Vector3 FamiliesEnterSkladPos = new Vector3(-37.29558f, -1492.427f, 31.21893f);

    
        public Blip FamiliesBlip;
    
        public ColShape FamiliesExitSkladColshape;
        public ColShape FamiliesColshape;
        public ColShape FamiliesEnterSkladColshape;/*
        public ColShape FamiliesEnterHouseColshape;
        public ColShape FamiliesEnterHouse2Colshape;
        public ColShape FamiliesExitHouseColshape;
        */

    private MenuManager MenuManager = new MenuManager();
    private Menu savedMenu = null;

    #region FamiliesFormeMenus
    private void Families_Form_Builder(Client client)
    {

        Menu menu = savedMenu;

        if (menu == null)
        {
            menu = new Menu("Families_Form", "Спец одежда", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true);
            menu.BannerColor = new Color(200, 0, 0, 90);
            menu.Callback = Families_Form_MenuManager;

            MenuItem menuItem = new MenuItem("Свободная", "Свободная одежда", "Svoboda");
            menuItem.ExecuteCallback = true;
            menu.Add(menuItem);

            MenuItem menuItem2 = new MenuItem("Ограбление", "Одежда для ограбления", "Gang");
            menuItem2.ExecuteCallback = true;
            menu.Add(menuItem2);

            MenuItem menuItem3 = new MenuItem("Работа", "Одежда для заданий", "Job");
            menuItem3.ExecuteCallback = true;
            menu.Add(menuItem3);

            MenuItem menuItem4 = new MenuItem("Личная", "Переодеться в личную одежду", "NoMarker1");
            menuItem4.ExecuteCallback = true;
            menu.Add(menuItem4);

            MenuItem menuItem5 = new MenuItem("~r~Закрыть", "Закрыть меню", "Close");
            menuItem5.ExecuteCallback = true;
            menu.Add(menuItem5);
        }
        MenuManager.OpenMenu(client, menu);
    }
    private void Families_Form_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
    {
        int gender = (client.hasData("gender_id")) ? client.getData("gender_id") : 0;
        if (menu.Id == "Families_Form" && menuItem.Id == "Svoboda")
        {
            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли  комплект со склада");
                API.shared.setPlayerClothes(client, 4, 78, 2);
                API.shared.setPlayerClothes(client, 3, 1, 0);
                API.shared.setPlayerClothes(client, 11, 79, 3);
                API.shared.setPlayerClothes(client, 6, 33, 6);
                API.shared.setPlayerClothes(client, 8, 1, 0);
                API.shared.setPlayerClothes(client, 7, 3, 0);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли  комплект со склада");
                API.shared.setPlayerClothes(client, 4, 26, 3);
                API.shared.setPlayerClothes(client, 3, 6, 0);
                API.shared.setPlayerClothes(client, 11, 88, 3);
                API.shared.setPlayerClothes(client, 6, 7, 8);
                API.shared.setPlayerClothes(client, 8, 1, 0);
                API.shared.setPlayerClothes(client, 7, 54, 0);
            }
        }

        else if (menu.Id == "Families_Form" && menuItem.Id == "Gang")
        {
            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 27, 0);
                API.shared.setPlayerClothes(client, 3, 21, 0);
                API.shared.setPlayerClothes(client, 11, 35, 6);
                API.shared.setPlayerClothes(client, 6, 9, 0);
                API.shared.setPlayerClothes(client, 8, 30, 20);
                API.shared.setPlayerClothes(client, 7, 83, 1);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 87, 20);
                API.shared.setPlayerClothes(client, 3, 21, 0);
                API.shared.setPlayerClothes(client, 11, 14, 13);
                API.shared.setPlayerClothes(client, 6, 8, 13);
                API.shared.setPlayerClothes(client, 8, 1, 0);
            }
        }

        else if (menu.Id == "Families_Form" && menuItem.Id == "Job")
        {
            if (gender == 1)
            {
                API.shared.setPlayerClothes(client, 4, 100, 18);
                API.shared.setPlayerClothes(client, 3, 166, 0);
                API.shared.setPlayerClothes(client, 11, 171, 7);
                API.shared.setPlayerClothes(client, 6, 26, 0);
                API.shared.setPlayerClothes(client, 8, 1, 0);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 87, 18);
                API.shared.setPlayerClothes(client, 3, 11, 0);
                API.shared.setPlayerClothes(client, 11, 26, 1);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 8, 1, 0);
            }
        }

        else if (menu.Id == "Families_Form" && menuItem.Id == "NoMarker1")
        {
            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы переоделись в личную одежду");
                PlayerFunctions.Player.LoadPlayerClothes(client);
            }


            else if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы переоделись в личную одежду");
                PlayerFunctions.Player.LoadPlayerClothes(client);
            }
        }

        else if (menu.Id == "Families_Form" && menuItem.Id == "Close")
        {
            savedMenu = menu;
            MenuManager.CloseMenu(client);
        }
    }

    #endregion

    public void onResourceStart()
    {
        List<Vehicle> FamiliesVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)(-2124201592), new Vector3(-45.59018, -1532.383, 31.55332), new Vector3(0.02310605, -0.2180445, -39.69064), 53, 53, 0), // families11
            API.createVehicle((VehicleHash)(-2124201592), new Vector3(-40.38178, -1526.026, 31.09028), new Vector3(-6.694684, 1.093951, -40.23819), 53, 53, 0), // families12
            API.createVehicle((VehicleHash)1131912276, new Vector3(-12.61453, -1532.327, 29.19826), new Vector3(-1.715695, 4.196757, -164.2012), 53, 53, 0), // families13
            API.createVehicle((VehicleHash)1131912276, new Vector3(-11.15531, -1532.167, 29.18323), new Vector3(-1.053743, 4.32654, -157.3693), 53, 53, 0), // families14
            API.createVehicle((VehicleHash)1131912276, new Vector3(-9.833751, -1532.242, 29.16027), new Vector3(-0.9421024, 4.497019, -172.9418), 53, 53, 0), // families15
            API.createVehicle((VehicleHash)(-810318068), new Vector3(-37.54389, -1508.445, 30.63148), new Vector3(-0.1305091, -2.362279, 141.6931), 53, 53, 0), // families1
            API.createVehicle((VehicleHash)(-810318068), new Vector3(-34.47415, -1510.953, 30.45715), new Vector3(-0.06061557, -2.575673, 141.5894), 53, 53, 0), // families2
            API.createVehicle((VehicleHash)2006918058, new Vector3(-31.24941, -1514.062, 30.40049), new Vector3(-0.1764233, -0.5045831, 140.0587), 53, 53, 0), // families3
            API.createVehicle((VehicleHash)2006918058, new Vector3(-27.15038, -1517.453, 30.29043), new Vector3(-0.2332101, -1.44904, 141.4607), 53, 53, 0), // families4
            API.createVehicle((VehicleHash)2006918058, new Vector3(-29.34662, -1515.799, 30.35909), new Vector3(-0.1400576, -1.50632, 141.4347), 53, 53, 0), // families6
            API.createVehicle((VehicleHash)(-1685021548), new Vector3(-6.774789, -1531.625, 29.20123), new Vector3(-0.1212434, -1.444437, 141.7038), 53, 53, 0), // families5
            API.createVehicle((VehicleHash)(-1685021548), new Vector3(-4.589884, -1533.402, 29.12038), new Vector3(-0.1749917, -2.250086, 142.2289), 53, 53, 0), // families7
            API.createVehicle((VehicleHash)(-1685021548), new Vector3(-2.279714, -1535.388, 29.00533), new Vector3(-0.1018306, -2.25681, 140.7046), 53, 53, 0), // families8
            API.createVehicle((VehicleHash)1909141499, new Vector3(-56.26372, -1493.305, 31.44003), new Vector3(-0.2318163, -1.881661, 141.6039), 53, 53, 0), // families9
            API.createVehicle((VehicleHash)1909141499, new Vector3(-58.52039, -1491.498, 31.47738), new Vector3(-0.2566622, -0.430952, 140.2625), 53, 53, 0) // families10
        };

        Vehicle Familiesveh2;
        foreach (Vehicle Familiesveh in FamiliesVehicles)
        {
            API.setEntityData(Familiesveh, "gang_id", 1);
            API.setVehicleNumberPlate(Familiesveh, "Families");
            API.setVehicleEngineStatus(Familiesveh, false);

            Familiesveh2 = Familiesveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int isFamiliesGang = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "Families")
            {
                if (isFamiliesGang != 1)
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
        
        FamiliesBlip = API.createBlip(FamiliesBlippPos);
        API.setBlipSprite(FamiliesBlip, 84);
        API.setBlipScale(FamiliesBlip, 1.0f);
        API.setBlipColor(FamiliesBlip, 52);
        API.setBlipName(FamiliesBlip, "Families");
        API.setBlipShortRange(FamiliesBlip, true);
        /*
        FamiliesEnterHouseColshape = API.createCylinderColShape(FamiliesEnterHousePos, 0.50f, 1f);
        FamiliesExitHouseColshape = API.createCylinderColShape(FamiliesExitHousePos, 0.50f, 1f);
        FamiliesEnterHouse2Colshape = API.createCylinderColShape(FamiliesEnterHouse2Pos, 0.50f, 1f);
        FamiliesExitHouseColshape = API.createCylinderColShape(FamiliesExitHousePos, 0.50f, 1f);*/
        FamiliesEnterSkladColshape = API.createCylinderColShape(FamiliesEnterSkladPos, 0.50f, 1f);
        FamiliesExitSkladColshape = API.createCylinderColShape(FamiliesExitSkladPos, 0.50f, 1f);
        FamiliesColshape = API.createCylinderColShape(FamiliesColshaedPos, 0.50f, 1f);

        /*
        API.createMarker(1, FamiliesEnterHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в дом Families", FamiliesEnterHousePos, 15f, 0.65f);
        API.createMarker(1, FamiliesEnterHouse2Pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в дом Families", FamiliesEnterHouse2Pos, 15f, 0.65f);
        API.createMarker(1, FamiliesExitHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход с дома", FamiliesExitHousePos, 15f, 0.65f);*/
        API.createMarker(1, FamiliesEnterSkladPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в склад Families", FamiliesEnterSkladPos, 15f, 0.65f);
        API.createMarker(1, FamiliesExitSkladPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход со склада", FamiliesExitSkladPos, 15f, 0.65f);
        API.createMarker(1, FamiliesPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90, 1);
        API.createTextLabel("Склад с одеждой", FamiliesPos, 15f, 0.65f);
        /*

        FamiliesEnterHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                int lsFamiliesfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
                if (lsFamiliesfaction == 1)
                {
                    API.setEntityPosition(player, new Vector3(-14.20735f, -1440.696f, 31.10155f));
                    API.setEntityDimension(player, 1);
                    API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
                }
                else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
            }
        };

        FamiliesEnterHouse2Colshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                int lsFamiliesfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
                if (lsFamiliesfaction == 1)
                {
                    API.setEntityPosition(player, new Vector3(-14.20735f, -1440.696f, 31.10155f));
                    API.setEntityDimension(player, 1);
                    API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
                }
                else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
            }
        };
        */
        FamiliesEnterSkladColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsFamiliesfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsFamiliesfaction == 1)
            {
                API.setEntityPosition(player, new Vector3(1104.674f, -3099.414f, -38.99995f));
                API.setEntityDimension(player, 1);
                API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
            
        };

        FamiliesColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsBallasfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (lsBallasfaction == 1)
            {
                Families_Form_Builder(player);
                return;

            }
            else return;
        };
        /*
        FamiliesExitHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsFamiliesfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsFamiliesfaction == 1)
            {
                API.setEntityPosition(player, new Vector3(118.3365f, -1974.886f, 21.12725f));
                API.setEntityDimension(player, 0);
                API.sendPictureNotificationToPlayer(player, "Ave Families!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
        };
        */
        FamiliesExitSkladColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsFamiliesfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsFamiliesfaction == 1)
            {
                API.setEntityPosition(player, new Vector3(-41.07392f, -1489.989f, 31.38715f));
                API.setEntityDimension(player, 0);
                API.sendPictureNotificationToPlayer(player, "Ave Families!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else return;
        };
    }

    private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (Player.GetFractionId(player) != 1)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной банде");
                return;
            }
            else return;
        }
    }
}
