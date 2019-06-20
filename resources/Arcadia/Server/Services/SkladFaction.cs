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
    public class SkladFaction : Script
    {        

        public readonly Vector3 StartMarker = new Vector3(3582.406f, 3672.027f, 33.88863f); // Human Labs
        public readonly Vector3 FinishMarker = new Vector3(462.9848f, -1019.792f, 28.1074f); // LSPD
        public readonly Vector3 Vagos = new Vector3(-1116.123f, -1623.073f, 4.400273f);
        public readonly Vector3 Ballas = new Vector3(102.7764f, -1957.74f, 20.74321f);
        public readonly Vector3 Fame = new Vector3(-27.81647f, -1494.109f, 30.36209f);
        public readonly Vector3 Triads = new Vector3(-688.0639f, -878.2101f, 24.49905f);
        public readonly Vector3 LostMC = new Vector3(979.079f, -142.7521f, 74.23379f);
        public readonly Vector3 EMS = new Vector3(376.7799f, -1445.013f, 29.43156f);
        public readonly Vector3 MW = new Vector3(-2428.8f, 3304.362f, 32.97783f);
        public readonly Vector3 Madraz = new Vector3(1412.921f, 1118.118f, 114.8379f);
        public readonly Vector3 Army = new Vector3(804.0332f, -2134.005f, 29.39897f);


        public ColShape StartColshape;
        public ColShape FinishColshape;
        public ColShape VagosColshape;
        public ColShape BallasColshape;
        public ColShape FameColshape;
        public ColShape TriadsColshape;
        public ColShape LostMCColshape;
        public ColShape EMSColshape;
        public ColShape MWColshape;
        public ColShape MadrazColshape;
        public ColShape ArmyColshape;

        public SkladFaction()
        {
            API.onResourceStart += OnResourceStart;            
        }

        private const string DatabaseValues = "VALUES (@NameFaction, @Pistols, @Rifle, @SMG, @Shotgun, @Melee, @Armor, @Medicals)";
        public static string myConnectionString = "SERVER=localhost;" + "DATABASE=arcadia;" + "UID=root;" + "PASSWORD=qcoluk1ps3;";

        private MenuManager MenuManager = new MenuManager();
        private Menu savedMenu = null;
        private NetHandle Entity;

        #region StartStorageMenus
        private void StorageStartMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
            int Medicals = 0;


            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM sklad_faction_storages WHERE NameFaction = 'Start'";

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
                    Medicals += Database.Reader.GetInt32("Medicals");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "Start";

                menu = new Menu("StartStorage", "Склад Human Labs", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(200, 0, 0, 90),
                    Callback = StorageStart_MenuManager
                };

                MenuItem get_pistol_menu_item = new MenuItem("Загрузить материалы для пистолетов", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_pistol_menu_item);

                MenuItem get_rifle_menu_item = new MenuItem("Загрузить материалы для винтовок", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_rifle_menu_item);

                MenuItem get_smg_menu_item = new MenuItem("Загрузить материалы для SMG", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_smg_menu_item);

                MenuItem get_shotgun_menu_item = new MenuItem("Загрузить материалы для дробовиков", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_shotgun_menu_item);

                MenuItem get_tazer_menu_item = new MenuItem("Загрузить материалы для ближнего боя", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_tazer_menu_item);

                MenuItem get_armor_menu_item = new MenuItem("Загрузить материалы для бронижелетов", "~b~На складе: ~w~" + Convert.ToString(Armor), "get_armor_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_armor_menu_item);

                MenuItem get_extra_menu_item = new MenuItem("~b~Загрузить материалы для медикоментов", "~b~На складе: ~w~" + Convert.ToString(Medicals), "get_medicals_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_extra_menu_item);

                MenuItem CloseMenuItem = new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                };
                menu.Add(CloseMenuItem);
            }
            MenuManager.OpenMenu(client, menu);
        }


        private void StorageStart_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM sklad_faction_storages WHERE NameFaction = 'Start'";

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
            int Medicals = 0;

            string groupName = "FS";

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
                    Medicals += Database.Reader.GetInt32("Medicals");
                }
                Database.Reader.Close();
            }

            bool alreadyhavepistol = false;
            bool alreadyhaverifle = false;
            bool alreadyhavesmg = false;
            bool alreadyhaveshotgun = false;
            bool alreadyhavemelee = false;

            if (itemIndex == 0) // пистолеты
            {
                API.setEntitySyncedData(client, "IS_JOB", false);
                if (API.getEntitySyncedData(client, "IS_JOB") == false)
                {                   
                    if (Pistols != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?pistols", Pistols - 25);
                        Database.command.CommandText = "UPDATE sklad_faction_storages SET Pistols = ?pistols WHERE NameFaction = 'Start'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы со склада");
                        API.setEntitySyncedData(client, "IS_JOB", true);

                    }
                    else API.sendChatMessageToPlayer(client, "~r~На складе закончились материалы!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли материалы!");
            }
            if (itemIndex == 1) // винтовки
            {
                API.setEntitySyncedData(client, "IS_JOB1", false);
                if (API.getEntitySyncedData(client, "IS_JOB1") == false)
                {
                    if (Rifle != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?rifle", Rifle - 25);
                        Database.command.CommandText = "UPDATE sklad_faction_storages SET Rifle = ?rifle WHERE NameFaction = 'Start'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы со склада");
                        API.setEntitySyncedData(client, "IS_JOB1", true);

                    }
                    else API.sendChatMessageToPlayer(client, "~r~На складе закончились материалы!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли материалы!");
            }

            if (itemIndex == 2) // SMG
            {
                API.setEntitySyncedData(client, "IS_JOB2", false);
                if (API.getEntitySyncedData(client, "IS_JOB2") == false)
                {
                    if (SMG != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?smg", SMG - 25);
                        Database.command.CommandText = "UPDATE sklad_faction_storages SET SMG = ?smg WHERE NameFaction = 'Start'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы со склада");
                        API.setEntitySyncedData(client, "IS_JOB2", true);

                    }
                    else API.sendChatMessageToPlayer(client, "~r~На складе закончились материалы!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли материалы!");                
            }
            if (itemIndex == 3) // Дробовики
            {
                API.setEntitySyncedData(client, "IS_JOB3", false);
                if (API.getEntitySyncedData(client, "IS_JOB3") == false)
                {
                    if (Shotgun != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?shotgun", Shotgun - 25);
                        Database.command.CommandText = "UPDATE sklad_faction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'Start'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы со склада");
                        API.setEntitySyncedData(client, "IS_JOB3", true);

                    }
                    else API.sendChatMessageToPlayer(client, "~r~На складе закончились материалы!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли материалы!");
            }
            if (itemIndex == 4) // комплекты
            {
                API.setEntitySyncedData(client, "IS_JOB4", false);
                if (API.getEntitySyncedData(client, "IS_JOB4") == false)
                {
                    if (Melee != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?melee", Melee - 25);
                        Database.command.CommandText = "UPDATE sklad_faction_storages SET Melee = ?melee WHERE NameFaction = 'Start'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы со склада");
                        API.setEntitySyncedData(client, "IS_JOB4", true);

                    }
                    else API.sendChatMessageToPlayer(client, "~r~На складе закончились материалы!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли материалы!");
            }
            if (itemIndex == 5) // броня
            {
                API.setEntitySyncedData(client, "IS_JOB5", false);
                if (API.getEntitySyncedData(client, "IS_JOB5") == false)
                {
                    if (Armor != 0)
                    {
                        Database.command.Parameters.AddWithValue("?armor", Armor - 25);
                        Database.command.CommandText = "UPDATE sklad_faction_storages SET Armor = ?armor WHERE NameFaction = 'Start'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы со склада");
                        API.setEntitySyncedData(client, "IS_JOB5", true);

                    }
                    else API.sendChatMessageToPlayer(client, "~r~На складе закончились материалы!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли материалы!");
            }
            if (itemIndex == 6) // медикаменты
            {
                API.setEntitySyncedData(client, "IS_JOB6", false);
                if (API.getEntitySyncedData(client, "IS_JOB6") == false)
                {
                    if (Medicals != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?medicals", Medicals - 500);
                        Database.command.CommandText = "UPDATE sklad_faction_storages SET Medicals = ?medicals WHERE NameFaction = 'Start'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы со склада");
                        API.setEntitySyncedData(client, "IS_JOB6", true);

                    }
                    else API.sendChatMessageToPlayer(client, "~r~На складе закончились материалы!");
                }
                else API.sendChatMessageToPlayer(client, "~r~Вы уже взяли материалы!");
            }
            if (itemIndex == 7)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }
        #endregion
        #region FinishStorageMenus
        private void StorageFinishMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            int Pistols = 0;
            int Rifle = 0;
            int SMG = 0;
            int Shotgun = 0;
            int Melee = 0;
            int Armor = 0;
            int Medicals = 0;


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
                    Medicals += Database.Reader.GetInt32("Medicals");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameFaction = "Police";

                menu = new Menu("FinishStorage", "Склад LSPD", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(200, 0, 0, 90),
                    Callback = StorageFinish_MenuManager
                };

                MenuItem get_pistol_menu_item = new MenuItem("Разгрузить материалы для пистолетов", "~b~На складе: ~w~" + Convert.ToString(Pistols), "get_pistol_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_pistol_menu_item);

                MenuItem get_rifle_menu_item = new MenuItem("Разгрузить материалы для винтовок", "~b~На складе: ~w~" + Convert.ToString(Rifle), "get_rifle_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_rifle_menu_item);

                MenuItem get_smg_menu_item = new MenuItem("Разгрузить материалы для SMG", "~b~На складе: ~w~" + Convert.ToString(SMG), "get_smg_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_smg_menu_item);

                MenuItem get_shotgun_menu_item = new MenuItem("Разгрузить материалы для дробовиков", "~b~На складе: ~w~" + Convert.ToString(Shotgun), "get_shotgun_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_shotgun_menu_item);

                MenuItem get_tazer_menu_item = new MenuItem("Разгрузить материалы для ближнего боя", "~b~На складе: ~w~" + Convert.ToString(Melee), "get_tazer_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_tazer_menu_item);

                MenuItem get_armor_menu_item = new MenuItem("Разгрузить материалы для бронижелетов", "~b~На складе: ~w~" + Convert.ToString(Armor), "get_armor_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_armor_menu_item);

                MenuItem get_extra_menu_item = new MenuItem("~b~Разгрузить материалы для медикоментов", "~b~На складе: ~w~" + Convert.ToString(Medicals), "get_medicals_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_extra_menu_item);

                MenuItem CloseMenuItem = new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                };
                menu.Add(CloseMenuItem);
            }
            MenuManager.OpenMenu(client, menu);
        }


        private void StorageFinish_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
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
            int Medicals = 0;

            string groupName = "FS";

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
                    Medicals += Database.Reader.GetInt32("Medicals");
                }
                Database.Reader.Close();
            }


            if (itemIndex == 0) // пистолеты
            {


                if (API.getEntitySyncedData(client, "IS_JOB") == true)
                {
                    if (Pistols != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?pistols", Pistols + 25);
                        Database.command.CommandText = "UPDATE faction_storages SET Pistols = ?pistols WHERE NameFaction = 'Police'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы на склад");
                        API.setEntitySyncedData(client, "IS_JOB", false);

                    }
                    else API.sendChatMessageToPlayer(client, "~rВ машине больше нет материалов!");
                }
                else API.sendChatMessageToPlayer(client, "~r~В машине больше нет загруженных материалов!");
            }
            if (itemIndex == 1) // винтовки
            {


                if (API.getEntitySyncedData(client, "IS_JOB1") == true)
                {
                    if (Rifle != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?rifle", Rifle + 25);
                        Database.command.CommandText = "UPDATE faction_storages SET Rifle = ?rifle WHERE NameFaction = 'Police'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы на склад");
                        API.setEntitySyncedData(client, "IS_JOB1", false);

                    }
                    else API.sendChatMessageToPlayer(client, "~rВ машине больше нет материалов!");
                }
                else API.sendChatMessageToPlayer(client, "~r~В машине больше нет загруженных материалов!");
            }
            if (itemIndex == 2) // SMG
            {


                if (API.getEntitySyncedData(client, "IS_JOB2") == true)
                {
                    if (SMG != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?smg", SMG + 25);
                        Database.command.CommandText = "UPDATE faction_storages SET SMG = ?smg WHERE NameFaction = 'Police'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы на склад");
                        API.setEntitySyncedData(client, "IS_JOB2", false);

                    }
                    else API.sendChatMessageToPlayer(client, "~rВ машине больше нет материалов!");
                }
                else API.sendChatMessageToPlayer(client, "~r~В машине больше нет загруженных материалов!");
            }
            if (itemIndex == 3) // Дробовики
            {


                if (API.getEntitySyncedData(client, "IS_JOB3") == true)
                {
                    if (Shotgun != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?shotgun", Shotgun + 25);
                        Database.command.CommandText = "UPDATE faction_storages SET Shotgun = ?shotgun WHERE NameFaction = 'Police'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы на склад");
                        API.setEntitySyncedData(client, "IS_JOB3", false);

                    }
                    else API.sendChatMessageToPlayer(client, "~rВ машине больше нет материалов!");
                }
                else API.sendChatMessageToPlayer(client, "~r~В машине больше нет загруженных материалов!");
            }
            if (itemIndex == 4) // комплекты
            {


                if (API.getEntitySyncedData(client, "IS_JOB4") == true)
                {
                    if (Melee != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?melee", Melee + 25);
                        Database.command.CommandText = "UPDATE faction_storages SET Melee = ?melee WHERE NameFaction = 'Police'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы на склад");
                        API.setEntitySyncedData(client, "IS_JOB4", false);

                    }
                    else API.sendChatMessageToPlayer(client, "~rВ машине больше нет материалов!");
                }
                else API.sendChatMessageToPlayer(client, "~r~В машине больше нет загруженных материалов!");
            }
            if (itemIndex == 5) // броня
            {


                if (API.getEntitySyncedData(client, "IS_JOB5") == true)
                {
                    if (Armor != 0)
                    {
                        Database.command.Parameters.AddWithValue("?armor", Armor + 25);
                        Database.command.CommandText = "UPDATE faction_storages SET Armor = ?armor WHERE NameFaction = 'Police'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы на склад");
                        API.setEntitySyncedData(client, "IS_JOB5", false);

                    }
                    else API.sendChatMessageToPlayer(client, "~rВ машине больше нет материалов!");
                }
                else API.sendChatMessageToPlayer(client, "~r~В машине больше нет загруженных материалов!");
            }
            if (itemIndex == 6) // медикаменты
            {

                if (API.getEntitySyncedData(client, "IS_JOB6") == true)
                {
                    if (Medicals != 0)
                    {
                        Database.connection.Open();
                        Database.command.Parameters.AddWithValue("?medicals", Medicals + 500);
                        Database.command.CommandText = "UPDATE faction_storages SET Medicals = ?medicals WHERE NameFaction = 'Police'";
                        Database.command.ExecuteNonQuery();
                        Database.connection.Close();
                        API.sendChatMessageToPlayer(client, "~g~Вы загрузили материалы на склад");
                        API.setEntitySyncedData(client, "IS_JOB6", false);

                    }
                    else API.sendChatMessageToPlayer(client, "~rВ машине больше нет материалов!");
                }
                else API.sendChatMessageToPlayer(client, "~r~В машине больше нет загруженных материалов!");
            }
            if (itemIndex == 7)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }
        #endregion

        public void OnResourceStart()
        {
            LoadAllStoragesFromDB();

            StartColshape = API.createCylinderColShape(StartMarker, 1f, 3f);
            FinishColshape = API.createCylinderColShape(FinishMarker, 1f, 3f);
            VagosColshape = API.createCylinderColShape(Vagos, 1f, 3f);
            BallasColshape = API.createCylinderColShape(Ballas, 1f, 3f);
            FameColshape = API.createCylinderColShape(Fame, 1f, 3f);
            TriadsColshape = API.createCylinderColShape(Triads, 1f, 3f);
            LostMCColshape = API.createCylinderColShape(LostMC, 1f, 3f);
            EMSColshape = API.createCylinderColShape(EMS, 1f, 3f);
            MWColshape = API.createCylinderColShape(MW, 1f, 3f);
            MadrazColshape = API.createCylinderColShape(Madraz, 1f, 3f);
            ArmyColshape = API.createCylinderColShape(Army, 1f, 3f);

            API.createMarker(27, StartMarker - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад Human Labs", StartMarker, 15f, 3f);
            API.createMarker(27, FinishMarker - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад материалов LSPD", FinishMarker, 15f, 3f);
            API.createMarker(27, Vagos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад материалов Vagos", Vagos, 15f, 3f);
            API.createMarker(27, Ballas - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад материалов Ballas", Ballas, 15f, 3f);
            API.createMarker(27, Fame - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад материалов Famelies", Fame, 15f, 3f);
            API.createMarker(27, Triads - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад материалов Triads", Triads, 15f, 3f);
            API.createMarker(27, LostMC - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад материалов LostMC", LostMC, 15f, 3f);
            API.createMarker(27, EMS - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад материалов EMS", EMS, 15f, 3f);
            API.createMarker(27, MW - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад материалов MW", MW, 15f, 3f);
            API.createMarker(27, Madraz - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад материалов Madraz", Madraz, 15f, 3f);
            API.createMarker(27, Army - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(3f, 3f, 3f), 100, 50, 220, 90);
            API.createTextLabel("Склад материалов Army", Army, 15f, 3f);

            StartColshape.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                int isMerryWeatherfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
                if (isMerryWeatherfaction == 3)
                {
                    if (API.getPlayerVehicleSeat(player) == -1)
                    {
                        StorageStartMenuBuilder(player);
                    }
                }
                else API.sendChatMessageToPlayer(player, "~y~Вы не принадлежите данной фракции~b~");
            };

            StartColshape.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                MenuManager.CloseMenu(player);
            };

            FinishColshape.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                int isMerryWeatherfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;                
                    if (isMerryWeatherfaction == 3)
                    {
                        if (API.getPlayerVehicleSeat(player) == -1)
                        {
                            StorageFinishMenuBuilder(player);
                            return;
                        }
                    }
                    else API.sendChatMessageToPlayer(player, "~y~Вы не принадлежите данной фракции~b~");                
            };
            FinishColshape.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                MenuManager.CloseMenu(player);
            };
        }

        public static readonly List<Storage> StorageList = new List<Storage>();

        public static void LoadAllStoragesFromDB()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            DataTable result = Database.ExecutePreparedStatement("SELECT * FROM sklad_faction_storages", parameters);

            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Storage FactionStorage = new Storage
                    {                       
                        NameFaction = (string)row["NameFaction"],                                              
                        Pistols = (int)row["Pistols"],
                        Rifle = (int)row["Rifle"],
                        SMG = (int)row["SMG"],
                        Shotgun = (int)row["Shotgun"],                        
                        Melee = (int)row["Melee"],
                        Armor = (int)row["Armor"],
                        Medicals = (int)row["Medicals"]
                    };                    
                }
                API.shared.consoleOutput(LogCat.Info, result.Rows.Count + "Складов загружено!");
            }
            else
            {
                API.shared.consoleOutput(LogCat.Info, "Не удалось загрузить склады!");
            }
        }

        public static void AddNewSkladStorage(string NameFaction)
        {

            const int Pistols = 1000;
            const int Rifle = 1000;
            const int SMG = 1500;
            const int Shotgun = 2500;
            const int Melee = 2500;
            const int Armor = 2500;            
            const int Medicals = 2500;

            switch (NameFaction)
            {
                case "Start":

                    Dictionary<string, string> parametersStatr = new Dictionary<string, string>
                    {                        
                        { "@NameFaction", NameFaction },                        
                        { "@Pistols",  Pistols.ToString() },
                        { "@Rifle",  Rifle.ToString() },
                        { "@SMG",  SMG.ToString() },
                        { "@Shotgun",  Shotgun.ToString() },
                        { "@Melee",  Melee.ToString() },
                        { "@Armor",  Armor.ToString() },
                        { "@Medicals",  Medicals.ToString() }
                    };

                    DataTable resultStatr = Database.ExecutePreparedStatement("INSERT INTO sklad_faction_storages (NameFaction, Pistols, Rifle, SMG, Shotgun, Melee, Armor, Medicals) " +
                        DatabaseValues, parametersStatr);

                    break;
            }
        }
        public static void ReloadSkladStorages()
        {
            StorageList.ForEach(FactionStorage =>
            {
                API.shared.deleteColShape(FactionStorage.ColShape);
            });
            StorageList.Clear();
            LoadAllStoragesFromDB();
        }

        [Command("addsklad")]
        public void AddStorageCommand(Client sender, string NameFaction)
        {
            AddNewSkladStorage(NameFaction);
            API.sendChatMessageToPlayer(sender, "~g~Склад успешно создан");
        }
        [Command("reloadsklad")]
        public void ReloadStoragesCommand(Client sender)
        {
            ReloadSkladStorages();
            API.sendChatMessageToPlayer(sender, "~g~Склады перезагружены!");
        }
    }
}
