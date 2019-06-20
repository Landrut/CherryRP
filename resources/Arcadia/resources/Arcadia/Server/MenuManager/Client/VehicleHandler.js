// EVENTS
API.onUpdate.connect(Update);
API.onServerEventTrigger.connect(OnServerEvent);

// VARIABLES
var player = API.getLocalPlayer();
var resX = API.getScreenResolutionMaintainRatio().Width;
var resY = API.getScreenResolutionMaintainRatio().Height;
var controls = false;

function OnServerEvent(eventName, args) {
    switch (eventName) {
        case "DISABLE_CONTROLS":
            controls = args[0];
            break;
        default:
            break;
    }
}

function Update() {
    if (controls) {
        API.disableAllControlsThisFrame();
    }
    if (!API.getHudVisible()) {
        return;
    }
    if (API.isPlayerInAnyVehicle(player)) {
        var veh = API.getPlayerVehicle(player);
        if (API.hasEntitySyncedData(veh, "Local_Fuel")) {
            var currentFuel = Math.round(API.getEntitySyncedData(veh, "Local_Fuel"));
            API.drawText("~o~Топливо: ~w~" + currentFuel + "/100", resX - 1540, 1000, 0.7, 255, 255, 255, 255, 4, 1, false, true, 0);
        }
    }
};