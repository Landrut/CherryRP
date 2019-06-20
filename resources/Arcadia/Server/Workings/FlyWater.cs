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

public class FlyWater : Script
{
    public FlyWater()
    {
        API.onResourceStart += onResourceStart;
        API.onClientEventTrigger += OnClientEvent;
        misStartColshapeFW.onEntityEnterColShape += MisStartShapeFW;
    }

    public ColShape FWStartColshape;
    public ColShape FWEndColshape;

    public Marker FWmarker;
    public Blip FWblip;
    public int taskdone;


    int sallary;
    public readonly Vector3 StartFW = new Vector3(2150.614f, 4790.182f, 40.99051f);
    ColShape misStartColshapeFW = API.shared.createCylinderColShape(new Vector3(2150.614, 4790.182, 40.99051), 2f, 2f);


    public void onResourceStart()
    {
        API.createMarker(1, StartFW - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Трудоустройство", StartFW, 15f, 0.65f);
        Blip misBlip = API.createBlip(new Vector3(2150.614, 4790.182, 40.99051));
        API.setBlipSprite(misBlip, 251);
        API.setBlipName(misBlip, "Орошитель полей");
        API.setBlipColor(misBlip, 5);
        FWStartColshape = API.createCylinderColShape(new Vector3(2150.614, 4790.182, 40.99051), 2f, 2f);
        Ped pointPed = API.createPed((PedHash)(1498487404), new Vector3(2150.614, 4790.182, 40.99051), 126);

    }

    private void OnClientEvent(Client player, string eventName, params object[] arguments)
    {
        if (eventName == "MissionTriggers")
        {

            if (FWStartColshape.containsEntity(player.handle))
            {
                if (API.getEntitySyncedData(player, "IS_JOB") == false)
                {
                    VehicleHash Sadovnik = API.vehicleNameToModel("Duster");
                    Vehicle myvehicle = API.createVehicle(Sadovnik, new Vector3(2133.792, 4782.84, 41.31388), new Vector3(10.64469, 0.001745113, 27.89867), 0, 0, 0);
                    API.setPlayerIntoVehicle(player, myvehicle, -1);
                    nextChek(player);
                    API.delay(1000, true, () =>
                    {
                        API.sendNotificationToPlayer(player, "~g~Отправляйтесь к отмеченной точне на карте");
                        API.sendNotificationToPlayer(player, "~g~Чтобы орошить поле нажми ~g~Q ~ w~,");

                    });
                    API.setEntitySyncedData(player, "IS_JOB", true);
                    API.setEntitySyncedData(myvehicle, "Vehicle_Use", "fw_job");
                    API.setEntitySyncedData(myvehicle, "OwnerName", player.name);

                }

                else
                {

                    API.sendNotificationToPlayer(player, "~g~Приятного Вечера!");
                    API.sendNotificationToPlayer(player, "~g~Оплата производится на ваш счет в банке!");
                    API.triggerClientEvent(player, "DeleteNextPoint");
                    API.setEntitySyncedData(player, "IS_JOB", false);

                    return;
                }

            }

        }
        if (eventName == "objComplete")
        {

            if (FWEndColshape.containsEntity(player.handle))
            {                
                    taskdone++;

                    if (taskdone == 5)
                    {
                        API.sendPictureNotificationToPlayer(player, "Ты славно потрудился, можешь забрать свои деньги", "CHAR_PLANESITE", 0, 3, "Директор", "Обращение к работнику");

                        API.triggerClientEvent(player, "EndMissionFW");

                    }
                    else
                    {
                        API.sendPictureNotificationToPlayer(player, "Отлично! Можешь лететь к следующему месту", "CHAR_PLANESITE", 0, 3, "Директор", "Обращение к работнику");
                        sallary += 310;
                        nextChek(player);
                    }
                }
                else API.sendPictureNotificationToPlayer(player, "Cначала нужно переодеться", "CHAR_PLANESITE", 0, 3, "Директор", "Обращение к работнику");

            };


        }

    private void MisStartShapeFW(ColShape shape, NetHandle entity)
    {

        if (shape == null) { return; }
        if (entity == null) { return; }

        if (API.getEntityType(entity) != EntityType.Player) { return; }

        var player = API.getPlayerFromHandle(entity);

        if (player == null) { return; }

        if (((player = API.getPlayerFromHandle(entity)) == null) || ((player.isInVehicle) == true)) { return; }

        if (API.getEntitySyncedData(player, "Just_Send") == true) { return; }


        if (API.getEntitySyncedData(player, "IS_JOB") == true)
        {
            API.sendNotificationToPlayer(player, "~w~Чтобы уволиться используйте клавишу ~g~ E~w~.");
            API.setEntitySyncedData(player, "Just_Send", true);

            API.delay(2000, true, () =>
            {
                API.setEntitySyncedData(player, "Just_Send", false);
            });

        }

        else
        {
            API.sendNotificationToPlayer(player, "~w~Ты готов подзаработать? Используюйте ~g~E~w~ чтобы начать.");

            API.setEntitySyncedData(player, "Just_Send", true);


            API.delay(2000, true, () =>
            {
                API.setEntitySyncedData(player, "Just_Send", false);
            });
        }
    }



    public void nextChek(Client player)
    {
        Client Work = API.getPlayerFromHandle(player);
        if (FWEndColshape != null)
        {
            API.deleteColShape(FWEndColshape);
        }


        Random random = new Random();

        List<Vector3> Chek = new List<Vector3>();
        Chek.Add(new Vector3(2040.913, 4881.631, 53.42401));
        Chek.Add(new Vector3(2004.802, 4902.233, 56.23709));
        Chek.Add(new Vector3(2038.335, 4953.105, 57.30499));
        Chek.Add(new Vector3(2077.671, 4910.747, 54.17388));
        Chek.Add(new Vector3(2210.151, 5048.334, 62.84881));
        Chek.Add(new Vector3(2265.051, 5084.158, 68.04928));
        Chek.Add(new Vector3(2304.021, 5128.435, 64.59335));
        Chek.Add(new Vector3(2196.849, 5182.958, 71.29945));
        Chek.Add(new Vector3(2135.659, 5168.211, 70.36449));
        Chek.Add(new Vector3(1799.324, 4975.749, 59.4357));
        Chek.Add(new Vector3(2489.021, 4853.134, 59.45409));
        Chek.Add(new Vector3(2544.249, 4803.546, 56.76982));
        Chek.Add(new Vector3(2637.925, 4702.519, 63.98448));
        Chek.Add(new Vector3(2628.305, 4582.524, 55.22521));
        Chek.Add(new Vector3(2560.616, 4532.78, 52.03459));
        Chek.Add(new Vector3(2612.037, 4480.017, 47.19995));
        Chek.Add(new Vector3(2656.933, 4455.315, 51.25177));
        Chek.Add(new Vector3(2584.192, 4424.479, 53.60506));
        Chek.Add(new Vector3(2538.57, 4386.791, 49.09418));
        Chek.Add(new Vector3(2517.643, 4345.052, 53.6712));
        Chek.Add(new Vector3(2851.234, 4635.848, 76.14326));
        Chek.Add(new Vector3(2851.234, 4635.848, 76.14326));



        Vector3 FWdespoint = Chek[random.Next(Chek.Count)];

        FWEndColshape = API.createCylinderColShape(FWdespoint, 21f, 25f);


        API.triggerClientEvent(player, "nextChekFW", FWdespoint, FWdespoint.X, FWdespoint.Y, FWblip, FWmarker);
    }

}

