using System;
using System.Linq;
using CherryMPServer;
//
using CherryMPServer.Constant;
//

namespace VisionController
{
    [Flags]
    public enum AnimationFlags
    {
        None = 0,
        Loop = 1,
        StayInEndFrame = 2,
        UpperBodyOnly = 16,
        AllowRotation = 32,
        CancelableWithMovement = 128,
        RagdollOnCollision = 4194304
    }

    public class VisionController : Script
    {
        readonly int[] MaleHelmetsUp = { 117, 119 };
        readonly int[] MaleHelmetsDown = { 116, 118 };
        readonly int[] FemaleHelmetsUp = { 116, 118 };
        readonly int[] FemaleHelmetsDown = { 115, 117 };

        readonly int HelmetSlot = 0;
        readonly string HelmetDrawableKey = "NextHelmetDrawable";
        readonly string HelmetTextureKey = "NextHelmetTexture";

        public VisionController()
        {
            API.onClientEventTrigger += CombatHelmets_EventTrigger;
        }

        public void CombatHelmets_EventTrigger(Client player, string eventName, params object[] args)
        {
            switch (eventName)
            {
                case "ToggleHelmet":
                    {
                        PedHash playerModel = (PedHash)player.model;
                        if (!(playerModel == PedHash.FreemodeMale01 || playerModel == PedHash.FreemodeFemale01)) return;
                        if (player.hasData(HelmetDrawableKey) || player.hasData(HelmetTextureKey) || player.isInVehicle) return;
                        int playerHat = player.getAccessoryDrawable(HelmetSlot);

                        int nextHelmetDrawable = -1;
                        bool visorGoingDown = false;

                        if (playerModel == PedHash.FreemodeMale01)
                        {
                            if (MaleHelmetsUp.Contains(playerHat))
                            {
                                nextHelmetDrawable = MaleHelmetsDown[Array.IndexOf(MaleHelmetsUp, playerHat)];
                                visorGoingDown = true;
                            }
                            else if (MaleHelmetsDown.Contains(playerHat))
                            {
                                nextHelmetDrawable = MaleHelmetsUp[Array.IndexOf(MaleHelmetsDown, playerHat)];
                                visorGoingDown = false;
                            }
                        }
                        else if (playerModel == PedHash.FreemodeFemale01)
                        {
                            if (FemaleHelmetsUp.Contains(playerHat))
                            {
                                nextHelmetDrawable = FemaleHelmetsDown[Array.IndexOf(FemaleHelmetsUp, playerHat)];
                                visorGoingDown = true;
                            }
                            else if (FemaleHelmetsDown.Contains(playerHat))
                            {
                                nextHelmetDrawable = FemaleHelmetsUp[Array.IndexOf(FemaleHelmetsDown, playerHat)];
                                visorGoingDown = false;
                            }
                        }

                        if (nextHelmetDrawable != -1)
                        {
                            player.setData(HelmetDrawableKey, nextHelmetDrawable);
                            player.setData(HelmetTextureKey, player.getAccessoryTexture(HelmetSlot));
                            player.playAnimation("anim@mp_helmets@on_foot", (visorGoingDown) ? "visor_down" : "visor_up", (int)(AnimationFlags.UpperBodyOnly | AnimationFlags.AllowRotation));
                            player.triggerEvent("ActivateAnimReporter", (visorGoingDown) ? "visor_down" : "visor_up");
                        }

                        break;
                    }

                case "HelmetAnimComplete":
                    {
                        if (!player.hasData(HelmetDrawableKey) || !player.hasData(HelmetTextureKey)) return;
                        int helmetDrawable = player.getData(HelmetDrawableKey);
                        PedHash playerModel = (PedHash)player.model;

                        if (playerModel == PedHash.FreemodeMale01)
                        {
                            switch (helmetDrawable)
                            {
                                case 116:
                                case 117:
                                    API.sendNativeToPlayer(player, Hash.SET_NIGHTVISION, (helmetDrawable == 116));
                                    break;

                                case 118:
                                case 119:
                                    API.sendNativeToPlayer(player, Hash.SET_SEETHROUGH, (helmetDrawable == 118));
                                    break;
                            }
                        }
                        else if (playerModel == PedHash.FreemodeFemale01)
                        {
                            switch (helmetDrawable)
                            {
                                case 115:
                                case 116:
                                    API.sendNativeToPlayer(player, Hash.SET_NIGHTVISION, (helmetDrawable == 115));
                                    break;

                                case 117:
                                case 118:
                                    API.sendNativeToPlayer(player, Hash.SET_SEETHROUGH, (helmetDrawable == 117));
                                    break;
                            }
                        }

                        player.setAccessories(HelmetSlot, helmetDrawable, player.getData(HelmetTextureKey));
                        player.resetData(HelmetDrawableKey);
                        player.resetData(HelmetTextureKey);
                        break;
                    }
            }
        }
    }
}
