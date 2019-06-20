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

namespace Formstorage
{
    public class Formstorage : Script
    {
        public Formstorage()
        {
            API.onResourceStart += onResourceStart;            
        }

        public readonly Vector3 PolicePos = new Vector3(457.7044f, -992.4735f, 30.6896f);
        public readonly Vector3 PoliceColshapePos = new Vector3(457.7044f, -992.4735f, 30.6896f);

        public readonly Vector3 MWPos = new Vector3(-2425.677f, 3274.232f, 32.97781f);
        public readonly Vector3 MWColshapePos = new Vector3(-2425.677f, 3274.232f, 32.97781f);

        public ColShape PoliceColshape;
        public ColShape MWColshape;

        private MenuManager MenuManager = new MenuManager();
        private Menu savedMenu = null;

        #region PoliceFormeMenus
        private void Police_Form_Builder(Client client)
        {

            Menu menu = savedMenu;

            if (menu == null)
            {
                menu = new Menu("Police_Form", "Полицейская форма", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true);
                menu.BannerColor = new Color(200, 0, 0, 90);
                menu.Callback = Police_Form_MenuManager;

                MenuItem menuItem = new MenuItem("Патрульная", "Форма для выезда в город", "Patrol");
                menuItem.ExecuteCallback = true;
                menu.Add(menuItem);

                MenuItem menuItem2 = new MenuItem("Депортамент", "Форма для депортамента", "Dep");
                menuItem2.ExecuteCallback = true;
                menu.Add(menuItem2);

                MenuItem menuItem3 = new MenuItem("Тренеровка", "Форма для общей тренировки", "Trening");
                menuItem3.ExecuteCallback = true;
                menu.Add(menuItem3);

                MenuItem menuItem4 = new MenuItem("S.W.A.T.", "Спец форма", "SWAT");
                menuItem4.ExecuteCallback = true;
                menu.Add(menuItem4);

                MenuItem menuItem5 = new MenuItem("Личная", "Переодеться в личную одежду", "NoMarker");
                menuItem5.ExecuteCallback = true;
                menu.Add(menuItem5);

                MenuItem menuItem6 = new MenuItem("~r~Закрыть", "Закрыть меню", "Close");
                menuItem6.ExecuteCallback = true;
                menu.Add(menuItem6);
            }
            MenuManager.OpenMenu(client, menu);
        }
        private void Police_Form_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            int gender = (client.hasData("gender_id")) ? client.getData("gender_id") : 0;
            if (menu.Id == "Police_Form" && menuItem.Id == "Patrol")
            {
                if (gender == 0)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада одежду патрульной службы");
                    API.shared.setPlayerClothes(client, 8, 129, 0);
                    API.shared.setPlayerClothes(client, 6, 21, 0);
                    API.shared.setPlayerClothes(client, 4, 35, 0);
                    API.shared.setPlayerClothes(client, 11, 55, 0);
                    API.shared.setPlayerClothes(client, 3, 0, 0);
                    API.shared.setPlayerClothes(client, 10, 8, 1);
                    API.shared.setPlayerAccessory(client, 0, 8, 0);
                }


