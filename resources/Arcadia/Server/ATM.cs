using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//
//
using CherryMPServer;
using CherryMPShared;
//;
using CherryMPServer.Constant;

using MenuManagement;
using PlayerFunctions;

namespace Arcadia.Server
{
    public class ATM : Script
    {
        public ATM()
        {
            API.onResourceStart += OnResourceStart;
        }
        public readonly Vector3 ATM1pos = new Vector3(-56.94f, -1752.193f, 29.42f);
        public readonly Vector3 ATM2pos = new Vector3(147.7821f, -1035.184f, 29.34314f);
        public readonly Vector3 ATM3pos = new Vector3(129.3736f, -1292.111f, 29.26953f);
        public readonly Vector3 ATM4pos = new Vector3(-3144.083f, 1127.451f, 20.85531f);
        public readonly Vector3 ATM5pos = new Vector3(-1205.595f, -324.7567f, 37.85835f);
        public readonly Vector3 ATM6pos = new Vector3(111.1306f, -775.5909f, 31.43831f);
        public readonly Vector3 ATM7pos = new Vector3(-866.2137f, -187.5871f, 37.65998f);
        public readonly Vector3 ATM8pos = new Vector3(-1316.04f, -834.9092f, 16.96181f);
        public readonly Vector3 ATM9pos = new Vector3(288.5416f, -1256.827f, 29.44076f);
        public readonly Vector3 ATM10pos = new Vector3(1171.578f, 2702.242f, 38.17542f);
        public readonly Vector3 ATM11pos = new Vector3(-97.04198f, 6455.195f, 31.46142f);

        public Blip ATM1;
        public ColShape ATM1col;
        public Blip ATM2;
        public ColShape ATM2col;
        public Blip ATM3;
        public ColShape ATM3col;
        public Blip ATM4;
        public ColShape ATM4col;
        public Blip ATM5;
        public ColShape ATM5col;
        public Blip ATM6;
        public ColShape ATM6col;
        public Blip ATM7;
        public ColShape ATM7col;
        public Blip ATM8;
        public ColShape ATM8col;
        public Blip ATM9;
        public ColShape ATM9col;
        public Blip ATM10;
        public ColShape ATM10col;
        public Blip ATM11;
        public ColShape ATM11col;

        private MenuManager MenuManager = new MenuManager();
        private Menu savedMenu = null;

        private void ATM_Menu(Client client)
        {
            Menu menuATM = savedMenu;

            if (menuATM == null)
            {
                menuATM = new Menu("ATM", "Банкомат", "Операции со счётом", 0, 0, Menu.MenuAnchor.MiddleRight, false, true)
                {
                    BannerColor = new Color(200, 0, 0, 90),
                    Callback = ATM_MenuManager
                };

                MenuItem menuItem = new MenuItem("Баланс", "Узнать баланс вашего счёта", "Ballance");
                menuItem.ExecuteCallback = true;
                menuATM.Add(menuItem);

                menuItem = new MenuItem("Ввести сумму", "Ввести сумму для снятия со счета", "withdraw");
                menuItem.SetInput("", 10, InputType.Number);
                menuATM.Add(menuItem);

                MenuItem menuItem2 = new MenuItem("~g~Снять со счета", "Снять деньги", "close");
                menuItem2.ExecuteCallback = true;
                menuATM.Add(menuItem2);

                MenuItem menuItem3 = new MenuItem("~r~Закрыть", "Закрыть меню", "close");
                menuItem3.ExecuteCallback = true;
                menuATM.Add(menuItem3);
            }
            MenuManager.OpenMenu(client, menuATM);
        }

        private void ATM_MenuManager(Client client, Menu menuATM, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Client player = API.getPlayerFromHandle(client);
            if (itemIndex == 0)
            {
                DisplayTestMenudata(client, player);
            }
            else if (itemIndex == 2)
            {
                int amount = 0;
                if (data["withdraw"] != null)
                    amount = (int)data["withdraw"];

                if (amount < 100)
                {
                    API.sendNotificationToPlayer(player, "~b~Минимальная сумма для снятия = 100~g~$");
                    return;
                }
                else WithDraw(player, amount);
            }
            else if (itemIndex == 3)
            {
                MenuManager.CloseMenu(client);
            }
        }

        private void DisplayTestMenudata(Client client, Client player)
        {
            client.sendPictureNotificationToPlayer("На вашем счету: ~g~" + Player.GetBankMoney(player) + "$", "CHAR_BANK_FLEECA", 0, 3, "Fleeca Bank", "Состояние счёта");
        }

        private void WithDraw(Client player, int amount)
        {
            if (PlayerFunctions.Player.GetBankMoney(player) < amount)
            {
                API.sendNotificationToPlayer(player, "~r~У вас нет столько денег на счету!");
                return;
            }
            else
            {
                API.playPlayerAnimation(player, 0, "mp_common_heist", "a_atm_mugging");
                Player.ChangeMoney(player, +amount);
                Player.ChangeBankMoney(player, -amount);
                API.sendPictureNotificationToPlayer(player, "С вашего счета снято: ~g~" + amount + "$", "CHAR_BANK_FLEECA", 0, 3, "Fleeca Bank", "Операция");
                API.sendPictureNotificationToPlayer(player, "Ваш новый баланс: ~g~" + Player.GetBankMoney(player) + "$", "CHAR_BANK_FLEECA", 0, 3, "Fleeca Bank", "Состояние счёта");
            }
        }

        private void ATM_MenuClose(Client client)
        {
            MenuManager.CloseMenu(client);
        }

