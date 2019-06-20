using System;
using CherryMPServer;
//
//
using CherryMPServer.Constant;
using CherryMPServer;
using CherryMPShared;
//;

using PlayerFunctions;
using MySQL;
using CustomSkin;

using System.Security.Cryptography;
using System.Text;

public class AccEvents : Script
{
    public AccEvents()
    {
        API.onPlayerConnected += OnPlayerConnectedHandler;
        API.onPlayerDisconnected += OnPlayerDisconnectedHandler;
        API.onPlayerFinishedDownload += OnPlayerFinishedDownload;
    }

    public static readonly Vector3 _startPos = new Vector3(-1553.339f, -1081.579f, 91.79541f);
    public static readonly Vector3 _startCamPos = new Vector3(-1553.339f, -1081.579f, 91.79541f);

    public void OnPlayerFinishedDownload(Client player)
    {
        API.shared.setEntityTransparency(player.handle, 0);
        if (Database.playerExists(player))
            Player.Login(player);
        else
            Player.Register(player);
    }
    public void OnPlayerConnectedHandler(Client player)
    {
        Database.Debug(2, "Новое подключение: [" + player.name + "] " + "[" + player.address + "] " + "ожидание действий...");
        player.setData("InGame", 0);

        player.position = _startPos;
        player.freeze(true);

        API.sendChatMessageToPlayer(player, "~w~Добро пожаловать на ~r~Cherry ~w~Role Play");
        API.sendChatMessageToPlayer(player, "~g~Ваш игровой ник: ~w~" + player.name + ".");
        API.setEntityTransparency(player, 255);
        
    }
    public void OnPlayerDisconnectedHandler(Client player, string reason)
    {
        //PlayerFunctions.Player.UpdatePlayerClothes(player);
        //Database.SavePlayerClothes(player);
        Database.Save_Account(player);
        player.setData("InGame", 0);
        
    }
}

