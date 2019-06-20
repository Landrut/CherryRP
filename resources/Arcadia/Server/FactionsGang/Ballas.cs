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

public class Ballas : Script
{
    public Ballas()
    {
        API.onResourceStart += onResourceStart;
    }

    public readonly Vector3 BallasBlippPos = new Vector3(113.4786f, -1974.115f, 21.32017f);
    public readonly Vector3 BallasEnterHousePos = new Vector3(114.1165f, -1961.005f, 21.33419f);
    public readonly Vector3 BallasEnterHouse2Pos = new Vector3(113.4786f, -1974.115f, 21.32017f);
    public readonly Vector3 BallasEnterSkladPos = new Vector3(104.7308f, -1975.81f, 20.95654f);
    public readonly Vector3 BallasExitHousePos = new Vector3(-14.41827f, -1427.617f, 31.10148f);
    public readonly Vector3 BallasExitSkladPos = new Vector3(1087.883f, -3099.252f, -38.99994f);
    public readonly Vector3 BallasPos = new Vector3(1100.834f, -3101.895f, -38.99995f);
    public readonly Vector3 BallasColshaedPos = new Vector3(1100.834f, -3101.895f, -38.99995f);
    /*
    public readonly Vector3 BallasDutyPos = new Vector3(2106.055f, 2947.804f, -61.90189f);
    public readonly Vector3 BallasArmoryPos = new Vector3(2117.457f, 2944.275f, -61.9019f);
    */

    public Blip BallasBlip;
    public ColShape BallasColshape;
    public ColShape BallasEnterSkladColshape;
    public ColShape BallasEnterHouseColshape;
    public ColShape BallasEnterHouse2Colshape;
    public ColShape BallasExitHouseColshape;
    public ColShape BallasExitSkladColshape;/*
    public ColShape BallasDutyColshape;
    public ColShape BallasArmoryColshape;*/

    private MenuManager MenuManager = new MenuManager();
    private Menu savedMenu = null;

