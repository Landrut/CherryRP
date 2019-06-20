using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//
using CherryMPServer.Constant;
//
using CherryMPServer;
using CherryMPShared;
//;
using Newtonsoft.Json;
using PlayerFunctions;

namespace HouseScript
{
    #region House Class
    public class House
    {
        public Guid ID { get; }
        public string Owner { get; private set; }
        public int Type { get; private set; }
        public Vector3 Position { get; }
        public int Price { get; private set; }
        public bool Locked { get; private set; }
        public int Dimension { get; set; }

        // customization
        public string Name { get; private set; }
        //public List<HouseFurniture> Furnitures { get; }

        // storage
        public long Money { get; private set; }
        public List<HouseWeapon> Weapons { get; }

        // entities
        [JsonIgnore]
        private Blip Blip;

        [JsonIgnore]
        private Marker Marker;

        [JsonIgnore]
        private ColShape ColShape;

        [JsonIgnore]
        private TextLabel Label;

        // misc
        [JsonIgnore]
        private List<NetHandle> PlayersInside = new List<NetHandle>();

        [JsonIgnore]
        private Client OwnerHandle;

        [JsonIgnore]
        private DateTime LastSave;

        public House(Guid id, string owner, int type, Vector3 position, int price, bool locked, string name = "", long money = 0, List<HouseWeapon> weapons = null)
        {
            ID = id;
            Owner = owner;
            Type = type;
            Position = position;
            Price = price;
            Locked = locked;

            Name = name;
            //Furnitures = (furnitures == null) ? new List<HouseFurniture>() : furnitures;

            Money = money;
            Weapons = (weapons == null) ? new List<HouseWeapon>() : weapons;

            // create blip
            Blip = API.shared.createBlip(position);

            if (string.IsNullOrEmpty(owner))
            {
                Blip.sprite = 374;
                Blip.color = 3;
                //Blip.scale = 0.99f;
                Blip.name = "Продается дом";
            }
            else
            {
                Blip.sprite = 40;
                //Blip.scale = 0.99f;
                Blip.color = 75;
                Blip.name = "Дом игрока";
            }

            //Blip.color = 0;
            Blip.scale = 0.65f;
            Blip.shortRange = true;
            Blip.setSyncedData("PlayersInside", 0);

            // create marker
            Marker = API.shared.createMarker(1, position - new Vector3(0.0, 0.0, 1.0), new Vector3(), new Vector3(), new Vector3(1.0, 1.0, 0.5), 150, 64, 196, 255);

            // create colshape
            ColShape = API.shared.createCylinderColShape(position, 0.85f, 0.85f);
            ColShape.onEntityEnterColShape += (s, ent) =>
            {
                Client player;

                if ((player = API.shared.getPlayerFromHandle(ent)) != null)
                {
                    player.setData("HouseMarker_ID", ID);
                    player.triggerEvent("ShowHouseText", 1);
                }
            };

            ColShape.onEntityExitColShape += (s, ent) =>
            {
                Client player;

                if ((player = API.shared.getPlayerFromHandle(ent)) != null)
                {
                    player.resetData("HouseMarker_ID");
                    player.triggerEvent("ShowHouseText", 0);
                }
            };

            // create text label
            Label = API.shared.createTextLabel("House", position, 15f, 0.65f);
            UpdateLabel();

            OwnerHandle = null;
        }

        private void UpdateLabel()
        {
            if (string.IsNullOrEmpty(Owner))
            {
                Label.text = string.Format("~b~Продается дом~n~~n~~w~Интерьер: ~b~{0}~n~~w~Цена: ~g~${1:n0}", HouseTypes.HouseTypeList[Type].Name, Price);
            }
            else
            {
                Label.text = string.Format("~b~Дом~n~~n~~w~{0}~n~~w~Владелец: ~b~{1}~n~{2}", Name, Owner, ((Locked) ? "~r~Закрыт" : "~g~Открыт"));
            }
        }

