using System;
using System.Collections.Generic;
using System.Linq;
//
//
using CherryMPServer;
using CherryMPServer.Constant;
using CherryMPShared;
//;
using System.Reflection;

using PlayerFunctions;
using MySQL;

public class Commands : Script
{
    public List<Vector3> TPpoints = new List<Vector3>
    {
        new Vector3 (-237.172, -650.3887, 33.30411),
    };

    // чат

    [Command("tpcord")]
    public void TpCord(Client player, float x, float y, float z)
    {
        Vector3 pos = new Vector3(x, y, z);
        API.setEntityPosition(player, pos);
    }

    [Command("weapon", Alias = "gun")]
    public void GiveWeaponCommand(Client sender, WeaponHash weapon)
    {
        API.givePlayerWeapon(sender, weapon, 9999, true, true);
    }

    [Command("loadipl")]
    public void LoadIplCommand(Client sender, string ipl)
    {
        API.requestIpl(ipl);
        API.consoleOutput("LOADED IPL " + ipl);
        API.sendChatMessageToPlayer(sender, "Loaded IPL ~b~" + ipl + "~w~.");
    }

    [Command("removeipl")]
    public void RemoveIplCommand(Client sender, string ipl)
    {
        API.removeIpl(ipl);
        API.consoleOutput("REMOVED IPL " + ipl);
        API.sendChatMessageToPlayer(sender, "Removed IPL ~b~" + ipl + "~w~.");
    }

    [Command("powerup")]
    public void PowerUp(Client sender, float power)
    {
        NetHandle vehicle = API.getPlayerVehicle(sender);
        API.setVehicleEnginePowerMultiplier(vehicle, power);
    }
    [Command("setdim")]
    public void SetDimensionCommand(Client sender, string idOrName, int dimension)
    {
        if (Player.GetRightsLevel(sender) >= 3)
        {
            Client target = API.exported.playerids.findPlayer(sender, idOrName);
            API.setEntityDimension(target, dimension);
            API.sendChatMessageToPlayer(sender, "~y~Вы установили " + "~w~" + target.name + " ~w~" + target.dimension + " ~y~вирт мир");
        }
        else return;
    }

    [Command("me", GreedyArg = true)]
    public void MeCommand(Client player, string text)
    {
        var msg = "* " + player.name + " " + text;
        var players = API.getPlayersInRadiusOfPlayer(30, player);

        foreach (Client c in players)
        {
            API.sendChatMessageToPlayer(c, "~#eb7ae3~", msg);
        }
    }

    [Command("do", GreedyArg = true)]
    public void DoCommand(Client player, string text)
    {
        var msg = text + " | " + player.name;
        var players = API.getPlayersInRadiusOfPlayer(30, player);

        foreach (Client c in players)
        {
            API.sendChatMessageToPlayer(c, "~#eb7ae3~", msg);
        }
    }

    [Command("coffee", GreedyArg = true)]
    public void CoffeeCommand(Client sender)
    {

        var prop = API.createObject(API.getHashKey("prop_cs_paper_cup"), API.getEntityPosition(sender.handle), new Vector3());
        API.attachEntityToEntity(prop, sender.handle, "IK_R_Hand", new Vector3(0.09f, 0f, -0.027f), new Vector3(110f, 200f, -33f));

    }

    [Command("pat")]
    public void PatCommand(Client sender, string model, string bone, float x1, float y1, float z1, float x2, float y2, float z2)
    {

        var prop = API.createObject(API.getHashKey(model), API.getEntityPosition(sender.handle), new Vector3());
        API.attachEntityToEntity(prop, sender.handle, bone, new Vector3(x1, y1, z1), new Vector3(x2, y2, z2));

    }

    [Command("odel")]
    public void OdelCommand(Client sender, string model)
    {
        int modelhash = API.getHashKey(model);
        Vector3 position = API.getEntityPosition(sender);
        API.deleteObject(sender, position, modelhash);

    }

