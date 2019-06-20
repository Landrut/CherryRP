//
//
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
using Arcadia.Server.Managers;
using Arcadia.Server.Models;
using Arcadia.Server.XMLDatabase;
//;

// пивасик
/*

public class Admin : Script
{

public void findPlayer(Client sender, string idOrName)
{ }
public void getIdFromClient(Client target)
{ }
public void getClientFromId(Client sender, int id)
{ }

[Command("ahelp")]
public void ahelp_command(Client sender)
{
    if (Player.GetRightsLevel(sender) >= 3)
    {
        API.sendChatMessageToPlayer(sender, "~r~Команды администратора:");
    }
    else return;

    if (Player.GetRightsLevel(sender) >= 3)
    {
        API.sendChatMessageToPlayer(sender, "~r~[1]: ~w~/a /an /getdim /slap /flip");
    }
    if (Player.GetRightsLevel(sender) >= 4)
    {
        API.sendChatMessageToPlayer(sender, "~r~[2]: ~w~/tp /goto /kick /getmoney");
    }
    if (Player.GetRightsLevel(sender) >= 5)
    {
        API.sendChatMessageToPlayer(sender, "~r~[3]: ~w~");
    }
    if (Player.GetRightsLevel(sender) >= 6)
    {
        API.sendChatMessageToPlayer(sender, "~r~[4]: ~w~");
    }
    if (Player.GetRightsLevel(sender) >= 7)
    {
        API.sendChatMessageToPlayer(sender, "~r~[5]: ~w~/comp /prop");
    }
    if (Player.GetRightsLevel(sender) >= 8)
    {
        API.sendChatMessageToPlayer(sender, "~r~[6]: ~w~/setfaction /setrank /setjob /setjobrank /createblip");
    }
    if (Player.GetRightsLevel(sender) >= 9)
    {
        API.sendChatMessageToPlayer(sender, "~r~[7]: ~w~/makeadmin /makeplayer /setmoney /addmoney /setbankmoney /addbankmoney");
    }
}
// Команды с хелпера
[Command("a", GreedyArg = true)]
public void AdminChatCommand(Client sender, string message)
{
    string groupName = "A";
    var chatMessage = "~r~[" + groupName + "] ~r~" + sender.name + ": ~w~" + message;
    if (Player.GetRightsLevel(sender) >= 3)
    {
        foreach (Client client in API.getAllPlayers())
        {
            if (Player.GetRightsLevel(client) >= 3)
            {
                API.sendChatMessageToPlayer(client, chatMessage);
            }
        }
    }
    else return;
}

[Command("mute")]
public void MuteCommand(Client sender, string idOrName, int time)
{
    Client target = API.exported.findPlayer(sender, idOrName);
}

[Command("time")]
public void SetTime(Client sender, int hours, int minutes)
{
    if (Player.GetRightsLevel(sender) >= 3)
    {
        API.setTime(hours, minutes);
    }
}

[Command("tphere")]
public void TeleportPlayerToPlayerCommand2(Client sender, Client target)
{
    if (API.getEntityData(sender, "adminlevel") > 3)
    {
        var pos = API.getEntityPosition(sender.handle);
        target.dimension = sender.dimension;
        API.createParticleEffectOnPosition("scr_rcbarry1", "scr_alien_teleport", pos, new Vector3(), 1f);

        API.setEntityPosition(target.handle, API.getEntityPosition(sender.handle));
    }
}

[Command("weather")]
public void weather(Client sender, int weather)
{
    if (Player.GetRightsLevel(sender) >= 3)
    {
        API.setWeather(weather);
    }
}

[Command("report", GreedyArg = true)]
public void ReportCommand(Client sender, string message)
{
    var reportmessage = "~r~[REPORT]~w~ от ~b~" + sender.name + ": ~w~" + message;
    var showreportmessage = "~r~[REPORT]~w~ " + message;
    foreach (Client client in API.getAllPlayers())
    {
        if (Player.GetRightsLevel(client) >= 3)
        {
            API.sendChatMessageToPlayer(client, reportmessage);
        }
    }
    API.sendChatMessageToPlayer(sender, showreportmessage);
}

[Command("an", GreedyArg = true)]
public void AnswerCommand(Client sender, string idOrName, string message)
{
    if (Player.GetRightsLevel(sender) >= 3)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        API.sendChatMessageToPlayer(target, "~r~[Ответ] ~w~" + sender.name + ": ~w~" + message);
        foreach (Client client in API.getAllPlayers())
        {
            if (Player.GetRightsLevel(client) >= 3)
            {
                API.sendChatMessageToPlayer(client, "~r~[A] " + sender.name + " для " + target.name + ": ~w~" + message);
            }
        }
    }
    else return;
}

[Command("getdim")]
public void GetDimensionCommang(Client sender, string idOrName)
{
    if (Player.GetRightsLevel(sender) >= 3)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        API.getEntityDimension(target);
        API.sendChatMessageToPlayer(sender, "~y~У игрока " + "~w~" + target.name + " ~y~установлен ~w~" + target.dimension + " ~y~dimension");
    }
    else return;
}

[Command("hp", GreedyArg = true)]
public void Kill(Client sender, string idOrName)
{
    if (Player.GetRightsLevel(sender) >= 3)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        API.setPlayerHealth(target, 100);
    }
}


[Command("kick")]
public void KickCommand(Client sender, string idOrName, string reason)
{
    if (Player.GetRightsLevel(sender) >= 3)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        API.kickPlayer(target, reason);
        API.sendChatMessageToAll(sender.name + "~r~ кикнул игрока" + " ~w~" + target.name + "\n~r~Причина: ~w~" + reason);
    }
    else return;
}

[Command("123")]
public void TestCommand(Client client)
{
    if (Player.GetRightsLevel(client) >= 3)
    {
        API.shared.setPlayerClothes(client, 11, 73, 0);
        API.shared.setPlayerClothes(client, 8, 159, 0);
        API.shared.setPlayerClothes(client, 3, 0, 0);
        API.shared.setPlayerClothes(client, 4, 11, 1);
        API.shared.setPlayerClothes(client, 6, 7, 0);
        API.shared.setPlayerClothes(client, 9, 8, 1);
        API.shared.setPlayerClothes(client, 7, 83, 0);
        API.shared.setPlayerClothes(client, 0, 58, 2);
        API.shared.setPlayerClothes(client, 1, 121, 0);
    }
    else return;
}

[Command("slap")]
public void SlapCommand(Client sender, string idOrName, string reason = null)
{
    if (Player.GetRightsLevel(sender) >= 3)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        Vector3 pos = API.getEntityPosition(target);
        API.setEntityPosition(target, new Vector3(pos.X, pos.Y, pos.Z + 2));
        int health = API.getPlayerHealth(target);
        API.setPlayerHealth(target, health - 5);
        API.sendChatMessageToPlayer(target, "~y~Администратор ~w~" + sender.name + " ~y~шлёпнул вас");
        foreach (Client client in API.getAllPlayers())
        {
            if (Player.GetRightsLevel(client) >= 3)
            {
                API.sendChatMessageToPlayer(client, "~r~[A] ~w~" + sender.name + " шлёпнул игрока: ~w~" + target.name);
            }
        }
        if (reason != null)
        {
            API.setEntityPosition(target, new Vector3(pos.X, pos.Y, pos.Z + 2));
            API.setPlayerHealth(target, health - 5);
            API.sendChatMessageToPlayer(target, "~y~Администратор ~w~" + sender.name + " ~y~шлёпнул вас\n~y~Причина: ~w~" + reason);
            foreach (Client client in API.getAllPlayers())
            {
                if (Player.GetRightsLevel(client) >= 3)
                {
                    API.sendChatMessageToPlayer(client, "~r~[A] ~w~" + sender.name + " ~y~шлёпнул игрока ~w~" + target.name + "\n~y~Причина: ~w~" + reason);
                }
            }
        }
    }
    else return;
}

[Command("flip")]
public void FlipCommand(Client sender, string idOrName)
{
    if (Player.GetRightsLevel(sender) >= 3)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        if (API.isPlayerInAnyVehicle(target) == false)
        {
            API.sendChatMessageToPlayer(sender, "~r~[Ошибка] ~y~Игрок не в транспортном средстве");
        }
        else
        {
            Vector3 rot = API.getEntityRotation(target.handle);
            var veh = API.getPlayerVehicle(target);

            API.setEntityRotation(target, new Vector3(rot.X + 100, rot.Y + 100, rot.Z + 100));
            API.sendChatMessageToPlayer(target, "~y~Администратор ~w~" + sender.name + " ~y~перевернул ваше т/с");
            foreach (Client client in API.getAllPlayers())
            {
                if (Player.GetRightsLevel(client) >= 3)
                {
                    API.sendChatMessageToPlayer(client, "~r~[A] " + sender.name + " ~y~перевернул т/с игрока ~w~" + target.name);
                }
            }
        }
    }
    else return;
}

[Command("goto")]
public void GoToCommand(Client sender, string idOrName)

{
    if (Player.GetRightsLevel(sender) >= 3)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        var pos = API.getEntityPosition(target.handle);
        API.setEntityPosition(sender, new Vector3(pos.X, pos.Y, pos.Z));
        API.createParticleEffectOnPosition("scr_rcbarry1", "scr_alien_teleport", pos, new Vector3(), 1f);
        API.sendChatMessageToPlayer(sender, "~y~Вы телепортировались к игроку " + "~w~" + target.name);
    }
    else return;
}


[Command("tp", "~y~Варианты: ~w~\n/tp [point]\n/tp list")]
public void tpCommand(Client sender, string tpname)
{
    if (Player.GetRightsLevel(sender) >= 4)
    {
        if (tpname != "list")
        {
            if (tpname == "air")
            {
                Vector3 air = new Vector3(-1015.712f, -2695.697f, 13.978f);
                API.setEntityPosition(sender, air);
            }
            else if (tpname == "lspd")
            {
                Vector3 pos = new Vector3(426, -973.975f, 30.9009f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "square")
            {
                Vector3 pos = new Vector3(163.855f, -989.027f, 30.09193f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "motel")
            {
                Vector3 pos = new Vector3(569.5208f, -1761.553f, 29.16898f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "ems")
            {
                Vector3 pos = new Vector3(330.424f, -1394.233f, 32.50927f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "taxi")
            {
                Vector3 pos = new Vector3(902.5723f, -171.4045f, 74.07551f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "major")
            {
                Vector3 pos = new Vector3(236.3338f, -404.0505f, 47.92436f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "ballas")
            {
                Vector3 pos = new Vector3(102.5615f, -1938.749f, 20.80372f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "Svalka")
            {
                Vector3 pos = new Vector3(2404.343f, 3127.818f, 48.1535f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "families")
            {
                Vector3 pos = new Vector3(-81.83427f, -1467.749f, 32.33204f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "vagos")
            {
                Vector3 pos = new Vector3(-1104.128f, -1637.284f, 4.615984f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "marabunta")
            {
                Vector3 pos = new Vector3(454.931f, -1769.917f, 28.8726f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "Angar")
            {
                Vector3 pos = new Vector3(899.5518, -3246.038, -98.04907);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "maze")
            {
                Vector3 pos = new Vector3(-75f, -818f, 326f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "maze")
            {
                Vector3 pos = new Vector3(-75f, -818f, 326f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "humane")
            {
                Vector3 pos = new Vector3(3544.456f, 3775.215f, 29.92668f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "mw")
            {
                Vector3 pos = new Vector3(-2336.165f, 3250.165f, 32.98258f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "army")
            {
                Vector3 pos = new Vector3(815.4872f, -2126.45f, 29.31619f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "Madraz")
            {
                Vector3 pos = new Vector3(1394.461f, 1141.83f, 114.6081f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "Lost")
            {
                Vector3 pos = new Vector3(1005.78f, -114.4409f, 73.97013f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "Triads")
            {
                Vector3 pos = new Vector3(-681.3458f, -877.0311f, 24.00662f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "fbi")
            {
                Vector3 pos = new Vector3(95.74465f, -743.8686f, 45.75499f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "FW")
            {
                Vector3 pos = new Vector3(2150.614f, 4790.182f, 40.99051f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "gov")
            {
                Vector3 pos = new Vector3(-1378.543f, -506.376f, 33.15742f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "redneck")
            {
                Vector3 pos = new Vector3(2453.326f, 4972.59f, 46.81021f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "Sad")
            {
                Vector3 pos = new Vector3(-1203.779f, -726.9702f, 20.95681f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "Gover")
            {
                Vector3 pos = new Vector3(233.2751f, -410.4284f, 48.11195f);
                API.setEntityPosition(sender, pos);
            }
            else if (tpname == "av")
            {
                Vector3 pos = new Vector3(-1573.813f, -574.872253f, 86.50459f);
                API.setEntityPosition(sender, pos);
                API.setEntityDimension(sender, 0);
                //API.freezePlayer(sender, true);
            }

        }
        if (tpname == "list")
        {
            API.sendChatMessageToPlayer(sender, "~y~Список телепортов: ");
            API.sendChatMessageToPlayer(sender, "~w~air, lspd, square, motel, ems, taxi, major\nballas, families, vagos, marabunta, loader");
        }
    }
    else return;
}

[Command("getmoney")]
public void GetMoney(Client sender, Client target)
{
    if (Player.GetRightsLevel(sender) >= 4)
    {
        API.sendChatMessageToPlayer(sender, "" + Player.GetMoney(target));
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("fixveh")]
public void FixedCarCommand(Client sender, string idOrName)
{
    if (Player.GetRightsLevel(sender) >= 4)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        if (API.isPlayerInAnyVehicle(target) == true)
        {
            var veh = API.getPlayerVehicle(target);

            API.repairVehicle(veh);
            API.sendChatMessageToPlayer(target, "~y~Администратор ~w~" + sender.name + " ~y~починил Ваше т/с.");
        }
        else
        {
            API.sendChatMessageToPlayer(sender, "~r~[Ошибка] ~y~Игрок не находится в транспортном средстве.");
            API.sendChatMessageToPlayer(target, "~y~Сядьте в т/с которое требуется починить.");
        }
    }
    else
    {
        API.sendChatMessageToPlayer(sender, "~r~[Ошибка] ~y~Недостаточно прав!");
    }
}

[Command("comp")]
public void SetPedClothesCommand(Client sender, int slot, int drawable, int texture)
{
    if (Player.GetRightsLevel(sender) >= 7)
    {
        API.setPlayerClothes(sender, slot, drawable, texture);
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("prop")]
public void SetPedAccessoriesCommand(Client sender, int slot, int drawable, int texture)
{
    if (Player.GetRightsLevel(sender) >= 7)
    {
        API.setPlayerAccessory(sender, slot, drawable, texture);
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("setfaction")]
public void SetFactionCommand(Client sender, string idOrName, int factionid)
{
    if (Player.GetRightsLevel(sender) >= 8)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        Player.SetFactionID(target, factionid);
        API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ определил вас во фракцию: " + Faction.GetPlayerFractionInfo(target, Player.GetFractionId(target)));

        string groupName = "A";
        var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил фракцию " + Faction.GetPlayerFractionInfo(target, Player.GetFractionId(target)) + "игроку ~b~" + target.name;

        foreach (Client client in API.getAllPlayers())
        {
            if (Player.GetRightsLevel(client) >= 3)
            {
                API.sendChatMessageToPlayer(client, chatMessage);
            }
        }
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("setrank")]
public void SetRankCommand(Client sender, string idOrName, int factionrankid)
{
    if (Player.GetRightsLevel(sender) >= 8)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);

        Player.SetFactionRank(target, factionrankid);
        API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ установил вам ранг: " + Faction.GetPlayerFactionRank(target, Player.GetFractionRank(target)));

        string groupName = "A";
        var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил ранг " + Faction.GetPlayerFactionRank(target, Player.GetFractionRank(target)) + " игроку ~b~" + target.name;

        foreach (Client client in API.getAllPlayers())
        {
            if (Player.GetRightsLevel(client) >= 3)
            {
                API.sendChatMessageToPlayer(client, chatMessage);
            }
        }
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("setjob")]
public void SetJobCommand(Client sender, string idOrName, int jobid)
{
    if (Player.GetRightsLevel(sender) >= 8)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        Player.SetJobID(target, jobid);
        API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ определил вв работу: " + JobFactionModel.GetPlayerJobFactionInfo(target, Player.GetJobId(target)));

        string groupName = "A";
        var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил работу " + JobFactionModel.GetPlayerJobFactionInfo(target, Player.GetJobId(target)) + "игроку ~b~" + target.name;

        foreach (Client client in API.getAllPlayers())
        {
            if (Player.GetRightsLevel(client) >= 3)
            {
                API.sendChatMessageToPlayer(client, chatMessage);
            }
        }
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("setjobrank")]
public void SetJobRankCommand(Client sender, string idOrName, int jobrank)
{
    if (Player.GetRightsLevel(sender) >= 8)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);

        Player.SetJobRank(target, jobrank);
        API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ установил вам должность: " + JobFactionModel.GetPlayerJobRank(target, Player.GetJobRank(target)));

        string groupName = "A";
        var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил должность " + JobFactionModel.GetPlayerJobRank(target, Player.GetJobRank(target)) + " игроку ~b~" + target.name;

        foreach (Client client in API.getAllPlayers())
        {
            if (Player.GetRightsLevel(client) >= 3)
            {
                API.sendChatMessageToPlayer(client, chatMessage);
            }
        }
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("setgang")]
public void SetGangCommand(Client sender, string idOrName, int gangid)
{
    if (Player.GetRightsLevel(sender) >= 8)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        Player.SetGangID(target, gangid);
        API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ установил вам банду: " + GangFactionModel.GetPlayerGangFactionInfo(target, Player.GetGangId(target)));

        string groupName = "A";
        var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил банду " + GangFactionModel.GetPlayerGangFactionInfo(target, Player.GetGangId(target)) + "игроку ~b~" + target.name;

        foreach (Client client in API.getAllPlayers())
        {
            if (Player.GetRightsLevel(client) >= 3)
            {
                API.sendChatMessageToPlayer(client, chatMessage);
            }
        }
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}


[Command("setgangrank")]
public void SetGangRankCommand(Client sender, string idOrName, int jobrank)
{
    if (Player.GetRightsLevel(sender) >= 8)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);

        Player.SetGangRank(target, jobrank);
        API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ установил вам должность: " + JobFactionModel.GetPlayerJobRank(target, Player.GetJobRank(target)));

        string groupName = "A";
        var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил должность " + JobFactionModel.GetPlayerJobRank(target, Player.GetJobRank(target)) + " игроку ~b~" + target.name;

        foreach (Client client in API.getAllPlayers())
        {
            if (Player.GetRightsLevel(client) >= 3)
            {
                API.sendChatMessageToPlayer(client, chatMessage);
            }
        }
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("makeadmin")]
public void MakeAdminCommand(Client sender, string idOrName, int lvl)
{
    if (Player.GetRightsLevel(sender) > 9)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);

        if (Player.GetRightsLevel(target) > 9)
        {
            API.sendChatMessageToPlayer(sender, "~r~Вы не можете применить эту команду к администратору выше вашей должности!");
            return;
        }
        else
        {

            Player.SetRightsLevel(target, lvl);
            API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ выдал вам права " + Rights.GetAdminRank(Player.GetRightsLevel(target)) + "~y~сервера!");

            string groupName = "A";
            var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ выдал права " + Rights.GetAdminRank(Player.GetRightsLevel(target)) + "игроку ~b~" + target.name;

            foreach (Client client in API.getAllPlayers())
            {
                if (Player.GetRightsLevel(client) >= 3)
                {
                    API.sendChatMessageToPlayer(client, chatMessage);
                }
            }
        }
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("makeplayer")]
public void MakePlayerCommand(Client sender, string idOrName)
{
    if (Player.GetRightsLevel(sender) > 9)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        if (Player.GetRightsLevel(target) > 9)
        {
            API.sendChatMessageToPlayer(sender, "~r~Вы не можете применить эту команду к администратору выше вашей должности!");
            return;
        }
        else
        {
            Player.SetRightsLevel(target, 1);
            API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ снял вас с поста администратора сервера!");

            string groupName = "A";
            var chatMessage = "~r~[" + groupName + "] ~w~" + "Администратор ~b~" + sender.name + "~w~ снял ~b~" + target.name + " ~w~с поста!";

            foreach (Client client in API.getAllPlayers())
            {
                if (Player.GetRightsLevel(client) >= 3)
                {
                    API.sendChatMessageToPlayer(client, chatMessage);
                }
            }
        }
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("setmoney")]
public void SetMoney(Client sender, Client target, int amount)
{
    if (Player.GetRightsLevel(sender) >= 9)
    {
        Player.SetMoney(target, amount);
        API.sendChatMessageToPlayer(sender, "~y~Игроку " + "~w~" + target.name + " ~y~установлено ~g~" + amount + " ~g~$");
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("addmoney")]
public void AddMoney(Client sender, Client target, int amount)
{
    if (Player.GetRightsLevel(sender) >= 9)
    {
        Player.ChangeMoney(target, +amount);
        API.sendChatMessageToPlayer(sender, "~y~Игроку " + "~w~" + target.name + " ~y~добавлено ~g~" + amount + " ~g~$");
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("setbankmoney")]
public void SetBankMoney(Client sender, Client target, int amount)
{
    if (Player.GetRightsLevel(sender) >= 9)
    {
        Player.SetBankMoney(target, amount);
        API.sendChatMessageToPlayer(sender, "~y~Игроку " + "~w~" + target.name + " ~y~установлено в банке ~g~" + amount + " ~g~$");
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("addbankmoney")]
public void AddBankMoney(Client sender, Client target, int amount)
{
    if (Player.GetRightsLevel(sender) >= 9)
    {
        Player.ChangeBankMoney(target, +amount);
        API.sendChatMessageToPlayer(sender, "~y~Игроку " + "~w~" + target.name + " ~y~добавлено в банк ~g~" + amount + " ~g~$");
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}

[Command("createblip", "/createblip [name] [model] [color] [range 1-0] [shortrange] [dimension]")]
public void CreateBlip(Client sender, string _name, int _model, int _color, int _range, bool _shortrange, int _dimension)
{
    if (Player.GetRightsLevel(sender) >= 8)
    {
        _name = _name.Replace("_", " ");

        var _createdBlip = API.createBlip(sender.position, _range, _dimension);
        _createdBlip.sprite = _model;
        _createdBlip.name = _name;
        _createdBlip.color = _color;
        _createdBlip.shortRange = _shortrange;

        BlipManager.BlipsOnMap.Add(_createdBlip);
        db_Blips.AddBlip(new Arcadia.Server.Models.Blip { Color = _color, Dimension = _dimension, ModelId = _model, Name = _name, Position = sender.position, Range = _range, ShortRange = _shortrange });
    }
    else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
}
}
*/
public class Admins : Script
{

