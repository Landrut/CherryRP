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
using MenuManagement;

public class MerryWeather : Script
{
    public MerryWeather()
    {
        API.onResourceStart += onResourceStart;
    }

    public readonly Vector3 MerryWeatherBlipPos = new Vector3(112.2704f, -629.2507f, 44.22956f);
    public readonly Vector3 MerryWeatherDutyPos = new Vector3(-2349.84f, 3266.322f, 32.81076f);
    public readonly Vector3 MerryWeatherPos = new Vector3(-2425.677f, 3274.232f, 32.97781f);
    public readonly Vector3 MerryWeatherColshapePos = new Vector3(-2425.677f, 3274.232f, 32.97781f);
    /*
    public readonly Vector3 MerryweatherArmoryPos = new Vector3(-2345.665f, 3232.539f, 34.74294f);
    */
    public readonly Vector3 MerryweatherRepairPos = new Vector3(-1828.519f, 2977.739f, 32.80995f);

    public Blip MerryweatherBlip;

    public ColShape MerryWeatherColshape;
    public ColShape MerryweatherDutyColshape;
    public ColShape MerryweatherArmoryColshape;
    public ColShape MerryweatherRepairColshape;

    private MenuManager MenuManager = new MenuManager();
    private Menu savedMenu = null;

    #region MerryWeatherFormeMenus
    private void MerryWeather_Form_Builder(Client client)
    {

        Menu menu = savedMenu;

        if (menu == null)
        {
            menu = new Menu("MerryWeather_Form", "Тактическая форма", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true)
            {
                BannerColor = new Color(200, 0, 0, 90),
                Callback = MerryWeather_Form_MenuManager
            };

            menu.Add(new MenuItem("Тактическая форма №1", "Тактическая форма №1", "Tactical01")
            {
                ExecuteCallback = true
            });

            menu.Add(new MenuItem("Тактическая форма №2", "Тактическая форма №2", "Tactical02")
            {
                ExecuteCallback = true
            });

            menu.Add(new MenuItem("Тактическая форма №3", "Тактическая форма №3", "Tactical03")
            {
                ExecuteCallback = true
            });

            menu.Add(new MenuItem("Личная", "Переодеться в личную одежду", "NoMarker1")
            {
                ExecuteCallback = true
            });

            menu.Add(new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
            {
                ExecuteCallback = true
            });
        }
        MenuManager.OpenMenu(client, menu);
    }
    private void MerryWeather_Form_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
    {
        int gender = (client.hasData("gender_id")) ? client.getData("gender_id") : 0;
        if (menu.Id == "MerryWeather_Form" && menuItem.Id == "Tactical01")
        {
            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли тактический комплект со склада");
                API.shared.setPlayerClothes(client, 11, 46, 2);
                API.shared.setPlayerClothes(client, 8, 156, 0);
                API.shared.setPlayerClothes(client, 3, 18, 0);
                API.shared.setPlayerClothes(client, 4, 49, 0);
                API.shared.setPlayerClothes(client, 6, 66, 4);
                API.shared.setPlayerClothes(client, 2, 0, 0);
                API.shared.setPlayerClothes(client, 1, 126, 0);
                API.shared.setPlayerAccessory(client, 0, 123, 0);
            }

            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли тактический комплект со склада");
                API.shared.setPlayerClothes(client, 11, 61, 4);
                API.shared.setPlayerClothes(client, 8, 131, 15);
                API.shared.setPlayerClothes(client, 3, 17, 0);
                API.shared.setPlayerClothes(client, 4, 9, 7);
                API.shared.setPlayerClothes(client, 6, 12, 7);
                API.shared.setPlayerAccessory(client, 0, 58, 2);
            }
        }

        else if (menu.Id == "MerryWeather_Form" && menuItem.Id == "Tactical02")
        {
            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли тактический комплект со склада");
                API.shared.setPlayerClothes(client, 11, 73, 0);
                API.shared.setPlayerClothes(client, 8, 159, 0);
                API.shared.setPlayerClothes(client, 3, 0, 0);
                API.shared.setPlayerClothes(client, 4, 11, 1);
                API.shared.setPlayerClothes(client, 6, 7, 0);
                API.shared.setPlayerClothes(client, 9, 8, 1);
                API.shared.setPlayerAccessory(client, 7, 83, 0);
                API.shared.setPlayerAccessory(client, 0, 58, 2);
                API.shared.setPlayerClothes(client, 1, 121, 0);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли тактический комплект со склада");
                API.shared.setPlayerClothes(client, 11, 13, 0);
                API.shared.setPlayerClothes(client, 8, 122, 0);
                API.shared.setPlayerClothes(client, 3, 11, 0);
                API.shared.setPlayerClothes(client, 4, 25, 0);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 9, 11, 1);
                API.shared.setPlayerAccessory(client, 7, 112, 0);
                API.shared.setPlayerAccessory(client, 0, 58, 2);
                API.shared.setPlayerClothes(client, 1, 121, 0);
            }
        }

        else if (menu.Id == "MerryWeather_Form" && menuItem.Id == "Tactical03")
        {
            if (gender == 1)
            {
                API.shared.setPlayerClothes(client, 11, 251, 0);
                API.shared.setPlayerClothes(client, 8, 155, 0);
                API.shared.setPlayerClothes(client, 3, 18, 0);
                API.shared.setPlayerClothes(client, 4, 30, 0);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 1, 46, 0);
                API.shared.setPlayerAccessory(client, 0, 105, 20);
            }


            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли тактический комплект со склада");
                API.shared.setPlayerClothes(client, 11, 243, 0);
                API.shared.setPlayerClothes(client, 8, 125, 0);
                API.shared.setPlayerClothes(client, 3, 17, 0);
                API.shared.setPlayerClothes(client, 4, 33, 0);
                API.shared.setPlayerClothes(client, 6, 27, 0);
                API.shared.setPlayerClothes(client, 1, 46, 0);
                API.shared.setPlayerAccessory(client, 0, 106, 20);
            }
        }

        else if (menu.Id == "MerryWeather_Form" && menuItem.Id == "NoMarker1")
        {
            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы переоделись в личную одежду");
                PlayerFunctions.Player.LoadPlayerClothes(client);
            }


            else if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы переоделись в личную одежду");
                PlayerFunctions.Player.LoadPlayerClothes(client);
            }
        }

        else if (menu.Id == "MerryWeather_Form" && menuItem.Id == "Close")
        {
            savedMenu = menu;
            MenuManager.CloseMenu(client);
        }
        else return;
    }

    #endregion

    public void onResourceStart()
    {
        API.requestIpl("gr_grdlc_interior_placement_interior_0_grdlc_int_01_milo_");
        API.requestIpl("gr_grdlc_interior_placement_interior_1_grdlc_int_02_milo_");

        MerryweatherBlip = API.createBlip(MerryWeatherBlipPos);

        API.setBlipSprite(MerryweatherBlip, 527);
        API.setBlipScale(MerryweatherBlip, 1.0f);
        API.setBlipColor(MerryweatherBlip, 1);
        API.setBlipName(MerryweatherBlip, "Merryweather");
        API.setBlipShortRange(MerryweatherBlip, true);

        MerryweatherDutyColshape = API.createCylinderColShape(MerryWeatherDutyPos, 0.50f, 1f);
        /*
        MerryweatherArmoryColshape = API.createCylinderColShape(MerryweatherArmoryPos, 0.50f, 1f);
        */
        MerryweatherRepairColshape = API.createCylinderColShape(MerryweatherRepairPos, 0.50f, 1f);
        MerryWeatherColshape = API.createCylinderColShape(MerryWeatherColshapePos, 0.50f, 1f);

        API.createMarker(25, MerryWeatherDutyPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        API.createTextLabel("Получение формы", MerryWeatherDutyPos, 15f, 0.65f);
        API.createMarker(1, MerryWeatherPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Получение формы", MerryWeatherPos, 15f, 0.65f);
        /*
        API.createMarker(25, MerryweatherArmoryPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        API.createTextLabel("Получение снаряжения", MerryweatherArmoryPos, 15f, 0.65f);
        */



        List<Vehicle> MeryWetherVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)1653666139, new Vector3(-2359.317, 3349.458, 32.93865), new Vector3(-0.2454477, -0.0149856, -29.56068), 0, 36, 0), // Mary1
            API.createVehicle((VehicleHash)1653666139, new Vector3(-2368.548, 3354.809, 32.93827), new Vector3(-0.2646534, 0.01963812, -29.46087), 0, 36, 0), // Mary2
            API.createVehicle((VehicleHash)1653666139, new Vector3(-2378.567, 3360.616, 32.93843), new Vector3(-0.2677503, 0.002282868, -28.66977), 0, 36, 0), // Mary3
            API.createVehicle((VehicleHash)1653666139, new Vector3(-2387.9, 3365.675, 32.93805), new Vector3(-0.2541145, 0.005066502, -28.11527), 0, 36, 0), // Mary4
            API.createVehicle((VehicleHash)1945374990, new Vector3(-2377.011, 3384.723, 33.06487), new Vector3(0.04109205, 0.2735018, 150.6099), 0, 36, 0), // Mary5
            API.createVehicle((VehicleHash)1945374990, new Vector3(-2368.475, 3380.1, 33.0632), new Vector3(0.07536086, -0.00156876, 150.5195), 0, 36, 0), // Mary6
            API.createVehicle((VehicleHash)1945374990, new Vector3(-2358.937, 3374.618, 33.06356), new Vector3(0.1092122, 0.0008588478, 150.8325), 0, 36, 0), // Mary7
            API.createVehicle((VehicleHash)1945374990, new Vector3(-2349.279, 3368.774, 33.06382), new Vector3(-0.04592398, -0.007899155, 149.8007), 0, 36, 0), // Mary8
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2337.332, 3315.02, 32.5999), new Vector3(0.244209, -0.03501998, 62.34147), 0, 28, 0), // Mary9
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2355.68, 3324.131, 32.59917), new Vector3(0.2412973, 0.001630595, -117.8575), 0, 28, 0), // Mary10
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2303.011, 3280.051, 32.59669), new Vector3(0.2014661, 0.0005587249, 60.35796), 0, 28, 0), // Mary11
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2304.552, 3277.228, 32.59656), new Vector3(0.2046905, 0.004473435, 60.9233), 0, 28, 0), // Mary12
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2306.301, 3274.208, 32.59611), new Vector3(0.1485702, 0.006754613, 60.8162), 0, 28, 0), // Mary13
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2308.005, 3271.266, 32.59635), new Vector3(0.1835782, -0.001359488, 60.42923), 0, 28, 0), // Mary14
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2309.73, 3268.259, 32.59729), new Vector3(0.1860843, 0.001378618, 60.86025), 0, 28, 0), // Mary15
            API.createVehicle((VehicleHash)(-511601230), new Vector3(-2412.855, 3272.002, 32.11257), new Vector3(-0.2349459, 0.01428522, 60.49224), 0, 28, 0), // Mary16
            API.createVehicle((VehicleHash)(-511601230), new Vector3(-2411.188, 3274.884, 32.11304), new Vector3(-0.2236331, -0.1650059, 58.94695), 0, 28, 0), // Mary17
            API.createVehicle((VehicleHash)(-432008408), new Vector3(-2409.429, 3277.904, 32.74189), new Vector3(-0.1034639, 0.0006182863, 60.63686), 0, 28, 0), // Mary18
            API.createVehicle((VehicleHash)(-432008408), new Vector3(-2407.657, 3280.932, 32.74213), new Vector3(-0.07692336, 0.009856001, 61.04718), 0, 28, 0), // Mary19
            API.createVehicle((VehicleHash)(-432008408), new Vector3(-2405.94, 3283.909, 32.74171), new Vector3(-0.1007174, -0.002721234, 59.75434), 0, 28, 0), // Mary20
            API.createVehicle((VehicleHash)(-432008408), new Vector3(-2404.153, 3287.053, 32.74208), new Vector3(-0.007895218, -4.097717E-05, 62.67538), 0, 28, 0), // Mary21
            API.createVehicle((VehicleHash)683047626, new Vector3(-2430.665, 3282.708, 33.05544), new Vector3(0.6033719, -0.01089913, -119.4423), 0, 28, 0), // Mary22
            API.createVehicle((VehicleHash)683047626, new Vector3(-2428.866, 3285.744, 33.05391), new Vector3(0.5289949, 0.008518927, -118.3361), 0, 28, 0), // Mary23
            API.createVehicle((VehicleHash)683047626, new Vector3(-2427.2, 3288.54, 33.05656), new Vector3(0.4242814, 0.001648789, -119.4394), 0, 28, 0), // Mary24
            API.createVehicle((VehicleHash)683047626, new Vector3(-2425.455, 3291.75, 33.05443), new Vector3(0.4905708, 0.01726652, -118.8627), 0, 28, 0), // Mary25
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2424.148, 3294.58, 32.59882), new Vector3(0.1106486, -0.02463895, -118.8251), 0, 28, 0), // Mary26
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2422.466, 3297.533, 32.59979), new Vector3(0.1998291, 0.0002312006, -119.2993), 0, 28, 0), // Mary27
            API.createVehicle((VehicleHash)2071877360, new Vector3(-2347.345, 3242.453, 32.75851), new Vector3(-0.06813934, -0.01978259, -30.49128), 0, 28, 0), // Mary28
            API.createVehicle((VehicleHash)2071877360, new Vector3(-2343.543, 3249.305, 32.7574), new Vector3(0.03692268, -0.01599846, -28.4003), 0, 28, 0), // Mary29
            API.createVehicle((VehicleHash)(-2107990196), new Vector3(-2419.262, 3323.065, 33.10115), new Vector3(0.1060149, 0.008154558, -117.6202), 0, 28, 0), // Mary30
            API.createVehicle((VehicleHash)(-2107990196), new Vector3(-2416.648, 3328.398, 33.10049), new Vector3(0.05105883, 0.03153393, -117.7863), 0, 28, 0), // Mary31
            API.createVehicle((VehicleHash)(-2107990196), new Vector3(-2413.413, 3334.379, 33.10099), new Vector3(0.01527569, 0.01102693, -118.1876), 0, 28, 0), // Mary32
            API.createVehicle((VehicleHash)745926877, new Vector3(-2195.58, 3190.212, 32.70917), new Vector3(0.0003529487, 0.0589204, -29.84916), 0, 28, 0), // Mary33
            API.createVehicle((VehicleHash)745926877, new Vector3(-2187.35, 3185.435, 32.70818), new Vector3(0.002407401, 0.06197568, -33.87919), 0, 28, 0), // Mary34
            API.createVehicle((VehicleHash)788747387, new Vector3(-2197.154, 3178.155, 32.7093), new Vector3(0.007515812, 0.05991996, -28.655), 0, 28, 0), // Mary35
            API.createVehicle((VehicleHash)1254014755, new Vector3(-2188.369, 3207.902, 32.46917), new Vector3(-0.01295536, 0.007228957, -123.541), 0, 28, 0), // Mary36
            API.createVehicle((VehicleHash)1254014755, new Vector3(-2163.266, 3193.331, 32.46914), new Vector3(-0.004345556, -0.005180583, 60.17538), 0, 28, 0), // Mary37
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2166.823, 3235.714, 32.58024), new Vector3(0.2383244, -0.03436871, 150.4771), 0, 28, 0), // Mary38
            API.createVehicle((VehicleHash)(-2064372143), new Vector3(-2141.161, 3220.046, 32.5809), new Vector3(0.05740222, 0.002996811, 149.9898), 0, 28, 0), // Mary39
            API.createVehicle((VehicleHash)165154707, new Vector3(-2143.113, 3245.749, 33.97924), new Vector3(-0.02574513, 0.001697058, 150.3372), 0, 28, 0), // Mary40
            API.createVehicle((VehicleHash)1682114128, new Vector3(-2315.299, 3327.056, 32.32494), new Vector3(-0.03280766, 0.00376665, 149.0947), 66, 157, 0), // carMwPatrol
            API.createVehicle((VehicleHash)1682114128, new Vector3(-2317.944, 3328.51, 32.32617), new Vector3(0.03113135, 0.02903057, 148.7201), 17, 73, 0) // carMwPatrol2
        };

        Vehicle meryveh2;
        foreach (Vehicle meryveh in MeryWetherVehicles)
        {
            API.setEntityData(meryveh, "fraction_id", 3);
            API.setVehicleNumberPlate(meryveh, "M.W.");
            API.setVehicleEngineStatus(meryveh, false);

            meryveh2 = meryveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int isMerryWeatherfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "M.W.")
            {
                if (isMerryWeatherfaction != 3)
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

        MerryWeatherColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int isMerryWeatherfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (isMerryWeatherfaction == 3)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 1)
                {
                    MerryWeather_Form_Builder(player);
                    return;
                }
                else API.sendChatMessageToPlayer(player, "~y~Вы не на смене ~b~");
            }
            else API.sendChatMessageToPlayer(player, "~y~Вы не принадлежите данной фракции~b~");
        };

        bool weaponcare = false;
        /*
        MerryweatherArmoryColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int lsMerryweatherfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0; ;
            if (lsMerryweatherfaction == 3)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 1)
                {
                    if (weaponcare == false)
                    {
                        API.sendChatMessageToPlayer(player, "~y~Вы взяли оружие и снаряжение для ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                        API.sendPictureNotificationToPlayer(player, "Отметил в журнале. Свободен!", "CHAR_MP_MERRYWEATHER", 0, 3, "Пост наблюдения", "Обращение к сотруднику");
                        API.givePlayerWeapon(player, WeaponHash.AssaultSMG, 300, false, true);
                        API.givePlayerWeapon(player, WeaponHash.HeavyPistol, 150, false, true);
                        API.givePlayerWeapon(player, WeaponHash.BullpupShotgun, 35, false, true);
                        API.givePlayerWeapon(player, WeaponHash.Parachute, 1, false, true);
                        API.setPlayerArmor(player, 100);
                        weaponcare = true;
                        return;
                    }
                    if (weaponcare == true)
                    {
                        API.removePlayerWeapon(player, WeaponHash.AssaultSMG);
                        API.removePlayerWeapon(player, WeaponHash.HeavyPistol);
                        API.removePlayerWeapon(player, WeaponHash.BullpupShotgun);
                        API.removePlayerWeapon(player, WeaponHash.Parachute);
                        API.setPlayerArmor(player, 0);
                        API.sendPictureNotificationToPlayer(player, "Проверил, все в норме.", "CHAR_MP_MERRYWEATHER", 0, 3, "Пост наблюдения", "Обращение к сотруднику");
                        weaponcare = false;
                        return;
                    }
                }
                else
                {
                    API.sendPictureNotificationToPlayer(player, "Небыло распорежения на выдачу твоего снаряжения!", "CHAR_MP_MERRYWEATHER", 0, 3, "Пост наблюдения", "Обращение к сотруднику");
                }
                return;
            }
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                API.sendPictureNotificationToPlayer(player, "Ты че тут трешся,гражданский?!", "CHAR_MP_MERRYWEATHER", 0, 3, "Пост наблюдения", "Обращение к посетителю");
            }
        };
        */

          
                MerryweatherDutyColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int isMerryWeatherfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0; ;
            if (isMerryWeatherfaction == 3)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 0)
                {
                    Faction.SetPlayerFactionClothes(player);
                    Player.SetPlayerOnDuty(player);                   
                    API.sendChatMessageToPlayer(player,"~y~Вы переоделись в форму: ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
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
            if (Player.GetFractionId(player) != 3)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции");
                return;
            }
            else return;
        }
    }
}
