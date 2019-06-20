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

public class Triads : Script
{
    public Triads()
    {
        API.onResourceStart += onResourceStart;
    }
    
    public readonly Vector3 TriadsBlippPos = new Vector3(-682.6123f, -883.0906f, 24.49906f);/*
    public readonly Vector3 TriadsEnterHousePos = new Vector3(114.1165f, -1961.005f, 21.33419f);
    public readonly Vector3 TriadsEnterHouse2Pos = new Vector3(113.4786f, -1974.115f, 21.32017f);    
    public readonly Vector3 TriadsExitHousePos = new Vector3(-14.41827f, -1427.617f, 31.10148f);*/
    public readonly Vector3 TriadsExitSkladPos = new Vector3(1087.883f, -3099.252f, -38.99994f);
    public readonly Vector3 TriadsEnterSkladPos = new Vector3(-675.8881f, -885.0637f, 24.45569f);
    public readonly Vector3 TriadsPos = new Vector3(1100.834f, -3101.895f, -38.99995f);
    public readonly Vector3 TriadsColshaedPos = new Vector3(1100.834f, -3101.895f, -38.99995f);


    public Blip TriadsBlip;
    
        public ColShape TriadsEnterSkladColshape;
        public ColShape TriadsColshape;
        public ColShape TriadsExitSkladColshape;/*
        public ColShape TriadsEnterHouseColshape;
        public ColShape TriadsEnterHouse2Colshape;
        public ColShape TriadsExitHouseColshape;
        */

    private MenuManager MenuManager = new MenuManager();
    private Menu savedMenu = null;

    #region TriadsFormeMenus
    private void Triads_Form_Builder(Client client)
    {

        Menu menu = savedMenu;

        if (menu == null)
        {
            menu = new Menu("Triads_Form", "Спец одежда", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true);
            menu.BannerColor = new Color(200, 0, 0, 90);
            menu.Callback = Triads_Form_MenuManager;

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
    private void Triads_Form_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
    {
        int gender = (client.hasData("gender_id")) ? client.getData("gender_id") : 0;
        if (menu.Id == "Triads_Form" && menuItem.Id == "Svoboda")
        {
            if (gender == 1)
            {
                API.shared.setPlayerClothes(client, 4, 54, 1);
                API.shared.setPlayerClothes(client, 3, 25, 0);
                API.shared.setPlayerClothes(client, 11, 25, 4);
                API.shared.setPlayerClothes(client, 6, 42, 3);
                API.shared.setPlayerClothes(client, 8, 37, 0);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли  комплект со склада");
                API.shared.setPlayerClothes(client, 4, 10, 1);
                API.shared.setPlayerClothes(client, 3, 14, 0);
                API.shared.setPlayerClothes(client, 11, 107, 3);
                API.shared.setPlayerClothes(client, 6, 20, 3);
                API.shared.setPlayerClothes(client, 8, 1, 0);
            }
        }

        else if (menu.Id == "Triads_Form" && menuItem.Id == "Gang")
        {
            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 30, 0);
                API.shared.setPlayerClothes(client, 3, 18, 0);
                API.shared.setPlayerClothes(client, 11, 251, 0);
                API.shared.setPlayerClothes(client, 6, 36, 1);
                API.shared.setPlayerClothes(client, 8, 161, 0);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 52, 2);
                API.shared.setPlayerClothes(client, 3, 31, 0);
                API.shared.setPlayerClothes(client, 11, 32, 0);
                API.shared.setPlayerClothes(client, 6, 10, 0);
                API.shared.setPlayerClothes(client, 8, 1, 0);
            }
        }

