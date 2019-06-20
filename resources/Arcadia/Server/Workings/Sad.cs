using CherryMPServer;
using CherryMPServer.Managers;
using CherryMPServer.Constant;
using CherryMPShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerFunctions;

using MenuManagement;
using PlayerFunctions;

using CustomSkin;


public class Sadovnik : Script
{
    public Sadovnik()
    {
        API.onResourceStart += onResourceStart;
        API.onClientEventTrigger += OnClientEvent;
    }

    public ColShape misStartColshape;
    public ColShape misEndColshape;

    public Marker marker;
    public Blip desblip;
    public int taskdone;

    bool work = false;
    bool game = false;
    int sallary;
    public readonly Vector3 Start = new Vector3(-1214.192f, -721.4907f, 21.65023f);


    public void onResourceStart()
    {
        API.createMarker(1, Start - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Трудоустройство", Start, 15f, 0.65f);
        Blip misBlip = API.createBlip(new Vector3(-1214.192, -721.4907, 21.65023));
        API.setBlipSprite(misBlip, 120);
        API.setBlipName(misBlip, "Садовник");
        API.setBlipColor(misBlip, 5);
        misStartColshape = API.createCylinderColShape(new Vector3(-1214.192, -721.4907, 21.65023), 2f, 2f);

        misStartColshape.onEntityEnterColShape += (shape, entity) =>
        {
            Client player;

            if ((player = API.getPlayerFromHandle(entity)) != null)
            {
                API.sendPictureNotificationToPlayer(player, "Нажми Е чтобы начать работу у меня.", "CHAR_PLANESITE", 0, 3, "Директор", "Обращение к работнику");
            }
        };

    }

    public void OnClientEvent(Client player, string eventName, params object[] arguments)
    {
        if (eventName == "StartMission")
        {

            if (misStartColshape.containsEntity(player.handle))
            {
                if (work == false)
                {
                    VehicleHash Sadovnik = API.vehicleNameToModel("UtilliTruck2");
                    Vehicle myvehicle = API.createVehicle(Sadovnik, new Vector3(-1203.779, -726.9702, 20.95681), new Vector3(0, 0, 143.9855), 0, 0, 0);
                    API.setPlayerIntoVehicle(player, myvehicle, -1);
                    API.setEntityData(player, "Садовник", true);
                    API.sendPictureNotificationToPlayer(player, "Подойдя к грядке нажми Y", "CHAR_PLANESITE", 0, 3, "Директор", "Обращение к работнику");
                    API.setPlayerClothes(player, 11, 49, 0);
                    API.setPlayerClothes(player, 3, 14, 0);
                    API.setPlayerClothes(player, 8, 3, 0);
                    API.setPlayerClothes(player, 4, 35, 0);
                    API.setPlayerClothes(player, 6, 26, 0);
                    API.setPlayerClothes(player, 5, 45, 0);
                    nextChek(player);
                    work = true;
                    game = true;
                    return;
                }
                if (work == true)
                {
                    API.setEntityData(player, "Садовник", false);
                    API.sendNotificationToPlayer(player, "~b~Вы закончили работу.\nЗаработано: ~g~" + sallary + "$");
                    API.deleteColShape(misEndColshape);

                    API.triggerClientEvent(player, "EndMission");
                    Player.ChangeMoney(player, +sallary);
                    PlayerFunctions.Player.LoadPlayerClothes(player);
                    sallary = 0;

                    work = false;
                    return;
                }
            }


        }

        if (work == true)
        {
            if (eventName == "objComplete")
            {

                if (misEndColshape.containsEntity(player.handle))
                {
                    taskdone++;

                    if (taskdone == 10)
                    {
                        API.sendPictureNotificationToPlayer(player, "Ты славно потрудился, можешь забрать свои деньги", "CHAR_PLANESITE", 0, 3, "Директор", "Обращение к работнику");
                        taskdone = 0;

                    }
                    else
                    {
                        API.sendPictureNotificationToPlayer(player, "Отлично! Можешь идти к следующему месту", "CHAR_PLANESITE", 0, 3, "Директор", "Обращение к работнику");
                        sallary += 110;
                    }
                    nextChek(player);
                }

            }
        }
    }


    public void nextChek(Client player)
    {

        if (misEndColshape != null)
        {
            API.deleteColShape(misEndColshape);
        }


        Random random = new Random();

        List<Vector3> Chek = new List<Vector3>();
        Chek.Add(new Vector3(-1947.109, 145.9868, 84.6527));
        Chek.Add(new Vector3(-1950.992, 221.4481, 86.089816));
        Chek.Add(new Vector3(-1945.57, 211.2292, 85.99818));
        Chek.Add(new Vector3(-1997.916, 245.178, 87.61542));
        Chek.Add(new Vector3(-1988.987, 249.4505, 87.61527));
        Chek.Add(new Vector3(-1979.506, 230.2452, 87.61514));
        Chek.Add(new Vector3(-2001.851, 337.301, 91.23067));
        Chek.Add(new Vector3(-2025.755, 432.4018, 103.0506));
        Chek.Add(new Vector3(-2011.478, 423.0543, 102.7496));
        Chek.Add(new Vector3(-2003.078, 421.4672, 102.2997));
        Chek.Add(new Vector3(-1926.249, 429.2332, 102.7185));
        Chek.Add(new Vector3(-1963.899, 448.9217, 100.883));
        Chek.Add(new Vector3(-1904.154, 401.7386, 96.29613));
        Chek.Add(new Vector3(-1921.833, 417.0835, 96.36589));
        Chek.Add(new Vector3(-1932.92, 344.7702, 93.64937));
        Chek.Add(new Vector3(-1901.767, 361.6198, 93.17836));
        Chek.Add(new Vector3(-1055.118, 323.6361, 66.65169));
        Chek.Add(new Vector3(-1019.296, 365.9043, 71.17007));
        Chek.Add(new Vector3(-1003.389, 442.6249, 79.90125));
        Chek.Add(new Vector3(-475.889, 583.2806, 127.726));
        Chek.Add(new Vector3(-326.176, 541.1616, 121.2831));
        Chek.Add(new Vector3(-335.9444, 395.491, 111.0473));
        Chek.Add(new Vector3(404.7615, -1869.101, 26.388));
        Chek.Add(new Vector3(477.9772, -1771.027, 28.62825));
        Chek.Add(new Vector3(424.4225, -1480.406, 29.32712));
        Chek.Add(new Vector3(-4.522765, -1446.518, 30.55281));
        Chek.Add(new Vector3(-56.81565, -1480.823, 32.12996));
        Chek.Add(new Vector3(-125.6303, -1593.227, 34.1228));
        Chek.Add(new Vector3(263.9044, -1584.5, 32.76719));
        Chek.Add(new Vector3(268.6972, -1592.464, 31.76658));
        Chek.Add(new Vector3(285.2422, -1594.142, 30.53217));
        Chek.Add(new Vector3(306.2298, -1588.932, 31.76645));
        Chek.Add(new Vector3(299.0612, -1628.267, 32.77478));
        Chek.Add(new Vector3(284.906, -1633.535, 32.77384));
        Chek.Add(new Vector3(413.9816, -2108.605, 20.18277));



        Vector3 despoint = Chek[random.Next(Chek.Count)];

        misEndColshape = API.createCylinderColShape(despoint, 2f, 2f);


        API.triggerClientEvent(player, "nextChek", despoint, despoint.X, despoint.Y, desblip, marker);
    }

}