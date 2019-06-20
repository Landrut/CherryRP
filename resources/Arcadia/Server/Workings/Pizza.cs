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


using CustomSkin;


    public class PizzaJob : Script
    {

        ColShape misStartColshapePizza = API.shared.createCylinderColShape(new Vector3(287.571, -963.9154, 29.31863), 1f, 3f);

        ColShape point21 = API.shared.createCylinderColShape(new Vector3(253.123, -344.4588, 44.52695), 1f, 3f);
        ColShape point22 = API.shared.createCylinderColShape(new Vector3(-29.66966, -347.3757, 46.00793), 1f, 3f);
        ColShape point23 = API.shared.createCylinderColShape(new Vector3(-115.9471, -601.7228, 36.28189), 1f, 3f);
        ColShape point24 = API.shared.createCylinderColShape(new Vector3(44.67562, -803.0397, 31.52142), 1f, 3f);
        ColShape point25 = API.shared.createCylinderColShape(new Vector3(-18.47215, -979.4948, 29.49973), 1f, 3f);
        ColShape point26 = API.shared.createCylinderColShape(new Vector3(66.00465, -1007.603, 29.35742), 1f, 3f);
        ColShape point27 = API.shared.createCylinderColShape(new Vector3(242.2793, -1115.86, 29.32181), 1f, 3f);
        ColShape point28 = API.shared.createCylinderColShape(new Vector3(381.9003, -1025.041, 29.53661), 1f, 3f);
        ColShape point29 = API.shared.createCylinderColShape(new Vector3(254.226, -1013.345, 29.26914), 1f, 3f);

        int sallary;
    

        public PizzaJob()
        {
            API.onResourceStart += onResourceStart;
            API.onUpdate += OnUpdateHandler;
            API.onClientEventTrigger += OnClientEvent;           
            API.onEntityEnterColShape += OnEntityEnterColShape;
            misStartColshapePizza.onEntityEnterColShape += MisStartShapePizza;
        

    }


        public void onResourceStart()
        {
            Ped pointPed = API.createPed((PedHash)(1498487404), new Vector3(287.3737, -963.6613, 29.41864), 126);
            point21.setData("number", 21);
            point22.setData("number", 22);
            point23.setData("number", 23);
            point24.setData("number", 24);
            point25.setData("number", 25);
            point26.setData("number", 26);
            point27.setData("number", 27);
            point28.setData("number", 28);
            point29.setData("number", 29);

        }


    public void OnClientEvent(Client player, string eventName, params object[] arguments)
    {
        
        if (eventName == "MissionTriggers")
            {

                if (misStartColshapePizza.containsEntity(player.handle))
                {

                if (API.getEntitySyncedData(player, "IS_JOB") == true)
                {
                    API.sendNotificationToPlayer(player, "~g~Приятного Вечера!");
                    API.sendNotificationToPlayer(player, "~g~Оплата производится на ваш счет в банке!");
                    API.setEntitySyncedData(player, "NextCheck", 0);
                    API.triggerClientEvent(player, "DeleteNextPoint");
                    API.setEntitySyncedData(player, "IS_JOB", false);

                    return;
                }
                else                       
                    {
                    
                    VehicleHash taxi = API.vehicleNameToModel("Faggio");
                    Vehicle myvehicle = API.createVehicle(taxi, new Vector3(291.1025, -959.7261, 28.89764), new Vector3(0, 0, -84.32785), 0, 0, 0);
                    API.setPlayerIntoVehicle(player, myvehicle, -1);


                    API.triggerClientEvent(player, "NextPointPizza", 253.123, -344.4588, 44.52695 + 1, 0);
                    API.setEntitySyncedData(player, "NextCheck", 21);
                    API.delay(1000, true, () =>
                    {
                        API.sendNotificationToPlayer(player, "~g~Отправляйтесь к отмеченной точне на карте");
                        API.sendNotificationToPlayer(player, "~g~Вернитесь к своей начальной точке и используйте ~g~E ~ w~, чтобы закончить работу.");

                    });
                    API.setEntitySyncedData(player, "IS_JOB", true);
                    API.setEntitySyncedData(myvehicle, "Vehicle_Use", "pizza_job");
                    API.setEntitySyncedData(myvehicle, "OwnerName", player.name);
                }
                }
            }

            if (eventName == "SetSallaryPizzaing") 
            {
                sallary += 60;
            Player.ChangeMoney(player, +sallary);
            API.sendNotificationToPlayer(player, "~g~На ваш счет было зачислено 60" +"$");
                


            }
        }

        private void OnEntityEnterColShape(ColShape shape, NetHandle entity)
        {
       
            if (shape == null) { return; }
            if (entity == null) { return; }

            if (API.getEntityType(entity) != EntityType.Player) { return; }

            var player = API.getPlayerFromHandle(entity);

            if (player == null) { return; }
            
            switch (shape.getData("number"))
            {
                case 21:
                    if (API.getEntitySyncedData(player, "NextCheck") != 21) { return; }
                    API.triggerClientEvent(player, "NextPointPizza", -29.66966, -347.3757, 46.00793 + 1, 1);
                    API.setEntitySyncedData(player, "NextCheck", 22);
                    break;
                case 22:
                    if (API.getEntitySyncedData(player, "NextCheck") != 22) { return; }
                    API.triggerClientEvent(player, "NextPointPizza", -115.9471, -601.7228, 36.28189 + 1, 1);
                    API.setEntitySyncedData(player, "NextCheck", 23);
                    break;
                case 23:
                    if (API.getEntitySyncedData(player, "NextCheck") != 23) { return; }
                    API.triggerClientEvent(player, "NextPointPizza", 44.67562, -803.0397, 31.52142 + 1, 1);
                    API.setEntitySyncedData(player, "NextCheck", 24);
                    break;
                case 24:
                    if (API.getEntitySyncedData(player, "NextCheck") != 24) { return; }
                    API.triggerClientEvent(player, "NextPointPizza", -18.47215, -979.4948, 29.49973 + 1, 1);
                    API.setEntitySyncedData(player, "NextCheck", 25);
                    break;
                case 25:
                    if (API.getEntitySyncedData(player, "NextCheck") != 25) { return; }
                    API.triggerClientEvent(player, "NextPointPizza", 66.00465, -1007.603, 29.35742 + 1, 1);
                    API.setEntitySyncedData(player, "NextCheck", 26);
                    break;
                case 26:
                    if (API.getEntitySyncedData(player, "NextCheck") != 26) { return; }
                    API.triggerClientEvent(player, "NextPointPizza", 242.2793, -1115.86, 29.32181 + 1, 1);
                    API.setEntitySyncedData(player, "NextCheck", 27);
                    break;
                case 27:
                    if (API.getEntitySyncedData(player, "NextCheck") != 27) { return; }
                    API.triggerClientEvent(player, "NextPointPizza", 381.9003, -1025.041, 29.53661 + 1, 1);
                    API.setEntitySyncedData(player, "NextCheck", 28);
                    break;
                case 28:
                    if (API.getEntitySyncedData(player, "NextCheck") != 28) { return; }
                    API.triggerClientEvent(player, "NextPointPizza", 254.226, -1013.345, 29.26914 + 1, 1);
                    API.setEntitySyncedData(player, "NextCheck", 29);
                    break;
                case 29:
                    if (API.getEntitySyncedData(player, "NextCheck") != 29) { return; }
                    API.triggerClientEvent(player, "NextPointPizza", 253.123, -344.4588, 44.52695 + 1, 1);
                    API.setEntitySyncedData(player, "NextCheck", 21);
                    break;

            }

        }

        private void MisStartShapePizza(ColShape shape, NetHandle entity)
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


    public void OnPlayerExitVehicleHandler(Client player, NetHandle myvehicle, int fromSeat)
    {        
            if (!IsInRangeOf(player.position, new Vector3(287.571, -963.9154, 29.31863), 8f)) { return; }
            if ((API.getEntitySyncedData(player, "IS_JOB") == false) || (!API.hasEntitySyncedData(player, "IS_JOB"))) { return; }

            if ((API.hasEntitySyncedData(myvehicle, "OwnerName")) && (API.getEntitySyncedData(myvehicle, "OwnerName") == player.name) && (API.getEntitySyncedData(myvehicle, "Vehicle_Use") == "pizza_job") && (fromSeat == -1))
            {
                API.deleteEntity(myvehicle);
            }
        }


        public void OnUpdateHandler()
        {

        }





        public static bool IsInRangeOf(Vector3 playerPos, Vector3 target, float range)
        {
            var direct = new Vector3(target.X - playerPos.X, target.Y - playerPos.Y, target.Z - playerPos.Z);
            var len = direct.X * direct.X + direct.Y * direct.Y + direct.Z * direct.Z;
            return range * range > len;
        }


    }