        private void UpdateBlip()
        {
            int count = PlayersInside.Count;
            Blip.setSyncedData("PlayersInside", count);

            if (count < 1)
            {
                if (Blip.sprite != 40)
                {
                    Blip.sprite = 40;

                    if (OwnerHandle != null) UpdateBlipForOwner();
                }

                API.shared.sendNativeToAllPlayers(Hash.HIDE_NUMBER_ON_BLIP, Blip.handle);
            }
            else
            {
                if (Blip.sprite != 417)
                {
                    Blip.sprite = 417;

                    if (OwnerHandle != null) UpdateBlipForOwner();
                }

                API.shared.sendNativeToAllPlayers(Hash.SHOW_NUMBER_ON_BLIP, Blip.handle, count);
            }
        }

        public void SetOwner(Client player)
        {
            if (OwnerHandle != null) OwnerHandle.triggerEvent("ResetHouseBlip", Blip.handle);
            Owner = (player == null) ? string.Empty : player.name;

            Blip.sprite = (player == null) ? 350 : 40;
            Blip.color = 0;
            Blip.shortRange = true;

            SetOwnerHandle(player);
            UpdateLabel();
            Save();
        }

        public void SetOwnerHandle(Client player)
        {
            OwnerHandle = player;
            if (player != null) player.triggerEvent("UpdateHouseBlip", Blip.handle);
        }

        public void UpdateBlipForOwner()
        {
            if (OwnerHandle == null) return;
            OwnerHandle.triggerEvent("UpdateHouseBlip", Blip.handle);
        }

        public void SetName(string new_name)
        {
            Name = new_name;

            UpdateLabel();
            Save();
        }

        public void SetLock(bool locked)
        {
            Locked = locked;

            UpdateLabel();
            Save();
        }

        public void SetType(int new_type)
        {
            Type = new_type;

            UpdateLabel();
            Save();
        }

        public void SetPrice(int new_price)
        {
            Price = new_price;

            UpdateLabel();
            Save();
        }

        public void ChangeMoney(int amount)
        {
            Money += amount;

            Save();
        }

        public void SendPlayer(Client player)
        {
            player.position = HouseTypes.HouseTypeList[Type].Position;
            player.dimension = Dimension;
            player.setData("InsideHouse_ID", ID);

            if (!PlayersInside.Contains(player.handle)) PlayersInside.Add(player.handle);
            UpdateBlip();
        }

        public void RemovePlayer(Client player, bool set_pos = true)
        {
            if (set_pos)
            {
                player.position = Position;
                player.dimension = 0;
            }

            player.resetData("InsideHouse_ID");

            if (PlayersInside.Contains(player.handle)) PlayersInside.Remove(player.handle);
            UpdateBlip();
        }

        public void RemoveAllPlayers(bool exit = false)
        {
            for (int i = PlayersInside.Count - 1; i >= 0; i--)
            {
                Client player = API.shared.getEntityFromHandle<Client>(PlayersInside[i]);

                if (player != null)
                {
                    player.position = Position;
                    player.dimension = 0;

                    player.resetData("InsideHouse_ID");
                }

                PlayersInside.RemoveAt(i);
            }

            if (!exit) UpdateBlip();
        }

        public void Save(bool force = false)
        {
            if (!force && DateTime.Now.Subtract(LastSave).TotalSeconds < Main.SAVE_INTERVAL) return;

            File.WriteAllText(Main.HOUSE_SAVE_DIR + Path.DirectorySeparatorChar + ID + ".json", JsonConvert.SerializeObject(this, Formatting.Indented));
            LastSave = DateTime.Now;
        }

        public void Destroy(bool exit = false)
        {
            //foreach (HouseFurniture furniture in Furnitures) furniture.Destroy();
            RemoveAllPlayers(exit);

            Blip.delete();
            Marker.delete();
            API.shared.deleteColShape(ColShape);
            Label.delete();
        }
    }
    #endregion

