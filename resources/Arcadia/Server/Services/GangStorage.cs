using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CherryMPServer;
using CherryMPShared;
using CherryMPServer.Constant;
using MySQL;
using PlayerFunctions;

using Arcadia.Server.Models;
using Newtonsoft.Json;
using MenuManagement;
using MySql.Data.MySqlClient;

namespace Arcadia.Server.Services
{
    public class GangStorageService : Script
    {

        public GangStorageService()
        {
            API.onResourceStart += OnResourceStart;
            API.onEntityEnterColShape += ColShapeTrigger;
        }

        public void OnResourceStart()
        {
            LoadAllStoragesFromDB();
        }

        public static string myConnectionString = "SERVER=localhost;" + "DATABASE=arcadia;" + "UID=root;" + "PASSWORD=qcoluk1ps3;";

        private MenuManager MenuManager = new MenuManager();
        private Menu savedMenu = null;
        #region FamiliesStorageMenus

        private void StorageFamiliesMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Narko = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM gangfaction_storages WHERE NameFaction = 'Families'";

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    Pistols += Database.Reader.GetInt32("Pistols");
                    Rifle += Database.Reader.GetInt32("Rifle");
                    SMG += Database.Reader.GetInt32("SMG");
                    Shotgun += Database.Reader.GetInt32("Shotgun");
                    Melee += Database.Reader.GetInt32("Melee");
                    Narko += Database.Reader.GetInt32("Narko");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "Families";

                menu = new Menu("FamiliesStorage", "Склад Families", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false);
                menu.BannerColor = new Color(99, 7, 99, 1);
                menu.Callback = StorageFamilies_MenuManager;

