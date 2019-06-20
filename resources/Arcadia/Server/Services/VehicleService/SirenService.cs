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

public class SirenService : Script
{
    public int variant;

    public SirenService()
    {
        API.onClientEventTrigger += onClientEventTrigger;
        API.onVehicleSirenToggle += onVehicleSirenToggle;
        API.onResourceStart += onResourceStart;
    }

    public void onResourceStart()
    {
        variant = 1;
        API.consoleOutput("Variant: " + variant + " is active.");
    }

    //Variant 1. Press G to toggle the siren on/off
    public void onClientEventTrigger(Client player, string eventName, params object[] arguments)
    {
        var players = API.getPlayersInRadiusOfPlayer(700, player);

        if (variant != 1) return;

        if (eventName != "sirenToggle") return;

        if (API.isPlayerInAnyVehicle(player) != true) return;

        if (API.getPlayerVehicleSeat(player) != -1) return;

        if (API.getEntityData(player.vehicle, "REAL_SIREN_STATE") == null || API.getEntityData(player.vehicle, "REAL_SIREN_STATE") == true)
        {
            API.sendNativeToAllPlayers(0xD8050E0EB60CF274, player.vehicle.handle, true);
            API.setEntityData(player.vehicle, "REAL_SIREN_STATE", false);
        }
        else
        {
            API.sendNativeToAllPlayers(0xD8050E0EB60CF274, player.vehicle, false);
            API.setEntityData(player.vehicle, "REAL_SIREN_STATE", true);
        }
    }

    private void onVehicleSirenToggle(NetHandle entity, bool oldValue)
    {
        var player = API.getPlayerFromHandle(entity);
        //Variant 2. Press E to toggle between: ELS/Siren on -> ELS on/Siren off -> Both off
        if (variant == 2)
        {
            if (API.getVehicleSirenState(player.vehicle) == true)
            {
                //ELS/Siren on
                if (API.getEntityData(player.vehicle, "REAL_SIREN_STATE") != null) return;

                API.sendNativeToPlayer(player, 0xD8050E0EB60CF274, player.vehicle, false);
                API.setEntityData(player.vehicle, "REAL_SIREN_STATE", true);
            }
            else
            {
                //ELS on/Siren off
                if (API.getEntityData(player.vehicle, "REAL_SIREN_STATE") == true)
                {
                    //this turns ELS off
                    API.sendNativeToPlayer(player, 0xF4924635A19EB37D, player.vehicle, true);
                    //this turns off the siren
                    API.sendNativeToPlayer(player, 0xD8050E0EB60CF274, player.vehicle, true);
                    API.setEntityData(player.vehicle, "REAL_SIREN_STATE", false);
                }
                //ELS/Siren off
                else
                {
                    API.setEntityData(player.vehicle, "REAL_SIREN_STATE", null);
                }
            }
        }
        else if (variant == 3)
        {
            //Variant 3. Turns on the ELS-Lights via a command. the siren gets controlled via E Key. 
            //
            if (API.getVehicleSirenState(player.vehicle) == true)
            {
                if (API.getEntityData(player.vehicle, "ELS_STATE") == true) return;

                API.sendNativeToPlayer(player, 0xF4924635A19EB37D, player.vehicle, false);
            }
            else
            {
                if (API.getEntityData(player.vehicle, "ELS_STATE") != true) return;

                API.sendNativeToPlayer(player, 0xF4924635A19EB37D, player.vehicle, true);
                if (API.getEntityData(player.vehicle, "REAL_SIREN_STATE") == null || API.getEntityData(player.vehicle, "REAL_SIREN_STATE") == true)
                {
                    API.sendNativeToPlayer(player, 0xD8050E0EB60CF274, player.vehicle, true);
                    API.setEntityData(player.vehicle, "REAL_SIREN_STATE", false);
                }
                else
                {
                    API.sendNativeToPlayer(player, 0xD8050E0EB60CF274, player.vehicle, false);
                    API.setEntityData(player.vehicle, "REAL_SIREN_STATE", true);
                }
            }
        }
    }
    //Command to toggle the ELS on/off. You can also implement it in youre own player-menus pretty easy.
    [Command("togels")]
    public void elstoggle(Client player)
    {
        if (variant != 3) return;

        if (API.getVehicleSirenState(player.vehicle) == true)
        {
            API.setEntityData(player.vehicle, "ELS_STATE", false);
            API.sendNativeToPlayer(player, 0xF4924635A19EB37D, player.vehicle, false);
        }
        else
        {
            API.setEntityData(player.vehicle, "ELS_STATE", true);
            API.sendNativeToPlayer(player, 0xF4924635A19EB37D, player.vehicle, true);
        }
    }
}