    public class Main : Script
    {
        // settings
        public static string HOUSE_SAVE_DIR = "Server/Data/HouseData";
        public static int PLAYER_HOUSE_LIMIT = 1;
        public static int HOUSE_MONEY_LIMIT = 200000;
        public static int HOUSE_WEAPON_LIMIT = 3;
        public static int HOUSE_FURNITURE_LIMIT = 5;
        public static bool RESET_DIMENSION_ON_DEATH = true;
        public static int SAVE_INTERVAL = 120;

        public static List<House> Houses = new List<House>();
        public static int DimensionID = 1;

        public Main()
        {
            API.onResourceStart += House_Init;

            API.onPlayerFinishedDownload += House_PlayerJoin;
            API.onClientEventTrigger += House_ClientEvent;
            API.onPlayerDeath += House_PlayerDeath;
            API.onPlayerDisconnected += House_PlayerLeave;

            API.onResourceStop += House_Exit;
        }

        #region Methods
        public static Guid GetGuid()
        {
            Guid new_guid;

            do
            {
                new_guid = Guid.NewGuid();
            } while (Houses.Count(h => h.ID == new_guid) > 0);

            return new_guid;
        }

        public void RemovePlayerFromHouseList(Client player)
        {
            if (player.hasData("InsideHouse_ID"))
            {
                House house = Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                if (house == null) return;

                house.RemovePlayer(player, false);
            }
        }
        #endregion

        #region Events
        public void House_Init()
        {
            DimensionID = 1;

            // load settings
            if (API.hasSetting("houseDirName")) HOUSE_SAVE_DIR = API.getSetting<string>("houseDirName");

            HOUSE_SAVE_DIR = API.getResourceFolder() + Path.DirectorySeparatorChar + HOUSE_SAVE_DIR;
            if (!Directory.Exists(HOUSE_SAVE_DIR)) Directory.CreateDirectory(HOUSE_SAVE_DIR);

            if (API.hasSetting("playerHouseLimit")) PLAYER_HOUSE_LIMIT = API.getSetting<int>("playerHouseLimit");
            if (API.hasSetting("houseMoneyLimit")) HOUSE_MONEY_LIMIT = API.getSetting<int>("houseMoneyLimit");
            if (API.hasSetting("houseWeaponLimit")) HOUSE_WEAPON_LIMIT = API.getSetting<int>("houseWeaponLimit");
            if (API.hasSetting("houseFurnitureLimit")) HOUSE_FURNITURE_LIMIT = API.getSetting<int>("houseFurnitureLimit");
            if (API.hasSetting("resetDimensionOnDeath")) RESET_DIMENSION_ON_DEATH = API.getSetting<bool>("resetDimensionOnDeath");
            if (API.hasSetting("saveInterval")) SAVE_INTERVAL = API.getSetting<int>("saveInterval");

            API.consoleOutput("-> Лимит домов игрока: {0}", ((PLAYER_HOUSE_LIMIT == 0) ? "Отключен" : PLAYER_HOUSE_LIMIT.ToString()));
            API.consoleOutput("-> Лимит сейфа в домах: ${0:n0}", HOUSE_MONEY_LIMIT);
            API.consoleOutput("-> Лимит оружия в домах: {0}", ((HOUSE_WEAPON_LIMIT == 0) ? "Отключен" : HOUSE_WEAPON_LIMIT.ToString()));
            API.consoleOutput("-> Лимит мебели в домах: {0}", ((HOUSE_FURNITURE_LIMIT == 0) ? "Отключен" : HOUSE_FURNITURE_LIMIT.ToString()));
            API.consoleOutput("-> Удаление вирт. мира при смерти: {0}", ((RESET_DIMENSION_ON_DEATH) ? "Включен" : "Отключен"));
            API.consoleOutput("-> Интервал сохранения: {0}", TimeSpan.FromSeconds(SAVE_INTERVAL).ToString(@"hh\:mm\:ss"));

            // load houses
            foreach (string file in Directory.EnumerateFiles(HOUSE_SAVE_DIR, "*.json"))
            {
                House house = JsonConvert.DeserializeObject<House>(File.ReadAllText(file));
                house.Dimension = DimensionID;
                //foreach (HouseFurniture furniture in house.Furnitures) furniture.Create(DimensionID);

                Houses.Add(house);
                DimensionID++;
            }

            API.consoleOutput("Загружено {0} домов.", Houses.Count);
        }