        else if (menu.Id == "Triads_Form" && menuItem.Id == "Job")
        {
            if (gender == 1)
            {
                API.shared.setPlayerClothes(client, 4, 77, 0);
                API.shared.setPlayerClothes(client, 3, 165, 0);
                API.shared.setPlayerClothes(client, 11, 79, 0);
                API.shared.setPlayerClothes(client, 6, 76, 0);
                API.shared.setPlayerClothes(client, 8, 1, 0);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 87, 18);
                API.shared.setPlayerClothes(client, 3, 11, 0);
                API.shared.setPlayerClothes(client, 11, 26, 1);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 8, 21, 0);
            }
        }

        else if (menu.Id == "Triads_Form" && menuItem.Id == "NoMarker1")
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

        else if (menu.Id == "Triads_Form" && menuItem.Id == "Close")
        {
            savedMenu = menu;
            MenuManager.CloseMenu(client);
        }
    }

    #endregion

    public void onResourceStart()
    {
        List<Vehicle> TriadsVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)525509695, new Vector3(-688.0331, -878.4191, 24.12306), new Vector3(0.03659963, -0.03119095, -91.52714), 64, 64, 0), // TriadsCar1
            API.createVehicle((VehicleHash)525509695, new Vector3(-676.874, -879.6544, 24.10276), new Vector3(1.084085, 1.156864, 88.97991), 64, 64, 0), // TriadsCar2
            API.createVehicle((VehicleHash)(-1150599089), new Vector3(-687.6174, -881.3586, 23.99254), new Vector3(0.01710866, 0.0005826718, -88.6818), 64, 64, 0), // TriadsCar3
            API.createVehicle((VehicleHash)2016857647, new Vector3(-678.2902, -882.7294, 23.94796), new Vector3(1.997275, -0.007252271, 89.34797), 64, 64, 0), // TriadsCar4
            API.createVehicle((VehicleHash)(-1150599089), new Vector3(-687.7012, -884.2819, 23.99237), new Vector3(0.01659092, 0.0003377934, -91.05949), 64, 64, 0), // TriadsCar5
            API.createVehicle((VehicleHash)2016857647, new Vector3(-678.2981, -885.3539, 23.95789), new Vector3(1.494558, 1.181111, 89.5949), 64, 64, 0), // TriadsCar6
            API.createVehicle((VehicleHash)(-1150599089), new Vector3(-687.6018, -887.2274, 23.99235), new Vector3(0.01835677, 0.0003356829, -91.59695), 64, 64, 0), // TriadsCar7
            API.createVehicle((VehicleHash)(-1150599089), new Vector3(-686.6471, -894.0988, 23.992), new Vector3(0.0184506, 0.002637321, -89.65945), 64, 64, 0), // TriadsCar8
            API.createVehicle((VehicleHash)(-808457413), new Vector3(-686.6836, -890.6437, 24.23056), new Vector3(-0.187356, 0.05119783, -88.90845), 64, 64, 0), // TriadsCar9
            API.createVehicle((VehicleHash)2016857647, new Vector3(-673.2737, -890.0396, 23.97547), new Vector3(0.7589247, 0.009489098, -91.9906), 64, 64, 0), // TriadsCar10
            API.createVehicle((VehicleHash)2016857647, new Vector3(-675.1964, -895.0075, 23.97615), new Vector3(0.7604096, -0.02103574, -87.3699), 64, 64, 0), // TriadsCar11
            API.createVehicle((VehicleHash)(-1289178744), new Vector3(-690.781, -900.3255, 23.14839), new Vector3(0.3837395, -12.53173, -90.63002), 64, 64, 0), // TriadsCar12
            API.createVehicle((VehicleHash)(-1842748181), new Vector3(-690.8271, -901.0881, 23.14853), new Vector3(1.476083, -13.66463, -92.16705), 64, 64, 0), // TriadsCar13
            API.createVehicle((VehicleHash)(-1842748181), new Vector3(-690.8544, -901.8947, 23.14302), new Vector3(1.556911, -15.00044, -91.33201), 64, 64, 0), // TriadsCar14
            API.createVehicle((VehicleHash)(-1289178744), new Vector3(-690.7877, -902.7263, 23.14978), new Vector3(1.034112, -13.66264, -87.45145), 64, 64, 0), // TriadsCar15
            API.createVehicle((VehicleHash)1753414259, new Vector3(-692.6675, -908.2852, 23.13894), new Vector3(1.370257, -14.99604, 94.63046), 64, 64, 0), // TriadsCar16
            API.createVehicle((VehicleHash)1753414259, new Vector3(-692.6384, -909.5264, 23.13765), new Vector3(1.814916, -14.99137, 89.30798), 64, 64, 0), // TriadsCar17
            API.createVehicle((VehicleHash)1753414259, new Vector3(-692.665, -910.6943, 23.13726), new Vector3(1.861416, -14.99256, 91.31325), 64, 64, 0), // TriadsCar18
            API.createVehicle((VehicleHash)1753414259, new Vector3(-692.629, -911.948, 23.1678), new Vector3(-0.3808375, -14.99947, 87.33505), 64, 64, 0) // TriadsCar19
        };

        Vehicle Triadsveh2;
        foreach (Vehicle Triadsveh in TriadsVehicles)
        {
            API.setEntityData(Triadsveh, "gang_id", 5);
            API.setVehicleNumberPlate(Triadsveh, "Triads");
            API.setVehicleEngineStatus(Triadsveh, false);

            Triadsveh2 = Triadsveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int isTriadsGang = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "Triads")
            {
                if (isTriadsGang != 5)
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
        
        TriadsBlip = API.createBlip(TriadsBlippPos);
        API.setBlipSprite(TriadsBlip, 84);
        API.setBlipScale(TriadsBlip, 1.0f);
        API.setBlipColor(TriadsBlip, 63);
        API.setBlipName(TriadsBlip, "Triads");
        API.setBlipShortRange(TriadsBlip, true);
        /*
        TriadsEnterHouseColshape = API.createCylinderColShape(TriadsEnterHousePos, 0.50f, 1f);
        TriadsExitHouseColshape = API.createCylinderColShape(TriadsExitHousePos, 0.50f, 1f);
        TriadsEnterHouse2Colshape = API.createCylinderColShape(TriadsEnterHouse2Pos, 0.50f, 1f);
        TriadsExitHouseColshape = API.createCylinderColShape(TriadsExitHousePos, 0.50f, 1f);*/
        TriadsEnterSkladColshape = API.createCylinderColShape(TriadsEnterSkladPos, 0.50f, 1f);
        TriadsExitSkladColshape = API.createCylinderColShape(TriadsExitSkladPos, 0.50f, 1f);
        TriadsColshape = API.createCylinderColShape(TriadsColshaedPos, 0.50f, 1f);

        /*
        API.createMarker(1, TriadsEnterHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в дом Triads", TriadsEnterHousePos, 15f, 0.65f);
        API.createMarker(1, TriadsEnterHouse2Pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в дом Triads", TriadsEnterHouse2Pos, 15f, 0.65f);
        API.createMarker(1, TriadsExitHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход с дома", TriadsExitHousePos, 15f, 0.65f);*/
        API.createMarker(1, TriadsEnterSkladPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в склад Triads", TriadsEnterSkladPos, 15f, 0.65f);
        API.createMarker(1, TriadsExitSkladPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход со склада", TriadsExitSkladPos, 15f, 0.65f);
        API.createMarker(1, TriadsPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90, 5);
        API.createTextLabel("Склад с одеждой", TriadsPos, 15f, 0.65f);

        /*
        TriadsEnterHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                int lsTriadsfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
                if (lsTriadsfaction == 5)
                {
                    API.setEntityPosition(player, new Vector3(-14.20735f, -1440.696f, 31.10155f));
                    API.setEntityDimension(player, 5);
                    API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
                }
                else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
            }
        };

        TriadsEnterHouse2Colshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                int lsTriadsfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
                if (lsTriadsfaction == 5)
                {
                    API.setEntityPosition(player, new Vector3(-14.20735f, -1440.696f, 31.10155f));
                    API.setEntityDimension(player, 5);
                    API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
                }
                else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
            }
        };
        */
        TriadsEnterSkladColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsTriadsfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsTriadsfaction == 5)
            {
                API.setEntityPosition(player, new Vector3(1104.674f, -3099.414f, -38.99995f));
                API.setEntityDimension(player, 5);
                API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");


        };
        /*
        TriadsExitHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsTriadsfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsTriadsfaction == 5)
            {
                API.setEntityPosition(player, new Vector3(118.3365f, -1974.886f, 21.12725f));
                API.setEntityDimension(player, 0);
                API.sendPictureNotificationToPlayer(player, "Ave Triads!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
        };
        */
        TriadsExitSkladColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsTriadsfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsTriadsfaction == 5)
            {
                API.setEntityPosition(player, new Vector3(112.0286f, -1978.42f, 20.95936f));
                API.setEntityDimension(player, 0);
                API.sendPictureNotificationToPlayer(player, "Ave Triads!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else return;
        };

        TriadsColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsBallasfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (lsBallasfaction == 5)
            {
                Triads_Form_Builder(player);
                return;

            }
            else return;
        };
    }

    private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (Player.GetFractionId(player) != 5)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной банде");
                return;
            }
            else return;
        }
    }
}
