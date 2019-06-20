using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//
//
using CherryMPServer;
using CherryMPShared;
//;

    // я люблю пивасик )))00)

namespace Slash_Role_Play.Map
{
    public class MapBlips : Script
    {
        public MapBlips()
        {
            API.onResourceStart += onResourceStart;
            API.onClientEventTrigger += onClientEvent;
        }

        public readonly Vector3 arendaPos = new Vector3(-1013f, -2696f, 13.98f);

        public ColShape arenda;

        private void onResourceStart()
        {

            arenda = API.createCylinderColShape(arendaPos, 1f, 3f);
            API.createMarker(1, arendaPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.50f, 1.50f, 1.50f), 100, 0, 35, 255);
            API.createTextLabel("Аренда велосипеда\n Стоимость: ~g~50$", arendaPos, 15f, 0.65f);

            arenda.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendChatMessageToPlayer(player, "~y~Для аренды велосипеда нажмите клавишу ~g~R");
            };

        }

        int arenda_price = 50;

        public void onClientEvent(Client player, string EventName, params object[] arguments)
        {

            if (PlayerFunctions.Player.GetMoney(player) < arenda_price && arenda.containsEntity(player))
            {
                API.sendChatMessageToPlayer(player, "~r~У вас недостаточно денег на аренду велосипеда");
                return;
            }

            if (arenda.containsEntity(player))
            {
                EventName = "arenda";
                PlayerFunctions.Player.ChangeMoney(player, -arenda_price);
                var veh = API.createVehicle(VehicleHash.Scorcher, new Vector3(-1020f, -2706f, 13.632f), new Vector3(0f, 0f, 120f), 0, 0);
                API.setPlayerIntoVehicle(player, veh, -1);
                API.sendChatMessageToPlayer(player, "~g~Вы успешно арендовали велосипед.");
                API.sendNotificationToPlayer(player, "Вы потратили ~g~" + arenda_price + "$");
            }
        }
    }
}