                if (gender == 1)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада одежду патрульной службы");
                    API.shared.setPlayerClothes(client, 11, 48, 0);
                    API.shared.setPlayerClothes(client, 3, 14, 0);
                    API.shared.setPlayerClothes(client, 4, 34, 0);
                    API.shared.setPlayerClothes(client, 6, 25, 0);
                    API.shared.setPlayerClothes(client, 7, 0, 0);
                    API.shared.setPlayerClothes(client, 8, 35, 0);
                    API.shared.setPlayerClothes(client, 10, 8, 3);
                    API.shared.setPlayerAccessory(client, 0, 120, 0);
                }
            }

            else if (menu.Id == "Police_Form" && menuItem.Id == "Dep")
            {
                if (gender == 0)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли форму депортамента со склада");
                    API.shared.setPlayerClothes(client, 11, 16, 0);
                    API.shared.setPlayerClothes(client, 4, 46, 0);
                    API.shared.setPlayerClothes(client, 6, 24, 0);
                    API.shared.setPlayerClothes(client, 3, 19, 0);
                    API.shared.setPlayerClothes(client, 8, 15, 0);
                    API.shared.setPlayerAccessory(client, 0, 8, 0);
                }


                if (gender == 1)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли форму депортаментау со склада");
                    API.shared.setPlayerClothes(client, 6, 24, 0);
                    API.shared.setPlayerClothes(client, 4, 48, 0);
                    API.shared.setPlayerClothes(client, 3, 20, 0);
                    API.shared.setPlayerClothes(client, 11, 49, 0);
                    API.shared.setPlayerClothes(client, 8, 159, 0);
                    API.shared.setPlayerAccessory(client, 0, 120, 0);
                }
            }

            else if (menu.Id == "Police_Form" && menuItem.Id == "Trening")
            {
                if (gender == 0)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада тренеровочный комплект одежды");
                    API.shared.setPlayerClothes(client, 11, 50, 0);
                    API.shared.setPlayerClothes(client, 3, 27, 0);
                    API.shared.setPlayerAccessory(client, 0, 8, 0);
                    API.shared.setPlayerClothes(client, 4, 9, 7);
                    API.shared.setPlayerClothes(client, 6, 24, 0);
                    API.shared.setPlayerClothes(client, 8, 15, 0);
                }


                if (gender == 1)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада тренеровочный комплект одежды");
                    API.shared.setPlayerClothes(client, 3, 24, 0);
                    API.shared.setPlayerClothes(client, 11, 233, 0);
                    API.shared.setPlayerClothes(client, 6, 24, 0);
                    API.shared.setPlayerClothes(client, 4, 33, 0);
                    API.shared.setPlayerClothes(client, 8, 159, 0);
                    API.shared.setPlayerAccessory(client, 0, 120, 0);
                }
            }

            else if (menu.Id == "Police_Form" && menuItem.Id == "SWAT")
            {
                if (gender == 0)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект S.W.A.T. со склада");
                    API.shared.setPlayerClothes(client, 11, 251, 25);
                    API.shared.setPlayerAccessory(client, 0, 8, 0);
                    API.shared.setPlayerClothes(client, 4, 98, 25);
                    API.shared.setPlayerClothes(client, 3, 115, 0);
                    API.shared.setPlayerClothes(client, 6, 24, 0);
                    API.shared.setPlayerClothes(client, 8, 15, 0);
                }


                else if (gender == 1)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект S.W.A.T. со склада");
                    API.shared.setPlayerClothes(client, 11, 259, 17);
                    API.shared.setPlayerClothes(client, 4, 101, 17);
                    API.shared.setPlayerClothes(client, 6, 24, 0);
                    API.shared.setPlayerClothes(client, 3, 24, 0);
                    API.shared.setPlayerClothes(client, 8, 159, 0);
                    API.shared.setPlayerAccessory(client, 0, 120, 0);
                }
            }

            else if (menu.Id == "Police_Form" && menuItem.Id == "NoMarker")
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

            else if (menu.Id == "Police_Form" && menuItem.Id == "Close")
            {
                savedMenu = menu;
                MenuManager.CloseMenu(client);
            }
        }

        #endregion
        #region MWFormeMenus
        private void MW_Form_Builder(Client client)
        {

            Menu menu = savedMenu;

            if (menu == null)
            {
                menu = new Menu("MW_Form", "Тактическая форма", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true);
                menu.BannerColor = new Color(200, 0, 0, 90);
                menu.Callback = MW_Form_MenuManager;

                MenuItem menuItem = new MenuItem("Тактическая форма №1", "Тактическая форма №1", "Tactical01");
                menuItem.ExecuteCallback = true;
                menu.Add(menuItem);

                MenuItem menuItem2 = new MenuItem("Тактическая форма №2", "Тактическая форма №2", "Tactical02");
                menuItem2.ExecuteCallback = true;
                menu.Add(menuItem2);

                MenuItem menuItem3 = new MenuItem("Тактическая форма №3", "Тактическая форма №3", "Tactical03");
                menuItem3.ExecuteCallback = true;
                menu.Add(menuItem3);

                MenuItem menuItem4 = new MenuItem("Личная", "Переодеться в личную одежду", "NoMarker");
                menuItem4.ExecuteCallback = true;
                menu.Add(menuItem4);

                MenuItem menuItem5 = new MenuItem("~r~Закрыть", "Закрыть меню", "Close");
                menuItem5.ExecuteCallback = true;
                menu.Add(menuItem5);
            }
            MenuManager.OpenMenu(client, menu);
        }
        private void MW_Form_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            int gender = (client.hasData("gender_id")) ? client.getData("gender_id") : 0;
            if (menu.Id == "MW_Form" && menuItem.Id == "Tactical01")
            {
                if (gender == 0)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли тактический комплект со склада");
                    API.shared.setPlayerClothes(client, 11, 53, 2);
                    API.shared.setPlayerClothes(client, 8, 126, 0);
                    API.shared.setPlayerClothes(client, 3, 17, 0);
                    API.shared.setPlayerClothes(client, 4, 47, 0);
                    API.shared.setPlayerClothes(client, 6, 51, 6);
                    API.shared.setPlayerClothes(client, 1, 126, 0);
                    API.shared.setPlayerAccessory(client, 0, 124, 0);
                }


                if (gender == 1)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли тактический комплект со склада");
                    API.shared.setPlayerClothes(client, 11, 48, 0);
                    API.shared.setPlayerClothes(client, 3, 14, 0);
                    API.shared.setPlayerClothes(client, 4, 34, 0);
                    API.shared.setPlayerClothes(client, 6, 25, 0);
                    API.shared.setPlayerClothes(client, 7, 0, 0);
                    API.shared.setPlayerClothes(client, 8, 35, 0);
                    API.shared.setPlayerClothes(client, 10, 8, 3);
                    API.shared.setPlayerAccessory(client, 0, 120, 0);
                }
            }

            else if (menu.Id == "MW_Form" && menuItem.Id == "Tactical02")
            {
                if (gender == 0)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли тактический комплект со склада");
                    API.shared.setPlayerClothes(client, 11, 61, 4);
                    API.shared.setPlayerClothes(client, 8, 131, 15);
                    API.shared.setPlayerClothes(client, 3, 17, 0);
                    API.shared.setPlayerClothes(client, 4, 9, 7);
                    API.shared.setPlayerClothes(client, 6, 12, 7);
                    API.shared.setPlayerAccessory(client, 0, 58, 2);
                }


                if (gender == 1)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли тактический комплект со склада");
                    API.shared.setPlayerClothes(client, 6, 24, 0);
                    API.shared.setPlayerClothes(client, 4, 48, 0);
                    API.shared.setPlayerClothes(client, 3, 20, 0);
                    API.shared.setPlayerClothes(client, 11, 49, 0);
                    API.shared.setPlayerClothes(client, 8, 159, 0);
                    API.shared.setPlayerAccessory(client, 0, 120, 0);
                }
            }

            else if (menu.Id == "MW_Form" && menuItem.Id == "Tactical03")
            {
                if (gender == 0)
                {
                    API.shared.setPlayerClothes(client, 11, 243, 0);
                    API.shared.setPlayerClothes(client, 8, 125, 0);
                    API.shared.setPlayerClothes(client, 3, 17, 0);
                    API.shared.setPlayerClothes(client, 4, 33, 0);
                    API.shared.setPlayerClothes(client, 6, 27, 6);
                    API.shared.setPlayerClothes(client, 1, 46, 0);
                    API.shared.setPlayerAccessory(client, 0, 112, 2);
                }


                if (gender == 1)
                {
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли тактический комплект со склада");
                    API.shared.setPlayerClothes(client, 3, 24, 0);
                    API.shared.setPlayerClothes(client, 11, 233, 0);
                    API.shared.setPlayerClothes(client, 6, 24, 0);
                    API.shared.setPlayerClothes(client, 4, 33, 0);
                    API.shared.setPlayerClothes(client, 8, 159, 0);
                    API.shared.setPlayerAccessory(client, 0, 120, 0);
                }
            }

            else if (menu.Id == "MW_Form" && menuItem.Id == "NoMarker")
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

            else if (menu.Id == "MW_Form" && menuItem.Id == "Close")
            {
                savedMenu = menu;
                MenuManager.CloseMenu(client);
            }
        }

        #endregion

        public void onResourceStart()
        {
            PoliceColshape = API.createCylinderColShape(PoliceColshapePos, 1.50f, 1f);
            MWColshape = API.createCylinderColShape(MWColshapePos, 1.50f, 1f);

            API.createMarker(1, PolicePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createTextLabel("Получение формы", PolicePos, 15f, 0.65f);
            API.createMarker(1, MWPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createTextLabel("Получение формы", MWPos, 15f, 0.65f);
        
            PoliceColshape.onEntityEnterColShape += (shape, Entity) =>
    {
        Client player;
        player = API.getPlayerFromHandle(Entity);
        int ispolicefaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
        if (ispolicefaction == 2)
        {
            int onduty = Player.IsPlayerOnDuty(player);
            if (onduty == 1)
            {
                Police_Form_Builder(player);
                return;
            }
            else API.sendChatMessageToPlayer(player, "~y~Вы не на смене ~b~");
        }
        else API.sendChatMessageToPlayer(player, "~y~Вы не принадлежите данной фракции~b~");
    };

            MWColshape.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                int isMeryWetheraction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
                if (isMeryWetheraction == 3)
                {
                    int onduty = Player.IsPlayerOnDuty(player);
                    if (onduty == 1)
                    {
                        MW_Form_Builder(player);
                        return;
                    }
                    else API.sendChatMessageToPlayer(player, "~y~Вы не на смене ~b~");
                }
                else API.sendChatMessageToPlayer(player, "~y~Вы не принадлежите данной фракции~b~");
            };
        }
    }
}