        public void House_PlayerJoin(Client player)
        {
            API.delay(2000, true, () => { foreach (House house in Houses.Where(h => h.Owner == player.name)) house.SetOwnerHandle(player); });
        }

        public void House_ClientEvent(Client player, string event_name, params object[] args)
        {
            switch (event_name)
            {
                case "HouseInteract":
                    {
                        if (!player.hasData("HouseMarker_ID")) return;

                        House house = Houses.FirstOrDefault(h => h.ID == player.getData("HouseMarker_ID"));
                        if (house == null) return;

                        if (string.IsNullOrEmpty(house.Owner))
                        {
                            // not owned house
                            player.triggerEvent("House_PurchaseMenu", API.toJson(new { Interior = HouseTypes.HouseTypeList[house.Type].Name, Price = house.Price }));
                        }
                        else
                        {
                            // owned house
                            if (house.Locked)
                            {
                                if (house.Owner == player.name)
                                {
                                    house.SendPlayer(player);
                                }
                                else
                                {
                                    player.sendNotification("Ошибка\n~r~Это может делать только владелец.");
                                }
                            }
                            else
                            {
                                house.SendPlayer(player);
                            }
                        }

                        break;
                    }

                case "HousePurchase":
                    {
                        if (!player.hasData("HouseMarker_ID")) return;

                        House house = Houses.FirstOrDefault(h => h.ID == player.getData("HouseMarker_ID"));
                        if (house == null) return;

                        if (!string.IsNullOrEmpty(house.Owner))
                        {
                            player.sendNotification("Ошибка\n~r~Этот дом приобретён.");
                            return;
                        }

                        if (house.Price > PlayerFunctions.Player.GetMoney(player))
                        {
                            player.sendNotification("Ошибка\n~r~Вы не можете позволить себе этот дом.");
                            return;
                        }

                        if (PLAYER_HOUSE_LIMIT > 0 && Houses.Count(h => h.Owner == player.name) >= PLAYER_HOUSE_LIMIT)
                        {
                            player.sendNotification("Ошибка\n~r~Вы не можете иметь больше домов.");
                            return;
                        }

                        player.sendNotification("Дом приобретен\n~g~Поздравляем, вы приобрели этот дом!");

                        house.SetLock(true);
                        house.SetOwner(player);
                        house.SendPlayer(player);

                        PlayerFunctions.Player.ChangeMoney(player, -house.Price);
                        break;
                    }

                case "HouseMenu":
                    {
                        if (!player.hasData("InsideHouse_ID")) return;

                        House house = Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка\n~r~Только владелец имеет доступ к меню дома.");
                            return;
                        }

                        player.triggerEvent("HouseMenu", API.toJson(house));
                        break;
                    }

                case "HouseSetName":
                    {
                        if (!player.hasData("InsideHouse_ID") || args.Length < 1) return;

                        House house = Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка\n~r~Это может делать только владелец.");
                            return;
                        }

                        string new_name = args[0].ToString();
                        if (new_name.Length > 32)
                        {
                            player.sendNotification("Ошибка\n~r~Имя дома не может быть больше 32-ух символов.");
                            return;
                        }

                        house.SetName(new_name);
                        player.sendNotification(string.Format("Успешно\n~g~Имя дома изменено на: ~w~\"{0}\"", new_name));
                        break;
                    }

                case "HouseSetLock":
                    {
                        if (!player.hasData("InsideHouse_ID") || args.Length < 1) return;

                        House house = Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка/n~r~Это может делать только владелец.");
                            return;
                        }

