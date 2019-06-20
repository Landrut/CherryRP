using System;
using System.Collections.Generic;
using System.Linq;
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

public class JobFactionModel : Script
{
    public static String GetPlayerJobFactionInfo(Client player, int job)
    {
        switch (job)
        {
            case 0: return "~w~Безработный ~w~";
            case 1: return "~y~Taxi ~w~";
            case 2: return "~g~Delivery ~w~";
            case 3: return "~c~Mechanic ~w~";
            case 4: return "~r~AmmuNation ~w~";
            case 5: return "~m~Miner ~w~";
            case 6: return "~g~LumberJack ~w~";
            case 7: return "~g~Svalkajob ~w~";
            default: return "";
        }
    }

    public static String GetPlayerJobRank(Client player, int jobrank)
    {

        int job = (player.hasData("job_id")) ? player.getData("job_id") : 0;

        if (job == 1)
        {
            switch (jobrank)
            {
                case 1: return "Таксист";
                case 2: return "Директор";
                default: return "";
            }
        }
        if (job == 2)
        {
            switch (jobrank)
            {
                case 1: return "Перевозчик";
                case 2: return "Директор";
                default: return "";
            }
        }
        if (job == 3)
        {
            switch (jobrank)
            {
                case 1: return "Механик";
                case 2: return "Директор";
                default: return "";
            }
        }
        if (job == 4)
        {
            switch (jobrank)
            {
                case 1: return "Оружейник";
                case 2: return "Директор";
                default: return "";
            }
        }
        if (job == 5)
        {
            switch (jobrank)
            {
                case 1: return "Шахтёр";
                case 2: return "Директор";
                default: return "";
            }
        }
        if (job == 6)
        {
            switch (jobrank)
            {
                case 1: return "Лесоруб";
                case 2: return "Директор";
                default: return "";
            }
        }
        if (job == 7)
        {
            switch (jobrank)
            {
                case 1: return "Водитель автопогрузщика";                
                default: return "";
            }
        }
        else return "-";
    }

    [Command("jobinvite")]
    public void FactionJobInviteCommand(Client sender, string idOrName)
    {
        int rank = Player.GetJobRank(sender);
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        int jobIDOfTarget = Player.GetJobId(target);
        int jobIDOfSender = Player.GetJobId(sender);
        var inviteMessageTarget = "~b~" + sender.name + "~w~ принял вас на работу " + GetPlayerJobFactionInfo(sender, Player.GetJobId(sender));
        var inviteMessageSender = "~b~" + target.name + "~w~ был принят вами на работу " + GetPlayerJobFactionInfo(sender, Player.GetJobId(sender));

        if (Player.GetJobRank(sender) != 2)
        {
            API.sendChatMessageToPlayer(sender, "~r~Ваш ранг не позволяет вам использовать данную команду");
        }
        else
        {
            if (jobIDOfSender == jobIDOfTarget)
            {
                API.sendChatMessageToPlayer(sender, "~b~ " + target.name + "~y~ уже работает у вас");
                return;
            }
            else
            {
                Player.SetJobID(target, jobIDOfSender);
                Player.SetJobRank(target, 1);
                API.sendChatMessageToPlayer(sender, inviteMessageSender);
                API.sendChatMessageToPlayer(target, inviteMessageTarget);
            }
        }
    }

    [Command("jobuninvite")]
    public void FactionJobUninviteCommand(Client sender, string idOrName)
    {
        int rank = Player.GetJobRank(sender);
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        int jobIDOfTarget = Player.GetJobId(target);
        int jobIDOfSender = Player.GetJobId(sender);
        var uninviteMessageTarget = "~b~" + sender.name + "~w~ уволил вас из " + GetPlayerJobFactionInfo(sender, Player.GetJobId(sender));
        var uninviteMessageSender = "~b~" + target.name + "~w~ был уволен вами из " + GetPlayerJobFactionInfo(sender, Player.GetJobId(sender));

        if (Player.GetJobRank(sender) != 2)
        {
            API.sendChatMessageToPlayer(sender, "~r~Ваш ранг не позволяет вам использовать данную команду");
        }
        else
        {
            if (jobIDOfTarget == 0)
            {
                API.sendChatMessageToPlayer(sender, "~b~ " + target.name + "~y~ не состоит в вашей фракции");
                return;
            }
            else
            {
                Player.SetJobID(target, 0);
                API.sendChatMessageToPlayer(sender, uninviteMessageSender);
                API.sendChatMessageToPlayer(target, uninviteMessageTarget);
            }
        }
    }

    /*[Command("giverank")]
    public void GiveRankCommand(Client sender, string idOrName, int giverank)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);

        int senderfaction = Player.GetFractionId(sender);
        int targetfaction = Player.GetFractionId(target);

        var rankupmessage = "~r~" + sender.name + "~w~ повысил вас до " + giverank + " ранга";
        var rankdownmessage = "~r~" + sender.name + "~w~ понизил вас до " + giverank + " ранга";

        var rankupmessagesender = "~y~Вы повысили ~b~" + target.name + " ~y~до " + giverank + " ранга";
        var rankdownmessagesender = "~y~Вы понизили ~b~" + target.name + " ~y~до " + giverank + " ранга";

        if (Player.GetFractionRank(sender) <= 8)
        {
            API.sendChatMessageToPlayer(sender, "~r~Ваш ранг не позволяет вам использовать данную команду");
        }
        else
        {
            if (targetfaction == 0)
            {
                API.sendChatMessageToPlayer(sender, "~b~ " + target.name + "~y~ не состоит в вашей фракции");
                return;
            }
            else
            {
                if (Player.GetFractionRank(target) < giverank)
                {
                    Player.SetFactionRank(target, giverank);
                    API.sendChatMessageToPlayer(target, rankupmessage);
                    return;
                }
                else
                {
                    Player.SetFactionRank(target, giverank);
                    API.sendChatMessageToPlayer(target, rankdownmessage);
                    return;
                }
            }
        }
    }*/

    [Command("jr", GreedyArg = true)]
    public void FractionChatCommand(Client sender, string message)
    {
        int rank = Player.GetJobRank(sender);
        string groupName = "JR";
        var chatMessage = "~b~[" + groupName + " ID: " + "~b~" + API.exported.playerids.getIdFromClient(sender) + "] ~b~" + GetPlayerJobRank(sender, Player.GetJobRank(sender)) + " | " + sender.name + ": ~w~" + message;
        if (Player.GetJobId(sender) == 0)
        {
            API.sendChatMessageToPlayer(sender, "~r~Вы нигде не работаете!");
            return;
        }
        if (Player.GetJobId(sender) != 0)
        {
            foreach (Client client in API.getAllPlayers())
            {
                if (Player.GetJobId(client) == Player.GetJobId(sender))
                {
                    API.sendChatMessageToPlayer(client, chatMessage);
                }
            }
            return;
        }
    }

}