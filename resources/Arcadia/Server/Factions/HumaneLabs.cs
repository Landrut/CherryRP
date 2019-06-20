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

public class HumaneLabs : Script
{
    public HumaneLabs()
    {
        API.onResourceStart += onResourceStart;
    }

    public readonly Vector3 HumaneLabsBlipPos = new Vector3(3512.98f, 3753.429f, 30.1174f);
    public readonly Vector3 HumaneEnterMarkerPos = new Vector3(3512.98f, 3753.429f, 30.1174f);
    public readonly Vector3 HumaneExitMarkerPos = new Vector3(3515f, 3753.429f, 30.1174f);
    public readonly Vector3 HumaneLabsEnterInsidePos = new Vector3(3561.12f, 3696.016f, 30.12131f);
    public readonly Vector3 HumaneLabsDutyPos = new Vector3(3553.257f, 3656.479f, 28.12189f);
    public readonly Vector3 HumaneLabsExitGarageInsidePos = new Vector3(3625.328f, 3743.666f, 28.6901f);
    public readonly Vector3 HumaneLabsEnterGarageInsidePos = new Vector3(3620.97f, 3737.343f, 28.6901f);
    public readonly Vector3 HumaneLabsExitPos = new Vector3(3558.983f, 3696.132f, 30.12158f);
   
    public Blip HumaneBlip;
    
    public ColShape HumaneLabsEnterColshape;
    public ColShape HumaneLabsDutyColshape;
    public ColShape HumaneLabsExitColshape;

    public void onResourceStart()
    {

        HumaneBlip = API.createBlip(HumaneLabsBlipPos);
        API.setBlipSprite(HumaneBlip, 499);
        API.setBlipScale(HumaneBlip, 1.0f);
        API.setBlipColor(HumaneBlip, 5);
        API.setBlipName(HumaneBlip, "Лаборатория");
        API.setBlipShortRange(HumaneBlip, true);

        HumaneLabsEnterColshape = API.createCylinderColShape(HumaneEnterMarkerPos, 0.50f, 1f);
        HumaneLabsDutyColshape = API.createCylinderColShape(HumaneLabsDutyPos, 0.50f, 1f);
        HumaneLabsExitColshape = API.createCylinderColShape(HumaneLabsExitPos, 0.50f, 1f);

        API.createMarker(1, HumaneEnterMarkerPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createMarker(1, HumaneLabsExitPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 50, 220, 90);
        API.createMarker(1, HumaneLabsDutyPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(0.85f, 0.85f, 0.85f), 100, 0, 0, 255);

        HumaneLabsEnterColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                API.setEntityPosition(player, HumaneLabsEnterInsidePos);
                //API.setEntityDimension(player, 1);
            }
        };

        HumaneLabsExitColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
            else
            {
                API.setEntityPosition(player, HumaneExitMarkerPos);
                //API.setEntityDimension(player, 1);
            }
        };

        HumaneLabsDutyColshape.onEntityEnterColShape += (shape, Entity) =>
        {
            Client player;
            player = API.getPlayerFromHandle(Entity);
            int ismedicfaction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0; ;
            string groupName = "R";
            var chatMessageOnDuty = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)) + " | " + player.name + "~y~ вышел на смену";
            var chatMessageOffDuty = "~b~[" + groupName + "] ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)) + " | " + player.name + "~y~ окончил смену";
            if (ismedicfaction == 6)
            {
                int onduty = Player.IsPlayerOnDuty(player);
                if (onduty == 0)
                {
                    Faction.SetPlayerFactionClothes(player);
                    Player.SetPlayerOnDuty(player);
                    foreach (Client client in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(client) == 6 && Player.IsPlayerOnDuty(client) == 1)
                        {
                            API.sendChatMessageToPlayer(client, chatMessageOnDuty);
                        }
                    }
                    API.sendChatMessageToPlayer(player, "~y~Вы вышли на смену как ~b~" + Faction.GetPlayerFactionRank(player, Player.GetFractionRank(player)));
                }
                else
                {
                    Player.RemovePlayerOnDuty(player);
                    foreach (Client client in API.getAllPlayers())
                    {
                        if (Player.GetFractionId(client) == 6 && Player.IsPlayerOnDuty(client) == 1)
                        {
                            API.sendChatMessageToPlayer(client, chatMessageOffDuty);
                        }
                    }
                    PlayerFunctions.Player.LoadPlayerClothes(player);
                    API.sendChatMessageToPlayer(player, "~y~Вы окончили смену");
                }
                return;
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~Вы не состоите в данной фракции!");
            }
            if (API.isPlayerInAnyVehicle(player) == true)
            {
                return;
            }
        };

    }
}