                        bool new_state = Convert.ToBoolean(args[0]);
                        house.SetLock(new_state);

                        player.sendNotification(((new_state) ? "Успешно/n~g~Дом закрыт." : "~g~Дом открыт."));
                        break;
                    }

                case "HouseSafe":
                    {
                        if (!player.hasData("InsideHouse_ID") || args.Length < 2) return;

                        House house = Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка/n~r~Это может делать только владелец.");
                            return;
                        }

                        int type = Convert.ToInt32(args[0]);
                        int amount = 0;

                        if (!int.TryParse(args[1].ToString(), out amount))
                        {
                            player.sendNotification("Ошибка/n~r~Invalid amount.");
                            return;
                        }

                        if (amount < 1) return;
                        if (type == 0)
                        {
                            if (PlayerFunctions.Player.GetMoney(player) < amount)
                            {
                                player.sendNotification("Ошибка/n~r~У вас недостаточно денег.");
                                return;
                            }

                            if (house.Money + amount > HOUSE_MONEY_LIMIT)
                            {
                                player.sendNotification("Ошибка/n~r~Достигнут лимит наличия денег в доме.");
                                return;
                            }

                            PlayerFunctions.Player.ChangeMoney(player, -amount);

                            house.ChangeMoney(amount);
                            player.sendNotification(string.Format("Успешно/n~g~Вы положили ${0:n0} в сейф.", amount));
                            player.triggerEvent("HouseUpdateSafe", API.toJson(new { Money = house.Money }));
                        }
                        else
                        {
                            if (house.Money < amount)  
                            {
                                player.sendNotification("Ошибка/n~r~В сейфе нет столько денег.");
                                return;
                            }

                            PlayerFunctions.Player.ChangeMoney(player, +amount);

                            house.ChangeMoney(-amount);
                            player.sendNotification(string.Format("Успешно/n~g~Вы взяли ${0:n0} из сейфа.", amount));
                            player.triggerEvent("HouseUpdateSafe", API.toJson(new { Money = house.Money }));
                        }

                        break;
                    }

                case "HouseSell":
                    {
                        if (!player.hasData("InsideHouse_ID")) return;

                        House house = Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка/n~r~Это может делать только владелец.");
                            return;
                        }

                        if (house.Money > 0)
                        {
                            player.sendNotification("Ошибка/n~r~Заберите деньги из сейфа перед его продажей.");
                            return;
                        }

                        if (house.Weapons.Count > 0)
                        {
                            player.sendNotification("Ошибка/n~r~Заберите оружие из оружейного шкафчика перед продажей дома.");
                            return;
                        }

                        //if (house.Furnitures.Count > 0)
                        //{
                        //    player.sendNotification("Ошибка/n~r~Продайте мебель перед продажей дома.");
                        //    return;
                        //}

                        int price = (int)Math.Round(house.Price * 0.8);
                        PlayerFunctions.Player.ChangeMoney(player, +price);

                        house.RemoveAllPlayers();
                        house.SetOwner(null);

                        player.sendNotification(string.Format("Успешно/n~g~Дом продан за ${0:n0}.", price));
                        break;
                    }

                case "HouseLeave":
                    {
                        if (!player.hasData("InsideHouse_ID")) return;

                        House house = Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        house.RemovePlayer(player);
                        break;
                    }
            }
        }

        public void House_PlayerDeath(Client player, NetHandle killer, int weapon)
        {
            if (RESET_DIMENSION_ON_DEATH) player.dimension = 0;
            RemovePlayerFromHouseList(player);
        }

        public void House_PlayerLeave(Client player, string reason)
        {
            RemovePlayerFromHouseList(player);
            foreach (House house in Houses.Where(h => h.Owner == player.name)) house.SetOwnerHandle(null);
        }

        public void House_Exit()
        {
            foreach (House house in Houses)
            {
                house.Save(true);
                house.Destroy(true);
            }

            Houses.Clear();
        }
        #endregion
    }
}