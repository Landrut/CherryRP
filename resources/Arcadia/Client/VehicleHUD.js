var res_X = API.getScreenResolutionMaintainRatio().Width;
var res_Y = API.getScreenResolutionMaintainRatio().Height;

API.onUpdate.connect(function () {
    var player = API.getLocalPlayer();
    var inVeh = API.isPlayerInAnyVehicle(player);

    if (inVeh) {
        var veh = API.getPlayerVehicle(player);
        var vel = API.getEntityVelocity(veh);
        var health = API.getVehicleHealth(veh);
        var maxhealth = API.returnNative("GET_ENTITY_MAX_HEALTH", 0, veh);
        var healthpercent = Math.floor((health / maxhealth) * 100);
        var speed = Math.sqrt(
            vel.X * vel.X +
            vel.Y * vel.Y +
            vel.Z * vel.Z
            );
        var displaySpeedMph = Math.round(speed * 2.23693629);
        API.drawText(`${displaySpeedMph}`, res_X - 60, res_Y - 200, 1, 255, 255, 255, 255, 4, 2, false, true, 0);
        API.drawText(`MPH`, res_X - 20, res_Y - 180, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
        API.drawText(`Состояние:`, res_X - 70, res_Y - 225, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
        API.drawText(`${healthpercent}%`, res_X - 20, res_Y - 225, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
        if (healthpercent < 60) {
            API.drawText(`${healthpercent}%`, res_X - 20, res_Y - 225, 0.5, 219, 122, 46, 255, 4, 2, false, true, 0);
        }
        if (healthpercent < 30) {
            API.drawText(`${healthpercent}%`, res_X - 20, res_Y - 225, 0.5, 219, 46, 46, 255, 4, 2, false, true, 0);
        }
    }
});