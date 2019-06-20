API.onKeyUp.connect(function (e, key) {
    if (key.KeyCode == Keys.ControlKey) {
        API.triggerServerEvent("ToggleCrouch");
    }
})

API.onEntityStreamIn.connect(function (entity, entityType) {
    if (entityType == 6 && API.hasEntitySyncedData(entity, "IsCrouched")) {
        API.setPlayerMovementClipset(entity, "move_ped_crouched", 0.1);
        // API.setPlayerStrafeClipSet(entity, "move_ped_crouched_strafing");
    }
});

API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case "EnableCrouch":
            API.setPlayerMovementClipset(args[0], "move_ped_crouched", 0.1);
            // API.setPlayerStrafeClipSet(args[0], "move_ped_crouched_strafing");
            break;

        case "DisableCrouch":
            API.resetPlayerMovementClipset(args[0]);
            //  API.resetPlayerStrafeClipSet(args[0]);
            break;
    }
});