    #region BallasFormeMenus
    private void Ballas_Form_Builder(Client client)
    {

        Menu menu = savedMenu;

        if (menu == null)
        {
            menu = new Menu("Ballas_Form", "Спец одежда", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true);
            menu.BannerColor = new Color(200, 0, 0, 90);
            menu.Callback = Ballas_Form_MenuManager;

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
    private void Ballas_Form_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
    {
        int gender = (client.hasData("gender_id")) ? client.getData("gender_id") : 0;
        if (menu.Id == "Ballas_Form" && menuItem.Id == "Svoboda")
        {
            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли  комплект со склада");
                API.shared.setPlayerClothes(client, 4, 11, 13);
                API.shared.setPlayerClothes(client, 3, 0, 0);
                API.shared.setPlayerClothes(client, 11, 10, 7);
                API.shared.setPlayerClothes(client, 6, 10, 0);
                API.shared.setPlayerClothes(client, 8, 1, 0);
                API.shared.setPlayerClothes(client, 7, 1, 0);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли  комплект со склада");
                API.shared.setPlayerClothes(client, 4, 26, 1);
                API.shared.setPlayerClothes(client, 3, 6, 0);
                API.shared.setPlayerClothes(client, 11, 88, 8);
                API.shared.setPlayerClothes(client, 6, 46, 8);
                API.shared.setPlayerClothes(client, 8, 1, 6);
                API.shared.setPlayerClothes(client, 7, 53, 1);
            }
        }

        else if (menu.Id == "Ballas_Form" && menuItem.Id == "Gang")
        {
            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 11, 13);
                API.shared.setPlayerClothes(client, 3, 3, 0);
                API.shared.setPlayerClothes(client, 11, 3, 1);
                API.shared.setPlayerClothes(client, 6, 11, 0);
                API.shared.setPlayerClothes(client, 8, 1, 0);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 87, 22);
                API.shared.setPlayerClothes(client, 3, 25, 0);
                API.shared.setPlayerClothes(client, 11, 38, 2);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 8, 1, 6);
                API.shared.setPlayerClothes(client, 7, 112, 2);
            }
        }

        else if (menu.Id == "Ballas_Form" && menuItem.Id == "Job")
        {
            if (gender == 1)
            {
                API.shared.setPlayerClothes(client, 4, 151, 1);
                API.shared.setPlayerClothes(client, 3, 166, 0);
                API.shared.setPlayerClothes(client, 11, 207, 2);
                API.shared.setPlayerClothes(client, 6, 33, 1);
                API.shared.setPlayerClothes(client, 8, 1, 6);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 26, 1);
                API.shared.setPlayerClothes(client, 3, 12, 0);
                API.shared.setPlayerClothes(client, 11, 32, 6);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 8, 1, 6);
            }
        }

        else if (menu.Id == "Ballas_Form" && menuItem.Id == "NoMarker1")
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

        else if (menu.Id == "Ballas_Form" && menuItem.Id == "Close")
        {
            savedMenu = menu;
            MenuManager.CloseMenu(client);
        }
    }

    #endregion

    public void onResourceStart()
    {
        List<Vehicle> BallasVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)(-1800170043), new Vector3(117.8346, -1946.407, 20.26613), new Vector3(-2.727736, 0.1218222, 67.33416), 145, 145, 0), // ballas4
            API.createVehicle((VehicleHash)(-1800170043), new Vector3(114.2758, -1951.271, 20.29742), new Vector3(-1.451169, 2.274462, 35.97584), 145, 145, 0), // ballas5
            API.createVehicle((VehicleHash)2006918058, new Vector3(85.93259, -1971.65, 20.59956), new Vector3(-1.763473, -0.06562591, -39.0846), 145, 145, 0), // ballas6
            API.createVehicle((VehicleHash)2006918058, new Vector3(90.27245, -1966.43, 20.55661), new Vector3(-0.08075362, -0.07456936, -37.58418), 145, 145, 0), // ballas7
            API.createVehicle((VehicleHash)2006918058, new Vector3(95.2224, -1960.523, 20.55673), new Vector3(-0.2375896, -0.05199248, -39.8713), 145, 145, 0), // ballas8
            API.createVehicle((VehicleHash)(-810318068), new Vector3(128.9779, -1941.76, 20.33699), new Vector3(-0.6545486, -0.6038433, 121.9672), 145, 145, 0), // ballas9
            API.createVehicle((VehicleHash)(-810318068), new Vector3(127.2235, -1939.058, 20.40319), new Vector3(1.262105, -0.3669098, 125.1938), 145, 145, 0), // ballas10
            API.createVehicle((VehicleHash)886934177, new Vector3(95.78653, -1947.003, 20.22255), new Vector3(0.09466925, 3.107312, -140.4106), 145, 145, 0), // ballas11
            API.createVehicle((VehicleHash)886934177, new Vector3(91.90303, -1940.216, 20.21433), new Vector3(0.07179563, 1.97289, -152.8737), 145, 145, 0), // ballas12
            API.createVehicle((VehicleHash)(-1800170043), new Vector3(116.345, -1949.097, 20.26765), new Vector3(-2.516167, -0.06391499, 52.73186), 145, 145, 0), // ballas13
            API.createVehicle((VehicleHash)16646064, new Vector3(113.6814, -1933.289, 20.06761), new Vector3(-0.06047838, 2.985837, 35.17613), 145, 145, 0), // ballas 1
            API.createVehicle((VehicleHash)16646064, new Vector3(106.7973, -1928.843, 20.06257), new Vector3(-0.1275377, 2.522511, 74.48629), 145, 145, 0), // ballas 2
            API.createVehicle((VehicleHash)1131912276, new Vector3(103.0413, -1955.284, 20.11524), new Vector3(-2.40192, 5.427038, -10.4458), 145, 145, 0), // ballas15
        API.createVehicle((VehicleHash)1131912276, new Vector3(105.1545, -1955.687, 20.12253), new Vector3(-1.754057, 3.214152, -5.969268), 145, 145, 0), // ballas16
        API.createVehicle((VehicleHash)1131912276, new Vector3(101.4087, -1955.293, 20.12555), new Vector3(-1.527987, 4.791868, 9.902953), 145, 145, 0) // ballas17
    };

        Vehicle Ballasveh2;
        foreach (Vehicle Ballasveh in BallasVehicles)
        {
            API.setEntityData(Ballasveh, "gang_id", 3);
            API.setVehicleNumberPlate(Ballasveh, "Ballas");
            API.setVehicleEngineStatus(Ballasveh, false);

            Ballasveh2 = Ballasveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int isBallasGang = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "Ballas")
            {
                if (isBallasGang != 3)
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

        BallasBlip = API.createBlip(BallasBlippPos);
        API.setBlipSprite(BallasBlip, 84);
        API.setBlipScale(BallasBlip, 1.0f);
        API.setBlipColor(BallasBlip, 27);
        API.setBlipName(BallasBlip, "Ballas");
        API.setBlipShortRange(BallasBlip, true);

        BallasColshape = API.createCylinderColShape(BallasColshaedPos, 0.50f, 1f);
        BallasEnterHouseColshape = API.createCylinderColShape(BallasEnterHousePos, 0.50f, 1f);
        BallasExitHouseColshape = API.createCylinderColShape(BallasExitHousePos, 0.50f, 1f);
        BallasEnterHouse2Colshape = API.createCylinderColShape(BallasEnterHouse2Pos, 0.50f, 1f);
        BallasEnterSkladColshape = API.createCylinderColShape(BallasEnterSkladPos, 0.50f, 1f);
        BallasExitSkladColshape = API.createCylinderColShape(BallasExitSkladPos, 0.50f, 1f);
        BallasExitHouseColshape = API.createCylinderColShape(BallasExitHousePos, 0.50f, 1f);/*
        BallasDutyColshape = API.createCylinderColShape(BallasDutyPos, 0.50f, 1f);
        BallasArmoryColshape = API.createCylinderColShape(BallasArmoryPos, 0.50f, 1f);*/
        API.createMarker(1, BallasEnterHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в дом Ballas", BallasEnterHousePos, 15f, 0.65f);
        API.createMarker(1, BallasEnterHouse2Pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в дом Ballas", BallasEnterHouse2Pos, 15f, 0.65f);
        API.createMarker(1, BallasEnterSkladPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в склад Ballas", BallasEnterSkladPos, 15f, 0.65f);
        API.createMarker(1, BallasExitSkladPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход со склада", BallasExitSkladPos, 15f, 0.65f);
        API.createMarker(1, BallasExitHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход с дома", BallasExitHousePos, 15f, 0.65f);
        API.createMarker(1, BallasPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90, 3);
        API.createTextLabel("Склад с одеждой", BallasPos, 15f, 0.65f);
        /*
        API.createMarker(23, BallasDutyPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        API.createTextLabel("Шкаф", BallasDutyPos, 15f, 0.65f);
        API.createMarker(23, BallasArmoryPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        API.createTextLabel("Оружейная", BallasArmoryPos, 15f, 0.65f);
        */
        BallasEnterHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                int lsBallasfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
                if (lsBallasfaction == 3)
                {
                    API.setEntityPosition(player, new Vector3(-14.20735f, -1440.696f, 31.10155f));
                    API.setEntityDimension(player, 3);
                    API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
                }
                else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
            }
        };

        BallasColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsBallasfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (lsBallasfaction == 3)
            {
                Ballas_Form_Builder(player);
                return;

            }
            else return;
        };

        BallasEnterHouse2Colshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                int lsBallasfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
                if (lsBallasfaction == 3)
                {
                    API.setEntityPosition(player, new Vector3(-14.20735f, -1440.696f, 31.10155f));
                    API.setEntityDimension(player, 3);
                    API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
                }
                else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
            }
        };

        BallasEnterSkladColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsBallasfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsBallasfaction == 3)
            {
                API.setEntityPosition(player, new Vector3(1104.674f, -3099.414f, -38.99995f));
                API.setEntityDimension(player, 3);
                API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");


        };

        BallasExitHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsBallasfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsBallasfaction == 3)
            {
                API.setEntityPosition(player, new Vector3(118.3365f, -1974.886f, 21.12725f));
                API.setEntityDimension(player, 0);
                API.sendPictureNotificationToPlayer(player, "Ave Ballas!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
        };

        BallasExitSkladColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsBallasfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsBallasfaction == 3)
            {
                API.setEntityPosition(player, new Vector3(112.0286f, -1978.42f, 20.95936f));
                API.setEntityDimension(player, 0);
                API.sendPictureNotificationToPlayer(player, "Ave Ballas!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else return;
        };
    }   
    
    private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (Player.GetFractionId(player) != 3)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной банде");
                return;
            }
            else return;
        }
    }
}