    public void findPlayer(Client sender, string idOrName)
    { }
    public void getIdFromClient(Client target)
    { }
    public void getClientFromId(Client sender, int id)
    { }

    [Command("ahelp")]
    public void ahelp_command(Client sender)
    {
        if (Player.GetRightsLevel(sender) >= 3)
        {
            API.sendChatMessageToPlayer(sender, "~r~Команды администратора:");
        }
        else return;

        if (Player.GetRightsLevel(sender) >= 3)
        {
            API.sendChatMessageToPlayer(sender, "~r~[1]: ~w~/a /an /getdim /slap /flip /onwork");
        }
        if (Player.GetRightsLevel(sender) >= 4)
        {
            API.sendChatMessageToPlayer(sender, "~r~[2]: ~w~/tp /goto /kick /getmoney");
        }
        if (Player.GetRightsLevel(sender) >= 5)
        {
            API.sendChatMessageToPlayer(sender, "~r~[3]: ~w~");
        }
        if (Player.GetRightsLevel(sender) >= 6)
        {
            API.sendChatMessageToPlayer(sender, "~r~[4]: ~w~");
        }
        if (Player.GetRightsLevel(sender) >= 7)
        {
            API.sendChatMessageToPlayer(sender, "~r~[5]: ~w~/comp /prop");
        }
        if (Player.GetRightsLevel(sender) >= 8)
        {
            API.sendChatMessageToPlayer(sender, "~r~[6]: ~w~/setfaction /setrank /setjob /setjobrank /createblip");
        }
        if (Player.GetRightsLevel(sender) >= 9)
        {
            API.sendChatMessageToPlayer(sender, "~r~[7]: ~w~/makeadmin /makeplayer /setmoney /addmoney /setbankmoney /addbankmoney");
        }
    }
    // Команды с хелпера
    [Command("onwork")]
    public void CommandOnWork(Client sender, bool mode = false)
    {
        if (Player.GetRightsLevel(sender) >= 3)
        {
            if (mode == true)
            {
                int Transparency = API.getEntityTransparency(sender);
                if (Transparency >= 100)
                {
                    API.setEntityTransparency(sender, Transparency = 0);
                    API.setEntityInvincible(sender, true);
                    API.sendChatMessageToAll(sender.name + "~r~ вышел на дежурство.");
                }
                else
                {
                    API.setEntityTransparency(sender, Transparency = 255);
                    API.setEntityInvincible(sender, false);
                    API.sendChatMessageToAll(sender.name + "~r~ закончил дежурство");
                }
            }
            else
            {
                int Transparency = API.getEntityTransparency(sender);
                if (Transparency != 100)
                {
                    API.setEntityTransparency(sender, Transparency = 100);
                    API.setEntityInvincible(sender, true);
                    API.sendChatMessageToAll(sender.name + "~r~ вышел на дежурство.");
                }
                else
                {
                    API.setEntityTransparency(sender, Transparency = 255);
                    API.setEntityInvincible(sender, false);
                    API.sendChatMessageToAll(sender.name + "~r~ закончил дежурство");
                }
            }
        }
        else return;
    }