    [Command("anima")]
    public void AnimaComand(Client sender, int flag, string Dict, string name)
    {
        API.playPlayerAnimation(sender, flag, Dict, name);
    }

    [Command("try", GreedyArg = true)]
    public void TryCommand(Client player, string text)
    {
        Random rnd = new Random();

        string luck = "~g~удачно";
        string fault = "~r~неудачно";

        int res = rnd.Next(1, 100);

        if (res < 50)
        {
            var msg = "* " + player.name + " " + text + " | " + luck;

            var players = API.getPlayersInRadiusOfPlayer(30, player);

            foreach (Client c in players)
            {
                API.sendChatMessageToPlayer(c, "~#eb7ae3~", msg);
            }
            return;
        }

        if (res > 50)
        {
            var msg = "* " + player.name + " " + text + " | " + fault;

            var players = API.getPlayersInRadiusOfPlayer(30, player);

            foreach (Client c in players)
            {
                API.sendChatMessageToPlayer(c, "~#eb7ae3~", msg);
            }
            return;
        }
    }

    [Command("w", GreedyArg = true)]
    public void WhisperCommand(Client player, string message)
    {
        var msg = "~b~[" + API.exported.playerids.getIdFromClient(player) + "] " + "~c~" + player.name + " шепчет: " + message;
        var players = API.getPlayersInRadiusOfPlayer(10, player);

        foreach (Client c in players)
        {
            API.sendChatMessageToPlayer(c, msg);
        }
    }

    [Command("s", GreedyArg = true)]
    public void ShoutCommand(Client player, string message)
    {
        var msg = "~b~[" + API.exported.playerids.getIdFromClient(player) + "] " + "~w~" + player.name + " кричит: " + message;
        var players = API.getPlayersInRadiusOfPlayer(10, player);

        foreach (Client c in players)
        {
            API.sendChatMessageToPlayer(c, msg);
        }
    }

    [Command("b", GreedyArg = true)]
    public void NonrpCommand(Client player, string message)
    {
        var msg = "~b~[" + API.exported.playerids.getIdFromClient(player) + "] " + "~w~" + player.name + ": " + "~b~" + message;
        var players = API.getPlayersInRadiusOfPlayer(30, player);

        foreach (Client c in players)
        {
            API.sendChatMessageToPlayer(c, msg);
        }
    }

    [Command("tp")]
    public void tpCommand(Client sender, Vector3 tpname)
    {
        Vector3 lspd = new Vector3 (-237.172, -650.3887, 33.30411);

        API.setEntityPosition(sender, lspd);
    }

    [Command("gettransp")]
    public void GetTranparencyCommand(Client sender)
    {
        var transp = API.getEntityTransparency(sender);
        API.sendChatMessageToPlayer(sender, "Transp: " + transp);
    }

    [Command("onwork")]
    public void CommandOnWork(Client sender, bool mode = false)
    {
        if (mode == true)
        {
            int Transparency = API.getEntityTransparency(sender);
            if (Transparency >= 100)
            {
                API.setEntityTransparency(sender, Transparency = 0);
                API.setEntityInvincible(sender, true);
            }
            else
            {
                API.setEntityTransparency(sender, Transparency = 255);
                API.setEntityInvincible(sender, false);
            }
        }
        else
        {
            int Transparency = API.getEntityTransparency(sender);
            if (Transparency != 100)
            {
                API.setEntityTransparency(sender, Transparency = 100);
                API.setEntityInvincible(sender, true);
            }
            else
            {
                API.setEntityTransparency(sender, Transparency = 255);
                API.setEntityInvincible(sender, false);
            }
        }
    }

    [Command("skin")]
    public void ChangeSkinCommand(Client sender, PedHash model)
    {
        API.setPlayerSkin(sender, model);
        API.sendNativeToPlayer(sender, Hash.SET_PED_DEFAULT_COMPONENT_VARIATION, sender.handle);
    }

