API.onResourceStart.connect(function () {
    API.enableInteriorProp(246529, "walls_01");

    API.enableInteriorProp(247553, "equipment_upgrade");
});

API.onUpdate.connect(function () {
    var player = API.getLocalPlayer();
    var InteriorID = API.getInteriorIdFromEntity(player);
    var inVeh = API.isPlayerInAnyVehicle(player);
    if (!inVeh) {
        API.drawText(`Интерьер:`, res_X - 70, res_Y - 225, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
        API.drawText(`${InteriorID}`, res_X - 20, res_Y - 225, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
    }
});