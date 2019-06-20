//
using CherryMPServer;
//

namespace Rappel
{
    public class HelicopterRapelingService : Script
    {
        public HelicopterRapelingService()
        {
            API.onClientEventTrigger += Rappel_EventTrigger;
        }

        public void Rappel_EventTrigger(Client player, string eventName, params object[] args)
        {
            if (eventName == "RappelFromHelicopter")
            {
                API.sendNativeToPlayersInRangeInDimension(player.position, 150f, player.dimension, Hash.CLEAR_PED_TASKS, player.handle);
                API.sendNativeToPlayersInRangeInDimension(player.position, 150f, player.dimension, Hash.TASK_RAPPEL_FROM_HELI, player.handle, 1092616192);
            }
        }
    }
}