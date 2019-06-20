using CherryMPServer;
using CherryMPServer.Managers;
using CherryMPServer.Constant;
using CherryMPShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerFunctions;
using Arcadia.Server.Managers;
using Arcadia.Server.Models;
using Arcadia.Server.XMLDatabase;
using Arcadia.Server.Models;
using Newtonsoft.Json;
using MenuManagement;

namespace VehicleDoors
{
    public class VehicleDoors : Script
    {
        //  0 = Front Left Door
        //  1 = Front Right Door
        //  2 = Back Left Door
        //  3 = Back Right Door
        //  4 = Hood
        //  5 = Trunk
        //  6 = Back
        //  7 = Back2

        public VehicleDoors()
        {
            API.onResourceStart += OnResourceStart;
            API.onClientEventTrigger += onClientEvent;
        }

        public void OnResourceStart()
        {
            API.consoleOutput("[Двери авто] Загрукжен");
        }
        private MenuManager MenuManager = new MenuManager();
        private Menu savedMenu = null;

        private void CarFunc_Menu_Builder(Client client)
        {

            Menu menu = savedMenu;

            if (menu == null)
            {
                menu = new Menu("CarFunc", "Панель транспорта", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true);
                menu.BannerColor = new Color(200, 0, 0, 90);
                menu.Callback = CarFunc_MenuManager;

                MenuItem menuItem = new MenuItem("Капот/багажник", "Открытие/закрытие", "Bagaj");
                menuItem.ExecuteCallback = true;
                menu.Add(menuItem);

                menuItem = new MenuItem("Двери", "Открытие/закрытие дверей", "Dors");
                menuItem.ExecuteCallback = true;
                menu.Add(menuItem);

                MenuItem menuItem2 = new MenuItem("~g~Двигатель", "Завести/заглушить", "Engin");
                menuItem2.ExecuteCallback = true;
                menu.Add(menuItem2);

                MenuItem menuItem3 = new MenuItem("~r~Закрыть", "Закрыть меню", "Close");
                menuItem3.ExecuteCallback = true;
                menu.Add(menuItem3);
            }
            MenuManager.OpenMenu(client, menu);
        }


        private void Sub_Bagaj_Menu_Builder(Client client)
        {
            Menu MenuBagaj = new Menu("Bagaj", "Тип", "~y~Выберите часть", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true);
            MenuBagaj.BannerColor = new Color(200, 0, 0, 90);
            MenuBagaj.Callback = Bagaj_MenuManager;

            MenuItem menuItem = new MenuItem("Открыть/закрыть капот", "", "Dors4");
            menuItem.ExecuteCallback = true;
            MenuBagaj.Add(menuItem);

            menuItem = new MenuItem("Открыть/закрыть багажник", "", "Dors5");
            menuItem.ExecuteCallback = true;
            MenuBagaj.Add(menuItem);

            menuItem = new MenuItem("Открыть/закрыть заднюю дверь", "", "Dors6");
            menuItem.ExecuteCallback = true;
            MenuBagaj.Add(menuItem);

            menuItem = new MenuItem("Открыть/закрыть переднюю дверь", "", "Dors7");
            menuItem.ExecuteCallback = true;
            MenuBagaj.Add(menuItem);

            MenuItem menuItem3 = new MenuItem("~r~Назад", "Вернуться назад", "Back");
            menuItem3.ExecuteCallback = true;
            MenuBagaj.Add(menuItem3);

            MenuManager.OpenMenu(client, MenuBagaj);
        }

