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

public class Vagos : Script
{
    public Vagos()
    {
        API.onResourceStart += onResourceStart;
    }

    public readonly Vector3 VagosBlippPos = new Vector3(-1104.128f, -1637.284f, 4.615984f);/*
    public readonly Vector3 VagosEnterHousePos = new Vector3(114.1165f, -1961.005f, 21.33419f);
    public readonly Vector3 VagosEnterHouse2Pos = new Vector3(113.4786f, -1974.115f, 21.32017f);*/
    public readonly Vector3 VagosEnterSkladPos = new Vector3(-1104.128f, -1637.284f, 4.615984f);/*
    public readonly Vector3 VagosExitHousePos = new Vector3(-14.41827f, -1427.617f, 31.10148f);*/
    public readonly Vector3 VagosExitSkladPos = new Vector3(1087.883f, -3099.252f, -38.99994f);
    public readonly Vector3 VagosPos = new Vector3(1100.834f, -3101.895f, -38.99995f);
    public readonly Vector3 VagosColshaedPos = new Vector3(1100.834f, -3101.895f, -38.99995f);


    public Blip VagosBlip;

    /*
    public ColShape VagosEnterHouseColshape;
    public ColShape VagosEnterHouse2Colshape;
    public ColShape VagosExitHouseColshape;*/
    public ColShape VagosExitSkladColshape;
    public ColShape VagosEnterSkladColshape;
    public ColShape VagosColshape;

    private MenuManager MenuManager = new MenuManager();
    private Menu savedMenu = null;