                MenuItem get_pistol_menu_item = new MenuItem("Взять пистолет", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item");
                get_pistol_menu_item.ExecuteCallback = true;
                menu.Add(get_pistol_menu_item);

                MenuItem get_rifle_menu_item = new MenuItem("Взять винтовку", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item");
                get_rifle_menu_item.ExecuteCallback = true;
                menu.Add(get_rifle_menu_item);

                MenuItem get_smg_menu_item = new MenuItem("Взять пистолет-пулемёт", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item");
                get_smg_menu_item.ExecuteCallback = true;
                menu.Add(get_smg_menu_item);

                MenuItem get_shotgun_menu_item = new MenuItem("Взять дробовик", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item");
                get_shotgun_menu_item.ExecuteCallback = true;
                menu.Add(get_shotgun_menu_item);

                MenuItem get_tazer_menu_item = new MenuItem("Взять комплект ближнего боя", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item");
                get_tazer_menu_item.ExecuteCallback = true;
                menu.Add(get_tazer_menu_item);

                MenuItem get_Narko_menu_item = new MenuItem("Взять травку", "~b~На складе: ~w~" + Convert.ToString(Narko), "get_Narko_menu_item");
                get_Narko_menu_item.ExecuteCallback = true;
                menu.Add(get_Narko_menu_item);


                if (Locked == "0")
                {
                    State = "~g~Открыт";
                }
                else State = "~r~Закрыт";

                MenuItem Lock_Or_Unlock_Storage = new MenuItem("~y~Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage");
                Lock_Or_Unlock_Storage.ExecuteCallback = true;
                menu.Add(Lock_Or_Unlock_Storage);

                MenuItem CloseMenuItem = new MenuItem("~r~Закрыть", "Закрыть меню", "Close");
                CloseMenuItem.ExecuteCallback = true;
                menu.Add(CloseMenuItem);
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StorageFamilies_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM gangfaction_storages WHERE NameFaction = 'Families'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Narko = 0;
            string Locked = "";
            int Lockstate;

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    Pistols += Database.Reader.GetInt32("Pistols");
                    Rifle += Database.Reader.GetInt32("Rifle");
                    SMG += Database.Reader.GetInt32("SMG");
                    Shotgun += Database.Reader.GetInt32("Shotgun");
                    Melee += Database.Reader.GetInt32("Melee");
                    Narko += Database.Reader.GetInt32("Narko");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            string groupName = "FS";
            bool alreadyhavepistol = false;
            bool alreadyhaverifle = false;
            bool alreadyhavesmg = false;
            bool alreadyhaveshotgun = false;
            bool alreadyhavemelee = false;
            bool alreadyhavenarko = false;

            if (itemIndex == 0) // пистолеты
            {
                client.setData("HAVE_PISTOL", false);
                if (Locked == "0")
                {
                    if (client.getData("HAVE_PISTOL") == false)
                    {
                        if (Pistols != 0)
                        {
                            var chatMessagePistol = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада пистолет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?pistols", Pistols - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Pistols = ?pistols WHERE NameFaction = 'Families'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли пистолет со склада");
                            API.givePlayerWeapon(client, WeaponHash.APPistol, 72, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessagePistol);
                                }
                            }
                            alreadyhavepistol = true;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились пистолеты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли пистолет!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
                client.setData("HAVE_PISTOL", true);
            }
            if (itemIndex == 1) // винтовки
            {
                if (Locked == "0")
                {
                    if (alreadyhaverifle == false)
                    {
                        if (Rifle != 0)
                        {
                            var chatMessageRifle = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада винтовку";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?rifle", Rifle - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Rifle = ?rifle WHERE NameFaction = 'Families'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли винтовку со склада");
                            API.givePlayerWeapon(client, WeaponHash.BullpupRifle, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageRifle);
                                }
                            }
                            alreadyhaverifle = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились винтовки!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли винтовку!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 2) // SMG
            {
                if (Locked == "0")
                {
                    if (alreadyhavesmg == false)
                    {
                        if (SMG != 0)
                        {
                            var chatMessageSMG = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада SMG";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?smg", SMG - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET SMG = ?smg WHERE NameFaction = 'Families'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада SMG");
                            API.givePlayerWeapon(client, WeaponHash.AssaultSMG, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageSMG);
                                }
                            }
                            alreadyhavesmg = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились SMG!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли SMG!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 3) // Дробовики
            {
                if (Locked == "0")
                {
                    if (alreadyhaveshotgun == false)
                    {
                        if (Shotgun != 0)
                        {
                            var chatMessageShotgun = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада дробовик";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'Families'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли дробовик со склада");
                            API.givePlayerWeapon(client, WeaponHash.HeavyShotgun, 40, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageShotgun);
                                }
                            }
                            alreadyhaveshotgun = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились дробовики!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли дробовик!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 4) // комплекты
            {
                if (Locked == "0")
                {
                    if (alreadyhavemelee == false)
                    {
                        if (Melee != 0)
                        {
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада комплект";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?melee", Melee - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Melee = ?melee WHERE NameFaction = 'Families'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                            API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Flashlight, 1, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageMelee);
                                }
                            }
                            alreadyhavemelee = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились комплекты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли комплект!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 5) // Наркотики
            {
                if (Locked == "0")
                {
                    if (alreadyhavenarko == false)
                    {
                        if (Narko != 0)
                        {
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада наркотики";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?narko", Melee - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Melee = ?melee WHERE NameFaction = 'Families'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Временно не доступно!");

                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageMelee);
                                }
                            }
                            alreadyhavemelee = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились наркотики!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли комплект!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }

            if (itemIndex == 6)
            {
                if (Player.GetGangRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~r~ закрыл склад банды";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.CommandText = "UPDATE gangfaction_storages SET Locked = '1' WHERE NameFaction = 'Families'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад банды!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetGangId(player) == 1)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StorageFamiliesMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageOpenStorage = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~g~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.CommandText = "UPDATE gangfaction_storages SET Locked = '0' WHERE NameFaction = 'Families'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetGangId(player) == 1)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageOpenStorage);
                            }
                        }
                        StorageFamiliesMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 7)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }
        #endregion
        #region VagosStorageMenus

        private void StorageVagosMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Narko = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM gangfaction_storages WHERE NameFaction = 'Vagos'";

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    Pistols += Database.Reader.GetInt32("Pistols");
                    Rifle += Database.Reader.GetInt32("Rifle");
                    SMG += Database.Reader.GetInt32("SMG");
                    Shotgun += Database.Reader.GetInt32("Shotgun");
                    Melee += Database.Reader.GetInt32("Melee");
                    Narko += Database.Reader.GetInt32("Narko");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "Vagos";

                menu = new Menu("VagosStorage", "Склад Vagos", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false);
                menu.BannerColor = new Color(99, 7, 99, 1);
                menu.Callback = StorageVagos_MenuManager;

                MenuItem get_pistol_menu_item = new MenuItem("Взять пистолет", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item");
                get_pistol_menu_item.ExecuteCallback = true;
                menu.Add(get_pistol_menu_item);

                MenuItem get_rifle_menu_item = new MenuItem("Взять винтовку", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item");
                get_rifle_menu_item.ExecuteCallback = true;
                menu.Add(get_rifle_menu_item);

                MenuItem get_smg_menu_item = new MenuItem("Взять пистолет-пулемёт", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item");
                get_smg_menu_item.ExecuteCallback = true;
                menu.Add(get_smg_menu_item);

                MenuItem get_shotgun_menu_item = new MenuItem("Взять дробовик", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item");
                get_shotgun_menu_item.ExecuteCallback = true;
                menu.Add(get_shotgun_menu_item);

                MenuItem get_tazer_menu_item = new MenuItem("Взять комплект ближнего боя", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item");
                get_tazer_menu_item.ExecuteCallback = true;
                menu.Add(get_tazer_menu_item);

                MenuItem get_Narko_menu_item = new MenuItem("Взять травку", "~b~На складе: ~w~" + Convert.ToString(Narko), "get_Narko_menu_item");
                get_Narko_menu_item.ExecuteCallback = true;
                menu.Add(get_Narko_menu_item);


                if (Locked == "0")
                {
                    State = "~g~Открыт";
                }
                else State = "~r~Закрыт";

                MenuItem Lock_Or_Unlock_Storage = new MenuItem("~y~Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage");
                Lock_Or_Unlock_Storage.ExecuteCallback = true;
                menu.Add(Lock_Or_Unlock_Storage);

                MenuItem CloseMenuItem = new MenuItem("~r~Закрыть", "Закрыть меню", "Close");
                CloseMenuItem.ExecuteCallback = true;
                menu.Add(CloseMenuItem);
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StorageVagos_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM gangfaction_storages WHERE NameFaction = 'Vagos'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Narko = 0;
            string Locked = "";
            int Lockstate;

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    Pistols += Database.Reader.GetInt32("Pistols");
                    Rifle += Database.Reader.GetInt32("Rifle");
                    SMG += Database.Reader.GetInt32("SMG");
                    Shotgun += Database.Reader.GetInt32("Shotgun");
                    Melee += Database.Reader.GetInt32("Melee");
                    Narko += Database.Reader.GetInt32("Narko");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            string groupName = "FS";
            bool alreadyhavepistol = false;
            bool alreadyhaverifle = false;
            bool alreadyhavesmg = false;
            bool alreadyhaveshotgun = false;
            bool alreadyhavemelee = false;
            bool alreadyhavenarko = false;

            if (itemIndex == 0) // пистолеты
            {
                client.setData("HAVE_PISTOL", false);
                if (Locked == "0")
                {
                    if (client.getData("HAVE_PISTOL") == false)
                    {
                        if (Pistols != 0)
                        {
                            var chatMessagePistol = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада пистолет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?pistols", Pistols - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Pistols = ?pistols WHERE NameFaction = 'Vagos'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли пистолет со склада");
                            API.givePlayerWeapon(client, WeaponHash.APPistol, 72, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 2)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessagePistol);
                                }
                            }
                            alreadyhavepistol = true;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились пистолеты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли пистолет!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
                client.setData("HAVE_PISTOL", true);
            }
            if (itemIndex == 1) // винтовки
            {
                if (Locked == "0")
                {
                    if (alreadyhaverifle == false)
                    {
                        if (Rifle != 0)
                        {
                            var chatMessageRifle = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада винтовку";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?rifle", Rifle - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Rifle = ?rifle WHERE NameFaction = 'Vagos'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли винтовку со склада");
                            API.givePlayerWeapon(client, WeaponHash.BullpupRifle, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 2)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageRifle);
                                }
                            }
                            alreadyhaverifle = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились винтовки!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли винтовку!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 2) // SMG
            {
                if (Locked == "0")
                {
                    if (alreadyhavesmg == false)
                    {
                        if (SMG != 0)
                        {
                            var chatMessageSMG = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада SMG";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?smg", SMG - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET SMG = ?smg WHERE NameFaction = 'Vagos'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада SMG");
                            API.givePlayerWeapon(client, WeaponHash.AssaultSMG, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 2)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageSMG);
                                }
                            }
                            alreadyhavesmg = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились SMG!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли SMG!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 3) // Дробовики
            {
                if (Locked == "0")
                {
                    if (alreadyhaveshotgun == false)
                    {
                        if (Shotgun != 0)
                        {
                            var chatMessageShotgun = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада дробовик";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'Vagos'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли дробовик со склада");
                            API.givePlayerWeapon(client, WeaponHash.HeavyShotgun, 40, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 2)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageShotgun);
                                }
                            }
                            alreadyhaveshotgun = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились дробовики!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли дробовик!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 4) // комплекты
            {
                if (Locked == "0")
                {
                    if (alreadyhavemelee == false)
                    {
                        if (Melee != 0)
                        {
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада комплект";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?melee", Melee - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Melee = ?melee WHERE NameFaction = 'Vagos'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                            API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Flashlight, 1, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 2)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageMelee);
                                }
                            }
                            alreadyhavemelee = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились комплекты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли комплект!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 5) // Наркотики
            {
                if (Locked == "0")
                {
                    if (alreadyhavenarko == false)
                    {
                        if (Narko != 0)
                        {
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада наркотики";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?narko", Melee - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Melee = ?melee WHERE NameFaction = 'Vagos'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Временно не доступно!");

                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 2)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageMelee);
                                }
                            }
                            alreadyhavemelee = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились наркотики!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли комплект!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }

            if (itemIndex == 6)
            {
                if (Player.GetGangRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~r~ закрыл склад банды";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.CommandText = "UPDATE gangfaction_storages SET Locked = '1' WHERE NameFaction = 'Vagos'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад банды!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetGangId(player) == 2)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StorageVagosMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageOpenStorage = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~g~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.CommandText = "UPDATE gangfaction_storages SET Locked = '0' WHERE NameFaction = 'Vagos'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetGangId(player) == 2)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageOpenStorage);
                            }
                        }
                        StorageVagosMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 7)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }
        #endregion
        #region BallasStorageMenus

        private void StorageBallasMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Narko = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM gangfaction_storages WHERE NameFaction = 'Ballas'";

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    Pistols += Database.Reader.GetInt32("Pistols");
                    Rifle += Database.Reader.GetInt32("Rifle");
                    SMG += Database.Reader.GetInt32("SMG");
                    Shotgun += Database.Reader.GetInt32("Shotgun");
                    Melee += Database.Reader.GetInt32("Melee");
                    Narko += Database.Reader.GetInt32("Narko");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "Ballas";

                menu = new Menu("BallasStorage", "Склад Ballas", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false);
                menu.BannerColor = new Color(99, 7, 99, 1);
                menu.Callback = StorageBallas_MenuManager;

                MenuItem get_pistol_menu_item = new MenuItem("Взять пистолет", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item");
                get_pistol_menu_item.ExecuteCallback = true;
                menu.Add(get_pistol_menu_item);

                MenuItem get_rifle_menu_item = new MenuItem("Взять винтовку", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item");
                get_rifle_menu_item.ExecuteCallback = true;
                menu.Add(get_rifle_menu_item);

                MenuItem get_smg_menu_item = new MenuItem("Взять пистолет-пулемёт", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item");
                get_smg_menu_item.ExecuteCallback = true;
                menu.Add(get_smg_menu_item);

                MenuItem get_shotgun_menu_item = new MenuItem("Взять дробовик", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item");
                get_shotgun_menu_item.ExecuteCallback = true;
                menu.Add(get_shotgun_menu_item);

                MenuItem get_tazer_menu_item = new MenuItem("Взять комплект ближнего боя", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item");
                get_tazer_menu_item.ExecuteCallback = true;
                menu.Add(get_tazer_menu_item);

                MenuItem get_Narko_menu_item = new MenuItem("Взять травку", "~b~На складе: ~w~" + Convert.ToString(Narko), "get_Narko_menu_item");
                get_Narko_menu_item.ExecuteCallback = true;
                menu.Add(get_Narko_menu_item);


                if (Locked == "0")
                {
                    State = "~g~Открыт";
                }
                else State = "~r~Закрыт";

                MenuItem Lock_Or_Unlock_Storage = new MenuItem("~y~Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage");
                Lock_Or_Unlock_Storage.ExecuteCallback = true;
                menu.Add(Lock_Or_Unlock_Storage);

                MenuItem CloseMenuItem = new MenuItem("~r~Закрыть", "Закрыть меню", "Close");
                CloseMenuItem.ExecuteCallback = true;
                menu.Add(CloseMenuItem);
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StorageBallas_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM gangfaction_storages WHERE NameFaction = 'Ballas'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Narko = 0;
            string Locked = "";
            int Lockstate;

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    Pistols += Database.Reader.GetInt32("Pistols");
                    Rifle += Database.Reader.GetInt32("Rifle");
                    SMG += Database.Reader.GetInt32("SMG");
                    Shotgun += Database.Reader.GetInt32("Shotgun");
                    Melee += Database.Reader.GetInt32("Melee");
                    Narko += Database.Reader.GetInt32("Narko");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            string groupName = "FS";
            bool alreadyhavepistol = false;
            bool alreadyhaverifle = false;
            bool alreadyhavesmg = false;
            bool alreadyhaveshotgun = false;
            bool alreadyhavemelee = false;
            bool alreadyhavenarko = false;

            if (itemIndex == 0) // пистолеты
            {
                client.setData("HAVE_PISTOL", false);
                if (Locked == "0")
                {
                    if (client.getData("HAVE_PISTOL") == false)
                    {
                        if (Pistols != 0)
                        {
                            var chatMessagePistol = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада пистолет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?pistols", Pistols - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Pistols = ?pistols WHERE NameFaction = 'Ballas'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли пистолет со склада");
                            API.givePlayerWeapon(client, WeaponHash.APPistol, 72, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 3)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessagePistol);
                                }
                            }
                            alreadyhavepistol = true;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились пистолеты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли пистолет!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
                client.setData("HAVE_PISTOL", true);
            }
            if (itemIndex == 1) // винтовки
            {
                if (Locked == "0")
                {
                    if (alreadyhaverifle == false)
                    {
                        if (Rifle != 0)
                        {
                            var chatMessageRifle = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада винтовку";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?rifle", Rifle - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Rifle = ?rifle WHERE NameFaction = 'Ballas'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли винтовку со склада");
                            API.givePlayerWeapon(client, WeaponHash.BullpupRifle, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 3)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageRifle);
                                }
                            }
                            alreadyhaverifle = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились винтовки!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли винтовку!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 2) // SMG
            {
                if (Locked == "0")
                {
                    if (alreadyhavesmg == false)
                    {
                        if (SMG != 0)
                        {
                            var chatMessageSMG = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада SMG";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?smg", SMG - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET SMG = ?smg WHERE NameFaction = 'Ballas'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада SMG");
                            API.givePlayerWeapon(client, WeaponHash.AssaultSMG, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 3)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageSMG);
                                }
                            }
                            alreadyhavesmg = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились SMG!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли SMG!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 3) // Дробовики
            {
                if (Locked == "0")
                {
                    if (alreadyhaveshotgun == false)
                    {
                        if (Shotgun != 0)
                        {
                            var chatMessageShotgun = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада дробовик";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'Ballas'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли дробовик со склада");
                            API.givePlayerWeapon(client, WeaponHash.HeavyShotgun, 40, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 3)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageShotgun);
                                }
                            }
                            alreadyhaveshotgun = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились дробовики!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли дробовик!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 4) // комплекты
            {
                if (Locked == "0")
                {
                    if (alreadyhavemelee == false)
                    {
                        if (Melee != 0)
                        {
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада комплект";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?melee", Melee - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Melee = ?melee WHERE NameFaction = 'Ballas'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                            API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Flashlight, 1, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 3)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageMelee);
                                }
                            }
                            alreadyhavemelee = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились комплекты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли комплект!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 5) // Наркотики
            {
                if (Locked == "0")
                {
                    if (alreadyhavenarko == false)
                    {
                        if (Narko != 0)
                        {
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада наркотики";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?narko", Melee - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Melee = ?melee WHERE NameFaction = 'Ballas'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Временно не доступно!");

                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 3)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageMelee);
                                }
                            }
                            alreadyhavemelee = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились наркотики!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли комплект!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }

            if (itemIndex == 6)
            {
                if (Player.GetGangRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~r~ закрыл склад банды";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.CommandText = "UPDATE gangfaction_storages SET Locked = '1' WHERE NameFaction = 'Ballas'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад банды!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetGangId(player) == 3)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StorageBallasMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageOpenStorage = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~g~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.CommandText = "UPDATE gangfaction_storages SET Locked = '0' WHERE NameFaction = 'Ballas'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetGangId(player) == 3)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageOpenStorage);
                            }
                        }
                        StorageBallasMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 7)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }
        #endregion
        #region TriadsStorageMenus

