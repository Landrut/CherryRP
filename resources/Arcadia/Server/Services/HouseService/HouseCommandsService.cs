using System.IO;
using System.Linq;
//
//
using CherryMPServer;

namespace HouseScript
{
    public class Commands : Script
    {
        [Command("createhouse")]
        public void CMD_CreateHouse(Client player, int type, int price)
        {
            int RightsLevel = PlayerFunctions.Player.GetRightsLevel(player);

            if (RightsLevel < 9)
            {
                player.sendChatMessage("~r~ERROR: ~w~Эта команда вам недоступна.");
                return;
            }

            if (type < 0 || type >= HouseTypes.HouseTypeList.Count)
            {
                player.sendChatMessage("~r~ERROR: ~w~Неверный ID класса дома.");
                return;
            }

            House new_house = new House(Main.GetGuid(), string.Empty, type, player.position, price, false);
            new_house.Dimension = Main.DimensionID++;
            new_house.Save();

            Main.Houses.Add(new_house);
        }

        [Command("sethousename", GreedyArg = true)]
        public void CMD_HouseName(Client player, string new_name)
        {

            int RightsLevel = PlayerFunctions.Player.GetRightsLevel(player);

            if (RightsLevel < 9)
            {
                player.sendChatMessage("~r~ERROR: ~w~Эта команда вам недоступна.");
                return;
            }

            if (!player.hasData("HouseMarker_ID"))
            {
                player.sendChatMessage("~r~ERROR: ~w~Встаньте на маркер дома который хотите изменить.");
                return;
            }

            House house = Main.Houses.FirstOrDefault(h => h.ID == player.getData("HouseMarker_ID"));
            if (house == null) return;

            house.SetName(new_name);
            player.sendChatMessage(string.Format("~b~HOUSE SCRIPT: ~w~Имя дома изменено на ~y~\"{0}\".", new_name));
        }

        [Command("sethousetype")]
        public void CMD_HouseType(Client player, int new_type)
        {

            int RightsLevel = PlayerFunctions.Player.GetRightsLevel(player);

            if (RightsLevel < 9)
            {
                player.sendChatMessage("~r~ERROR: ~w~Эта команда вам недоступна.");
                return;
            }

            if (!player.hasData("HouseMarker_ID"))
            {
                player.sendChatMessage("~r~ERROR: ~w~Встаньте на маркер дома который хотите изменить.");
                return;
            }

            if (new_type < 0 || new_type >= HouseTypes.HouseTypeList.Count)
            {
                player.sendChatMessage("~r~ERROR: ~w~Неверный класс дома.");
                return;
            }

            House house = Main.Houses.FirstOrDefault(h => h.ID == player.getData("HouseMarker_ID"));
            if (house == null) return;

            house.SetType(new_type);
            player.sendChatMessage(string.Format("~b~HOUSE SCRIPT: ~w~Класс дома изменён на ~y~{0}.", HouseTypes.HouseTypeList[new_type].Name));
        }

        [Command("sethouseprice")]
        public void CMD_HousePrice(Client player, int new_price)
        {

            int RightsLevel = PlayerFunctions.Player.GetRightsLevel(player);

            if (RightsLevel < 9)
            {
                player.sendChatMessage("~r~ERROR: ~w~Эта команда вам недоступна.");
                return;
            }

            if (!player.hasData("HouseMarker_ID"))
            {
                player.sendChatMessage("~r~ERROR: ~w~Встаньте на маркер дома который хотите изменить.");
                return;
            }

            House house = Main.Houses.FirstOrDefault(h => h.ID == player.getData("HouseMarker_ID"));
            if (house == null) return;

            house.SetPrice(new_price);
            player.sendChatMessage(string.Format("~b~HOUSE SCRIPT: ~w~Цена дома изменена на ~g~${0:n0}.", new_price));
        }

        [Command("removehouse")]
        public void CMD_RemoveHouse(Client player)
        {

            int RightsLevel = PlayerFunctions.Player.GetRightsLevel(player);

            if (RightsLevel < 9)
            {
                player.sendChatMessage("~r~ERROR: ~w~Эта команда вам недоступна.");
                return;
            }

            if (!player.hasData("HouseMarker_ID"))
            {
                player.sendChatMessage("~r~ERROR: ~w~Встаньте на маркер дома который хотите изменить.");
                return;
            }

            House house = Main.Houses.FirstOrDefault(h => h.ID == player.getData("HouseMarker_ID"));
            if (house == null) return;

            house.Destroy();
            Main.Houses.Remove(house);

            string house_file = Main.HOUSE_SAVE_DIR + Path.DirectorySeparatorChar + house.ID + ".json";
            if (File.Exists(house_file)) File.Delete(house_file);
        }
    }
}