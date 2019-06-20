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
    public class StorageService : Script
    {

        public StorageService()
        {
            API.onResourceStart += OnResourceStart;
            API.onEntityEnterColShape += ColShapeTrigger;
        }

        public void OnResourceStart()
        {
            LoadAllStoragesFromDB();
        }

        private const string DatabaseValues = "VALUES (@FID, @NameFaction, @Locked, @Position, @Pistols, @Rifle, @SMG, @Shotgun, @Bullets, @Melee, @Armor)";
        public static string myConnectionString = "SERVER=localhost;" + "DATABASE=arcadia;" + "UID=root;" + "PASSWORD=qcoluk1ps3;";

        private MenuManager MenuManager = new MenuManager();
        private Menu savedMenu = null;

        #region EMSStorageMenus
        private void StorageEMSMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Medicals = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'EMS'";

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    Medicals += Database.Reader.GetInt32("Medicals");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "EMS";

                menu = new Menu("EMSStorage", "Склад EMS", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(200, 0, 0, 90),
                    Callback = StorageEMS_MenuManager
                };

                menu.Add(new MenuItem("Взять медикаменты", "~b~На складе: ~w~" + Convert.ToString(Medicals), "get_medicals_menu_item")
                {
                    ExecuteCallback = true
                });

                State = Locked == "0" ? "~g~Открыт" : "~r~Закрыт";

                menu.Add(new MenuItem("Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                });
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StorageEMS_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'EMS'";

            int Medicals = 0;
            string Locked = "";
            int Lockstate;

            string groupName = "FS";

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    Medicals += Database.Reader.GetInt32("Medicals");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (itemIndex == 0)
            {
                if (Locked == "0")
                {
                    if (Medicals != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?medicals", Medicals - 50);
                        Database.command.CommandText = "UPDATE faction_storages SET Medicals = ?medicals WHERE NameFaction = 'EMS'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы взяли 50 медикаментов со склада");
                        StorageEMSMenuBuilder(client);
                    }
                    else API.sendChatMessageToPlayer(client, "~r~На складе закончились медикаменты!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 1)
            {
                if (Player.GetFractionRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~r~ закрыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.Parameters.AddWithValue("?medicals", Lockstate);
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '1' WHERE NameFaction = 'EMS'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 1)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StorageEMSMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~r~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.Parameters.AddWithValue("?medicals", Lockstate);
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '0' WHERE NameFaction = 'EMS'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 1)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StorageEMSMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 2)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }
        #endregion

        #region PoliceStorageMenus

        private void StoragePoliceMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'Police'";

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
                    Armor += Database.Reader.GetInt32("Armor");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "Police";

                menu = new Menu("PoliceStorage", "Склад LSPD", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(0, 50, 170, 90),
                    Callback = StoragePolice_MenuManager
                };

                MenuItem get_pistol_menu_item = new MenuItem("Взять пистолет", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_pistol_menu_item);

                MenuItem get_rifle_menu_item = new MenuItem("Взять винтовку", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_rifle_menu_item);

                MenuItem get_smg_menu_item = new MenuItem("Взять пистолет-пулемёт", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_smg_menu_item);

                MenuItem get_shotgun_menu_item = new MenuItem("Взять дробовик", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_shotgun_menu_item);

                MenuItem get_tazer_menu_item = new MenuItem("Взять комплект шокер + фонарь", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_tazer_menu_item);

                MenuItem get_armor_menu_item = new MenuItem("Взять бронежилет", "~b~На складе: ~w~" + Convert.ToString(Armor), "get_armor_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_armor_menu_item);

                MenuItem get_extra_menu_item = new MenuItem("~b~Взять экстренное снаряжение", "~r~В экстренном случае", "get_extra_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_extra_menu_item);

                if (Locked == "0")
                {
                    State = "~g~Открыт";
                }
                else State = "~r~Закрыт";

                MenuItem Lock_Or_Unlock_Storage = new MenuItem("~y~Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage")
                {
                    ExecuteCallback = true
                };
                menu.Add(Lock_Or_Unlock_Storage);

                MenuItem CloseMenuItem = new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                };
                menu.Add(CloseMenuItem);
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StoragePolice_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'Police'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
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
                    Armor += Database.Reader.GetInt32("Armor");
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

            if (itemIndex == 0) // пистолеты
            {
                client.setData("HAVE_PISTOL", false);
                if (Locked == "0")
                {
                    if (client.getData("HAVE_PISTOL") == false)
                    {
                        if (Pistols != 0)
                        {
                            var chatMessagePistol = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада пистолет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?pistols", Pistols - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Pistols = ?pistols WHERE NameFaction = 'Police'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли пистолет со склада");
                            API.givePlayerWeapon(client, WeaponHash.Pistol, 72, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 2 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageRifle = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада винтовку";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?rifle", Rifle - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Rifle = ?rifle WHERE NameFaction = 'Police'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли винтовку со склада");
                            API.givePlayerWeapon(client, WeaponHash.CarbineRifle, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 2 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageSMG = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада SMG";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?smg", SMG - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET SMG = ?smg WHERE NameFaction = 'Police'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада SMG");
                            API.givePlayerWeapon(client, WeaponHash.SMG, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 2 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageShotgun = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада дробовик";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'Police'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли дробовик со склада");
                            API.givePlayerWeapon(client, WeaponHash.PumpShotgun, 40, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 2 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада комплект";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?melee", Melee - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Melee = ?melee WHERE NameFaction = 'Police'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                            API.givePlayerWeapon(client, WeaponHash.StunGun, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Flashlight, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Nightstick, 1, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 2 && Player.IsPlayerOnDuty(player) == 1)
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
            if (itemIndex == 5) // броня
            {
                if (Locked == "0")
                {
                    if (API.getPlayerArmor(client) == 0)
                    {
                        if (Armor != 0)
                        {
                            var chatMessageArmor = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада бронежилет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?armor", Armor - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Armor = ?armor WHERE NameFaction = 'Police'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли бронежилет со склада");
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 2 && Player.IsPlayerOnDuty(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageArmor);
                                }
                            }
                            API.setPlayerArmor(client, 100);
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились бронежилеты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~У вас уже есть бронежилет!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 6) // экстренное снаряжение
            {
                if (Locked == "0")
                {
                    var chatMessageExtra = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял экстренный комплект";
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли экстренный комплект со склада");
                    API.givePlayerWeapon(client, WeaponHash.Pistol, 24, true, true);
                    API.setPlayerArmor(client, 35);
                    API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                    API.givePlayerWeapon(client, WeaponHash.Nightstick, 1, true, true);
                    API.givePlayerWeapon(client, WeaponHash.Flashlight, 1, true, true);
                    foreach (Client player in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(player) == 2 && Player.IsPlayerOnDuty(player) == 1)
                        {
                            API.sendChatMessageToPlayer(player, chatMessageExtra);
                        }
                    }
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 7)
            {
                if (Player.GetFractionRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~r~ закрыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '1' WHERE NameFaction = 'Police'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 2)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StoragePoliceMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageOpenStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~g~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '0' WHERE NameFaction = 'Police'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 2)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageOpenStorage);
                            }
                        }
                        StoragePoliceMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 8)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }

        #endregion

        #region MeryWetherStorageMenus

        private void StorageMeryWetherMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'MeryWether'";

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
                    Armor += Database.Reader.GetInt32("Armor");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "MeryWether";

                menu = new Menu("MeryWetherStorage", "Склад MeryWether", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(255, 66, 66),
                    Callback = StorageMeryWether_MenuManager
                };

                menu.Add(new MenuItem("Взять пистолет", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять винтовку", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять пистолет-пулемёт", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять дробовик", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять комплект ближнего боя", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять бронежилет", "~b~На складе: ~w~" + Convert.ToString(Armor), "get_armor_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("~b~Взять экстренное снаряжение", "~r~В экстренном случае", "get_extra_menu_item")
                {
                    ExecuteCallback = true
                });

                State = Locked == "0" ? "~g~Открыт" : "~r~Закрыт";

                menu.Add(new MenuItem("~y~Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                });
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StorageMeryWether_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'MeryWether'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
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
                    Armor += Database.Reader.GetInt32("Armor");
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

            if (itemIndex == 0) // пистолеты
            {
                client.setData("HAVE_PISTOL", false);
                if (Locked == "0")
                {
                    if (client.getData("HAVE_PISTOL") == false)
                    {
                        if (Pistols != 0)
                        {
                            var chatMessagePistol = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада пистолет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?pistols", Pistols - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Pistols = ?pistols WHERE NameFaction = 'MaryWeather'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли пистолет со склада");
                            API.givePlayerWeapon(client, WeaponHash.APPistol, 72, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 3 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageRifle = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада винтовку";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?rifle", Rifle - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Rifle = ?rifle WHERE NameFaction = 'MaryWeather'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли винтовку со склада");
                            API.givePlayerWeapon(client, WeaponHash.BullpupRifle, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 3 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageSMG = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада SMG";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?smg", SMG - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET SMG = ?smg WHERE NameFaction = 'MeryWeather'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада SMG");
                            API.givePlayerWeapon(client, WeaponHash.AssaultSMG, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 3 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageShotgun = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада дробовик";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'MeryWeather'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли дробовик со склада");
                            API.givePlayerWeapon(client, WeaponHash.HeavyShotgun, 40, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 3 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада комплект";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?melee", Melee - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Melee = ?melee WHERE NameFaction = 'MeryWeather'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                            API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Flashlight, 1, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 3 && Player.IsPlayerOnDuty(player) == 1)
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
            if (itemIndex == 5) // броня
            {
                if (Locked == "0")
                {
                    if (API.getPlayerArmor(client) == 0)
                    {
                        if (Armor != 0)
                        {
                            var chatMessageArmor = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада бронежилет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?armor", Armor - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Armor = ?armor WHERE NameFaction = 'MeryWeather'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли бронежилет со склада");
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 3 && Player.IsPlayerOnDuty(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageArmor);
                                }
                            }
                            API.setPlayerArmor(client, 100);
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились бронежилеты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~У вас уже есть бронежилет!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 6) // экстренное снаряжение
            {
                if (Locked == "0")
                {
                    var chatMessageExtra = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял экстренный комплект";
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли экстренный комплект со склада");
                    API.givePlayerWeapon(client, WeaponHash.Pistol50, 24, true, true);
                    API.setPlayerArmor(client, 75);
                    API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                    API.givePlayerWeapon(client, WeaponHash.Parachute, 1, true, true);
                    API.givePlayerWeapon(client, WeaponHash.CombatPDW, 120, true, true);
                    foreach (Client player in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(player) == 3 && Player.IsPlayerOnDuty(player) == 1)
                        {
                            API.sendChatMessageToPlayer(player, chatMessageExtra);
                        }
                    }
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 7)
            {
                if (Player.GetFractionRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~r~ закрыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '1' WHERE NameFaction = 'MeryWeather'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 3)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StoragePoliceMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageOpenStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~g~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '0' WHERE NameFaction = 'MeryWeather'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 3)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageOpenStorage);
                            }
                        }
                        StoragePoliceMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 8)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }
        #endregion

        #region IAAStorageMenus

        private void StorageIAAMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'IAA'";

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
                    Armor += Database.Reader.GetInt32("Armor");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "IAA";

                menu = new Menu("IAAStorage", "Склад IAA", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(0, 50, 170, 90),
                    Callback = StorageIAA_MenuManager
                };

                menu.Add(new MenuItem("Взять пистолет", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять винтовку", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять пистолет-пулемёт", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять дробовик", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять комплект шокер + фонарь", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять бронежилет", "~b~На складе: ~w~" + Convert.ToString(Armor), "get_armor_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("~b~Взять экстренное снаряжение", "~r~В экстренном случае", "get_extra_menu_item")
                {
                    ExecuteCallback = true
                });

                State = Locked == "0" ? "~g~Открыт" : "~r~Закрыт";

                menu.Add(new MenuItem("~y~Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                });
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StorageIAA_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'IAA'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
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
                    Armor += Database.Reader.GetInt32("Armor");
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

            if (itemIndex == 0) // пистолеты
            {
                client.setData("HAVE_PISTOL", false);
                if (Locked == "0")
                {
                    if (client.getData("HAVE_PISTOL") == false)
                    {
                        if (Pistols != 0)
                        {
                            var chatMessagePistol = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада пистолет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?pistols", Pistols - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Pistols = ?pistols WHERE NameFaction = 'IAA'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли пистолет со склада");
                            API.givePlayerWeapon(client, WeaponHash.APPistol, 72, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 4 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageRifle = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада винтовку";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?rifle", Rifle - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Rifle = ?rifle WHERE NameFaction = 'IAA'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли винтовку со склада");
                            API.givePlayerWeapon(client, WeaponHash.SpecialCarbine, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 4 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageSMG = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада SMG";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?smg", SMG - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET SMG = ?smg WHERE NameFaction = 'IAA'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада SMG");
                            API.givePlayerWeapon(client, WeaponHash.AssaultSMG, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 4 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageShotgun = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада дробовик";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'IAA'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли дробовик со склада");
                            API.givePlayerWeapon(client, WeaponHash.HeavyShotgun, 40, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 4 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада комплект";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?melee", Melee - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Melee = ?melee WHERE NameFaction = 'IAA'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                            API.givePlayerWeapon(client, WeaponHash.StunGun, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Flashlight, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Nightstick, 1, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 4 && Player.IsPlayerOnDuty(player) == 1)
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
            if (itemIndex == 5) // броня
            {
                if (Locked == "0")
                {
                    if (API.getPlayerArmor(client) == 0)
                    {
                        if (Armor != 0)
                        {
                            var chatMessageArmor = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада бронежилет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?armor", Armor - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Armor = ?armor WHERE NameFaction = 'IAA'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли бронежилет со склада");
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 4 && Player.IsPlayerOnDuty(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageArmor);
                                }
                            }
                            API.setPlayerArmor(client, 100);
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились бронежилеты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~У вас уже есть бронежилет!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 6) // экстренное снаряжение
            {
                if (Locked == "0")
                {
                    var chatMessageExtra = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял экстренный комплект";
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли экстренный комплект со склада");
                    API.givePlayerWeapon(client, WeaponHash.Pistol, 24, true, true);
                    API.setPlayerArmor(client, 35);
                    API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                    API.givePlayerWeapon(client, WeaponHash.HeavySniper, 1, true, true);
                    API.givePlayerWeapon(client, WeaponHash.Flashlight, 1, true, true);
                    foreach (Client player in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(player) == 4 && Player.IsPlayerOnDuty(player) == 1)
                        {
                            API.sendChatMessageToPlayer(player, chatMessageExtra);
                        }
                    }
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 7)
            {
                if (Player.GetFractionRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~r~ закрыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '1' WHERE NameFaction = 'IAA'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 4)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StorageIAAMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageOpenStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~g~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '0' WHERE NameFaction = 'IAA'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 4)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageOpenStorage);
                            }
                        }
                        StorageIAAMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 8)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }

        #endregion

        #region RedneckStorageMenus

        private void StorageRedneckMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'Redneck'";

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
                    Armor += Database.Reader.GetInt32("Armor");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "Redneck";

                menu = new Menu("RedneckStorage", "Склад Redneck", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(0, 50, 170, 90),
                    Callback = StorageRedneck_MenuManager
                };

                menu.Add(new MenuItem("Взять пистолет", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять винтовку", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять пистолет-пулемёт", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять дробовик", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять комплект шокер + фонарь", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять бронежилет", "~b~На складе: ~w~" + Convert.ToString(Armor), "get_armor_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("~b~Взять экстренное снаряжение", "~r~В экстренном случае", "get_extra_menu_item")
                {
                    ExecuteCallback = true
                });

                State = Locked == "0" ? "~g~Открыт" : "~r~Закрыт";

                menu.Add(new MenuItem("~y~Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                });
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StorageRedneck_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'Redneck'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
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
                    Armor += Database.Reader.GetInt32("Armor");
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

            if (itemIndex == 0) // пистолеты
            {
                client.setData("HAVE_PISTOL", false);
                if (Locked == "0")
                {
                    if (client.getData("HAVE_PISTOL") == false)
                    {
                        if (Pistols != 0)
                        {
                            var chatMessagePistol = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада пистолет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?pistols", Pistols - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Pistols = ?pistols WHERE NameFaction = 'Redneck'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли пистолет со склада");
                            API.givePlayerWeapon(client, WeaponHash.APPistol, 72, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 5 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageRifle = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада винтовку";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?rifle", Rifle - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Rifle = ?rifle WHERE NameFaction = 'Redneck'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли винтовку со склада");
                            API.givePlayerWeapon(client, WeaponHash.Gusenberg, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 5 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageSMG = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада SMG";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?smg", SMG - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET SMG = ?smg WHERE NameFaction = 'Redneck'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада SMG");
                            API.givePlayerWeapon(client, WeaponHash.MicroSMG, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 5 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageShotgun = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада дробовик";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'Redneck'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли дробовик со склада");
                            API.givePlayerWeapon(client, WeaponHash.Musket, 40, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 5 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада комплект";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?melee", Melee - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Melee = ?melee WHERE NameFaction = 'Redneck'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");

                            API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Hammer, 1, true, true);
                            API.givePlayerWeapon(client, WeaponHash.Bottle, 1, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 5 && Player.IsPlayerOnDuty(player) == 1)
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
            if (itemIndex == 5) // броня
            {
                if (Locked == "0")
                {
                    if (API.getPlayerArmor(client) == 0)
                    {
                        if (Armor != 0)
                        {
                            var chatMessageArmor = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада бронежилет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?armor", Armor - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Armor = ?armor WHERE NameFaction = 'Redneck'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли бронежилет со склада");
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 5 && Player.IsPlayerOnDuty(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageArmor);
                                }
                            }
                            API.setPlayerArmor(client, 25);
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились бронежилеты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~У вас уже есть бронежилет!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 6) // экстренное снаряжение
            {
                if (Locked == "0")
                {
                    var chatMessageExtra = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял экстренный комплект";
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли экстренный комплект со склада");
                    API.givePlayerWeapon(client, WeaponHash.Pistol, 24, true, true);
                    API.setPlayerArmor(client, 15);
                    API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                    foreach (Client player in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(player) == 5 && Player.IsPlayerOnDuty(player) == 1)
                        {
                            API.sendChatMessageToPlayer(player, chatMessageExtra);
                        }
                    }
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 7)
            {
                if (Player.GetFractionRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~r~ закрыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '1' WHERE NameFaction = 'Redneck'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 5)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StorageRedneckMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageOpenStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~g~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '0' WHERE NameFaction = 'Redneck'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 5)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageOpenStorage);
                            }
                        }
                        StorageRedneckMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 8)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }

        #endregion

        #region GovernmentStorageMenus

        private void StorageGovernmentMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'Government'";

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
                    Armor += Database.Reader.GetInt32("Armor");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "Government";

                menu = new Menu("GovernmentStorage", "Склад правительства", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(0, 50, 170, 90),
                    Callback = StorageGovernment_MenuManager
                };

                menu.Add(new MenuItem("Взять пистолет", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять винтовку", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять пистолет-пулемёт", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять дробовик", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять комплект шокер + фонарь", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("Взять бронежилет", "~b~На складе: ~w~" + Convert.ToString(Armor), "get_armor_menu_item")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("~b~Взять экстренное снаряжение", "~r~В экстренном случае", "get_extra_menu_item")
                {
                    ExecuteCallback = true
                });

                State = Locked == "0" ? "~g~Открыт" : "~r~Закрыт";

                menu.Add(new MenuItem("~y~Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage")
                {
                    ExecuteCallback = true
                });

                menu.Add(new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                });
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StorageGovernment_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'Government'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
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
                    Armor += Database.Reader.GetInt32("Armor");
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

            if (itemIndex == 0) // пистолеты
            {
                client.setData("HAVE_PISTOL", false);
                if (Locked == "0")
                {
                    if (client.getData("HAVE_PISTOL") == false)
                    {
                        if (Pistols != 0)
                        {
                            var chatMessagePistol = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада пистолет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?pistols", Pistols - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Pistols = ?pistols WHERE NameFaction = 'Government'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли пистолет со склада");
                            API.givePlayerWeapon(client, WeaponHash.SNSPistol, 72, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 7 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageRifle = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада винтовку";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?rifle", Rifle - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Rifle = ?rifle WHERE NameFaction = 'Government'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли винтовку со склада");
                            API.givePlayerWeapon(client, WeaponHash.AdvancedRifle, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 7 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageSMG = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада SMG";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?smg", SMG - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET SMG = ?smg WHERE NameFaction = 'Government'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада SMG");
                            API.givePlayerWeapon(client, WeaponHash.SMG, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 7 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageShotgun = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада дробовик";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'Government'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли дробовик со склада");
                            API.givePlayerWeapon(client, WeaponHash.PumpShotgun, 40, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 7 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада комплект";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?melee", Melee - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Melee = ?melee WHERE NameFaction = 'Government'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                            API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);

                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 7 && Player.IsPlayerOnDuty(player) == 1)
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
            if (itemIndex == 5) // броня
            {
                if (Locked == "0")
                {
                    if (API.getPlayerArmor(client) == 0)
                    {
                        if (Armor != 0)
                        {
                            var chatMessageArmor = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада бронежилет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?armor", Armor - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Armor = ?armor WHERE NameFaction = 'Government'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли бронежилет со склада");
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 7 && Player.IsPlayerOnDuty(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageArmor);
                                }
                            }
                            API.setPlayerArmor(client, 76);
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились бронежилеты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~У вас уже есть бронежилет!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 6) // экстренное снаряжение
            {
                if (Locked == "0")
                {
                    var chatMessageExtra = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял экстренный комплект";
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли экстренный комплект со склада");
                    API.givePlayerWeapon(client, WeaponHash.Pistol, 24, true, true);
                    API.setPlayerArmor(client, 45);
                    API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);
                    API.givePlayerWeapon(client, WeaponHash.Flashlight, 1, true, true);
                    API.givePlayerWeapon(client, WeaponHash.Nightstick, 1, true, true);
                    foreach (Client player in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(player) == 7 && Player.IsPlayerOnDuty(player) == 1)
                        {
                            API.sendChatMessageToPlayer(player, chatMessageExtra);
                        }
                    }
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 7)
            {
                if (Player.GetFractionRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~r~ закрыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '1' WHERE NameFaction = 'Government'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 7)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StorageGovernmentMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageOpenStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~g~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '0' WHERE NameFaction = 'Government'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 7)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageOpenStorage);
                            }
                        }
                        StorageGovernmentMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 8)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }

        #endregion

        #region ArmyStorageMenus

        private void StorageArmyMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
            string Locked = "";

            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'Army'";

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
                    Armor += Database.Reader.GetInt32("Armor");
                    Locked += Database.Reader.GetString("Locked");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "Army";

                menu = new Menu("ArmyStorage", "Склад Армии", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(0, 50, 170, 90),
                    Callback = StorageArmy_MenuManager
                };

                MenuItem get_pistol_menu_item = new MenuItem("Взять пистолет", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_pistol_menu_item);

                MenuItem get_rifle_menu_item = new MenuItem("Взять винтовку", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_rifle_menu_item);

                MenuItem get_smg_menu_item = new MenuItem("Взять пистолет-пулемёт", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_smg_menu_item);

                MenuItem get_shotgun_menu_item = new MenuItem("Взять дробовик", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_shotgun_menu_item);

                MenuItem get_tazer_menu_item = new MenuItem("Взять комплект шокер + фонарь", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_tazer_menu_item);

                MenuItem get_armor_menu_item = new MenuItem("Взять бронежилет", "~b~На складе: ~w~" + Convert.ToString(Armor), "get_armor_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_armor_menu_item);

                MenuItem get_extra_menu_item = new MenuItem("~b~Взять экстренное снаряжение", "~r~В экстренном случае", "get_extra_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_extra_menu_item);

                if (Locked == "0")
                {
                    State = "~g~Открыт";
                }
                else State = "~r~Закрыт";

                MenuItem Lock_Or_Unlock_Storage = new MenuItem("~y~Закрыть/Открыть склад", "Склад: " + State, "Lock_Or_Unlock_Storage")
                {
                    ExecuteCallback = true
                };
                menu.Add(Lock_Or_Unlock_Storage);

                MenuItem CloseMenuItem = new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                };
                menu.Add(CloseMenuItem);
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void StorageArmy_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM faction_storages WHERE NameFaction = 'Army'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
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
                    Armor += Database.Reader.GetInt32("Armor");
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

            if (itemIndex == 0) // пистолеты
            {
                client.setData("HAVE_PISTOL", false);
                if (Locked == "0")
                {
                    if (client.getData("HAVE_PISTOL") == false)
                    {
                        if (Pistols != 0)
                        {
                            var chatMessagePistol = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада пистолет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?pistols", Pistols - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Pistols = ?pistols WHERE NameFaction = 'Army'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли пистолет со склада");
                            API.givePlayerWeapon(client, WeaponHash.HeavyPistol, 72, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 8 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageRifle = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада винтовку";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?rifle", Rifle - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Rifle = ?rifle WHERE NameFaction = 'Army'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли винтовку со склада");
                            API.givePlayerWeapon(client, WeaponHash.CarbineRifle, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 8 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageSMG = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада SMG";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?smg", SMG - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET SMG = ?smg WHERE NameFaction = 'Army'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада SMG");
                            API.givePlayerWeapon(client, WeaponHash.CombatMG, 150, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 8 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageShotgun = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада дробовик";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'Army'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли дробовик со склада");
                            API.givePlayerWeapon(client, WeaponHash.Autoshotgun, 40, true, true);
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 8 && Player.IsPlayerOnDuty(player) == 1)
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
                            var chatMessageMelee = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада комплект";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?melee", Melee - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Melee = ?melee WHERE NameFaction = 'Army'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект со склада");
                            API.givePlayerWeapon(client, WeaponHash.Knife, 1, true, true);

                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 8 && Player.IsPlayerOnDuty(player) == 1)
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
            if (itemIndex == 5) // броня
            {
                if (Locked == "0")
                {
                    if (API.getPlayerArmor(client) == 0)
                    {
                        if (Armor != 0)
                        {
                            var chatMessageArmor = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял со склада бронежилет";
                            Database.connection.Open();
                            Database.command.Parameters.AddWithValue("?armor", Armor - 1);
                            Database.command.CommandText = "UPDATE faction_storages SET Armor = ?armor WHERE NameFaction = 'Army'";
                            Database.command.ExecuteNonQuery();
                            Database.connection.Close();
                            API.sendChatMessageToPlayer(client, "~g~Вы взяли бронежилет со склада");
                            foreach (Client player in API.getAllPlayers())
                            {
                                if (Player.GetFractionId(player) == 8 && Player.IsPlayerOnDuty(player) == 1)
                                {
                                    API.sendChatMessageToPlayer(player, chatMessageArmor);
                                }
                            }
                            API.setPlayerArmor(client, 100);
                        }
                        else API.sendChatMessageToPlayer(client, "~r~На складе закончились бронежилеты!");
                    }
                    else API.sendChatMessageToPlayer(client, "~r~У вас уже есть бронежилет!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 6) // экстренное снаряжение
            {
                if (Locked == "0")
                {
                    var chatMessageExtra = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~y~ взял экстренный комплект";
                    API.sendChatMessageToPlayer(client, "~g~Вы взяли экстренный комплект со склада");
                    API.givePlayerWeapon(client, WeaponHash.Pistol, 24, true, true);
                    API.setPlayerArmor(client, 45);
                    API.givePlayerWeapon(client, WeaponHash.RPG, 1, true, true);
                    API.givePlayerWeapon(client, WeaponHash.SniperRifle, 10, true, true);
                    API.givePlayerWeapon(client, WeaponHash.Grenade, 5, true, true);
                    foreach (Client player in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(player) == 8 && Player.IsPlayerOnDuty(player) == 1)
                        {
                            API.sendChatMessageToPlayer(player, chatMessageExtra);
                        }
                    }
                }
                else API.sendChatMessageToPlayer(client, "~r~Склад закрыт!");
            }
            if (itemIndex == 7)
            {
                if (Player.GetFractionRank(client) == 10)
                {

                    if (Locked == "0")
                    {
                        var chatMessageLockStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~r~ закрыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 1;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '1' WHERE NameFaction = 'Army'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы закрыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 8)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageLockStorage);
                            }
                        }
                        StorageArmyMenuBuilder(client);
                    }
                    else
                    {
                        var chatMessageOpenStorage = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(client, Player.GetFractionRank(client)) + " | " + client.name + "~g~ открыл склад фракции";
                        Database.connection.Open();
                        Lockstate = 0;
                        Database.command.CommandText = "UPDATE faction_storages SET Locked = '0' WHERE NameFaction = 'Army'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы открыли склад фракции!");
                        foreach (Client player in API.getAllPlayers())
                        {
                            if (Player.GetFractionId(player) == 8)
                            {
                                API.sendChatMessageToPlayer(player, chatMessageOpenStorage);
                            }
                        }
                        StorageArmyMenuBuilder(client);
                    }
                }

                else
                {
                    API.sendChatMessageToPlayer(client, "~r~Эта функция доступна только лидеру!");
                }
            }
            if (itemIndex == 8)
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

            int playerfactionid = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (colShape != null && colShape.getData("IS_FACTION_STORAGE") == true)
            {
                if (playerfactionid != 0)
                {
                    if (colShape.getData("fraction_id") == 1 && playerfactionid == 1)
                    {
                        int onduty = Player.IsPlayerOnDuty(player);
                        if (onduty == 1)
                        {
                            StorageEMSMenuBuilder(player);
                        }
                        else API.sendChatMessageToPlayer(player, "~r~Вы не на смене!");
                    }
                    if (colShape.getData("fraction_id") == 2 && playerfactionid == 2)
                    {
                        int onduty = Player.IsPlayerOnDuty(player);
                        if (onduty == 1)
                        {
                            StoragePoliceMenuBuilder(player);
                        }
                        else API.sendChatMessageToPlayer(player, "~r~Вы не на дежурстве!");
                    }
                    if (colShape.getData("fraction_id") == 3 && playerfactionid == 3)
                    {
                        int onduty = Player.IsPlayerOnDuty(player);
                        if (onduty == 1)
                        {
                            StorageMeryWetherMenuBuilder(player);
                        }
                        else API.sendChatMessageToPlayer(player, "~r~Вы не на смене!");
                    }
                }
                if (colShape.getData("fraction_id") == 4 && playerfactionid == 4)
                {
                    int onduty = Player.IsPlayerOnDuty(player);
                    if (onduty == 1)
                    {
                        StorageIAAMenuBuilder(player);
                    }
                    else API.sendChatMessageToPlayer(player, "~r~Вы не на дежурстве!");
                }
            }
            if (colShape.getData("fraction_id") == 5 && playerfactionid == 5)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 1)
                {
                    StorageRedneckMenuBuilder(player);
                }
                else API.sendChatMessageToPlayer(player, "~r~Вы не на дежурстве!");
            }
            if (colShape.getData("fraction_id") == 7 && playerfactionid == 7)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 1)
                {
                    StorageGovernmentMenuBuilder(player);
                }
                else API.sendChatMessageToPlayer(player, "~r~Вы не на дежурстве!");
            }
            if (colShape.getData("fraction_id") == 8 && playerfactionid == 8)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 1)
                {
                    StorageArmyMenuBuilder(player);
                }
                else API.sendChatMessageToPlayer(player, "~r~Вы не на дежурстве!");
            }            

        }
    

        public static readonly List<Storage> StorageList = new List<Storage>();

        public static void LoadAllStoragesFromDB()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            DataTable result = Database.ExecutePreparedStatement("SELECT * FROM faction_storages", parameters);

            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Storage FactionStorage = new Storage
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
                        Armor = (int)row["Armor"],
                        Medicals = (int)row["Medicals"]
                    };

                    Marker storagemarker = API.shared.createMarker(1, FactionStorage.Position - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
                    API.shared.createTextLabel("Склад " + FactionStorage.NameFaction, FactionStorage.Position, 15f, 0.65f);

                    ColShape FactionStorageColshape = API.shared.createCylinderColShape(FactionStorage.Position, 0.50f, 1f);
                    FactionStorageColshape.setData("fraction_id", FactionStorage.IdFraction);
                    FactionStorageColshape.setData("isLocked", FactionStorage.Locked);
                    FactionStorageColshape.setData("IS_FACTION_STORAGE", true);
                    FactionStorage.ColShape = FactionStorageColshape;

                    StorageList.Add(FactionStorage);
                }
                API.shared.consoleOutput(LogCat.Info, result.Rows.Count + "Складов загружено!");
            }
            else
            {
                API.shared.consoleOutput(LogCat.Info, "Не удалось загрузить склады!");
            }
        }

        public static void AddNewStorage(int IdFraction, string NameFaction, bool isLocked, Vector3 position)
        {

            const int Pistols = 25;
            const int Rifle = 10;
            const int SMG = 15;
            const int Shotgun = 25;
            const int Melee = 25;
            const int Armor = 25;
            const int Bullets = 500;
            const int Medicals = 250;

            switch (NameFaction)
            {

                case "EMS":
                    Dictionary<string, string> parametersEMS = new Dictionary<string, string>
                    {
                        { "@FID", IdFraction.ToString() },
                        { "@NameFaction", NameFaction },
                        { "@Locked", Convert.ToInt32(isLocked).ToString() },
                        { "@Position", JsonConvert.SerializeObject(position) },
                        { "@Medicals",  Medicals.ToString() }
                    };

                    DataTable resultEMS = Database.ExecutePreparedStatement("INSERT INTO faction_storages (FID, NameFaction, Locked, Position, Medicals) " +
                        "VALUES (@FID, @NameFaction, @Locked, @Position, @Medicals)", parametersEMS);

                    break;

                case "Police":

                    Dictionary<string, string> parametersPolice = new Dictionary<string, string>
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
                        { "@Armor",  Armor.ToString() }
                    };

                    DataTable resultPolice = Database.ExecutePreparedStatement("INSERT INTO faction_storages (FID, NameFaction, Locked, Position, Pistols, Rifle, SMG, Shotgun, Bullets, Melee, Armor) " +
                        DatabaseValues, parametersPolice);

                    break;

                case "MeryWether":

                    Dictionary<string, string> parametersMeryWether = new Dictionary<string, string>
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
                        { "@Armor",  Armor.ToString() }
                        
                    };

                    DataTable resultMeryWether = Database.ExecutePreparedStatement("INSERT INTO faction_storages (FID, NameFaction, Locked, Position, Pistols, Rifle, SMG, Shotgun, Bullets, Melee, Armor) " +
                        DatabaseValues, parametersMeryWether);

                    break;

                case "IAA":

                    Dictionary<string, string> parametersIAA = new Dictionary<string, string>
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
                        { "@Armor",  Armor.ToString() }
                    };

                    DataTable resultIAA = Database.ExecutePreparedStatement("INSERT INTO faction_storages (FID, NameFaction, Locked, Position, Pistols, Rifle, SMG, Shotgun, Bullets, Melee, Armor) " +
                        DatabaseValues, parametersIAA);

                    break;

                case "Redneck":

                    Dictionary<string, string> parametersRedneck = new Dictionary<string, string>
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
                        { "@Armor",  Armor.ToString() }
                    };

                    DataTable resultRedneck = Database.ExecutePreparedStatement("INSERT INTO faction_storages (FID, NameFaction, Locked, Position, Pistols, Rifle, SMG, Shotgun, Bullets, Melee, Armor) " +
                        DatabaseValues, parametersRedneck);

                    break;

                case "Government":

                    Dictionary<string, string> parametersGovernment = new Dictionary<string, string>
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
                        { "@Armor",  Armor.ToString() }
                    };

                    DataTable resultGovernment = Database.ExecutePreparedStatement("INSERT INTO faction_storages (FID, NameFaction, Locked, Position, Pistols, Rifle, SMG, Shotgun, Bullets, Melee, Armor) " +
                        DatabaseValues, parametersGovernment);

                    break;

                case "Army":


                    DataTable resultArmy = Database.ExecutePreparedStatement("INSERT INTO faction_storages (FID, NameFaction, Locked, Position, Pistols, Rifle, SMG, Shotgun, Bullets, Melee, Armor) " +
                        DatabaseValues, new Dictionary<string, string>
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
                        { "@Armor",  Armor.ToString() }
                    });

                    break;
            }
        }

        public static void ReloadStorages()
        {
            StorageList.ForEach(FactionStorage =>
            {
                API.shared.deleteColShape(FactionStorage.ColShape);
            });
            StorageList.Clear();
            LoadAllStoragesFromDB();
        }

        [Command("addstorage")]
        public void AddStorageCommand (Client sender, int IdFraction, string NameFaction, bool isLocked)
        {
            AddNewStorage(IdFraction, NameFaction, isLocked, sender.position);
            API.sendChatMessageToPlayer(sender, "~g~Склад успешно создан");
        }
        [Command("reloadstorages")]
        public void ReloadStoragesCommand(Client sender)
        {
            ReloadStorages();
            API.sendChatMessageToPlayer(sender, "~g~Склады перезагружены!");
        }
    }
}