        private void StorageTriadsMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Narko = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM gangfaction_storages WHERE NameFaction = 'Triads'";

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    Pistols += Database.Reader.GetInt32("Pistols");
                    Rifle += Database.Reader.GetInt32("Rifle");
                    SMG += Database.Reader.GetInt32("SMG");
                    Shotgun += Database.Reader.GetInt32("Shotgun");
                    Melee += Database.Reader.GetInt32("Melee");
                    Narko += Database.Reader.GetInt32("Narko");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "Triads";

                menu = new Menu("TriadsStorage", "Склад Triads", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false);
                menu.BannerColor = new Color(99, 7, 99, 1);
                menu.Callback = StorageTriads_MenuManager;

                MenuItem get_pistol_menu_item = new MenuItem("Взять пистолет", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item");
                get_pistol_menu_item.ExecuteCallback = true;
                menu.Add(get_pistol_menu_item);

                MenuItem get_rifle_menu_item = new MenuItem("Взять винтовку", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item");
                get_rifle_menu_item.ExecuteCallback = true;
                menu.Add(get_rifle_menu_item);

                MenuItem get_smg_menu_item = new MenuItem("Взять пистолет-пулемёт", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item");
                get_smg_menu_item.ExecuteCallback = true;
                menu.Add(get_smg_menu_item);

                MenuItem get_shotgun_menu_item = new MenuItem("Взять дробовик", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item");
                get_shotgun_menu_item.ExecuteCallback = true;
                menu.Add(get_shotgun_menu_item);

                MenuItem get_tazer_menu_item = new MenuItem("Взять комплект ближнего боя", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item");
                get_tazer_menu_item.ExecuteCallback = true;
                menu.Add(get_tazer_menu_item);

                MenuItem get_Narko_menu_item = new MenuItem("Взять травку", "~b~На складе: ~w~" + Convert.ToString(Narko), "get_Narko_menu_item");
                get_Narko_menu_item.ExecuteCallback = true;
                menu.Add(get_Narko_menu_item);


                if (Locked == "0")
                {
                    State = "~g~Открыт";
                }
                else State = "~r~Закрыт";

                MenuItem Lock_Or_Unlock_Storage = new MenuItem("~y~Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage");
                Lock_Or_Unlock_Storage.ExecuteCallback = true;
                menu.Add(Lock_Or_Unlock_Storage);

                MenuItem CloseMenuItem = new MenuItem("~r~Закрыть", "Закрыть меню", "Close");
                CloseMenuItem.ExecuteCallback = true;
                menu.Add(CloseMenuItem);
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StorageTriads_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM gangfaction_storages WHERE NameFaction = 'Triads'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Narko = 0;
            string Locked = "";
            int Lockstate;

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    Pistols += Database.Reader.GetInt32("Pistols");
                    Rifle += Database.Reader.GetInt32("Rifle");
                    SMG += Database.Reader.GetInt32("SMG");
                    Shotgun += Database.Reader.GetInt32("Shotgun");
                    Melee += Database.Reader.GetInt32("Melee");
                    Narko += Database.Reader.GetInt32("Narko");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            string groupName = "FS";
            bool alreadyhavepistol = false;
            bool alreadyhaverifle = false;
            bool alreadyhavesmg = false;
            bool alreadyhaveshotgun = false;
            bool alreadyhavemelee = false;
            bool alreadyhavenarko = false;

            if (itemIndex == 0) // пистолеты
            {
                client.setData("HAVE_PISTOL", false);
                if (Locked == "0")
                {
                    if (client.getData("HAVE_PISTOL") == false)
                    {
                        if (Pistols != 0)
                        {
                            var chatMessagePistol = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада пистолет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?pistols", Pistols - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Pistols = ?pistols WHERE NameFaction = 'Triads'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли пистолет со склада");
                            API.givePlayerWeapon(client, WeaponHash.APPistol, 72, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 5)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessagePistol);
                                }
                            }
                            alreadyhavepistol = true;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились пистолеты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли пистолет!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
                client.setData("HAVE_PISTOL", true);
            }
            if (itemIndex == 1) // винтовки
            {
                if (Locked == "0")
                {
                    if (alreadyhaverifle == false)
                    {
                        if (Rifle != 0)
                        {
                            var chatMessageRifle = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада винтовку";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?rifle", Rifle - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Rifle = ?rifle WHERE NameFaction = 'Triads'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли винтовку со склада");
                            API.givePlayerWeapon(client, WeaponHash.BullpupRifle, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageRifle);
                                }
                            }
                            alreadyhaverifle = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились винтовки!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли винтовку!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 2) // SMG
            {
                if (Locked == "0")
                {
                    if (alreadyhavesmg == false)
                    {
                        if (SMG != 0)
                        {
                            var chatMessageSMG = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада SMG";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?smg", SMG - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET SMG = ?smg WHERE NameFaction = 'Triads'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада SMG");
                            API.givePlayerWeapon(client, WeaponHash.AssaultSMG, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 5)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageSMG);
                                }
                            }
                            alreadyhavesmg = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились SMG!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли SMG!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 3) // Дробовики
            {
                if (Locked == "0")
                {
                    if (alreadyhaveshotgun == false)
                    {
                        if (Shotgun != 0)
                        {
                            var chatMessageShotgun = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада дробовик";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'Triads'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли дробовик со склада");
                            API.givePlayerWeapon(client, WeaponHash.HeavyShotgun, 40, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 5)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageShotgun);
                                }
                            }
                            alreadyhaveshotgun = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились дробовики!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли дробовик!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 4) // комплекты
            {
                if (Locked == "0")
                {
                    if (alreadyhavemelee == false)
                    {
                        if (Melee != 0)
                        {
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада комплект";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?melee", Melee - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Melee = ?melee WHERE NameFaction = 'Triads'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                            API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Flashlight, 1, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 5)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageMelee);
                                }
                            }
                            alreadyhavemelee = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились комплекты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли комплект!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 5) // Наркотики
            {
                if (Locked == "0")
                {
                    if (alreadyhavenarko == false)
                    {
                        if (Narko != 0)
                        {
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~y~ взял со склада наркотики";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?narko", Melee - 1);
                            Database.command.CommandText = "UPDATE gangfaction_storages SET Melee = ?melee WHERE NameFaction = 'Triads'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Временно не доступно!");

                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetGangId(player) == 5)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageMelee);
                                }
                            }
                            alreadyhavemelee = true;
                            return;
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились наркотики!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли комплект!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }

            if (itemIndex == 6)
            {
                if (Player.GetGangRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~r~ закрыл склад банды";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.CommandText = "UPDATE gangfaction_storages SET Locked = '1' WHERE NameFaction = 'Triads'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад банды!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetGangId(player) == 5)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StorageTriadsMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageOpenStorage = "~b~[" + groupName + "] ~b~" + GangFactionModel.GetPlayerGangRank(client, Player.GetGangRank(client)) + " | " + client.name + "~g~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.CommandText = "UPDATE gangfaction_storages SET Locked = '0' WHERE NameFaction = 'Triads'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetGangId(player) == 5)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageOpenStorage);
                            }
                        }
                        StorageTriadsMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 7)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }
        #endregion

        private void ColShapeTrigger(ColShape colShape, NetHandle entity)
        {
            var player = API.getPlayerFromHandle(entity);

            if (player == null) return;

            int playerfactionid = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;
            if (colShape != null && colShape.getData("IS_GANGFACTION_STORAGE") == true)
            {
                if (playerfactionid != 0)
                {
                    if (colShape.getData("gang_id") == 1 && playerfactionid == 1)
                    {
                        StorageFamiliesMenuBuilder(player);
                    }

                    if (colShape.getData("gang_id") == 2 && playerfactionid == 2)
                    {
                        StorageVagosMenuBuilder(player);
                    }

                    if (colShape.getData("gang_id") == 3 && playerfactionid == 3)
                    {
                        StorageBallasMenuBuilder(player);
                    }

                    if (colShape.getData("gang_id") == 5 && playerfactionid == 5)
                    {
                        StorageTriadsMenuBuilder(player);
                    }
                }
                else API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции!");
            }
        }

        public static readonly List<Storage> StorageList = new List<Storage>();

        public static void LoadAllStoragesFromDB()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            DataTable result = Database.ExecutePreparedStatement("SELECT * FROM gangfaction_storages", parameters);

            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Storage GangFactionStorage = new Storage
                    {
                        Id = (int)row["ID"],
                        IdFraction = (int)row["FID"],
                        NameFaction = (string)row["NameFaction"],
                        Position = JsonConvert.DeserializeObject<Vector3>((string)row["Position"]),
                        Locked = Convert.ToBoolean((int)row["Locked"]),
                        Pistols = (int)row["Pistols"],
                        Rifle = (int)row["Rifle"],
                        SMG = (int)row["SMG"],
                        Shotgun = (int)row["Shotgun"],
                        Bullets = (int)row["Bullets"],
                        Melee = (int)row["Melee"],
                        Narko = (int)row["Narko"],
                    };

                    Marker storagemarker = API.shared.createMarker(1, GangFactionStorage.Position - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
                    API.shared.createTextLabel("Склад " + GangFactionStorage.NameFaction, GangFactionStorage.Position, 15f, 0.65f);

                    ColShape GangFactionStorageColshape = API.shared.createCylinderColShape(GangFactionStorage.Position, 0.50f, 1f);
                    GangFactionStorageColshape.setData("gang_id", GangFactionStorage.IdFraction);
                    GangFactionStorageColshape.setData("isLocked", GangFactionStorage.Locked);
                    GangFactionStorageColshape.setData("IS_GANGFACTION_STORAGE", true);
                    GangFactionStorage.ColShape = GangFactionStorageColshape;

                    StorageList.Add(GangFactionStorage);
                }
                API.shared.consoleOutput(LogCat.Info, result.Rows.Count + "Складов загружено!");
            }
            else
            {
                API.shared.consoleOutput(LogCat.Info, "Не удалось загрузить склады!");
            }
        }

        public static void AddNewGangStorage(int IdFraction, string NameFaction, bool isLocked, Vector3 position)
        {

            const int Pistols = 25;
            const int Rifle = 10;
            const int SMG = 15;
            const int Shotgun = 25;
            const int Melee = 25;
            const int Narko = 25;
            const int Bullets = 500;


            switch (NameFaction)
            {
                case "Families":

                    Dictionary<string, string> parametersFamilies = new Dictionary<string, string>
                    {
                        { "@FID", IdFraction.ToString() },
                        { "@NameFaction", NameFaction },
                        { "@Locked", Convert.ToInt32(isLocked).ToString() },
                        { "@Position", JsonConvert.SerializeObject(position) },
                        { "@Pistols",  Pistols.ToString() },
                        { "@Rifle",  Rifle.ToString() },
                        { "@SMG",  SMG.ToString() },
                        { "@Shotgun",  Shotgun.ToString() },
                        { "@Bullets",  Bullets.ToString() },
                        { "@Melee",  Melee.ToString() },
                        { "@Narko",  Narko.ToString() }

                    };

                    DataTable resultFamilies = Database.ExecutePreparedStatement("INSERT INTO gangfaction_storages (FID, NameFaction, Locked, Position, Pistols, Rifle, SMG, Shotgun, Bullets, Melee, Narko) " +
                        "VALUES (@FID, @NameFaction, @Locked, @Position, @Pistols, @Rifle, @SMG, @Shotgun, @Bullets, @Melee, @Narko)", parametersFamilies);

                    break;

                case "Vagos":

                    Dictionary<string, string> parametersVagos = new Dictionary<string, string>
                    {
                        { "@FID", IdFraction.ToString() },
                        { "@NameFaction", NameFaction },
                        { "@Locked", Convert.ToInt32(isLocked).ToString() },
                        { "@Position", JsonConvert.SerializeObject(position) },
                        { "@Pistols",  Pistols.ToString() },
                        { "@Rifle",  Rifle.ToString() },
                        { "@SMG",  SMG.ToString() },
                        { "@Shotgun",  Shotgun.ToString() },
                        { "@Bullets",  Bullets.ToString() },
                        { "@Melee",  Melee.ToString() },
                        { "@Narko",  Narko.ToString() }

                    };

                    DataTable resultVagos = Database.ExecutePreparedStatement("INSERT INTO gangfaction_storages (FID, NameFaction, Locked, Position, Pistols, Rifle, SMG, Shotgun, Bullets, Melee, Narko) " +
                        "VALUES (@FID, @NameFaction, @Locked, @Position, @Pistols, @Rifle, @SMG, @Shotgun, @Bullets, @Melee, @Narko)", parametersVagos);

                    break;

                case "Ballas":

                    Dictionary<string, string> parametersBallas = new Dictionary<string, string>
                    {
                        { "@FID", IdFraction.ToString() },
                        { "@NameFaction", NameFaction },
                        { "@Locked", Convert.ToInt32(isLocked).ToString() },
                        { "@Position", JsonConvert.SerializeObject(position) },
                        { "@Pistols",  Pistols.ToString() },
                        { "@Rifle",  Rifle.ToString() },
                        { "@SMG",  SMG.ToString() },
                        { "@Shotgun",  Shotgun.ToString() },
                        { "@Bullets",  Bullets.ToString() },
                        { "@Melee",  Melee.ToString() },
                        { "@Narko",  Narko.ToString() }

                    };

                    DataTable resultBallas = Database.ExecutePreparedStatement("INSERT INTO gangfaction_storages (FID, NameFaction, Locked, Position, Pistols, Rifle, SMG, Shotgun, Bullets, Melee, Narko) " +
                        "VALUES (@FID, @NameFaction, @Locked, @Position, @Pistols, @Rifle, @SMG, @Shotgun, @Bullets, @Melee, @Narko)", parametersBallas);

                    break;

                case "Triads":

                    Dictionary<string, string> parametersTriads = new Dictionary<string, string>
                    {
                        { "@FID", IdFraction.ToString() },
                        { "@NameFaction", NameFaction },
                        { "@Locked", Convert.ToInt32(isLocked).ToString() },
                        { "@Position", JsonConvert.SerializeObject(position) },
                        { "@Pistols",  Pistols.ToString() },
                        { "@Rifle",  Rifle.ToString() },
                        { "@SMG",  SMG.ToString() },
                        { "@Shotgun",  Shotgun.ToString() },
                        { "@Bullets",  Bullets.ToString() },
                        { "@Melee",  Melee.ToString() },
                        { "@Narko",  Narko.ToString() }

                    };

                    DataTable resultTriads = Database.ExecutePreparedStatement("INSERT INTO gangfaction_storages (FID, NameFaction, Locked, Position, Pistols, Rifle, SMG, Shotgun, Bullets, Melee, Narko) " +
                        "VALUES (@FID, @NameFaction, @Locked, @Position, @Pistols, @Rifle, @SMG, @Shotgun, @Bullets, @Melee, @Narko)", parametersTriads);

                    break;
            }
        }

        public static void GangReloadStorages()
        {
            StorageList.ForEach(GangFactionStorage =>
            {
                API.shared.deleteColShape(GangFactionStorage.ColShape);
            });
            StorageList.Clear();
            LoadAllStoragesFromDB();
        }

        [Command("addstoragegang")]
        public void AddStorageCommand(Client sender, int IdFraction, string NameFaction, bool isLocked)
        {
            AddNewGangStorage(IdFraction, NameFaction, isLocked, sender.position);
            API.sendChatMessageToPlayer(sender, "~g~Склад успешно создан");
        }
        [Command("reloadstoragesgang")]
        public void ReloadStoragesCommand(Client sender)
        {
            GangReloadStorages();
            API.sendChatMessageToPlayer(sender, "~g~Склады перезагружены!");
        }
    }
}