        private void Sub_Dors_Menu_Builder(Client client)
        {
            Menu MenuDors = new Menu("Dors", "Открытие/закрытие дверей", "~y~Выберите нужную дверь", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true);
            MenuDors.BannerColor = new Color(200, 0, 0, 90);
            MenuDors.Callback = Dors_MenuManager;

            MenuItem menuItem = new MenuItem("Передняя левая", "", "Dors0");
            menuItem.ExecuteCallback = true;
            MenuDors.Add(menuItem);

            menuItem = new MenuItem("Передняя правая", "", "Dors1");
            menuItem.ExecuteCallback = true;
            MenuDors.Add(menuItem);

            menuItem = new MenuItem("Задняя левая", "", "Dors2");
            menuItem.ExecuteCallback = true;
            MenuDors.Add(menuItem);

            menuItem = new MenuItem("Задняя правая", "", "Dors3");
            menuItem.ExecuteCallback = true;
            MenuDors.Add(menuItem);

            MenuItem menuItem3 = new MenuItem("~r~Назад", "Вернуться назад", "Back");
            menuItem3.ExecuteCallback = true;
            MenuDors.Add(menuItem3);

            MenuManager.OpenMenu(client, MenuDors);
        }

        private void CarFunc_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            if (itemIndex == 0)
            {
                savedMenu = menu;
                Sub_Bagaj_Menu_Builder(client);
            }
            else if (itemIndex == 1)
            {
                savedMenu = menu;
                Sub_Dors_Menu_Builder(client);
            }
            else if (menu.Id == "CarFunc" && menuItem.Id == "Engin")
            {
                if (client.vehicle.engineStatus == true)
                {
                    client.vehicle.engineStatus = false;
                    API.sendNotificationToPlayer(client, "Двигатель заглушен");
                }
                else if (client.vehicle.engineStatus == false)
                {
                    client.vehicle.engineStatus = true;
                    API.sendNotificationToPlayer(client, "Двигатель заведён");
                }
            }
            else if (menu.Id == "CarFunc")
            {
                CarFunc_Menu_Builder(client);
                savedMenu = menu;
                MenuManager.CloseMenu(client);
            }
            else if (menu.Id == "CarFunc" && menuItem.Id == "Close")
            {
                savedMenu = menu;
                MenuManager.CloseMenu(client);
            }
        }

        bool open4 = false;
        bool open5 = false;
        bool open6 = false;
        bool open7 = false;

