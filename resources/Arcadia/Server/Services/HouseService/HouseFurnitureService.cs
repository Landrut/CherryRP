/*using System;
using System.Linq;
using System.Collections.Generic;
using CherryMPServer;
using CherryMPShared;
using CherryMPServer.Managers;
using CherryMPServer.Constant;
//
//
//;
using Newtonsoft.Json;

namespace HouseScript
{
    #region HouseFurniture Class
    public class HouseFurniture
    {
        public int Model { get; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public string Name { get; }
        public int Price { get; }

        [JsonIgnore]
        public CherryMPServer.Object Object { get; private set; }

        public HouseFurniture(int model, Vector3 position, Vector3 rotation, string name, int price)
        {
            Model = model;
            Position = position;
            Rotation = rotation;

            Name = name;
            Price = price;
        }

        public void Create(int dimension)
        {
            Object = API.shared.createObject(Model, Position, Rotation, dimension);
        }

        public void Destroy()
        {
            if (Object != null) Object.delete();
        }
    }
    #endregion

    #region Furniture Class
    public class Furniture
    {
        public string Name { get; }
        public string ModelName { get; }
        public int Price { get; }

        public Furniture(string name, string model_name, int price)
        {
            Name = name;
            ModelName = model_name;
            Price = price;
        }
    }
    #endregion

    public class HouseFurnitures : Script
    {
        public static List<Furniture> FurnitureList = new List<Furniture>
        {
            // name, model name, price
            new Furniture("Офисное кресло", "hei_prop_heist_off_chair", 250),
            new Furniture("Кресло директора", "prop_direct_chair_01", 400),
            new Furniture("Снеговик", "prop_prlg_snowpile", 50),
        };

        public HouseFurnitures()
        {
            API.onClientEventTrigger += HouseFurnitures_ClientEvent;
        }

        #region Events
        public void HouseFurnitures_ClientEvent(Client player, string event_name, params object[] args)
        {
            switch (event_name)
            {
                case "HouseFurnitureCatalogue":
                    {
                        if (!player.hasData("InsideHouse_ID")) return;

                        House house = Main.Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка/n~r~Это может делать только владелец.");
                            return;
                        }

                        player.triggerEvent("HouseFurnitureCatalogue", API.toJson(FurnitureList));
                        break;
                    }

                case "HouseBuyFurniture":
                    {
                        if (!player.hasData("InsideHouse_ID") || args.Length < 1) return;

                        House house = Main.Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка/n~r~Это может делать только владелец.");
                            return;
                        }

                        if (Main.HOUSE_FURNITURE_LIMIT > 0 && house.Furnitures.Count >= Main.HOUSE_FURNITURE_LIMIT)
                        {
                            player.sendNotification("Ошибка/n~r~Достигнут лимит мебели в доме.");
                            return;
                        }

                        int idx = Convert.ToInt32(args[0]);
                        if (idx < 0 || idx >= FurnitureList.Count) return;

                        if (PlayerFunctions.Player.GetMoney(player) < FurnitureList[idx].Price)
                        {
                            player.sendNotification("Ошибка/n~r~Вы не можете приобрести эту мебель.");
                            return;
                        }

                        PlayerFunctions.Player.ChangeMoney(player, -FurnitureList[idx].Price);

                        HouseFurniture new_furniture = new HouseFurniture(API.getHashKey(FurnitureList[idx].ModelName), player.position - new Vector3(0.0, 0.0, 0.5), new Vector3(), FurnitureList[idx].Name, FurnitureList[idx].Price);
                        house.Furnitures.Add(new_furniture);
                        new_furniture.Create(house.Dimension);
                        house.Save();

                        player.sendNotification(string.Format("Успешно/n~g~Куплен {0} за ${1:n0}.", FurnitureList[idx].Name, FurnitureList[idx].Price));

                        player.triggerEvent("HouseUpdateFurnitures", API.toJson(house.Furnitures));
                        break;
                    }

                case "HouseEditFurniture":
                    {
                        if (!player.hasData("InsideHouse_ID") || args.Length < 1) return;

                        House house = Main.Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка/n~r~Это может делать только владелец.");
                            return;
                        }

                        int idx = Convert.ToInt32(args[0]);
                        if (idx < 0 || idx >= house.Furnitures.Count) return;

                        player.setData("EditingFurniture_ID", idx);
                        player.triggerEvent("SetEditingHandle", house.Furnitures[idx].Object.handle);
                        break;
                    }

                case "HouseSaveFurniture":
                    {
                        if (!player.hasData("InsideHouse_ID") || !player.hasData("EditingFurniture_ID") || args.Length < 6) return;

                        House house = Main.Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка/n~r~Это может делать только владелец.");
                            return;
                        }

                        int idx = player.getData("EditingFurniture_ID");
                        if (idx < 0 || idx >= house.Furnitures.Count) return;

                        Vector3 new_pos = new Vector3(Convert.ToDouble(args[0]), Convert.ToDouble(args[1]), Convert.ToDouble(args[2]));
                        Vector3 new_rot = new Vector3(Convert.ToDouble(args[3]), Convert.ToDouble(args[4]), Convert.ToDouble(args[5]));

                        house.Furnitures[idx].Position = new_pos;
                        house.Furnitures[idx].Rotation = new_rot;

                        house.Furnitures[idx].Object.position = new_pos;
                        house.Furnitures[idx].Object.rotation = new_rot;
                        house.Save();

                        player.sendNotification(string.Format("Успешно/n~g~Изменено {0}.", house.Furnitures[idx].Name));
                        player.resetData("EditingFurniture_ID");
                        break;
                    }

                case "HouseResetFurniture":
                    {
                        if (!player.hasData("InsideHouse_ID") || !player.hasData("EditingFurniture_ID")) return;

                        House house = Main.Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка/n~r~Это может делать только владелец.");
                            return;
                        }

                        int idx = player.getData("EditingFurniture_ID");
                        if (idx < 0 || idx >= house.Furnitures.Count) return;

                        player.triggerEvent("ResetEntityPosition", house.Furnitures[idx].Position, house.Furnitures[idx].Rotation);
                        player.resetData("EditingFurniture_ID");
                        break;
                    }

                case "HouseSellFurniture":
                    {
                        if (!player.hasData("InsideHouse_ID") || args.Length < 1) return;

                        House house = Main.Houses.FirstOrDefault(h => h.ID == player.getData("InsideHouse_ID"));
                        if (house == null) return;

                        if (house.Owner != player.name)
                        {
                            player.sendNotification("Ошибка/n~r~Это может делать только владелец.");
                            return;
                        }

                        int idx = Convert.ToInt32(args[0]);
                        if (idx < 0 || idx >= house.Furnitures.Count) return;

                        HouseFurniture furniture = house.Furnitures[idx];
                        int price = (int)Math.Round(furniture.Price * 0.8);
                        PlayerFunctions.Player.ChangeMoney(player, +price);

                        house.Furnitures[idx].Destroy();
                        house.Furnitures.RemoveAt(idx);
                        house.Save();

                        player.sendNotification(string.Format("Успешно/n~g~Продан {0} за ${1:n0}.", furniture.Name, price));

                        player.triggerEvent("HouseUpdateFurnitures", API.toJson(house.Furnitures));
                        break;
                    }
            }
        }
        #endregion
    }
}*/