using System;
using System.IO;
//
//
using CherryMPServer;
using CherryMPServer.Constant;
using CherryMPShared;
//;
using MenuManagement;
using MySQL;
using Newtonsoft.Json;

using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using CustomSkin;

namespace PlayerFunctions
{
    public class Player : Script
    {

        public static string SAVE_DIRECTORY = "Server/Data/CustomCharacters";
        public static Dictionary<NetHandle, PlayerCustomization> CustomPlayerData = new Dictionary<NetHandle, PlayerCustomization>();

        public Player()
        {
            API.onChatMessage += OnChatMessageHandler;
        }

        public void findPlayer(Client sender, string idOrName)
        { }
        public void getIdFromClient(Client target)
        { }
        public void getClientFromId(Client sender, int id)
        { }

        public void OnChatMessageHandler(Client player, string message, CancelEventArgs e)
        {
            var msg = "~b~[" + API.exported.playerids.getIdFromClient(player) + "] " + "~w~" + player.name.Replace("_", " ") + " говорит: " + message;
            var players = API.getPlayersInRadiusOfPlayer(30, player);
            foreach (Client c in players)
            {
                API.sendChatMessageToPlayer(c, msg);
            }
            e.Cancel = true;
        }

        private MenuManager MenuManager = new MenuManager();
        private static Menu savedMenu = null;


        private static void RegisterMenu(Client client)
        {
            Menu menuReg = savedMenu;

            if (menuReg == null)
            {
                menuReg = new Menu("Reg", "Регистрация", "Регистрация нового аккаунта", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(200, 0, 0, 150),
                    Callback = menuRegManager
                };

                List<string> genderlist = new List<string>() { "Мужской", "Женский" };
                MenuItem menuItem = new ListItem("Пол персонажа", "Выберите пол вашего персонажа", "GenderList", genderlist, 0);
                menuReg.Add(menuItem);

                MenuItem menuItem2 = new MenuItem("Ввести пароль", "Введите ваш будущий пароль", "password");
                menuItem2.SetInput("", 10, InputType.Text);
                menuReg.Add(menuItem2);

                menuReg.Add(new MenuItem("~g~Готово", "Завершить регистрацию", "close")
                {
                    ExecuteCallback = true
                });
            }
            MenuManager.OpenMenu(client, menuReg);
        }

        private static void LoginMenu(Client client)
        {
            Menu menuLog = savedMenu;

            if (menuLog == null)
            {
                menuLog = new Menu("Login", "Авторизация", "Вход на сервер", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(200, 0, 0, 90),
                    Callback = menuLogManager
                };

                MenuItem menuItem = new MenuItem("Ввести пароль", "Введите ваш пароль", "password");
                menuItem.SetInput("", 20, InputType.Text);
                menuLog.Add(menuItem);

                menuLog.Add(new MenuItem("~g~Войти", "Войти на сервер", "close")
                {
                    ExecuteCallback = true
                });
            }
            MenuManager.OpenMenu(client, menuLog);
        }

        private static void menuRegManager(Client client, Menu menuATM, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Client player = API.shared.getPlayerFromHandle(client);

            if (itemIndex == 2)
            {
                string password = (string)data["password"];
                int genderIndex = data["GenderList"]["Index"];
                if (data["password"] == null)
                {
                    API.shared.sendNotificationToPlayer(player, "~r~Вы не ввели пароль!");
                    return;
                }
                if (password.Length <= 3)
                {
                    Player.Register(player);
                    Database.Debug(1, "Неудачная попытка регистрации [" + player.name + "]" + " [" + player.address + "] " + "| слишком короткий пароль | " + password);
                    player.sendNotification("Сервер/n~r~Пароль должен содержать не менее 3-ти символов!");
                    return;
                }
                if (data["password"] != null)
                    password = (string)data["password"];
                if (genderIndex == 0)
                {
                    Database.Register_Account(player, Player.sha256(password));
                }
                if (genderIndex == 1)
                {
                    Database.Register_Account(player, Player.sha256(password));
                }
                customskin_main.SendToCreator(player);
                Database.Debug(0, "Регистрация аккаунта [" + player.name + "]" + " [" + player.address + "] " + "прошла успешно | " + password);
                MenuManager.CloseMenu(client);
            }
        }

        private static void menuLogManager(Client client, Menu menuATM, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Client player = API.shared.getPlayerFromHandle(client);

            if (itemIndex == 1)
            {
                string password = (string)data["password"];
                if (data["password"] == null)
                {
                    API.shared.sendNotificationToPlayer(player, "~r~Вы не ввели пароль!");
                    return;
                }
                if (data["password"] != null)
                    password = (string)data["password"];
                player.setData("InGame", 1);
                Database.Login_Account(player, Player.sha256(password));
                MenuManager.CloseMenu(client);
                LoadPlayerClothes(player);
            }
        }

        public static void Register(Client player)
        {
            RegisterMenu(player);
        }