    #region VagosFormeMenus
    private void Vagos_Form_Builder(Client client)
    {

        Menu menu = savedMenu;

        if (menu == null)
        {
            menu = new Menu("Vagos_Form", "Спец одежда", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true);
            menu.BannerColor = new Color(200, 0, 0, 90);
            menu.Callback = Vagos_Form_MenuManager;

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
    private void Vagos_Form_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
    {
        int gender = (client.hasData("gender_id")) ? client.getData("gender_id") : 0;
        if (menu.Id == "Vagos_Form" && menuItem.Id == "Svoboda")
        {
            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли  комплект со склада");
                API.shared.setPlayerClothes(client, 4, 4, 3);
                API.shared.setPlayerClothes(client, 3, 5, 0);
                API.shared.setPlayerClothes(client, 11, 120, 1);
                API.shared.setPlayerClothes(client, 6, 10, 1);
                API.shared.setPlayerClothes(client, 8, 20, 0);
                API.shared.setPlayerClothes(client, 7, 2, 0);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли  комплект со склада");
                API.shared.setPlayerClothes(client, 4, 42, 5);
                API.shared.setPlayerClothes(client, 3, 14, 0);
                API.shared.setPlayerClothes(client, 11, 9, 12);
                API.shared.setPlayerClothes(client, 6, 127, 12);
                API.shared.setPlayerClothes(client, 8, 1, 0);
                API.shared.setPlayerClothes(client, 7, 120, 0);
            }
        }

        else if (menu.Id == "Vagos_Form" && menuItem.Id == "Gang")
        {
            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 11, 7);
                API.shared.setPlayerClothes(client, 3, 155, 0);
                API.shared.setPlayerClothes(client, 11, 140, 7);
                API.shared.setPlayerClothes(client, 6, 33, 2);
                API.shared.setPlayerClothes(client, 8, 1, 0);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 96, 0);
                API.shared.setPlayerClothes(client, 3, 24, 0);
                API.shared.setPlayerClothes(client, 11, 9, 12);
                API.shared.setPlayerClothes(client, 6, 14, 1);
                API.shared.setPlayerClothes(client, 8, 1, 0);
            }
        }

        else if (menu.Id == "Vagos_Form" && menuItem.Id == "Job")
        {
            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 84, 1);
                API.shared.setPlayerClothes(client, 3, 158, 0);
                API.shared.setPlayerClothes(client, 11, 171, 3);
                API.shared.setPlayerClothes(client, 6, 73, 0);
                API.shared.setPlayerClothes(client, 8, 1, 6);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                API.shared.setPlayerClothes(client, 4, 96, 0);
                API.shared.setPlayerClothes(client, 3, 14, 0);
                API.shared.setPlayerClothes(client, 11, 9, 12);
                API.shared.setPlayerClothes(client, 6, 251, 0);
                API.shared.setPlayerClothes(client, 8, 1, 6);
            }
        }

        else if (menu.Id == "Vagos_Form" && menuItem.Id == "NoMarker1")
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

        else if (menu.Id == "Vagos_Form" && menuItem.Id == "Close")
        {
            savedMenu = menu;
            MenuManager.CloseMenu(client);
        }
    }

    #endregion

    public void onResourceStart()
    {
        List<Vehicle> VagosVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)1026149675, new Vector3(-1091.292, -1631.868, 4.645689), new Vector3(-2.009441, -0.06329505, 124.8288), 88, 88, 0), // vagos1
        API.createVehicle((VehicleHash)1026149675, new Vector3(-1089.521, -1634.408, 4.639759), new Vector3(-2.104338, -0.1383575, 124.5769), 88, 88, 0), // vagos2
        API.createVehicle((VehicleHash)1373123368, new Vector3(-1117.595, -1592.994, 3.76597), new Vector3(-0.0173281, 0.01135228, 124.6758), 88, 88, 0), // vagos3
        API.createVehicle((VehicleHash)1373123368, new Vector3(-1115.133, -1596.395, 3.765976), new Vector3(0.09157834, 0.0003641945, 123.5742), 88, 88, 0), // vagos4
        API.createVehicle((VehicleHash)15219735, new Vector3(-1125.46, -1614.226, 3.726006), new Vector3(0.004550743, 0.002380557, -52.77967), 88, 88, 0), // vagos5
        API.createVehicle((VehicleHash)15219735, new Vector3(-1127.462, -1611.633, 3.72558), new Vector3(0.01174867, 0.004012431, -55.19894), 88, 88, 0), // vagos6
        API.createVehicle((VehicleHash)15219735, new Vector3(-1129.34, -1609.102, 3.725504), new Vector3(0.02358475, 0.0008148893, -55.33894), 88, 88, 0), // vagos7
        API.createVehicle((VehicleHash)1349725314, new Vector3(-1108.872, -1633.687, 3.993757), new Vector3(0.08935337, 0.1009422, -54.33676), 88, 88, 0), // vagos8
        API.createVehicle((VehicleHash)1830407356, new Vector3(-1104.315, -1615.906, 3.971699), new Vector3(-2.327531, -0.09552921, 122.2134), 88, 88, 0), // vagos9
        API.createVehicle((VehicleHash)1830407356, new Vector3(-1108.397, -1603.033, 4.004964), new Vector3(0.7152684, 0.7430759, 125.5744), 88, 88, 0), // vagos10
        
        API.createVehicle((VehicleHash)1871995513, new Vector3(-1076.855, -1650.388, 3.854206), new Vector3(-0.2692751, 0.06410404, 128.8933), 88, 88, 0), // vagos11
        API.createVehicle((VehicleHash)1871995513, new Vector3(-1081.753, -1670.133, 4.064116), new Vector3(0.02922753, 0.03286183, -53.81018), 88, 88, 0), // vagos12
        API.createVehicle((VehicleHash)1131912276, new Vector3(-1084.103, -1662.936, 4.008899), new Vector3(-8.19786, 3.357456, -60.83224), 88, 88, 0), // vagos13
        API.createVehicle((VehicleHash)1131912276, new Vector3(-1084.886, -1661.568, 3.988678), new Vector3(-7.817211, 3.733446, -54.53906), 88, 88, 0), // vagos14
        API.createVehicle((VehicleHash)1131912276, new Vector3(-1085.808, -1660.204, 3.932492), new Vector3(-7.697802, 5.949408, -53.51314), 88, 88, 0) // vagos15
        };
        

        Vehicle Vagosveh2;
        foreach (Vehicle Vagosveh in VagosVehicles)
        {
            API.setEntityData(Vagosveh, "gang_id", 2);
            API.setVehicleNumberPlate(Vagosveh, "Vagos");
            API.setVehicleEngineStatus(Vagosveh, false);

            Vagosveh2 = Vagosveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int isVagosGang = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "Vagos")
            {
                if (isVagosGang != 2)
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

        VagosBlip = API.createBlip(VagosBlippPos);
        API.setBlipSprite(VagosBlip, 84);
        API.setBlipScale(VagosBlip, 1.0f);
        API.setBlipColor(VagosBlip, 71);
        API.setBlipName(VagosBlip, "Vagos");
        API.setBlipShortRange(VagosBlip, true);
        /*
        VagosEnterHouseColshape = API.createCylinderColShape(VagosEnterHousePos, 0.50f, 1f);
        VagosExitHouseColshape = API.createCylinderColShape(VagosExitHousePos, 0.50f, 1f);
        VagosEnterHouse2Colshape = API.createCylinderColShape(VagosEnterHouse2Pos, 0.50f, 1f);*/
        VagosEnterSkladColshape = API.createCylinderColShape(VagosEnterSkladPos, 0.50f, 1f);
        VagosColshape = API.createCylinderColShape(VagosColshaedPos, 0.50f, 1f);
        VagosExitSkladColshape = API.createCylinderColShape(VagosExitSkladPos, 0.50f, 1f);/*
        VagosExitHouseColshape = API.createCylinderColShape(VagosExitHousePos, 0.50f, 1f);*/
                                                                                          /*
                                                                                           API.createMarker(1, VagosEnterHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
                                                                                           API.createTextLabel("Вход в дом Vagos", VagosEnterHousePos, 15f, 0.65f);
                                                                                           API.createMarker(1, VagosEnterHouse2Pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
                                                                                           API.createTextLabel("Вход в дом Vagos", VagosEnterHouse2Pos, 15f, 0.65f);        
                                                                                           API.createMarker(1, VagosExitHousePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
                                                                                           API.createTextLabel("Выход с дома", VagosExitHousePos, 15f, 0.65f);*/
        API.createMarker(1, VagosEnterSkladPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Вход в склад Vagos", VagosEnterSkladPos, 15f, 0.65f);
        API.createMarker(1, VagosExitSkladPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Выход со склада", VagosExitSkladPos, 15f, 0.65f);
        API.createMarker(1, VagosPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90, 2);
        API.createTextLabel("Склад с одеждой", VagosPos, 15f, 0.65f);
        /*
                VagosEnterHouseColshape.onEntityEnterColShape += (shape, Entity) =>
                {
                    Client player;
                    player = API.getPlayerFromHandle(Entity);
                    if (API.isPlayerInAnyVehicle(player) == true)
                    {
                        return;
                    }
                    else
                    {
                        int lsVagosfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
                        if (lsVagosfaction == 2)
                        {
                            API.setEntityPosition(player, new Vector3(-14.20735f, -1440.696f, 31.10155f));
                            API.setEntityDimension(player, 2);
                            API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
                        }
                        else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
                    }
                };

                VagosEnterHouse2Colshape.onEntityEnterColShape += (shape, Entity) =>
                {
                    Client player;
                    player = API.getPlayerFromHandle(Entity);
                    if (API.isPlayerInAnyVehicle(player) == true)
                    {
                        return;
                    }
                    else
                    {
                        int lsVagosfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
                        if (lsVagosfaction == 2)
                        {
                            API.setEntityPosition(player, new Vector3(-14.20735f, -1440.696f, 31.10155f));
                            API.setEntityDimension(player, 2);
                            API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
                        }
                        else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
                    }
                };*/

        VagosEnterSkladColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsVagosfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsVagosfaction == 2)
            {
                API.setEntityPosition(player, new Vector3(1104.674f, -3099.414f, -38.99995f));
                API.setEntityDimension(player, 2);
                API.sendPictureNotificationToPlayer(player, "Здарова чувак!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");


        };

        VagosColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsBallasfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (lsBallasfaction == 2)
            {
                Vagos_Form_Builder(player);
                return;

            }
            else return;
        };
        /*
        VagosExitHouseColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsVagosfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsVagosfaction == 2)
            {
                API.setEntityPosition(player, new Vector3(118.3365f, -1974.886f, 21.12725f));
                API.setEntityDimension(player, 0);
                API.sendPictureNotificationToPlayer(player, "Ave Vagos!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else API.sendNotificationToPlayer(player, "~r~Вы не состоите в данной банде");
        };*/

        VagosExitSkladColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsVagosfaction = (player.hasData("gang_id")) ? player.getData("gang_id") : 0; ;
            if (lsVagosfaction == 2)
            {
                API.setEntityPosition(player, new Vector3(-1108.242f, -1637.362f, 4.615959f));
                API.setEntityDimension(player, 0);
                API.sendPictureNotificationToPlayer(player, "Ave Vagos!", "CHAR_MP_GERALD", 0, 3, "Оливер", "С очень серьезным взглядом");
            }
            else return;
        };
    }

    private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (Player.GetFractionId(player) != 2)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной банде");
                return;
            }
            else return;
        }
    }
}