 /*

public class FlyWater : Script
{
    public FlyWater()
    {
        API.onResourceStart += onResourceStart;
        API.onClientEventTrigger += OnClientEvent;
        misStartColshapeFW.onEntityEnterColShape += MisStartShapeFW;
    }

    
    public ColShape FWEndColshape;

    public Marker FWmarker;
    public Blip FWblip;
    public int taskdone;


    int sallary;
    ColShape misStartColshapeFW = API.shared.createCylinderColShape(new Vector3(2150.614, 4790.182, 40.99051), 2f, 2f);
    


    public void onResourceStart()
    {        
        Blip misBlip = API.createBlip(new Vector3(2150.614, 4790.182, 40.99051));
        API.setBlipSprite(misBlip, 251);
        API.setBlipName(misBlip, "Орошитель полей");
        API.setBlipColor(misBlip, 5);        
        Ped pointPed = API.createPed((PedHash)(1498487404), new Vector3(2150.614, 4790.182, 40.99051), 126);

    }

    private void OnClientEvent(Client player, string eventName, params object[] arguments)
    {
        if (eventName == "MissionTriggers")
        {

            if (misStartColshapeFW.containsEntity(player.handle))
            {
                if (API.getEntitySyncedData(player, "IS_JOB") == false)
                {
                    VehicleHash Sadovnik = API.vehicleNameToModel("Duster");
                    Vehicle myvehicle = API.createVehicle(Sadovnik, new Vector3(2133.792, 4782.84, 41.31388), new Vector3(10.64469, 0.001745113, 27.89867), 0, 0, 0);
                    API.setPlayerIntoVehicle(player, myvehicle, -1);
                    nextChek(player);
                    API.delay(1000, true, () =>
                    {
                        API.sendNotificationToPlayer(player, "~g~Отправляйтесь к отмеченной точне на карте");
                        API.sendNotificationToPlayer(player, "~g~Чтобы орошить поле нажми ~g~Q ~ w~,");

                    });
                    API.setEntitySyncedData(player, "IS_JOB", true);
                    API.setEntitySyncedData(myvehicle, "Vehicle_Use", "fw_job");
                    API.setEntitySyncedData(myvehicle, "OwnerName", player.name);

                }

                else
                {

                    API.sendNotificationToPlayer(player, "~g~Приятного Вечера!");
                    API.sendNotificationToPlayer(player, "~g~Оплата производится на ваш счет в банке!");
                    API.triggerClientEvent(player, "DeleteNextPoint");
                    API.setEntitySyncedData(player, "IS_JOB", false);

                    return;
                }

            }

        }

        if (eventName == "objComplete")
        {

            if (FWEndColshape.containsEntity(player.handle))
            {
                nextChek(player);
            };


        }
    }

    private void MisStartShapeFW(ColShape shape, NetHandle entity)
    {

        if (shape == null) { return; }
        if (entity == null) { return; }

        if (API.getEntityType(entity) != EntityType.Player) { return; }

        var player = API.getPlayerFromHandle(entity);

        if (player == null) { return; }

        if (((player = API.getPlayerFromHandle(entity)) == null) || ((player.isInVehicle) == true)) { return; }

        if (API.getEntitySyncedData(player, "Just_Send") == true) { return; }


        if (API.getEntitySyncedData(player, "IS_JOB") == true)
        {
            API.sendNotificationToPlayer(player, "~w~Чтобы уволиться используйте клавишу ~g~ E~w~.");
            API.setEntitySyncedData(player, "Just_Send", true);

            API.delay(2000, true, () =>
            {
                API.setEntitySyncedData(player, "Just_Send", false);
            });

        }

        else
        {
            API.sendNotificationToPlayer(player, "~w~Ты готов подзаработать? Используюйте ~g~E~w~ чтобы начать.");

            API.setEntitySyncedData(player, "Just_Send", true);


            API.delay(2000, true, () =>
            {
                API.setEntitySyncedData(player, "Just_Send", false);
            });
        }
    }

    public void nextChek(Client player)
    {
        Client player = API.getPlayerFromHandle(player);
        if (FWEndColshape != null)
        {
            API.deleteColShape(FWEndColshape);
        }


        Random random = new Random();

        List<Vector3> Chek = new List<Vector3>();
        Chek.Add(new Vector3(2040.913, 4881.631, 53.42401));
        Chek.Add(new Vector3(2004.802, 4902.233, 56.23709));
        Chek.Add(new Vector3(2038.335, 4953.105, 57.30499));
        Chek.Add(new Vector3(2077.671, 4910.747, 54.17388));
        Chek.Add(new Vector3(2210.151, 5048.334, 62.84881));
        Chek.Add(new Vector3(2265.051, 5084.158, 68.04928));
        Chek.Add(new Vector3(2304.021, 5128.435, 64.59335));
        Chek.Add(new Vector3(2196.849, 5182.958, 71.29945));
        Chek.Add(new Vector3(2135.659, 5168.211, 70.36449));
        Chek.Add(new Vector3(1799.324, 4975.749, 59.4357));
        Chek.Add(new Vector3(2489.021, 4853.134, 59.45409));
        Chek.Add(new Vector3(2544.249, 4803.546, 56.76982));
        Chek.Add(new Vector3(2637.925, 4702.519, 63.98448));
        Chek.Add(new Vector3(2628.305, 4582.524, 55.22521));
        Chek.Add(new Vector3(2560.616, 4532.78, 52.03459));
        Chek.Add(new Vector3(2612.037, 4480.017, 47.19995));
        Chek.Add(new Vector3(2656.933, 4455.315, 51.25177));
        Chek.Add(new Vector3(2584.192, 4424.479, 53.60506));
        Chek.Add(new Vector3(2538.57, 4386.791, 49.09418));
        Chek.Add(new Vector3(2517.643, 4345.052, 53.6712));
        Chek.Add(new Vector3(2851.234, 4635.848, 76.14326));
        Chek.Add(new Vector3(2851.234, 4635.848, 76.14326));



        Vector3 FWdespoint = Chek[random.Next(Chek.Count)];

        FWEndColshape = API.createCylinderColShape(FWdespoint, 21f, 25f);


        API.triggerClientEvent(player, "nextChekFW", FWdespoint, FWdespoint.X, FWdespoint.Y, FWblip, FWmarker);
    }

}
*/