    [Command("a", GreedyArg = true)]
    public void AdminChatCommand(Client sender, string message)
    {
        string groupName = "A";
        var chatMessage = "~r~[" + groupName + "] ~r~" + sender.name + ": ~w~" + message;
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetRightsLevel(client) >= 3)
                    {
                        API.sendChatMessageToPlayer(client, chatMessage);
                    }
                }
            }
            else return;
        }
    }

    [Command("mute")]
    public void MuteCommand(Client sender, string idOrName, int time)
    {

        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                Client target = API.exported.findPlayer(sender, idOrName);
            }
            else return;
        }
    }

    [Command("an", GreedyArg = true)]
    public void AnswerCommand(Client sender, string idOrName, string message)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        if (Player.GetRightsLevel(sender) >= 3)
        {
            Client target = API.exported.playerids.findPlayer(sender, idOrName);
            API.sendChatMessageToPlayer(target, "~r~[Ответ] ~w~" + sender.name + ": ~w~" + message);
            foreach (Client client in API.getAllPlayers())
            {
                if (Player.GetRightsLevel(client) >= 3)
                {
                    API.sendChatMessageToPlayer(client, "~r~[A] " + sender.name + " для " + target.name + ": ~w~" + message);
                }
            }
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                API.sendChatMessageToPlayer(target, "~r~[Ответ] ~w~" + sender.name + ": ~w~" + message);
                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetRightsLevel(client) >= 3)
                    {
                        API.sendChatMessageToPlayer(client, "~r~[A] " + sender.name + " для " + target.name + ": ~w~" + message);
                    }
                }
            }
            else return;
        }
    }

    [Command("getdim")]
    public void GetDimensionCommang(Client sender, string idOrName)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                API.getEntityDimension(target);
                API.sendChatMessageToPlayer(sender, "~y~У игрока " + "~w~" + target.name + " ~y~установлен ~w~" + target.dimension + " ~y~dimension");
            }
            else return;
        }
    }


    [Command("kick")]
    public void KickCommand(Client sender, string idOrName, string reason)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                API.kickPlayer(target, reason);
                API.sendChatMessageToAll(sender.name + "~r~ кикнул игрока" + " ~w~" + target.name + "\n~r~Причина: ~w~" + reason);
            }
            else return;
        }
    }

    [Command("slap")]
    public void SlapCommand(Client sender, string idOrName, string reason = null)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                Vector3 pos = API.getEntityPosition(target);
                API.setEntityPosition(target, new Vector3(pos.X, pos.Y, pos.Z + 2));
                int health = API.getPlayerHealth(target);
                API.setPlayerHealth(target, health - 5);
                API.sendChatMessageToPlayer(target, "~y~Администратор ~w~" + sender.name + " ~y~шлёпнул вас");
                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetRightsLevel(client) >= 3)
                    {
                        API.sendChatMessageToPlayer(client, "~r~[A] ~w~" + sender.name + " шлёпнул игрока: ~w~" + target.name);
                    }
                }
                if (reason != null)
                {
                    API.setEntityPosition(target, new Vector3(pos.X, pos.Y, pos.Z + 2));
                    API.setPlayerHealth(target, health - 5);
                    API.sendChatMessageToPlayer(target, "~y~Администратор ~w~" + sender.name + " ~y~шлёпнул вас\n~y~Причина: ~w~" + reason);
                    foreach (Client client in API.getAllPlayers())
                    {
                        if (Player.GetRightsLevel(client) >= 3)
                        {
                            API.sendChatMessageToPlayer(client, "~r~[A] ~w~" + sender.name + " ~y~шлёпнул игрока ~w~" + target.name + "\n~y~Причина: ~w~" + reason);
                        }
                    }
                }
            }
            else return;
        }
    }

    [Command("flip")]
    public void FlipCommand(Client sender, string idOrName)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                if (API.isPlayerInAnyVehicle(target) == false)
                {
                    API.sendChatMessageToPlayer(sender, "~r~[Ошибка] ~y~Игрок не в транспортном средстве");
                }
                else
                {
                    Vector3 rot = API.getEntityRotation(target.handle);
                    var veh = API.getPlayerVehicle(target);

                    API.setEntityRotation(target, new Vector3(rot.X + 100, rot.Y + 100, rot.Z + 100));
                    API.sendChatMessageToPlayer(target, "~y~Администратор ~w~" + sender.name + " ~y~перевернул ваше т/с");
                    foreach (Client client in API.getAllPlayers())
                    {
                        if (Player.GetRightsLevel(client) >= 3)
                        {
                            API.sendChatMessageToPlayer(client, "~r~[A] " + sender.name + " ~y~перевернул т/с игрока ~w~" + target.name);
                        }
                    }
                }
            }
            else return;
        }
    }

    [Command("goto")]
    public void GoToCommand(Client sender, string idOrName)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                var pos = API.getEntityPosition(target.handle);
                API.setEntityPosition(sender, new Vector3(pos.X, pos.Y, pos.Z));
                API.createParticleEffectOnPosition("scr_rcbarry1", "scr_alien_teleport", pos, new Vector3(), 1f);
                API.sendChatMessageToPlayer(sender, "~y~Вы телепортировались к игроку " + "~w~" + target.name);
            }
            else return;
        }
    }

    [Command("tp", "~y~Варианты: ~w~\n/tp [point]\n/tp list")]
    public void tpCommand(Client sender, string tpname)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 4)
            {
                if (tpname != "list")
                {
                    if (tpname == "air")
                    {
                        Vector3 air = new Vector3(-1015.712f, -2695.697f, 13.978f);
                        API.setEntityPosition(sender, air);
                    }
                    else if (tpname == "lspd")
                    {
                        Vector3 pos = new Vector3(426, -973.975f, 30.9009f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "square")
                    {
                        Vector3 pos = new Vector3(163.855f, -989.027f, 30.09193f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "motel")
                    {
                        Vector3 pos = new Vector3(569.5208f, -1761.553f, 29.16898f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "ems")
                    {
                        Vector3 pos = new Vector3(330.424f, -1394.233f, 32.50927f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "taxi")
                    {
                        Vector3 pos = new Vector3(902.5723f, -171.4045f, 74.07551f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "major")
                    {
                        Vector3 pos = new Vector3(236.3338f, -404.0505f, 47.92436f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "ballas")
                    {
                        Vector3 pos = new Vector3(102.5615f, -1938.749f, 20.80372f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "Svalka")
                    {
                        Vector3 pos = new Vector3(2404.343f, 3127.818f, 48.1535f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "families")
                    {
                        Vector3 pos = new Vector3(-81.83427f, -1467.749f, 32.33204f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "vagos")
                    {
                        Vector3 pos = new Vector3(-1104.128f, -1637.284f, 4.615984f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "marabunta")
                    {
                        Vector3 pos = new Vector3(454.931f, -1769.917f, 28.8726f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "Angar")
                    {
                        Vector3 pos = new Vector3(899.5518, -3246.038, -98.04907);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "maze")
                    {
                        Vector3 pos = new Vector3(-75f, -818f, 326f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "maze")
                    {
                        Vector3 pos = new Vector3(-75f, -818f, 326f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "humane")
                    {
                        Vector3 pos = new Vector3(3544.456f, 3775.215f, 29.92668f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "mw")
                    {
                        Vector3 pos = new Vector3(-2336.165f, 3250.165f, 32.98258f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "army")
                    {
                        Vector3 pos = new Vector3(815.4872f, -2126.45f, 29.31619f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "Madraz")
                    {
                        Vector3 pos = new Vector3(1394.461f, 1141.83f, 114.6081f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "Lost")
                    {
                        Vector3 pos = new Vector3(1005.78f, -114.4409f, 73.97013f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "Triads")
                    {
                        Vector3 pos = new Vector3(-681.3458f, -877.0311f, 24.00662f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "fbi")
                    {
                        Vector3 pos = new Vector3(95.74465f, -743.8686f, 45.75499f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "FW")
                    {
                        Vector3 pos = new Vector3(2150.614f, 4790.182f, 40.99051f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "gov")
                    {
                        Vector3 pos = new Vector3(-1378.543f, -506.376f, 33.15742f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "redneck")
                    {
                        Vector3 pos = new Vector3(2453.326f, 4972.59f, 46.81021f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "Sad")
                    {
                        Vector3 pos = new Vector3(-1203.779f, -726.9702f, 20.95681f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "Gover")
                    {
                        Vector3 pos = new Vector3(233.2751f, -410.4284f, 48.11195f);
                        API.setEntityPosition(sender, pos);
                    }
                    else if (tpname == "av")
                    {
                        Vector3 pos = new Vector3(-1573.813f, -574.872253f, 86.50459f);
                        API.setEntityPosition(sender, pos);
                        API.setEntityDimension(sender, 0);
                        //API.freezePlayer(sender, true);
                    }

                }
                if (tpname == "list")
                {
                    API.sendChatMessageToPlayer(sender, "~y~Список телепортов: ");
                    API.sendChatMessageToPlayer(sender, "~w~air, lspd, square, motel, ems, taxi, major\nballas, families, vagos, marabunta, loader");
                }
            }
            else return;
        }
    }

    [Command("getmoney")]
    public void GetMoney(Client sender, Client target)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 4)
            {
                API.sendChatMessageToPlayer(sender, "" + Player.GetMoney(target));
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("fixveh")]
    public void FixedCarCommand(Client sender, string idOrName)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 4)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                if (API.isPlayerInAnyVehicle(target) == true)
                {
                    var veh = API.getPlayerVehicle(target);

                    API.repairVehicle(veh);
                    API.sendChatMessageToPlayer(target, "~y~Администратор ~w~" + sender.name + " ~y~починил Ваше т/с.");
                }
                else
                {
                    API.sendChatMessageToPlayer(sender, "~r~[Ошибка] ~y~Игрок не находится в транспортном средстве.");
                    API.sendChatMessageToPlayer(target, "~y~Сядьте в т/с которое требуется починить.");
                }
            }
            else
            {
                API.sendChatMessageToPlayer(sender, "~r~[Ошибка] ~y~Недостаточно прав!");
            }
        }
    }

    [Command("comp")]
    public void SetPedClothesCommand(Client sender, int slot, int drawable, int texture)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 7)
            {
                API.setPlayerClothes(sender, slot, drawable, texture);
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("prop")]
    public void SetPedAccessoriesCommand(Client sender, int slot, int drawable, int texture)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 7)
            {
                API.setPlayerAccessory(sender, slot, drawable, texture);
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("setfaction")]
    public void SetFactionCommand(Client sender, string idOrName, int factionid)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }

        else
        {
            if (Player.GetRightsLevel(sender) >= 8)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                Player.SetFactionID(target, factionid);
                API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ определил вас во фракцию: " + Faction.GetPlayerFractionInfo(target, Player.GetFractionId(target)));

                string groupName = "A";
                var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил фракцию " + Faction.GetPlayerFractionInfo(target, Player.GetFractionId(target)) + "игроку ~b~" + target.name;

                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetRightsLevel(client) >= 3)
                    {
                        API.sendChatMessageToPlayer(client, chatMessage);
                    }
                }
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }
    [Command("setrank")]
    public void SetRankCommand(Client sender, string idOrName, int factionrankid)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 8)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);

                Player.SetFactionRank(target, factionrankid);
                API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ установил вам ранг: " + Faction.GetPlayerFactionRank(target, Player.GetFractionRank(target)));

                string groupName = "A";
                var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил ранг " + Faction.GetPlayerFactionRank(target, Player.GetFractionRank(target)) + " игроку ~b~" + target.name;

                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetRightsLevel(client) >= 3)
                    {
                        API.sendChatMessageToPlayer(client, chatMessage);
                    }
                }
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("setjob")]
    public void SetJobCommand(Client sender, string idOrName, int jobid)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 8)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                Player.SetJobID(target, jobid);
                API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ определил вв работу: " + JobFactionModel.GetPlayerJobFactionInfo(target, Player.GetJobId(target)));

                string groupName = "A";
                var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил работу " + JobFactionModel.GetPlayerJobFactionInfo(target, Player.GetJobId(target)) + "игроку ~b~" + target.name;

                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetRightsLevel(client) >= 3)
                    {
                        API.sendChatMessageToPlayer(client, chatMessage);
                    }
                }
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("setjobrank")]
    public void SetJobRankCommand(Client sender, string idOrName, int jobrank)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 8)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);

                Player.SetJobRank(target, jobrank);
                API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ установил вам должность: " + JobFactionModel.GetPlayerJobRank(target, Player.GetJobRank(target)));

                string groupName = "A";
                var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил должность " + JobFactionModel.GetPlayerJobRank(target, Player.GetJobRank(target)) + " игроку ~b~" + target.name;

                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetRightsLevel(client) >= 3)
                    {
                        API.sendChatMessageToPlayer(client, chatMessage);
                    }
                }
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("setgang")]
    public void SetGangCommand(Client sender, string idOrName, int gangid)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 8)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                Player.SetGangID(target, gangid);
                API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ установил вам банду: " + GangFactionModel.GetPlayerGangFactionInfo(target, Player.GetGangId(target)));

                string groupName = "A";
                var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил банду " + GangFactionModel.GetPlayerGangFactionInfo(target, Player.GetGangId(target)) + "игроку ~b~" + target.name;

                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetRightsLevel(client) >= 3)
                    {
                        API.sendChatMessageToPlayer(client, chatMessage);
                    }
                }
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }


    [Command("setgangrank")]
    public void SetGangRankCommand(Client sender, string idOrName, int jobrank)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 8)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);

                Player.SetGangRank(target, jobrank);
                API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ установил вам должность: " + JobFactionModel.GetPlayerJobRank(target, Player.GetJobRank(target)));

                string groupName = "A";
                var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ установил должность " + JobFactionModel.GetPlayerJobRank(target, Player.GetJobRank(target)) + " игроку ~b~" + target.name;

                foreach (Client client in API.getAllPlayers())
                {
                    if (Player.GetRightsLevel(client) >= 3)
                    {
                        API.sendChatMessageToPlayer(client, chatMessage);
                    }
                }
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("makeadmin")]
    public void MakeAdminCommand(Client sender, string idOrName, int lvl)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) > 9)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);

                if (Player.GetRightsLevel(target) > 9)
                {
                    API.sendChatMessageToPlayer(sender, "~r~Вы не можете применить эту команду к администратору выше вашей должности!");
                    return;
                }
                else
                {

                    Player.SetRightsLevel(target, lvl);
                    API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ выдал вам права " + Rights.GetAdminRank(Player.GetRightsLevel(target)) + "~y~сервера!");

                    string groupName = "A";
                    var chatMessage = "~r~[" + groupName + "] ~w~" + "~b~" + sender.name + "~w~ выдал права " + Rights.GetAdminRank(Player.GetRightsLevel(target)) + "игроку ~b~" + target.name;

                    foreach (Client client in API.getAllPlayers())
                    {
                        if (Player.GetRightsLevel(client) >= 3)
                        {
                            API.sendChatMessageToPlayer(client, chatMessage);
                        }
                    }
                }
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("makeplayer")]
    public void MakePlayerCommand(Client sender, string idOrName)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) > 9)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                if (Player.GetRightsLevel(target) > 9)
                {
                    API.sendChatMessageToPlayer(sender, "~r~Вы не можете применить эту команду к администратору выше вашей должности!");
                    return;
                }
                else
                {
                    Player.SetRightsLevel(target, 1);
                    API.sendChatMessageToPlayer(target, "~y~Администратор " + "~b~" + sender.name + "~y~ снял вас с поста администратора сервера!");

                    string groupName = "A";
                    var chatMessage = "~r~[" + groupName + "] ~w~" + "Администратор ~b~" + sender.name + "~w~ снял ~b~" + target.name + " ~w~с поста!";

                    foreach (Client client in API.getAllPlayers())
                    {
                        if (Player.GetRightsLevel(client) >= 3)
                        {
                            API.sendChatMessageToPlayer(client, chatMessage);
                        }
                    }
                }
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("setmoney")]
    public void SetMoney(Client sender, Client target, int amount)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 9)
            {
                Player.SetMoney(target, amount);
                API.sendChatMessageToPlayer(sender, "~y~Игроку " + "~w~" + target.name + " ~y~установлено ~g~" + amount + " ~g~$");
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("addmoney")]
    public void AddMoney(Client sender, Client target, int amount)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 9)
            {
                Player.ChangeMoney(target, +amount);
                API.sendChatMessageToPlayer(sender, "~y~Игроку " + "~w~" + target.name + " ~y~добавлено ~g~" + amount + " ~g~$");
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("setbankmoney")]
    public void SetBankMoney(Client sender, Client target, int amount)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 9)
            {
                Player.SetBankMoney(target, amount);
                API.sendChatMessageToPlayer(sender, "~y~Игроку " + "~w~" + target.name + " ~y~установлено в банке ~g~" + amount + " ~g~$");
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("addbankmoney")]
    public void AddBankMoney(Client sender, Client target, int amount)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 9)
            {
                Player.ChangeBankMoney(target, +amount);
                API.sendChatMessageToPlayer(sender, "~y~Игроку " + "~w~" + target.name + " ~y~добавлено в банк ~g~" + amount + " ~g~$");
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }

    [Command("createblip", "/createblip [name] [model] [color] [range 1-0] [shortrange] [dimension]")]
    public void CreateBlip(Client sender, string _name, int _model, int _color, int _range, bool _shortrange, int _dimension)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 8)
            {
                _name = _name.Replace("_", " ");

                var _createdBlip = API.createBlip(sender.position, _range, _dimension);
                _createdBlip.sprite = _model;
                _createdBlip.name = _name;
                _createdBlip.color = _color;
                _createdBlip.shortRange = _shortrange;

                BlipManager.BlipsOnMap.Add(_createdBlip);
                db_Blips.AddBlip(new Arcadia.Server.Models.Blip { Color = _color, Dimension = _dimension, ModelId = _model, Name = _name, Position = sender.position, Range = _range, ShortRange = _shortrange });
            }
            else API.sendChatMessageToPlayer(sender, "~r~У вас недостаточно прав!");
        }
    }
    [Command("time")]
    public void SetTime(Client sender, int hours, int minutes)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                API.setTime(hours, minutes);
            }
        }
    }

    [Command("tphere")]
    public void TeleportPlayerToPlayerCommand2(Client sender, Client target)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (API.getEntityData(sender, "adminlevel") > 3)
            {
                var pos = API.getEntityPosition(sender.handle);
                target.dimension = sender.dimension;
                API.createParticleEffectOnPosition("scr_rcbarry1", "scr_alien_teleport", pos, new Vector3(), 1f);

                API.setEntityPosition(target.handle, API.getEntityPosition(sender.handle));
            }
        }
    }

    [Command("weather")]
    public void weather(Client sender, int weather)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                API.setWeather(weather);
            }
        }
    }
    [Command("hp", GreedyArg = true)]
    public void Kill(Client sender, string idOrName)
    {
        int Transparency = API.getEntityTransparency(sender);
        if (Transparency != 100)
        {
            API.sendChatMessageToPlayer(sender, "~y~Вы не в статусе администратора. ");
        }
        else
        {
            if (Player.GetRightsLevel(sender) >= 3)
            {
                Client target = API.exported.playerids.findPlayer(sender, idOrName);
                API.setPlayerHealth(target, 100);
            }
        }
    }
}
