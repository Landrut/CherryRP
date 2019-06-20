using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//
//
using CherryMPServer;
using CherryMPServer.Constant;
using CherryMPShared;
//;

using MySQL;
using PlayerFunctions;
using CustomSkin;
using System.Collections.Generic;
using System;

public class Army : Script
{
    public Army()
    {
        API.onResourceStart += onResourceStart;
    }
    public readonly Vector3 ArmyBlipPos = new Vector3(815.4872f, -2126.45f, 29.31619f);
    public readonly Vector3 ArmyDutyPos = new Vector3(844.0386f, -2118.429f, 30.52108f);


    public Blip ArmyBlip;

    public ColShape ArmyDutyColshape;


    public void onResourceStart()
    {
        API.requestIpl("hei_carrier");
        API.requestIpl("hei_carrier_DistantLights");
        API.requestIpl("hei_Carrier_int1");
        API.requestIpl(" hei_Carrier_int2");
        API.requestIpl("hei_Carrier_int3");
        API.requestIpl("hei_Carrier_int4");
        API.requestIpl("hei_Carrier_int5");
        API.requestIpl("hei_Carrier_int6");
        API.requestIpl("hei_carrier_LODLights");

        ArmyBlip = API.createBlip(ArmyBlipPos);

        API.setBlipSprite(ArmyBlip, 487);
        API.setBlipScale(ArmyBlip, 1.0f);
        API.setBlipColor(ArmyBlip, 1);
        API.setBlipName(ArmyBlip, "Army");
        API.setBlipShortRange(ArmyBlip, true);

        ArmyDutyColshape = API.createCylinderColShape(ArmyDutyPos, 0.50f, 1f);

        API.createMarker(25, ArmyDutyPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        API.createTextLabel("Получение формы", ArmyDutyPos, 15f, 0.65f);




        List<Vehicle> ArmyVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)(-823509173), new Vector3(835.278, -2118.111, 29.14787), new Vector3(-3.134582, 0.1412496, 84.99624), 111, 0, 0), // Army_Car
            API.createVehicle((VehicleHash)(-823509173), new Vector3(825.7736, -2117.493, 28.97602), new Vector3(-0.6848078, -0.7877105, 86.10221), 111, 0, 0), // Army_Car2
            API.createVehicle((VehicleHash)(-1146969353), new Vector3(819.2529, -2116.858, 28.82614), new Vector3(-0.4567509, 0.02510022, 175.3496), 129, 114, 0), // Army_Car3
            API.createVehicle((VehicleHash)(-1146969353), new Vector3(815.4255, -2116.641, 28.8228), new Vector3(-0.515286, -0.004955069, 174.6513), 50, 1, 0), // Army_Car4
            API.createVehicle((VehicleHash)1542143200, new Vector3(811.783, -2116.186, 28.81852), new Vector3(-0.1361366, 0.06066645, 174.3286), 109, 142, 0), // Army_Car5
            API.createVehicle((VehicleHash)321739290, new Vector3(796.8906, -2131.97, 29.02273), new Vector3(-0.3761546, -0.4808584, -91.71474), 111, 1, 0), // Army_Car6
            API.createVehicle((VehicleHash)321739290, new Vector3(802.748, -2134.069, 28.95463), new Vector3(-0.3783469, 1.837953, -0.5256016), 111, 1, 0), // Army_Car7
            API.createVehicle((VehicleHash)321739290, new Vector3(824.2248, -2145.892, 28.26281), new Vector3(1.05879, 0.09066314, -1.974083), 111, 1, 0), // Army_Car8
            API.createVehicle((VehicleHash)321739290, new Vector3(819.8726, -2145.749, 28.26754), new Vector3(1.322264, 0.08295331, 0.1562182), 111, 1, 0), // Army_Car9
            API.createVehicle((VehicleHash)321739290, new Vector3(822.0791, -2141.333, 28.48632), new Vector3(4.061747, 0.02709316, 0.1294673), 111, 1, 0), // Army_Car10
            API.createVehicle((VehicleHash)1074326203, new Vector3(816.5648, -2144.318, 29.04825), new Vector3(-1.541004, 0.7224154, -1.017093), 111, 1, 0), // Army_Car11
            API.createVehicle((VehicleHash)562680400, new Vector3(840.7809, -2128.794, 29.4499), new Vector3(-1.826458, -0.7987049, 89.28819), 100, 18, 0), // Army_Car12
            API.createVehicle((VehicleHash)(-1881846085), new Vector3(833.9366, -2139.382, 29.21306), new Vector3(0.05817845, 0.01292359, 92.44168), 3, 90, 0), // Army)Car13
            API.createVehicle((VehicleHash)(-1881846085), new Vector3(834.345, -2144.516, 29.20978), new Vector3(-0.03875372, 0.09531689, 91.14621), 25, 37, 0), // Army)Car14
            API.createVehicle((VehicleHash)(-432008408), new Vector3(836.9545, -2122.487, 29.49898), new Vector3(-2.854787, 0.1638338, 87.19384), 111, 1, 0), // Army)Car15
            API.createVehicle((VehicleHash)(-1281684762), new Vector3(3070.387, -4616.003, 15.76435), new Vector3(0.9020855, 0.0110398, 105.5503), 111, 1, 0), // Army_Avia
            API.createVehicle((VehicleHash)(-1281684762), new Vector3(3085.494, -4670.279, 15.76649), new Vector3(0.8437865, 0.01420063, 105.0808), 111, 1, 0), // Army_Avia2
            API.createVehicle((VehicleHash)(-1281684762), new Vector3(3103.457, -4733.889, 15.76549), new Vector3(0.9428105, 0.01248492, 106.8001), 111, 1, 0), // Army_Avia3
            API.createVehicle((VehicleHash)970385471, new Vector3(3110.832, -4763.3, 15.79739), new Vector3(-0.3174028, 0.04605205, -163.8904), 111, 1, 0), // Army_Avia4
            API.createVehicle((VehicleHash)(-1281684762), new Vector3(3118.982, -4787.949, 15.76489), new Vector3(0.9156489, 0.01041636, 106.074), 111, 1, 0), // Army_Avia5
            API.createVehicle((VehicleHash)867467158, new Vector3(3082.915, -4800.251, 0.9622337), new Vector3(1.220303, 1.229492, 92.30804), 111, 1, 0), // Army_Air7
            API.createVehicle((VehicleHash)867467158, new Vector3(3093.423, -4806.472, -1.177512), new Vector3(-0.0255864, 1.705873, 96.75589), 111, 1, 0), // Army_Air8
            API.createVehicle((VehicleHash)867467158, new Vector3(3091.502, -4802.824, -0.2093117), new Vector3(1.295923, -9.546061, 107.3001), 111, 1, 0), // Army_Air9
            API.createVehicle((VehicleHash)(-50547061), new Vector3(3063.357, -4817.089, 15.72355), new Vector3(0.05463891, 0.08693154, -73.23582), 111, 1, 0), // Army_Air10
            API.createVehicle((VehicleHash)(-82626025), new Vector3(3048.442, -4769.022, 15.75998), new Vector3(0.02154884, 0.002810734, -73.83563), 111, 1, 0), // Army_Air11
            API.createVehicle((VehicleHash)(-82626025), new Vector3(3044.498, -4755.862, 15.76002), new Vector3(0.02221027, 3.392583E-05, -68.26829), 111, 1, 0), // Army_Air12
            API.createVehicle((VehicleHash)(-82626025), new Vector3(3038.925, -4739.064, 15.76003), new Vector3(0.02096428, 0.003766929, -67.31271), 111, 1, 0), // Army_Air13
            API.createVehicle((VehicleHash)1543134283, new Vector3(3036.145, -4717.002, 15.64869), new Vector3(0.2546057, -0.002639442, 19.0266), 111, 1, 0), // Army_Air15
            API.createVehicle((VehicleHash)1543134283, new Vector3(3029.676, -4695.961, 15.64853), new Vector3(0.2587092, -0.005822643, 18.1099), 111, 1, 0), // Army_Air16
            API.createVehicle((VehicleHash)562680400, new Vector3(3091.75, -4720.145, 14.95638), new Vector3(0.009355691, -0.0002157671, 103.2022), 15, 154, 0), // Army_Air17
            API.createVehicle((VehicleHash)562680400, new Vector3(3091.029, -4715.801, 14.95754), new Vector3(0.009662921, 0.02678397, 105.7407), 7, 26, 0) // Army_Air18
        };
        Vehicle Armyveh2;
        foreach (Vehicle Armyveh in ArmyVehicles)
        {
            API.setEntityData(Armyveh, "fraction_id", 8);
            API.setVehicleNumberPlate(Armyveh, "Army");
            API.setVehicleEngineStatus(Armyveh, false);

            Armyveh2 = Armyveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int isArmyfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "Army")
            {
                if (isArmyfaction != 8)
                {
                    if (API.getPlayerVehicleSeat(client) == -1)
                    {
                        API.sendChatMessageToPlayer(client, "Вы не состоите в данной фракции!");
                        API.warpPlayerOutOfVehicle(client);
                        return;
                    }
                    else return;
                }
                else return;
            }
            else return;
        };

        ArmyDutyColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int isArmyfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0; ;
            if (isArmyfaction == 8)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 0)
                {
                    Faction.SetPlayerFactionClothes(player);
                    Player.SetPlayerOnDuty(player);
                    API.sendChatMessageToPlayer(player, "~y~Вы переоделись в форму: ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                    API.sendPictureNotificationToPlayer(player, "Сообщу постам о твоем прибытии", "CHAR_MP_MERRYWEATHER", 0, 3, "Пост наблюдения", "Обращение к сотруднику");
                }
                else
                {
                    Player.RemovePlayerOnDuty(player);

                    PlayerFunctions.Player.LoadPlayerClothes(player);
                    API.sendPictureNotificationToPlayer(player, "Запись есть, свободен!", "CHAR_MP_MERRYWEATHER", 0, 3, "Пост наблюдения", "Обращение к сотруднику");
                }
                return;
            }
            else
            {
                API.sendPictureNotificationToPlayer(player, "Ты кто такой?", "CHAR_MP_MERRYWEATHER", 0, 3, "Пост наблюдения", "Обращение к посетителю");
            }
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
        };

    }
    private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (Player.GetFractionId(player) != 8)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции");
                return;
            }
            else return;
        }
    }
}