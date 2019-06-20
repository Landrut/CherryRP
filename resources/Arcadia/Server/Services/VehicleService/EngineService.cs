using CherryMPServer;
//
//
using CherryMPServer.Constant;
using CherryMPServer;
using CherryMPShared;
//;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class EngineService : Script
{
    public EngineService()
    {
        API.onClientEventTrigger += onClientEvent;
    }

    private void onClientEvent(Client sender, string eventName, params object[] arguments)
    {
        if (eventName == "engine_on_off")
        {
            if (sender.vehicle.engineStatus == true)
            {
                sender.vehicle.engineStatus = false;
                API.sendNotificationToPlayer(sender, "Двигатель заглушен");
            }
            else if (sender.vehicle.engineStatus == false)
            {
                sender.vehicle.engineStatus = true;
                API.sendNotificationToPlayer(sender, "Двигатель заведён");
            }
        }
    }

    [Command("engineoff")]
    public void engineOff(Client player)
    {
        if (player.isInVehicle & API.getPlayerVehicleSeat(player) == -1)
        {
            player.vehicle.engineStatus = false;
        }
        else
        {
            API.sendNotificationToPlayer(player, "Вы должны быть на водительском месте.");
        }
    }

    [Command("engineon")]
    public void engineOn(Client player)
    {
        if (player.isInVehicle)
        {
            player.vehicle.engineStatus = true;
        }
        else
        {
            if (player.isInVehicle & API.getPlayerVehicleSeat(player) == -1)
                API.sendNotificationToPlayer(player, "Вы должны быть на водительском месте.");
        }
    }

}