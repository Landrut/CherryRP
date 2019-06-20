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

public class GangFactionModel : Script
{
    public static String GetPlayerGangFactionInfo(Client player, int gang)
    {
        switch (gang)
        {
            case 0: return "~w~Гражданский ~w~";
            case 1: return "~y~Families ~w~";
            case 2: return "~g~Vagos ~w~";
            case 3: return "~c~Ballas ~w~";
            case 4: return "~c~LostMC ~w~";
            case 5: return "~c~Triads ~w~";
            case 6: return "~c~Cortel_Madraz ~w~";

            default: return "";
        }
    }

    public static String GetPlayerGangRank(Client player, int gangrank)
    {

        int gang = (player.hasData("gang_id")) ? player.getData("gang_id") : 0;

        if (gang == 1)
        {
            switch (gangrank)
            {
                case 1: return "1";
                case 2: return "2";
                case 3: return "3";
                case 4: return "4";
                case 5: return "5";
                case 6: return "6";
                case 7: return "7";
                case 8: return "8";
                case 9: return "9";
                case 10: return "10";
                default: return "";
            }
        }
        if (gang == 2)
        {
            switch (gangrank)
            {
                case 1: return "1";
                case 2: return "2";
                case 3: return "3";
                case 4: return "4";
                case 5: return "5";
                case 6: return "6";
                case 7: return "7";
                case 8: return "8";
                case 9: return "9";
                case 10: return "10";
                default: return "";
            }
        }
        if (gang == 3)
        {
            switch (gangrank)
            {
                case 1: return "1";
                case 2: return "2";
                case 3: return "3";
                case 4: return "4";
                case 5: return "5";
                case 6: return "6";
                case 7: return "7";
                case 8: return "8";
                case 9: return "9";
                case 10: return "10";
                default: return "";
            }
        }
        if (gang == 4)
        {
            switch (gangrank)
            {
                case 1: return "1";
                case 2: return "2";
                case 3: return "3";
                case 4: return "4";
                case 5: return "5";
                case 6: return "6";
                case 7: return "7";
                case 8: return "8";
                case 9: return "9";
                case 10: return "10";
                default: return "";
            }
        }
        if (gang == 5)
        {
            switch (gangrank)
            {
                case 1: return "1";
                case 2: return "2";
                case 3: return "3";
                case 4: return "4";
                case 5: return "5";
                case 6: return "6";
                case 7: return "7";
                case 8: return "8";
                case 9: return "9";
                case 10: return "10";
                default: return "";
            }
        }
        if (gang == 6)
        {
            switch (gangrank)
            {
                case 1: return "1";
                case 2: return "2";
                case 3: return "3";
                case 4: return "4";
                case 5: return "5";
                case 6: return "6";
                case 7: return "7";
                case 8: return "8";
                case 9: return "9";
                case 10: return "10";
                default: return "";
            }
        }

        else return "-";
    }

    [Command("ganginvite")]
    public void FactionGangInviteCommand(Client sender, string idOrName)
    {
        int rank = Player.GetGangRank(sender);
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        int gangIDOfTarget = Player.GetGangId(target);
        int gangIDOfSender = Player.GetGangId(sender);
        var inviteMessageTarget = "~b~" + sender.name + "~w~ принял вас в банду " + GetPlayerGangFactionInfo(sender, Player.GetGangId(sender));
        var inviteMessageSender = "~b~" + target.name + "~w~ был принят вами в банду " + GetPlayerGangFactionInfo(sender, Player.GetGangId(sender));

        if (Player.GetGangRank(sender) != 2)
        {
            API.sendChatMessageToPlayer(sender, "~r~Ваш ранг не позволяет вам использовать данную команду");
        }
        else
        {
            if (gangIDOfSender == gangIDOfTarget)
            {
                API.sendChatMessageToPlayer(sender, "~b~ " + target.name + "~y~ уже у вас в банде");
                return;
            }
            else
            {
                Player.SetGangID(target, gangIDOfSender);
                Player.SetGangRank(target, 1);
                API.sendChatMessageToPlayer(sender, inviteMessageSender);
                API.sendChatMessageToPlayer(target, inviteMessageTarget);
            }
        }
    }