/*
public class Svalka : Script
{
    public Svalka()
    {
        API.onResourceStart += onResourceStart;
        API.onClientEventTrigger += OnClientEvent;

    }

    public ColShape SvalkaStartColshape;
    public ColShape SvalkaBaseColshape;
    public ColShape SvalkaEndColshape;

    public Marker Svalkamarker;
    public Marker Basemarker;
    public Blip Svalkablip;
    public Blip Baseblip;
    public int taskdone;

    bool workSvalka = false;
    bool gameSvalka = false;
    int sallary;
    public readonly Vector3 StartSvalka = new Vector3(2404.343f, 3127.818f, 48.1535f);

    private void OnPlayerExitVehicle(Client player, NetHandle vehicle, int fromSeat)
    {
        if (API.getVehicleNumberPlate(vehicle) == "Svalka")
        {
            API.triggerClientEvent(player, "EndMissionSvalka");
            API.triggerClientEvent(player, "BaseMissionSvalka");
            API.sendChatMessageToPlayer(player, "������� � ������!");
        }
    }

    private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (player.getData("IS_JOB_SVALKA") == true)
            {
                gameSvalka = true;

            }
            else
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~�� �� ���������� �� ������!");
                return;
            }
            return;
        }
    }

    public void onResourceStart()
    {
        API.createMarker(1, StartSvalka - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("���������������", StartSvalka, 15f, 0.65f);
        Blip misBlip = API.createBlip(new Vector3(2404.343, 3127.818, 48.1535));
        API.setBlipSprite(misBlip, 478);
        API.setBlipName(misBlip, "������� ����������");
        API.setBlipColor(misBlip, 5);
        SvalkaStartColshape = API.createCylinderColShape(new Vector3(2404.343, 3127.818, 48.1535), 2f, 2f);

        List<Vehicle> SvalkaVehicles = new List<Vehicle>();
        SvalkaVehicles.Add(API.createVehicle((VehicleHash)1491375716, new Vector3(2359.757, 3127.463, 47.65964), new Vector3(-0.1482346, 0.001313954, -13.87819), 124, 58, 0));

        Vehicle Svalkaveh2;
        foreach (Vehicle Svalkaveh in SvalkaVehicles)
        {
            API.setEntityData(Svalkaveh, "IS_JOB_SVALKA", true);
            API.setVehicleNumberPlate(Svalkaveh, "Svalka");
            API.setVehicleEngineStatus(Svalkaveh, false);

            Svalkaveh2 = Svalkaveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);

            if (API.getVehicleNumberPlate(vehicle) == "Svalka")
            {
                if (player.getData("IS_JOB_SVALKA") == false)
                {
                    if (API.getPlayerVehicleSeat(client) == -1)
                    {
                        API.sendChatMessageToPlayer(client, "������� ���������� �� ������!");
                        API.warpPlayerOutOfVehicle(client);
                        return;
                    }
                    else return;
                }
                else return;
            }
            else return;
        };

        SvalkaStartColshape.onEntityEnterColShape += (shape, entity) =>
        {
            Client player;

            if ((player = API.getPlayerFromHandle(entity)) != null)
            {
                API.sendPictureNotificationToPlayer(player, "��������� ������ ������?", "CHAR_PLANESITE", 0, 3, "��������", "��������� � ���������");
            }
        };



    }

    public void OnClientEvent(Client player, string eventName, params object[] arguments)
    {
        if (eventName == "StartSvalka")
        {

            if (SvalkaStartColshape.containsEntity(player.handle))
            {
                if (workSvalka == false)
                {

                    player.setData("IS_JOB_SVALKA", true);
                    API.sendPictureNotificationToPlayer(player, "�������� � ������ � �������� ������", "CHAR_PLANESITE", 0, 3, "��������", "��������� � ���������");
                    API.setPlayerClothes(player, 11, 49, 0);
                    API.setPlayerClothes(player, 3, 14, 0);
                    API.setPlayerClothes(player, 8, 3, 0);
                    API.setPlayerClothes(player, 4, 35, 0);
                    API.setPlayerClothes(player, 6, 26, 0);
                    API.setPlayerClothes(player, 5, 45, 0);

                    workSvalka = true;

                    return;
                }
                if (workSvalka == true)
                {
                    player.setData("IS_JOB_SVALKA", false);
                    API.sendNotificationToPlayer(player, "~b~�� ��������� ������.\n����������: ~g~" + sallary + "$");


                    API.triggerClientEvent(player, "EndMissionSvalka");
                    API.triggerClientEvent(player, "BaseMissionSvalka");
                    taskdone = 0;
                    Player.ChangeMoney(player, +sallary);
                    PlayerFunctions.Player.LoadPlayerClothes(player);
                    sallary = 0;

                    workSvalka = false;
                    gameSvalka = false;
                    return;
                }
            }


        }

        if (gameSvalka == true)
        {
            nextChek(player);

            if (eventName == "SvalkaMission")
            {

                if (SvalkaEndColshape.containsEntity(player.handle))
                {
                    taskdone++;

                    if (taskdone == 1)
                    {
                        API.sendPictureNotificationToPlayer(player, "�� ������ ����������, ������ ������� ���� ������. ����� B �� ������.", "CHAR_PLANESITE", 0, 3, "��������", "��������� � ���������");

                        API.triggerClientEvent(player, "EndMissionSvalka");
                        gameSvalka = false;
                        API.deleteColShape(SvalkaEndColshape);

                    }
                    BaseChek(player);
                }

            }
        }
        if (gameSvalka == false)
        {
            if (eventName == "BaseMission")
            {
                if (SvalkaBaseColshape.containsEntity(player.handle))
                {
                    taskdone = 0;
                    gameSvalka = true;
                    sallary += 310;
                    nextChek(player);
                    API.deleteColShape(SvalkaBaseColshape);
                    API.triggerClientEvent(player, "BaseMissionSvalka");
                }
            }
        }

    }

    public void BaseChek(Client player)
    {

        if (SvalkaBaseColshape != null)
        {
            API.deleteColShape(SvalkaBaseColshape);
        }


        Random random = new Random();

        List<Vector3> Base = new List<Vector3>();
        Base.Add(new Vector3(2357.676, 3136.937, 48.20874));
        Base.Add(new Vector3(2357.43, 3126.799, 48.20874));




        Vector3 Basedespoint = Base[random.Next(Base.Count)];

        SvalkaBaseColshape = API.createCylinderColShape(Basedespoint, 8f, 8f);


        API.triggerClientEvent(player, "nextBaseChek", Basedespoint, Basedespoint.X, Basedespoint.Y, Baseblip, Basemarker);
    }




    public void nextChek(Client player)
    {

        if (SvalkaEndColshape != null)
        {
            API.deleteColShape(SvalkaEndColshape);
        }


        Random random = new Random();

        List<Vector3> Chek = new List<Vector3>();
        Chek.Add(new Vector3(2409.064, 3032, 48.15259));
        Chek.Add(new Vector3(2431.029, 3153.047, 48.19677));
        Chek.Add(new Vector3(2341.429, 3055.884, 48.15186));
        Chek.Add(new Vector3(2357.676, 3136.937, 48.20874));
        Chek.Add(new Vector3(2368.607, 3039.661, 48.15235));
        Chek.Add(new Vector3(2388.042, 3034.88, 48.15289));
        Chek.Add(new Vector3(2353.74, 3032.172, 48.23861));
        Chek.Add(new Vector3(2347.238, 3070.565, 48.15216));
        Chek.Add(new Vector3(2364.937, 3071.026, 48.18328));
        Chek.Add(new Vector3(2350.069, 3075.251, 48.15232));
        Chek.Add(new Vector3(2335.623, 3048.81, 48.15165));
        Chek.Add(new Vector3(2388.03, 3052.556, 48.15289));
        Chek.Add(new Vector3(2391.237, 3078.848, 48.15306));
        Chek.Add(new Vector3(2405.66, 3085.012, 48.1529));
        Chek.Add(new Vector3(2407.247, 3116.759, 48.21737));
        Chek.Add(new Vector3(2404.329, 3141.416, 48.15341));
        Chek.Add(new Vector3(2412.813, 3146.467, 48.18349));
        Chek.Add(new Vector3(2419.6, 3126.485, 48.18661));
        Chek.Add(new Vector3(2427.369, 3136.97, 48.27655));
        Chek.Add(new Vector3(2415.294, 3091.238, 48.15292));
        Chek.Add(new Vector3(2406.732, 3066.384, 48.15286));
        Chek.Add(new Vector3(2401.027, 3051.739, 48.1528));
        Chek.Add(new Vector3(2432.24, 3101.827, 48.15308));
        Chek.Add(new Vector3(2403.408, 3097.446, 48.15304));
        Chek.Add(new Vector3(2372.927, 3080.116, 48.15312));

        Vector3 Svalkadespoint = Chek[random.Next(Chek.Count)];

        SvalkaEndColshape = API.createCylinderColShape(Svalkadespoint, 8f, 8f);


        API.triggerClientEvent(player, "nextChekSvalka", Svalkadespoint, Svalkadespoint.X, Svalkadespoint.Y, Svalkablip, Svalkamarker);
    }


}*/