        private void OnResourceStart()
        {
            ATM1 = API.createBlip(new Vector3(-56.94f, -1752.193f, 29.42f));
            API.setBlipSprite(ATM1, 108);
            API.setBlipColor(ATM1, 2);
            API.setBlipScale(ATM1, 0.70f);
            API.setBlipName(ATM1, "Банкомат");
            API.setBlipShortRange(ATM1, true);

            ATM2 = API.createBlip(new Vector3(147.7821f, -1035.184f, 29.34314f));
            API.setBlipSprite(ATM2, 108);
            API.setBlipColor(ATM2, 2);
            API.setBlipScale(ATM2, 0.70f);
            API.setBlipName(ATM2, "Банкомат");
            API.setBlipShortRange(ATM2, true);

            ATM4 = API.createBlip(ATM4pos);
            API.setBlipSprite(ATM4, 108);
            API.setBlipColor(ATM4, 2);
            API.setBlipScale(ATM4, 0.70f);
            API.setBlipName(ATM4, "Банкомат");
            API.setBlipShortRange(ATM4, true);

            ATM5 = API.createBlip(ATM5pos);
            API.setBlipSprite(ATM5, 108);
            API.setBlipColor(ATM5, 2);
            API.setBlipScale(ATM5, 0.70f);
            API.setBlipName(ATM5, "Банкомат");
            API.setBlipShortRange(ATM5, true);

            ATM6 = API.createBlip(ATM6pos);
            API.setBlipSprite(ATM6, 108);
            API.setBlipColor(ATM6, 2);
            API.setBlipScale(ATM6, 0.70f);
            API.setBlipName(ATM6, "Банкомат");
            API.setBlipShortRange(ATM6, true);

            ATM7 = API.createBlip(ATM7pos);
            API.setBlipSprite(ATM7, 108);
            API.setBlipColor(ATM7, 2);
            API.setBlipScale(ATM7, 0.70f);
            API.setBlipName(ATM7, "Банкомат");
            API.setBlipShortRange(ATM7, true);

            ATM8 = API.createBlip(ATM8pos);
            API.setBlipSprite(ATM8, 108);
            API.setBlipColor(ATM8, 2);
            API.setBlipScale(ATM8, 0.70f);
            API.setBlipName(ATM8, "Банкомат");
            API.setBlipShortRange(ATM8, true);

            ATM9 = API.createBlip(ATM9pos);
            API.setBlipSprite(ATM9, 108);
            API.setBlipColor(ATM9, 2);
            API.setBlipScale(ATM9, 0.70f);
            API.setBlipName(ATM9, "Банкомат");
            API.setBlipShortRange(ATM9, true);

            ATM10 = API.createBlip(ATM10pos);
            API.setBlipSprite(ATM10, 108);
            API.setBlipColor(ATM10, 2);
            API.setBlipScale(ATM10, 0.70f);
            API.setBlipName(ATM10, "Банкомат");
            API.setBlipShortRange(ATM10, true);

            ATM11 = API.createBlip(ATM11pos);
            API.setBlipSprite(ATM11, 108);
            API.setBlipColor(ATM11, 2);
            API.setBlipScale(ATM11, 0.70f);
            API.setBlipName(ATM11, "Банкомат");
            API.setBlipShortRange(ATM11, true);

            ATM1col = API.createCylinderColShape(ATM1pos, 0.50f, 1f);
            ATM2col = API.createCylinderColShape(ATM2pos, 0.50f, 1f);
            ATM3col = API.createCylinderColShape(ATM3pos, 0.50f, 1f);
            ATM4col = API.createCylinderColShape(ATM4pos, 0.50f, 1f);
            ATM5col = API.createCylinderColShape(ATM5pos, 0.50f, 1f);
            ATM6col = API.createCylinderColShape(ATM6pos, 0.50f, 1f);
            ATM7col = API.createCylinderColShape(ATM7pos, 0.50f, 1f);
            ATM8col = API.createCylinderColShape(ATM8pos, 0.50f, 1f);
            ATM9col = API.createCylinderColShape(ATM9pos, 0.50f, 1f);
            ATM10col = API.createCylinderColShape(ATM10pos, 0.50f, 1f);
            ATM11col = API.createCylinderColShape(ATM11pos, 0.50f, 1f);
            API.createMarker(1, ATM1pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ATM2pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ATM3pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ATM4pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ATM5pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ATM6pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ATM7pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ATM8pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ATM9pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ATM10pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ATM11pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
            ATM1col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                //API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 1");
                ATM_Menu(player);
            };
            ATM1col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };

            ATM2col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                //API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 2");
                ATM_Menu(player);
            };
            ATM2col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };

            ATM3col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 3");
                ATM_Menu(player);
            };
            ATM3col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };

            ATM4col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 3");
                ATM_Menu(player);
            };
            ATM4col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };

            ATM5col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 3");
                ATM_Menu(player);
            };
            ATM5col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };

            ATM6col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 3");
                ATM_Menu(player);
            };
            ATM6col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };

            ATM7col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 3");
                ATM_Menu(player);
            };
            ATM7col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };

            ATM8col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 3");
                ATM_Menu(player);
            };
            ATM8col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };

            ATM9col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 3");
                ATM_Menu(player);
            };
            ATM9col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };

            ATM10col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 3");
                ATM_Menu(player);
            };
            ATM10col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };

            ATM11col.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendNotificationToPlayer(player, "~b~[ADMIN INFO] ~w~ATM id = 3");
                ATM_Menu(player);
            };
            ATM11col.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                ATM_MenuClose(player);
            };
        }
    }
}