        private void Bagaj_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            if (menu.Id == "Bagaj" && menuItem.Id == "Dors4")
            {
                if (open4 == false)
                {

                    client.vehicle.openDoor(4);
                    open4 = true;
                    API.sendPictureNotificationToPlayer(client, "Капот открыт!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }

                else if (open4 == true)
                {
                    client.vehicle.closeDoor(4);
                    open4 = false;
                    API.sendPictureNotificationToPlayer(client, "Капот закрыт", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }
            }
            else if (menu.Id == "Bagaj" && menuItem.Id == "Dors5")
            {
                if (open5 == false)
                {

                    client.vehicle.openDoor(5);
                    open5 = true;
                    API.sendPictureNotificationToPlayer(client, "Багажник открыт!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }

                else if (open5 == true)
                {
                    client.vehicle.closeDoor(5);
                    open5 = false;
                    API.sendPictureNotificationToPlayer(client, "Багажник закрыт", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }
            }
            else if (menu.Id == "Bagaj" && menuItem.Id == "Dors6")
            {
                if (open6 == false)
                {

                    client.vehicle.openDoor(6);
                    open6 = true;
                    API.sendPictureNotificationToPlayer(client, "Задняя дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }

                else if (open6 == true)
                {
                    client.vehicle.closeDoor(6);
                    open6 = false;
                    API.sendPictureNotificationToPlayer(client, "Задняя дверь закрыта", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }
            }
            else if (menu.Id == "Bagaj" && menuItem.Id == "Dors7")
            {
                if (open7 == false)
                {

                    client.vehicle.openDoor(7);
                    open7 = true;
                    API.sendPictureNotificationToPlayer(client, "Задняя дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }

                else if (open7 == true)
                {
                    client.vehicle.closeDoor(7);
                    open7 = false;
                    API.sendPictureNotificationToPlayer(client, "Задняя дверь закрыта", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }
                else if (menu.Id == "Bagaj")
                {
                    CarFunc_Menu_Builder(client);
                }
                else if (menu.Id == "Bagaj" && menuItem.Id == "Back")
                {
                    CarFunc_Menu_Builder(client);
                }
            }
        }
        bool open0 = false;
        bool open1 = false;
        bool open2 = false;
        bool open3 = false;
        private void Dors_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            if (menu.Id == "Dors" && menuItem.Id == "Dors0")
            {
                if (open0 == false)
                {

                    client.vehicle.openDoor(0);
                    open0 = true;
                    API.sendPictureNotificationToPlayer(client, "Дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }

                else if (open0 == true)
                {
                    client.vehicle.closeDoor(0);
                    open0 = false;
                    API.sendPictureNotificationToPlayer(client, "Дверь закрыта", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }
            }
            else if (menu.Id == "Dors" && menuItem.Id == "Dors1")
            {
                if (open1 == false)
                {

                    client.vehicle.openDoor(1);
                    open1 = true;
                    API.sendPictureNotificationToPlayer(client, "Дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }

                else if (open1 == true)
                {
                    client.vehicle.closeDoor(1);
                    open1 = false;
                    API.sendPictureNotificationToPlayer(client, "Дверь закрыта", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }
            }
            else if (menu.Id == "Dors" && menuItem.Id == "Dors2")
            {
                if (open2 == false)
                {

                    client.vehicle.openDoor(2);
                    open2 = true;
                    API.sendPictureNotificationToPlayer(client, "Дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }

                else if (open2 == true)
                {
                    client.vehicle.closeDoor(2);
                    open2 = false;
                    API.sendPictureNotificationToPlayer(client, "Дверь закрыта", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }
            }
            else if (menu.Id == "Dors" && menuItem.Id == "Dors3")
            {
                if (open3 == false)
                {

                    client.vehicle.openDoor(3);
                    open3 = true;
                    API.sendPictureNotificationToPlayer(client, "Дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }

                else if (open3 == true)
                {
                    client.vehicle.closeDoor(3);
                    open3 = false;
                    API.sendPictureNotificationToPlayer(client, "Дверь закрыта", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
                    return;
                }
            }
            else if (menu.Id == "Dors")
            {
                CarFunc_Menu_Builder(client);
            }
            else if (menu.Id == "Dors" && menuItem.Id == "Back")
            {
                CarFunc_Menu_Builder(client);
            }
        }

        private void onClientEvent(Client player, string EventName, params object[] arguments)
        {
            if (EventName == "OpenmenuCar")
            {
                CarFunc_Menu_Builder(player);
            }
            else return;
        }
    }
}




/*
private void Opendors_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
{
    if (itemIndex == 0)
    {
        savedMenu = menu;
        if (client.vehicle.engineStatus == true)
        {
            client.vehicle.engineStatus = false;
            API.sendNotificationToPlayer(client, "Двигатель заглушен");
        }
        else if (client.vehicle.engineStatus == false)
        {
            client.vehicle.engineStatus = true;
            API.sendNotificationToPlayer(client, "Двигатель заведён");
        }if (client.vehicle.engineStatus == true)
    {
        client.vehicle.engineStatus = false;
        API.sendNotificationToPlayer(client, "Двигатель заглушен");
    }
    else if (client.vehicle.engineStatus == false)
    {
        client.vehicle.engineStatus = true;
        API.sendNotificationToPlayer(client, "Двигатель заведён");
    }
    }
    else if (itemIndex == 2)
    {
        savedMenu = menu;
        Sub_Dors1_Menu_Builder(client);
    }
    else if (itemIndex == 3)
    {
        savedMenu = menu;
        Sub_Bagaj_Menu_Builder(client);
    }
    else if (menu.Id == "CarFunc" && menuItem.Id == "Clear")
    {
        API.stopPlayerAnimation(client);
    }
    else if (menu.Id == "CarFunc")
    {
        OpenDors_Menu_Builder(client);
        savedMenu = menu;
        MenuManager.CloseMenu(client);
    }
    else if (menu.Id == "CarFunc" && menuItem.Id == "Close")
    {
        savedMenu = menu;
        MenuManager.CloseMenu(client);
    }
}

private void Dors_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
{
    bool open = false;
    bool open1 = false;
    bool open2 = false;
    bool open3 = false;

    if (menu.Id == "Dors1" && menuItem.Id == "OpenDor1")
    {
        if (open == false)
        {
            client.vehicle.openDoor(0);
            open = true;
            API.sendPictureNotificationToPlayer(client, "Дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
        if (open == true)
        {
            client.vehicle.closeDoor(0);
            open = false;
            API.sendPictureNotificationToPlayer(client, "Дверь закрыта", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
    }
    else if (menu.Id == "Dors1" && menuItem.Id == "OpenDor2")
    {
        if (open1 == false)
        {
            client.vehicle.openDoor(1);
            open1 = true;
            API.sendPictureNotificationToPlayer(client, "Дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
        if (open1 == true)
        {
            client.vehicle.closeDoor(1);
            open1 = false;
            API.sendPictureNotificationToPlayer(client, "Дверь закрыта", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
    }
    else if (menu.Id == "Dors1" && menuItem.Id == "OpenDor3")
    {
        if (open2 == false)
        {
            client.vehicle.openDoor(2);
            open2 = true;
            API.sendPictureNotificationToPlayer(client, "Дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
        if (open2 == true)
        {
            client.vehicle.closeDoor(2);
            open2 = false;
            API.sendPictureNotificationToPlayer(client, "Дверь закрыта", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
    }
    else if (menu.Id == "Dors1" && menuItem.Id == "OpenDor4")
    {
        if (open3 == false)
        {
            client.vehicle.openDoor(3);
            open3 = true;
            API.sendPictureNotificationToPlayer(client, "Дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
        if (open3 == true)
        {
            client.vehicle.closeDoor(3);
            open3 = false;
            API.sendPictureNotificationToPlayer(client, "Дверь закрыта", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
    }
}
private void Bagaj_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
{
    bool open4 = false;
    bool open6 = false;
    if (menu.Id == "Dors2" && menuItem.Id == "OpenDor4")
    {
        if (open4 == false)
        {

            client.vehicle.openDoor(4);
            open4 = true;
            API.sendPictureNotificationToPlayer(client, "Капот открыт!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }

        if (open4 == true)
        {
            client.vehicle.closeDoor(4);
            open4 = false;
            API.sendPictureNotificationToPlayer(client, "Капот закрыт", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
    }

    else if (menu.Id == "Dors2" && menuItem.Id == "OpenDor6")
    {
        if (open6 == false)
        {
            client.vehicle.openDoor(6);
            open6 = true;
            API.sendPictureNotificationToPlayer(client, "Багажник открыт!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
        if (open6 == true)
        {
            client.vehicle.closeDoor(6);
            open6 = false;
            API.sendPictureNotificationToPlayer(client, "Багажник закрыт", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
            return;
        }
    }
}
private void onClientEvent(Client player, string EventName, params object[] arguments)
{
    if (EventName == "OpenmenuCar")
    {
        OpenDors_Menu_Builder(player);
    }
    else return;
}
}
}



[Command("открыть", GreedyArg = true)]
public void OpenCommand(Client player, string door)
{
if (player.isInVehicle)
{
var doorId = Convert.ToInt32(door);
player.vehicle.openDoor(doorId);
API.sendPictureNotificationToPlayer(player, "Дверь открыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
return;
}

API.sendPictureNotificationToPlayer(player, "Вы должны быть в машине!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
return;
}


[Command("закрыть", GreedyArg = true)]
public void CloseCommand(Client player, string door)
{
if (player.isInVehicle)
{
var doorId = Convert.ToInt32(door);
player.vehicle.closeDoor(doorId);
API.sendPictureNotificationToPlayer(player, "Дверь закрыта!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
return;
}

API.sendPictureNotificationToPlayer(player, "Вы должны быть в машине!", "CHAR_CARSITE2", 0, 3, "Охранная система машины", "Обращение к водителю");
return;
}
}
}
*/