    [Command("Ganguninvite")]
    public void FactionGangUninviteCommand(Client sender, string idOrName)
    {
        int rank = Player.GetGangRank(sender);
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        int GangIDOfTarget = Player.GetGangId(target);
        int GangIDOfSender = Player.GetGangId(sender);
        var uninviteMessageTarget = "~b~" + sender.name + "~w~ уволил вас из " + GetPlayerGangFactionInfo(sender, Player.GetGangId(sender));
        var uninviteMessageSender = "~b~" + target.name + "~w~ был уволен вами из " + GetPlayerGangFactionInfo(sender, Player.GetGangId(sender));

        if (Player.GetGangRank(sender) != 2)
        {
            API.sendChatMessageToPlayer(sender, "~r~Ваш ранг не позволяет вам использовать данную команду");
        }
        else
        {
            if (GangIDOfTarget == 0)
            {
                API.sendChatMessageToPlayer(sender, "~b~ " + target.name + "~y~ не состоит в вашей фракции");
                return;
            }
            else
            {
                Player.SetGangID(target, 0);
                API.sendChatMessageToPlayer(sender, uninviteMessageSender);
                API.sendChatMessageToPlayer(target, uninviteMessageTarget);
            }
        }
    }

    [Command("giverank")]
    public void GiveRankCommand(Client sender, string idOrName, int giverank)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);

        int senderfaction = Player.GetGangId(sender);
        int targetfaction = Player.GetGangId(target);

        var rankupmessage = "~r~" + sender.name + "~w~ повысил вас до " + giverank + " ранга";
        var rankdownmessage = "~r~" + sender.name + "~w~ понизил вас до " + giverank + " ранга";

        var rankupmessagesender = "~y~Вы повысили ~b~" + target.name + " ~y~до " + giverank + " ранга";
        var rankdownmessagesender = "~y~Вы понизили ~b~" + target.name + " ~y~до " + giverank + " ранга";

        if (Player.GetGangRank(sender) <= 8)
        {
            API.sendChatMessageToPlayer(sender, "~r~Ваш ранг не позволяет вам использовать данную команду");
        }
        else
        {
            if (targetfaction == 0)
            {
                API.sendChatMessageToPlayer(sender, "~b~ " + target.name + "~y~ не состоит в вашей банде");
                return;
            }
            else
            {
                if (Player.GetGangRank(target) < giverank)
                {
                    Player.SetGangRank(target, giverank);
                    API.sendChatMessageToPlayer(target, rankupmessage);
                    return;
                }
                else
                {
                    Player.SetGangRank(target, giverank);
                    API.sendChatMessageToPlayer(target, rankdownmessage);
                    return;
                }
            }
        }
    }


    [Command("t", GreedyArg = true)]
    public void FractionChatCommand(Client sender, string message)
    {
        int rank = Player.GetGangRank(sender);
        string groupName = "t";
        var chatMessage = "~b~[" + groupName + " ID: " + "~b~" + API.exported.playerids.getIdFromClient(sender) + "] ~b~" + GetPlayerGangRank(sender, Player.GetGangRank(sender)) + " | " + sender.name + ": ~w~" + message;
        if (Player.GetGangId(sender) == 0)
        {
            API.sendChatMessageToPlayer(sender, "~r~Вы не состоите в банде!");
            return;
        }
        if (Player.GetGangId(sender) != 0)
        {
            foreach (Client client in API.getAllPlayers())
            {
                if (Player.GetGangId(client) == Player.GetGangId(sender))
                {
                    API.sendChatMessageToPlayer(client, chatMessage);
                }
            }
            return;
        }
    }

}