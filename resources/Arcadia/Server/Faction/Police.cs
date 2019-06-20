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

public class Police : Script
{
    public Police()
    {
        API.onResourceStart += onResourceStart;
    }

    public readonly Vector3 PoliceBlipPos = new Vector3(433.1855f, -981.7006f, 30.71008f);
    public readonly Vector3 PoliceDutyPos = new Vector3(450.8489f, -992.5302f, 30.68961f);
    public readonly Vector3 PolicePos = new Vector3(457.7044f, -992.4735f, 30.6896f);
    public readonly Vector3 PoliceColshapePos = new Vector3(457.7044f, -992.4735f, 30.6896f);
    //public readonly Vector3 PoliceArmoryPos = new Vector3(452.31f, -980.0972f, 30.6896f);

    public Blip PoliceBlip;

    public ColShape PoliceDutyColshape;
    //public ColShape PoliceArmoryColshape;

    public ColShape PoliceColshape;
    

    private MenuManager MenuManager = new MenuManager();
    private Menu savedMenu = null;

    #region PoliceFormeMenus
    private void Police_Form_Builder(Client client)
    {

        Menu menu = savedMenu;

        if (menu == null)
        {
            menu = new Menu("Police_Form", "Полицейская форма", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true)
            {
                BannerColor = new Color(200, 0, 0, 90),
                Callback = Police_Form_MenuManager
            };

            menu.Add(new MenuItem("Патрульная", "Форма для выезда в город", "Patrol")
            {
                ExecuteCallback = true
            });

            menu.Add(new MenuItem("Депортамент", "Форма для депортамента", "Dep")
            {
                ExecuteCallback = true
            });

            menu.Add(new MenuItem("Тренеровка", "Форма для общей тренировки", "Trening")
            {
                ExecuteCallback = true
            });

            menu.Add(new MenuItem("S.W.A.T.", "Спец форма", "SWAT")
            {
                ExecuteCallback = true
            });

            menu.Add(new MenuItem("Личная", "Переодеться в личную одежду", "NoMarker")
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
    private void Police_Form_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
    {
        int gender = (client.hasData("gender_id")) ? client.getData("gender_id") : 0;
        if (menu.Id == "Police_Form" && menuItem.Id == "Patrol")
        {
            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада одежду патрульной службы");
                API.shared.setPlayerClothes(client, 8, 129, 0);
                API.shared.setPlayerClothes(client, 6, 21, 0);
                API.shared.setPlayerClothes(client, 4, 35, 0);
                API.shared.setPlayerClothes(client, 11, 55, 0);
                API.shared.setPlayerClothes(client, 3, 0, 0);
                API.shared.setPlayerClothes(client, 10, 8, 1);
                API.shared.setPlayerAccessory(client, 0, 8, 0);
            }


            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада одежду патрульной службы");
                API.shared.setPlayerClothes(client, 11, 48, 0);
                API.shared.setPlayerClothes(client, 3, 14, 0);
                API.shared.setPlayerClothes(client, 4, 34, 0);
                API.shared.setPlayerClothes(client, 6, 25, 0);
                API.shared.setPlayerClothes(client, 7, 0, 0);
                API.shared.setPlayerClothes(client, 8, 35, 0);
                API.shared.setPlayerClothes(client, 10, 8, 3);
                API.shared.setPlayerAccessory(client, 0, 120, 0);
            }
        }

        else if (menu.Id == "Police_Form" && menuItem.Id == "Dep")
        {
            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли форму депортамента со склада");
                API.shared.setPlayerClothes(client, 11, 16, 0);
                API.shared.setPlayerClothes(client, 4, 46, 0);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 3, 19, 0);
                API.shared.setPlayerClothes(client, 8, 15, 0);
                API.shared.setPlayerAccessory(client, 0, 8, 0);
            }


            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли форму депортаментау со склада");
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 4, 48, 0);
                API.shared.setPlayerClothes(client, 3, 20, 0);
                API.shared.setPlayerClothes(client, 11, 49, 0);
                API.shared.setPlayerClothes(client, 8, 159, 0);
                API.shared.setPlayerAccessory(client, 0, 120, 0);
            }
        }

        else if (menu.Id == "Police_Form" && menuItem.Id == "Trening")
        {
            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада тренеровочный комплект одежды");
                API.shared.setPlayerClothes(client, 11, 50, 0);
                API.shared.setPlayerClothes(client, 3, 27, 0);
                API.shared.setPlayerAccessory(client, 0, 8, 0);
                API.shared.setPlayerClothes(client, 4, 9, 7);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 8, 15, 0);
            }


            if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли со склада тренеровочный комплект одежды");
                API.shared.setPlayerClothes(client, 3, 24, 0);
                API.shared.setPlayerClothes(client, 11, 233, 0);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 4, 33, 0);
                API.shared.setPlayerClothes(client, 8, 159, 0);
                API.shared.setPlayerAccessory(client, 0, 120, 0);
            }
        }

        else if (menu.Id == "Police_Form" && menuItem.Id == "SWAT")
        {
            if (gender == 0)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект S.W.A.T. со склада");
                API.shared.setPlayerClothes(client, 11, 251, 25);
                API.shared.setPlayerAccessory(client, 0, 8, 0);
                API.shared.setPlayerClothes(client, 4, 98, 25);
                API.shared.setPlayerClothes(client, 3, 115, 0);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 8, 15, 0);
            }


            else if (gender == 1)
            {
                API.sendChatMessageToPlayer(client, "~g~Вы взяли комплект S.W.A.T. со склада");
                API.shared.setPlayerClothes(client, 11, 259, 17);
                API.shared.setPlayerClothes(client, 4, 101, 17);
                API.shared.setPlayerClothes(client, 6, 24, 0);
                API.shared.setPlayerClothes(client, 3, 24, 0);
                API.shared.setPlayerClothes(client, 8, 159, 0);
                API.shared.setPlayerAccessory(client, 0, 120, 0);
            }
        }

        else if (menu.Id == "Police_Form" && menuItem.Id == "NoMarker")
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

        else if (menu.Id == "Police_Form" && menuItem.Id == "Close")
        {
            savedMenu = menu;
            MenuManager.CloseMenu(client);
        }
    }

    #endregion

    private void onResourceStart()
    {

        List<Vehicle> PoliceVehicles = new List<Vehicle>
        {
            API.createVehicle((VehicleHash)2046537925, new Vector3(408.575, -980.3037, 28.87436), new Vector3(0.07653642, 0.006250879, 50.62162), 111, 0, 0), // policecar1
            API.createVehicle((VehicleHash)2046537925, new Vector3(408.2172, -984.2205, 28.87307), new Vector3(0.02698381, -0.00483468, 52.18504), 111, 0, 0), // policecar2
            API.createVehicle((VehicleHash)2046537925, new Vector3(408.2721, -989.0557, 28.87272), new Vector3(0.01564, 0.003460356, 53.46225), 111, 0, 0), // policecar3
            API.createVehicle((VehicleHash)(-1627000575), new Vector3(408.0835, -993.5977, 28.87927), new Vector3(-0.1129259, -0.006557656, 51.38714), 111, 0, 0), // policecar4
            API.createVehicle((VehicleHash)1912215274, new Vector3(408.06, -998.3983, 29.02188), new Vector3(0.0166055, -0.005616191, 53.588), 111, 0, 0), // policecar5
            API.createVehicle((VehicleHash)1912215274, new Vector3(427.2122, -1028.304, 28.74593), new Vector3(0.1698515, 1.031104, 5.850772), 111, 0, 0), // policecar6
            API.createVehicle((VehicleHash)1912215274, new Vector3(430.2679, -1028.002, 28.69357), new Vector3(0.01568107, 1.038435, 7.077092), 111, 0, 0), // policecar7
            API.createVehicle((VehicleHash)1912215274, new Vector3(433.2898, -1027.618, 28.64005), new Vector3(-0.05488611, 1.109338, 6.741212), 111, 0, 0), // policecar8
            API.createVehicle((VehicleHash)(-1627000575), new Vector3(436.2347, -1027.368, 28.44386), new Vector3(-0.1664196, 1.051129, 4.204842), 111, 0, 0), // policecar9
            API.createVehicle((VehicleHash)(-1627000575), new Vector3(439.4106, -1027.157, 28.38727), new Vector3(-0.1323602, 1.066485, 6.270042), 111, 0, 0), // policecar10
            API.createVehicle((VehicleHash)(-1973172295), new Vector3(442.3672, -1026.805, 28.32507), new Vector3(-0.1991908, 1.086274, 4.806996), 1, 0, 0), // policecar11
            API.createVehicle((VehicleHash)(-1973172295), new Vector3(445.3407, -1026.488, 28.26944), new Vector3(-0.1381602, 1.094349, 5.276954), 1, 0, 0), // policecar12
            API.createVehicle((VehicleHash)(-1205689942), new Vector3(452.2735, -997.4243, 25.41239), new Vector3(-0.974342, -0.08793549, 179.7019), 111, 0, 0), // policecar13
            API.createVehicle((VehicleHash)456714581, new Vector3(447.3567, -996.8958, 25.75041), new Vector3(-0.7738963, -0.01550783, -179.8773), 111, 0, 0), // policecar14
            API.createVehicle((VehicleHash)(-34623805), new Vector3(430.3021, -1010.346, 28.03425), new Vector3(15.06246, -11.19892, -173.7246), 111, 0, 0), // policecar15
            API.createVehicle((VehicleHash)(-34623805), new Vector3(432.0983, -1010.299, 27.9743), new Vector3(15.21289, -8.647228, -176.6652), 111, 0, 0), // policecar16
            API.createVehicle((VehicleHash)(-34623805), new Vector3(433.6475, -1010.354, 27.946), new Vector3(15.36584, -7.607122, 179.1262), 111, 0, 0), // policecar17
            API.createVehicle((VehicleHash)456714581, new Vector3(436.8881, -1009.153, 28.08799), new Vector3(14.56102, -0.007781171, 176.7562), 111, 0, 0), // policecar18
            API.createVehicle((VehicleHash)353883353, new Vector3(450.1227, -981.2755, 44.0792), new Vector3(0.1084448, -0.009432706, 86.86617), 111, 0, 0), // policecar19
            API.createVehicle((VehicleHash)353883353, new Vector3(481.8991, -982.2967, 41.39547), new Vector3(-0.02937317, -0.007839713, 85.68633), 111, 0, 0) // policecar20
        };

        Vehicle polveh2;
        foreach (Vehicle polveh in PoliceVehicles)
        {
            API.setEntityData(polveh, "fraction_id", 2);
            API.setVehicleNumberPlate(polveh, "LSPD");
            API.setVehicleEngineStatus(polveh, false);

            polveh2 = polveh;
        }

        API.onPlayerEnterVehicle += (player, vehicle) =>
        {
            Client client;
            client = API.getPlayerFromHandle(player);
            int isPoliceFaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (API.getVehicleNumberPlate(vehicle) == "LSPD")
            {
                if (isPoliceFaction != 2)
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

        //PolVehiclesManager();

        PoliceBlip = API.createBlip(PoliceBlipPos);
        API.setBlipSprite(PoliceBlip, 60);
        API.setBlipScale(PoliceBlip, 1.0f);
        API.setBlipColor(PoliceBlip, 3);
        API.setBlipName(PoliceBlip, "Полицейский участок");
        API.setBlipShortRange(PoliceBlip, true);

        PoliceColshape = API.createCylinderColShape(PoliceColshapePos, 1.50f, 1f);
        PoliceDutyColshape = API.createCylinderColShape(PoliceDutyPos, 0.50f, 1f);
        //PoliceArmoryColshape = API.createCylinderColShape(PoliceArmoryPos, 0.50f, 1f);
        API.createMarker(1, PolicePos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createTextLabel("Получение формы", PolicePos, 15f, 0.65f);
        API.createMarker(1, PoliceDutyPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);
        //API.createMarker(1, PoliceArmoryPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 217, 217, 217);

        // Выход на дежурство
        PoliceDutyColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int ispolicefaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            string groupName = "R";
            var chatMessageOnDuty = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)) + " | " + player.name + "~y~ вышел на смену";
            var chatMessageOffDuty = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)) + " | " + player.name + "~y~ окончил смену";
            if (ispolicefaction == 2)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 0)
                {
                    Faction.SetPlayerFactionClothes(player);
                    Player.SetPlayerOnDuty(player);
                    foreach (Client client in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(client) == 2 && Player.IsPlayerOnDuty(client) == 1)
                        {
                            API.sendChatMessageToPlayer(client, chatMessageOnDuty);
                        }
                    }
                    API.sendChatMessageToPlayer(player, "~y~Вы вышли на смену как ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                }
                else
                {
                    Player.RemovePlayerOnDuty(player);
                    foreach (Client client in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(client) == 2 && Player.IsPlayerOnDuty(client) == 1)
                        {
                            API.sendChatMessageToPlayer(client, chatMessageOffDuty);
                        }
                    }
                    PlayerFunctions.Player.LoadPlayerClothes(player);
                    API.sendChatMessageToPlayer(player, "~y~Вы окончили смену");
                }
                return;
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции!");
            }
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
        };

        PoliceColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int ispolicefaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;
            if (ispolicefaction == 2)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 1)                
                 {
                    Police_Form_Builder(player);
                    return;
                }
                else API.sendChatMessageToPlayer(player, "~y~Вы не на смене ~b~");
            }
            else API.sendChatMessageToPlayer(player, "~y~Вы не принадлежите данной фракции~b~");
        };
        bool weaponcare = false;
        /*PoliceArmoryColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int ispolicefaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0; ;
            if (ispolicefaction == 2)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 1)
                {
                    if (weaponcare == false)
                    {
                        API.sendChatMessageToPlayer(player, "~y~Вы взяли оружие и снаряжение для ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                        API.givePlayerWeapon(player, WeaponHash.CombatPistol, 50, false, true);
                        API.givePlayerWeapon(player, WeaponHash.Flashlight, 1, false, true);
                        API.givePlayerWeapon(player, WeaponHash.Nightstick, 1, false, true);
                        API.givePlayerWeapon(player, WeaponHash.StunGun, 1, false, true);
                        API.setPlayerArmor(player, 100);
                        weaponcare = true;
                        return;
                    }
                    if (weaponcare == true)
                    {
                        API.removePlayerWeapon(player, WeaponHash.CombatPistol);
                        API.removePlayerWeapon(player, WeaponHash.Flashlight);
                        API.removePlayerWeapon(player, WeaponHash.Nightstick);
                        API.removePlayerWeapon(player, WeaponHash.StunGun);
                        API.setPlayerArmor(player, 0);
                        API.sendChatMessageToPlayer(player, "~y~Вы сдали оружие и снаряжение!");
                        weaponcare = false;
                        return;
                    }
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "~r~Вы не на дежурстве!");
                }
                return;
            }
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции!");
            }
        };*/
    }

    private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
    {
        if (API.getPlayerVehicleSeat(player) == -1 | API.getPlayerVehicleSeat(player) == -0)
        {
            if (Player.GetFractionId(player) != 2)
            {
                API.warpPlayerOutOfVehicle(player);
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции");
                return;
            }
            else return;
        }
    }

    [Command("duty")]
    public void dutyCommand(Client sender)
    {
        Faction.SetPlayerFactionClothes(sender);
    }
}
