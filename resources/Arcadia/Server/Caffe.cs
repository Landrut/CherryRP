using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleNativeMultiplayerServer;
using SimpleNativeMultiplayerShared;

namespace Caffe
{
    public class MapBlips : Script
    {
        public MapBlips()
        {
            API.onResourceStart += onResourceStart;
            API.onClientEventTrigger += onClientEvent;
        }

        public readonly Vector3 CaffePos = new Vector3(-1031.83f, -2658.923f, 13.83076f);
        public readonly Vector3 Caffe2Pos = new Vector3(-1034.824f, -2657.49f, 13.83076f);
        public readonly Vector3 Caffe3Pos = new Vector3(-1038.063f, -2656.071f, 13.83076f);

        public ColShape Caffe;
        public ColShape Caffe2;
        public ColShape Caffe3;

        private void onResourceStart()
        {

            Caffe = API.createCylinderColShape(CaffePos, 1f, 3f);
            API.createMarker(1, CaffePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.50f, 1.50f, 1.50f), 100, 0, 35, 255);
            API.createTextLabel("Воспользоваться компьютером\n Стоимость: ~g~300$", CaffePos, 15f, 0.65f);

            Caffe.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendChatMessageToPlayer(player, "~y~Для взаимодействия с компьютером нажмите ~g~E");
            };

            Caffe2 = API.createCylinderColShape(Caffe2Pos, 1f, 3f);
            API.createMarker(1, Caffe2Pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.50f, 1.50f, 1.50f), 100, 0, 35, 255);
            API.createTextLabel("Воспользоваться компьютером\n Стоимость: ~g~300$", Caffe2Pos, 15f, 0.65f);

            Caffe2.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendChatMessageToPlayer(player, "~y~Для взаимодействия с компьютером нажмите ~g~E");
            };

            Caffe3 = API.createCylinderColShape(Caffe3Pos, 1f, 3f);
            API.createMarker(1, Caffe3Pos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.50f, 1.50f, 1.50f), 100, 0, 35, 255);
            API.createTextLabel("Воспользоваться компьютером\n Стоимость: ~g~300$", Caffe3Pos, 15f, 0.65f);

            Caffe3.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                API.sendChatMessageToPlayer(player, "~y~Для взаимодействия с компьютером нажмите ~g~E");
            };

        }

        int Caffe_price = 300;

        public void onClientEvent(Client player, string EventName, params object[] arguments)
        {

            if (PlayerFunctions.Player.GetMoney(player) < Caffe_price && Caffe.containsEntity(player))
            {
                API.sendChatMessageToPlayer(player, "~r~У вас недостаточно денег для использования компьютера");
                return;
            }

            if (Caffe.containsEntity(player))
            {
                EventName = "Caffe";
                PlayerFunctions.Player.ChangeMoney(player, -Caffe_price);

                API.sendChatMessageToPlayer(player, "~g~Вы получили доступ к компьютеру");
                API.sendNotificationToPlayer(player, "Вы потратили ~g~" + Caffe_price + "$");
            }
        }
    }
}