    [Command("stats")]
    public void Command_Stats(Client player)
    {
        API.sendChatMessageToPlayer(player, "________ Статистика ________");
        API.sendChatMessageToPlayer(player, "Наличные: ~y~" + Player.GetMoney(player) + "~g~$");
        API.sendChatMessageToPlayer(player, "Статус: " + Rights.GetAdminRank(Player.GetRightsLevel(player)));
        API.sendChatMessageToPlayer(player, "Фракция: " + Faction.GetPlayerFractionInfo(player, Player.GetFractionId(player)));
        API.sendChatMessageToPlayer(player, "Ранг: " + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
        API.sendChatMessageToPlayer(player, "Работа: " + JobFactionModel.GetPlayerJobFactionInfo(player, Player.GetJobId(player)));
        API.sendChatMessageToPlayer(player, "Должность: " + JobFactionModel.GetPlayerJobRank(player, Player.GetJobRank(player)));
    }

    [Command("pos")]
    public void GetPosition(Client player)
    {
        Vector3 PlayerPos = API.getEntityPosition(player);
        Vector3 clientRotation = API.getEntityRotation(player.handle);
        API.sendChatMessageToPlayer(player, "X: " + PlayerPos.X + " Y: " + PlayerPos.Y + " Z: " + PlayerPos.Z + " R: " + clientRotation.Z);
    }

    private static Random Rnd = new Random();
    public Dictionary<Client, List<NetHandle>> VehicleHistory = new Dictionary<Client, List<NetHandle>>();
    public List<VehicleHash> BannedVehicles = new List<VehicleHash>
    {
        VehicleHash.CargoPlane,
    };

    private Client getVehicleOwner(NetHandle vehicle)
    {
        foreach (var client in VehicleHistory.Keys)
        {
            foreach (var v in VehicleHistory[client])
            {
                if (v.Value == vehicle.Value)
                    return client;
            }
        }
        return null;
    }

    private string getRandomNumberPlate(Client client = null)
    {
        if (client != null)
        {
            string strClientName = client.name;
            if (strClientName.Length <= 8)
                return strClientName.ToUpper();
        }
        string strCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string strRet = "";
        for (int i = 0; i < 8; i++)
        {
            strRet += strCharacters[Rnd.Next(strCharacters.Length)];
        }
        return strRet;
    }

    [Command("veh")]
    public void Vehicle(Client sender, VehicleHash vehicle, int color1, int color2)
    {
        Vehicle veh = API.createVehicle(vehicle, sender.position, sender.rotation, color1, color2);
        API.setPlayerIntoVehicle(sender, veh, -1);
    }

    [Command("livery")]
    public void Livery(Client sender, int livery)
    {
        API.setVehicleLivery(sender.vehicle, livery);
    }

    [Command("car", Alias = "v")]
    public void SpawnCarCommand(Client sender, VehicleHash model)
    {
        if (BannedVehicles.Contains(model))
        {
            sender.sendChatMessage("The vehicle ~r~" + model.ToString() + "~s~ is ~r~banned~s~!");
            return;
        }

        if (sender.vehicle != null && sender.vehicleSeat == -1)
        {
            NetHandle hv = sender.vehicle.handle;
            var owner = getVehicleOwner(hv);
            if (owner != null && VehicleHistory.ContainsKey(owner))
            {
                VehicleHistory[owner].Remove(hv);
            }
            sender.vehicle.delete();
        }

        var veh = API.createVehicle(model, sender.position, new Vector3(0, 0, sender.rotation.Z), 0, 0);
        veh.primaryColor = Rnd.Next(158);
        veh.secondaryColor = Rnd.Next(158);
        veh.numberPlate = getRandomNumberPlate(sender);
        veh.numberPlateStyle = Rnd.Next(6);

        API.setPlayerIntoVehicle(sender, veh, -1);
    }
}
