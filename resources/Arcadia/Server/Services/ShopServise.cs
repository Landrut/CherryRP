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
    public class ShopService : Script
    {

        public ShopService()
        {
            API.onResourceStart += OnResourceStart;
            API.onEntityEnterColShape += ColShapeTrigger;
        }

        public void OnResourceStart()
        {
            LoadAllStoragesFromDB();
        }

        private const string DatabaseValues = "VALUES (@NameShop, @Position, @eCola, @Sprunk, @Schokoriegel, @Wasser, @Chips, @Repair_Kit, @Bottle, @Flashlight, @Beer, @Caffe, @Benzin_Kanister, @Tabak, @Gamburger, @Viske)";
        public static string myConnectionString = "SERVER=localhost;" + "DATABASE=arcadia;" + "UID=root;" + "PASSWORD=qcoluk1ps3;";

        private MenuManager MenuManager = new MenuManager();
        private Menu savedMenu = null;

        #region Shop
        private void Shop_MenuManager(Client client)
        {
            Menu menu = savedMenu;

            int eCola = 0;
            int Sprunk = 0;
            int Schokoriegel = 0;
            int Wasser = 0;
            int Chips = 0;
            int Repair_Kit = 0;
            int Bottle = 0;
            int Flashlight = 0;
            int Beer = 0;
            int Caffe = 0;
            int Benzin_Kanister = 0;
            int Tabak = 0;
            int Gamburger = 0;
            int Viske = 0;


        Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM shop_storages WHERE NameShop = '24Shop'";

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {                   
                     eCola += Database.Reader.GetInt32("eCola");
                     Sprunk += Database.Reader.GetInt32("Sprunk");
                     Schokoriegel += Database.Reader.GetInt32("Schokoriegel");
                     Wasser += Database.Reader.GetInt32("Wasser");
                     Chips += Database.Reader.GetInt32("Chips");
                     Repair_Kit += Database.Reader.GetInt32("Repair_Kit");
                     Bottle += Database.Reader.GetInt32("Bottle");
                     Flashlight += Database.Reader.GetInt32("Flashlight");
                     Beer += Database.Reader.GetInt32("Beer");
                     Caffe += Database.Reader.GetInt32("Caffe");
                     Benzin_Kanister += Database.Reader.GetInt32("Benzin_Kanister");
                     Tabak += Database.Reader.GetInt32("Tabak");
                     Gamburger += Database.Reader.GetInt32("Gamburger");
                     Viske += Database.Reader.GetInt32("Viske");
                }
                Database.Reader.Close();
            }

            if (menu == null)
            {
                string State;
                string NameShop = "24Shop";

                menu = new Menu("Shop", "Магазин 24/7", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(200, 0, 0, 90),
                    Callback = Shop_MenuManager
                };

                MenuItem get_eCola_menu_item = new MenuItem("Купить eCola", "~b~На складе: ~w~" + Convert.ToString(eCola), "get_eCola_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_eCola_menu_item);

                MenuItem get_Sprunk_menu_item = new MenuItem("Купить Sprunk", "~b~На складе: ~w~" + Convert.ToString(Sprunk), "get_Sprunk_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Sprunk_menu_item);

                MenuItem get_Schokoriegel_menu_item = new MenuItem("Купить шоколад", "~b~На складе: ~w~" + Convert.ToString(Schokoriegel), "get_Schokoriegel_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Schokoriegel_menu_item);

                MenuItem get_Wasser_menu_item = new MenuItem("Купить Воды", "~b~На складе: ~w~" + Convert.ToString(Wasser), "get_Wasser_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Wasser_menu_item);

                MenuItem get_Chips_menu_item = new MenuItem("Купить чипсы", "~b~На складе: ~w~" + Convert.ToString(Chips), "get_Chips_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Chips_menu_item);

                MenuItem get_Repair_Kit_menu_item = new MenuItem("Купить Рем. комплект", "~b~На складе: ~w~" + Convert.ToString(Repair_Kit), "get_Repair_Kit_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Repair_Kit_menu_item);

                MenuItem get_Bottle_menu_item = new MenuItem("Купить бутылку", "~b~На складе: ~w~" + Convert.ToString(Bottle), "get_Bottle_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Bottle_menu_item);

                MenuItem get_Flashlight_menu_item = new MenuItem("Купить фонарик", "~b~На складе: ~w~" + Convert.ToString(Flashlight), "get_Flashlight_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Flashlight_menu_item);

                MenuItem get_Beer_menu_item = new MenuItem("Купить пиво", "~b~На складе: ~w~" + Convert.ToString(Beer), "get_Beer_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Beer_menu_item);

                MenuItem get_Caffe_menu_item = new MenuItem("Купить кофе", "~b~На складе: ~w~" + Convert.ToString(Caffe), "get_Caffe_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Caffe_menu_item);

                MenuItem get_Benzin_Kanister_menu_item = new MenuItem("Купить канистру", "~b~На складе: ~w~" + Convert.ToString(Benzin_Kanister), "get_Benzin_Kanister_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Benzin_Kanister_menu_item);

                MenuItem get_Tabak_menu_item = new MenuItem("Купить сигареты", "~b~На складе: ~w~" + Convert.ToString(Tabak), "get_Tabak_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Tabak_menu_item);

                MenuItem get_Gamburger_menu_item = new MenuItem("Купить гамбургер", "~b~На складе: ~w~" + Convert.ToString(Gamburger), "get_Gamburger_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Gamburger_menu_item);

                MenuItem get_Viske_menu_item = new MenuItem("Купить виски", "~b~На складе: ~w~" + Convert.ToString(Viske), "get_Viske_menu_item")
                {
                    ExecuteCallback = true
                };
                menu.Add(get_Viske_menu_item);

                MenuItem CloseMenuItem = new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                };
                menu.Add(CloseMenuItem);
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void Shop_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            int eCola = 0;
            int Sprunk = 0;
            int Schokoriegel = 0;
            int Wasser = 0;
            int Chips = 0;
            int Repair_Kit = 0;
            int Bottle = 0;
            int Flashlight = 0;
            int Beer = 0;
            int Caffe = 0;
            int Benzin_Kanister = 0;
            int Tabak = 0;
            int Gamburger = 0;
            int Viske = 0;

            int prise_eCola = 0;
            int prise_Sprunk = 0;
            int prise_Schokoriegel = 0;
            int prise_Wasser = 0;
            int prise_Chips = 0;
            int prise_Repair_Kit = 0;
            int prise_Bottle = 0;
            int prise_Flashlight = 0;
            int prise_Beer = 0;
            int prise_Caffe = 0;
            int prise_Benzin_Kanister = 0;
            int prise_Tabak = 0;
            int prise_Gamburger = 0;
            int prise_Viske = 0;


            Database.connection = new MySqlConnection(myConnectionString);
            Database.command = Database.connection.CreateCommand();
            Database.command.CommandText = "SELECT * FROM shop_storages WHERE NameShop = '24Shop'";

            using (Database.connection)
            {
                Database.connection.Open();
                Database.Reader = Database.command.ExecuteReader();

                while (Database.Reader.Read())
                {
                    eCola += Database.Reader.GetInt32("eCola");
                    Sprunk += Database.Reader.GetInt32("Sprunk");
                    Schokoriegel += Database.Reader.GetInt32("Schokoriegel");
                    Wasser += Database.Reader.GetInt32("Wasser");
                    Chips += Database.Reader.GetInt32("Chips");
                    Repair_Kit += Database.Reader.GetInt32("Repair_Kit");
                    Bottle += Database.Reader.GetInt32("Bottle");
                    Flashlight += Database.Reader.GetInt32("Flashlight");
                    Beer += Database.Reader.GetInt32("Beer");
                    Caffe += Database.Reader.GetInt32("Caffe");
                    Benzin_Kanister += Database.Reader.GetInt32("Benzin_Kanister");
                    Tabak += Database.Reader.GetInt32("Tabak");
                    Gamburger += Database.Reader.GetInt32("Gamburger");
                    Viske += Database.Reader.GetInt32("Viske");
                }
                Database.Reader.Close();
            }

            if (itemIndex == 0)
            {
                if (eCola != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?eCola", eCola - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET eCola = ?eCola WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили банку eCola");
                    Shop_MenuManager(client);
                    prise_eCola = 150;
                    PlayerFunctions.Player.ChangeMoney(client, -prise_eCola);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 1)
            {
                if (Sprunk != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Sprunk", Sprunk - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Sprunk = ?Sprunk WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили банку Sprunk");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 2)
            {
                if (Schokoriegel != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Schokoriegel", Schokoriegel - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Schokoriegel = ?Schokoriegel WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили шоколад");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 3)
            {
                if (Wasser != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Wasser", Wasser - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Schokoriegel = ?Wasser WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили бутылку воды");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 4)
            {
                if (Chips != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Chips", Chips - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Chips = ?Chips WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили пачку чипсов");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 5)
            {
                if (Repair_Kit != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Repair_Kit", Repair_Kit - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Repair_Kit = ?Repair_Kit WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили ремонтный комплект");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 6)
            {
                if (Bottle != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Bottle", Bottle - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Bottle = ?Bottle WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили бутылку");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 7)
            {
                if (Flashlight != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Flashlight", Flashlight - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Flashlight = ?Flashlight WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили фонарик");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 8)
            {
                if (Beer != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Beer", Beer - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Beer = ?Beer WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили бутылку пива");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 9)
            {
                if (Caffe != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Caffe", Caffe - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Caffe = ?Caffe WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили стакан коффе");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 10)
            {
                if (Benzin_Kanister != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Benzin_Kanister", Benzin_Kanister - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Benzin_Kanister = ?Benzin_Kanister WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили канистру для бензина");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 11)
            {
                if (Tabak != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Tabak", Tabak - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Tabak = ?Tabak WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили пачку сигарет");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 12)
            {
                if (Gamburger != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Gamburger", Gamburger - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Gamburger = ?Gamburger WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили гамбургер");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }

            else if (itemIndex == 13)
            {
                if (Viske != 0)
                {
                    Database.connection.Open();
                    Database.command.Parameters.AddWithValue("?Viske", Viske - 1);
                    Database.command.CommandText = "UPDATE shop_storages SET Viske = ?Viske WHERE NameShop = '24Shop'";
                    Database.command.ExecuteNonQuery();
                    Database.connection.Close();
                    API.sendChatMessageToPlayer(client, "~g~Вы купили бутылку вискарика");
                    Shop_MenuManager(client);
                    PlayerFunctions.Player.ChangeMoney(client, -150);
                    return;
                }
                else API.sendChatMessageToPlayer(client, "~r~На складе закончились товар.");
            }


            if (itemIndex == 14)
            {
                MenuManager.CloseMenu(client);
                return;
            }
        }
        #endregion       

        private void ColShapeTrigger(ColShape colShape, NetHandle entity)
        {
            var player = API.getPlayerFromHandle(entity);
            if (colShape != null && colShape.getData("IS_SHOP_STORAGE") == true)
            {
                Shop_MenuManager(player);
            }
            else return;
        }

        public static readonly List<ShopStorage> ShopStorageList = new List<ShopStorage>();

        public static void LoadAllStoragesFromDB()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            DataTable result = Database.ExecutePreparedStatement("SELECT * FROM shop_storages", parameters);

            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    ShopStorage ShopStorage = new ShopStorage
                    {
                                             
                        NameShop = (string)row["NameShop"],
                        Position = JsonConvert.DeserializeObject<Vector3>((string)row["Position"]),
                        eCola = (int)row["eCola"],
                        Sprunk = (int)row["Sprunk"],
                        Schokoriegel = (int)row["Schokoriegel"],
                        Wasser = (int)row["Wasser"],
                        Chips = (int)row["Chips"],
                        Repair_Kit = (int)row["Repair_Kit"],
                        Bottle = (int)row["Bottle"],
                        Flashlight = (int)row["Flashlight"],
                        Beer = (int)row["Beer"],
                        Caffe = (int)row["Caffe"],
                        Benzin_Kanister = (int)row["Benzin_Kanister"],
                        Tabak = (int)row["Tabak"],
                        Gamburger = (int)row["Gamburger"],
                        Viske = (int)row["Viske"]
                    };

                    Marker shopmarker = API.shared.createMarker(1, ShopStorage.Position - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
                    API.shared.createTextLabel("Магазин " + ShopStorage.NameShop, ShopStorage.Position, 15f, 0.65f);

                    ColShape ShopStorageColshape = API.shared.createCylinderColShape(ShopStorage.Position, 0.50f, 1f);                    
                    ShopStorageColshape.setData("IS_SHOP_STORAGE", true);
                    ShopStorage.ColShape = ShopStorageColshape;

                    ShopStorageList.Add(ShopStorage);
                }
                API.shared.consoleOutput(LogCat.Info, result.Rows.Count + "Складов загружено!");
            }
            else
            {
                API.shared.consoleOutput(LogCat.Info, "Не удалось загрузить склады!");
            }
        }

        public static void AddNewShop(string NameShop, Vector3 position)
        {

            const int eCola = 50;
            const int Sprunk = 50;
            const int Schokoriegel = 50;
            const int Wasser = 50;
            const int Chips = 50;
            const int Repair_Kit = 50;
            const int Bottle = 50;
            const int Flashlight = 50;
            const int Beer = 50;
            const int Caffe = 50;
            const int Benzin_Kanister = 50;
            const int Tabak = 50;
            const int Gamburger = 50;
            const int Viske = 50;

            switch (NameShop)
            {

                case "24Shop":
                    Dictionary<string, string> parameters24Shop = new Dictionary<string, string>
                    {
                        { "@NameShop", NameShop },
                        { "@Position", JsonConvert.SerializeObject(position) },
                        { "@eCola",  eCola.ToString() },
                        { "@Sprunk",  Sprunk.ToString() },
                        { "@Schokoriegel",  Schokoriegel.ToString() },
                        { "@Wasser",  Wasser.ToString() },
                        { "@Chips",  Chips.ToString() },
                        { "@Repair_Kit",  Repair_Kit.ToString() },
                        { "@Bottle",  Bottle.ToString() },
                        { "@Flashlight",  Flashlight.ToString() },
                        { "@Beer",  Beer.ToString() },
                        { "@Caffe",  Caffe.ToString() },
                        { "@Benzin_Kanister",  Benzin_Kanister.ToString() },
                        { "@Tabak",  Tabak.ToString() },
                        { "@Gamburger",  Gamburger.ToString() },
                        { "@Viske",  Viske.ToString() }

                    };

                    DataTable result24Shop = Database.ExecutePreparedStatement("INSERT INTO shop_storages (NameShop, Position, eCola, Sprunk, Schokoriegel, Wasser, Chips, Repair_Kit, Bottle, Flashlight, Beer, Caffe, Benzin_Kanister, Tabak, Gamburger, Viske) " +
                        "VALUES (@NameShop, @Position, @eCola, @Sprunk, @Schokoriegel, @Wasser, @Chips, @Repair_Kit, @Bottle, @Flashlight, @Beer, @Caffe, @Benzin_Kanister, @Tabak, @Gamburger, @Viske)", parameters24Shop);

                    break;
            }
        }

            public static void ReloadShop()
            {
            ShopStorageList.ForEach(ShopStorage =>
                {
                    API.shared.deleteColShape(ShopStorage.ColShape);
                });
            ShopStorageList.Clear();
                LoadAllStoragesFromDB();
            }

        [Command("addshop")]
        public void AddStorageCommand(Client sender, string NameShop)
        {
            AddNewShop(NameShop, sender.position);
            API.sendChatMessageToPlayer(sender, "~g~Магазин успешно создан");
        }
        [Command("ReloadShop")]
        public void ReloadShopCommand(Client sender)
        {
            ReloadShop();
            API.sendChatMessageToPlayer(sender, "~g~Магазины перезагружены!");
        }
    }
}
