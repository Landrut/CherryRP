using System;
using CherryMPServer;
//
//
using CherryMPServer.Constant;
using CherryMPServer;
using CherryMPShared;
//;
using MySQL;

using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

public class arcadia : Script
{
    public const Int32 MoneyCap = 2147483647; // Максимальное кол-во денег.
    public const Int32 BankMoneyCap = 2147483647; // Максимальное кол-во денег в банке.

    public arcadia()
    {
        API.onResourceStart += onResourceStart;
        API.onResourceStop += onResourceStop;
    }
    public void onResourceStart()
    {
        API.consoleOutput("Cherry Role Play запущен!");
    }
    public void onResourceStop()
    {
        API.consoleOutput("Завершение работы и сохранение данных...");
        var players = API.shared.getAllPlayers();
        foreach (var player in players)
        {
            //PlayerFunctions.Player.UpdatePlayerClothes(player);
            //Database.SavePlayerClothes(player);
            Database.Save_Account(player);
            player.setData("InGame", 0);
        }
    }
}
