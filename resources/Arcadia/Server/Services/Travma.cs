/*
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

namespace Travm.servise
{
    class Death : Script
    {

        public static Boolean canRespawn = false;

        public Death()
        {
            API.onPlayerDeath += onDeath;

        }

        private void onDeath(Client player, NetHandle entityKiller, int weapon)
        {
            if (canRespawn == false)
            {
                API.sendNativeToPlayer(player, Hash.DO_SCREEN_FADE_OUT, 200);
                API.setEntityData(player, "Travm", true);
                API.triggerClientEvent(player, "OnTravm");
                API.shared.sendNativeToPlayer(player, Hash.IGNORE_NEXT_RESTART, true);
                API.shared.sendNativeToPlayer(player, Hash._DISABLE_AUTOMATIC_RESPAWN, true);
                API.shared.sendNativeToPlayer(player, Hash.NETWORK_REQUEST_CONTROL_OF_ENTITY, player);
                API.shared.sendNativeToPlayer(player, Hash.FREEZE_ENTITY_POSITION, player, true);
                API.shared.sendNativeToPlayer(player, Hash.SET_PED_TO_RAGDOLL, player, true);
                API.sendNativeToPlayer(player, Hash.DO_SCREEN_FADE_IN, 200);
                API.delay(60000 * 3, true, () => { canRespawn = true; });
            }
            else if (canRespawn == true)
            {
                API.sendNativeToPlayer(player, Hash.DO_SCREEN_FADE_OUT, 200);
                API.setEntityData(player, "Travm", false);
                API.shared.sendNativeToPlayer(player, Hash.IGNORE_NEXT_RESTART, true);
                API.shared.sendNativeToPlayer(player, Hash.FREEZE_ENTITY_POSITION, player, false);
                API.shared.sendNativeToPlayer(player, Hash.SET_PED_TO_RAGDOLL, player, false);
                API.shared.sendNativeToPlayer(player, Hash.NETWORK_RESURRECT_LOCAL_PLAYER, player.position.X, player.position.Y, player.position.Z, player.rotation.Z, false, false);
                API.sendNativeToPlayer(player, Hash.DO_SCREEN_FADE_IN, 200);
                canRespawn = false;
                API.sendChatMessageToPlayer(player, "~b~Вас доставили в отделение скорой помощи.");
            }
        }
    }
}
*/