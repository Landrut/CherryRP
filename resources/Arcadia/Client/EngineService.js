API.onKeyDown.connect(function (sender, key) {
    var player = API.getLocalPlayer();
    if (API.isPlayerInAnyVehicle(player) && API.getPlayerVehicleSeat(player) == -1) {
        if (key.KeyCode === Keys.Up) {
            API.triggerServerEvent("engine_on_off");
        }
    }
});
API.onKeyDown.connect(function (sender, key) {
    var player = API.getLocalPlayer();
    if (API.isPlayerInAnyVehicle(player) && API.getPlayerVehicleSeat(player) == -1) {
        if (key.KeyCode === Keys.M) {
            API.triggerServerEvent("OpenmenuCar");
        }
    }
});