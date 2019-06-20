#region Bar
private void Bar_MenuManager(Client client)
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
    Database.command.CommandText = "SELECT * FROM shop_storages WHERE NameShop = 'Bar'";

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
        string NameShop = "Bar";

        menu = new Menu("Bar", "Барная стойка", "~y~Выберите действие", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
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

private void Bar_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
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
    Database.command.CommandText = "SELECT * FROM shop_storages WHERE NameShop = 'Bar'";

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
            Database.command.CommandText = "UPDATE shop_storages SET eCola = ?eCola WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Sprunk = ?Sprunk WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Schokoriegel = ?Schokoriegel WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Schokoriegel = ?Wasser WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Chips = ?Chips WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Repair_Kit = ?Repair_Kit WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Bottle = ?Bottle WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Flashlight = ?Flashlight WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Beer = ?Beer WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Caffe = ?Caffe WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Benzin_Kanister = ?Benzin_Kanister WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Tabak = ?Tabak WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Gamburger = ?Gamburger WHERE NameShop = 'Bar'";
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
            Database.command.CommandText = "UPDATE shop_storages SET Viske = ?Viske WHERE NameShop = 'Bar'";
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