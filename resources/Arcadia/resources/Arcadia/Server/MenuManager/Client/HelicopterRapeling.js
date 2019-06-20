var rappelVehicles = [-1660661558, 353883353, 837858166]; // maverick, emergency maverick, annihilator
var rappelMinHeight = 15.0;
var rappelMaxHeight = 45.0;

API.onKeyDown.connect(function (e, key) {
    if (key.KeyCode == Keys.X) {
        if (API.isChatOpen() || !API.isPlayerInAnyVehicle(API.getLocalPlayer())) return;

        var vehicle = API.getPlayerVehicle(API.getLocalPlayer());
        if (rappelVehicles.indexOf(API.getEntityModel(vehicle)) == -1) return;

        var seat = API.getPlayerVehicleSeat(API.getLocalPlayer());
        if (seat < 1) {
            API.sendChatMessage("~r~Ошибка: ~w~Вы не можете спуститься с этого сиденья.");
            return;
        }

        if (API.returnNative("GET_ENTITY_SPEED", 7, vehicle) > 10.0) {
            API.sendChatMessage("~r~Ошибка: ~w~Вертолёт слишком быстро двигается.");
            return;
        }

        var taskStatus = API.returnNative("GET_SCRIPT_TASK_STATUS", 0, API.getLocalPlayer(), -275944640);
        if (taskStatus == 0 || taskStatus == 1) {
            API.sendChatMessage("~r~Ошибка: ~w~Вы уже спускаетесь.");
            return;
        }

        var entityHeight = API.returnNative("GET_ENTITY_HEIGHT_ABOVE_GROUND", 7, vehicle);
        if (entityHeight < rappelMinHeight || entityHeight > rappelMaxHeight) {
            API.sendChatMessage("~r~Ошибка: ~w~Вертолет слишком высоко или низко для спуска.");
            return;
        }

        if (!API.isEntityUpright(vehicle, 15.0)) {
            API.sendChatMessage("~r~Ошибка: ~w~Вертолету нужно почти неподвижное состояние.");
            return;
        }

        if (API.isEntityUpsidedown(vehicle)) {
            API.sendChatMessage("~r~Ошибка: ~w~Вы не можете спуститься с верха вертолёта.");
            return;
        }

        API.triggerServerEvent("RappelFromHelicopter");
    }
});