        public static void Login(Client player)
        {
            LoginMenu(player);
            //Client player = API.shared.getPlayerFromHandle(client);
            //MySQL.Database.Login_Account(player, "qwerty");
            //LoadPlayerClothes(player);
        }

        public static string sha256(string randomString)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString), 0, Encoding.UTF8.GetByteCount(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static int GetMoney(Client player)
        {
            return (player.hasData("cash_amount")) ? player.getData("cash_amount") : 0;
        }

        public static void ChangeMoney(Client player, int amount)
        {
            if (!player.hasData("cash_amount")) return;
            player.setData("cash_amount", Clamp(player.getData("cash_amount") + amount, arcadia.MoneyCap * -1, arcadia.MoneyCap));
            API.shared.triggerClientEvent(player, "UpdateMoneyHUD", Convert.ToString(player.getData("cash_amount")), Convert.ToString(amount));
        }

        public static void SetMoney(Client player, int amount)
        {
            if (!player.hasData("cash_amount")) return;
            player.setData("cash_amount", Clamp(amount, arcadia.MoneyCap * -1, arcadia.MoneyCap));
            API.shared.triggerClientEvent(player, "UpdateMoneyHUD", Convert.ToString(player.getData("cash_amount")), Convert.ToString(amount));
        }

        internal static int getPlayerHealth(Client player)
        {
            throw new NotImplementedException();
        }

        public static void ChangeBankMoney(Client player, int amount)
        {
            if (!player.hasData("bank_amount")) return;
            player.setData("bank_amount", Clamp(player.getData("bank_amount") + amount, arcadia.BankMoneyCap * -1, arcadia.BankMoneyCap));
        }

        public static void SetBankMoney(Client player, int amount)
        {
            if (!player.hasData("bank_amount")) return;
            player.setData("bank_amount", Clamp(amount, arcadia.BankMoneyCap * -1, arcadia.BankMoneyCap));
        }

        public static int GetBankMoney(Client player)
        {
            return (player.hasData("bank_amount")) ? player.getData("bank_amount") : 0;
        }

        public static int GetRightsLevel(Client player)
        {
            return (player.hasData("rights_level")) ? player.getData("rights_level") : 0;
        }

        public static void SetRightsLevel(Client player, int adminlvl)
        {
            if (!player.hasData("rights_level")) return;
            player.setData("rights_level", Clamp(adminlvl, 1, 9));
        }
        // Операции с фракциями, ранги/фракции
        public static void SetFactionID(Client player, int factionid)
        {
            if (!player.hasData("fraction_id")) return;
            player.setData("fraction_id", Clamp(factionid, 0, 10));
        }

        public static void SetFactionRank(Client player, int factionrank)
        {
            if (!player.hasData("fractionrank_id")) return;
            player.setData("fractionrank_id", Clamp(factionrank, 1, 10));
        }

        // управление работами

        public static void SetJobID(Client player, int jobid)
        {
            if (!player.hasData("job_id")) return;
            player.setData("job_id", Clamp(jobid, 0, 6));
        }

        public static void SetJobRank(Client player, int jobrank)
        {
            if (!player.hasData("jobrank_id")) return;
            player.setData("jobrank_id", Clamp(jobrank, 1, 2));
        }

        // управление Бандами

        public static void SetGangID(Client player, int gangid)
        {
            if (!player.hasData("gang_id")) return;
            player.setData("gang_id", Clamp(gangid, 0, 10));
        }

        public static void SetGangRank(Client player, int gangrank)
        {
            if (!player.hasData("gangrank_id")) return;
            player.setData("gangrank_id", Clamp(gangrank, 1, 10));
        }

        // чтение данных фракций

        public static int GetFractionId(Client player)
        {
            return (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
        }

        public static int GetFractionRank(Client player)
        {
            return (player.hasData("fractionrank_id")) ? player.getData("fractionrank_id") : 0;
        }

        // чтение данных работ

        public static int GetJobId(Client player)
        {
            return (player.hasData("job_id")) ? player.getData("job_id") : 0;
        }

        public static int GetJobRank(Client player)
        {
            return (player.hasData("jobrank_id")) ? player.getData("jobrank_id") : 0;
        }

        // чтение данных банд

        public static int GetGangId(Client player)
        {
            return (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
        }

        public static int GetGangRank(Client player)
        {
            return (player.hasData("gangrank_id")) ? player.getData("gang_id") : 0;
        }

        // Статус выхода на дежурство во фракции
        public static void SetPlayerOnDuty(Client player, int onduty = 1)
        {
            if (!player.hasData("onduty_status")) return;
            player.setData("onduty_status", Clamp(onduty, 0, 1));
        }

        public static void RemovePlayerOnDuty(Client player, int onduty = 0)
        {
            if (!player.hasData("onduty_status")) return;
            player.setData("onduty_status", Clamp(onduty, 0, 1));
        }

        public static int IsPlayerOnDuty(Client player)
        {
            return (player.hasData("onduty_status")) ? player.getData("onduty_status") : 0;
        }

        public static int GetPlayerGender(Client player)
        {
            return (player.hasData("gender_id")) ? player.getData("gender_id") : 0;
        }

        public static void SetPlayerGender(Client player, int gender)
        {
            if (!player.hasData("gender_id")) return;
            player.setData("gender_id", Clamp(gender, 0, 1));
        }

        public static void LoadPlayerClothes(Client player)
        {
            //API.shared.setPlayerClothes(player, 1, player.hasData("mask_id") ? player.getData("mask_id") : 0, 0);
            API.shared.setPlayerClothes(player, 3, player.hasData("torso_id") ? player.getData("torso_id") : 0, 0);
            API.shared.setPlayerClothes(player, 4, player.hasData("legs_id") ? player.getData("legs_id") : 0, player.hasData("legstexture_id") ? player.getData("legstexture_id") : 0);
            API.shared.setPlayerClothes(player, 5, player.hasData("bag_id") ? player.getData("bag_id") : 0, 0);
            API.shared.setPlayerClothes(player, 6, player.hasData("feet_id") ? player.getData("feet_id") : 0, player.hasData("feettexture_id") ? player.getData("feettexture_id") : 0);
            API.shared.setPlayerClothes(player, 7, player.hasData("accessorie_id") ? player.getData("accessorie_id") : 0, player.hasData("accessorietexture_id") ? player.getData("accessorietexture_id") : 0);
            API.shared.setPlayerClothes(player, 8, player.hasData("undershirt_id") ? player.getData("undershirt_id") : 0, player.hasData("undershirttexture_id") ? player.getData("undershirttexture_id") : 0);
            API.shared.setPlayerClothes(player, 10, player.hasData("decal_id") ? player.getData("decal_id") : 0, 0);
            API.shared.setPlayerClothes(player, 11, player.hasData("top_id") ? player.getData("top_id") : 0, player.hasData("toptexture_id") ? player.getData("toptexture_id") : 0);

            //API.shared.setPlayerAccessory(player, 0, player.hasData("hat_id") ? player.getData("hat_id") : 0, player.hasData("hattexture_id") ? player.getData("hattexture_id") : 0);
            //API.shared.setPlayerAccessory(player, 1, player.hasData("glasses_id") ? player.getData("glasses_id") : 0, player.hasData("glassestexture_id") ? player.getData("glassestexture_id") : 0);
            //API.shared.setPlayerAccessory(player, 2, player.hasData("ears_id") ? player.getData("ears_id") : 0, 0);
            //API.shared.setPlayerAccessory(player, 6, player.hasData("watches_id") ? player.getData("watches_id") : 0, 0);
            //API.shared.setPlayerAccessory(player, 7, player.hasData("braceletes_id") ? player.getData("braceletes_id") : 0, 0);
            //return (player.hasData("mask_id")) ? player.getData("mask_id") : 0;
        }

        public static void UpdatePlayerClothes(Client player)
        {
            //player.setData("mask_id", API.shared.getPlayerClothesDrawable(player, 1));
            player.setData("torso_id", API.shared.getPlayerClothesDrawable(player, 3));
            player.setData("legs_id", API.shared.getPlayerClothesDrawable(player, 4));
            player.setData("legstexture_id", API.shared.getPlayerClothesTexture(player, 4));
            player.setData("bag_id", API.shared.getPlayerClothesDrawable(player, 5));
            player.setData("feet_id", API.shared.getPlayerClothesDrawable(player, 6));
            player.setData("feettexture_id", API.shared.getPlayerClothesTexture(player, 6));
            player.setData("accessorie_id", API.shared.getPlayerClothesDrawable(player, 7));
            player.setData("accessorietexture_id", API.shared.getPlayerClothesTexture(player, 7));
            player.setData("undershirt_id", API.shared.getPlayerClothesDrawable(player, 8));
            player.setData("undershirttexture_id", API.shared.getPlayerClothesTexture(player, 8));
            player.setData("decal_id", API.shared.getPlayerClothesDrawable(player, 10));
            player.setData("top_id", API.shared.getPlayerClothesDrawable(player, 11));
            player.setData("toptexture_id", API.shared.getPlayerClothesTexture(player, 11));
            //player.setData("hat_id", API.shared.getPlayerAccessoryDrawable(player, 0));
            //player.setData("hattexture_id", API.shared.getPlayerAccessoryTexture(player, 0));
            // player.setData("glasses_id", API.shared.getPlayerAccessoryDrawable(player, 1));
            // player.setData("glassestexture_id", API.shared.getPlayerAccessoryTexture(player, 1));
            // player.setData("ears_id", API.shared.getPlayerAccessoryDrawable(player, 2));
            //player.setData("watches_id", API.shared.getPlayerAccessoryDrawable(player, 6));
            //player.setData("braceletes_id", API.shared.getPlayerAccessoryDrawable(player, 7));
        }

        public static void Spawn_Player(Client player)
        {
            API.shared.setEntityTransparency(player, 255);
        }
    }
}
