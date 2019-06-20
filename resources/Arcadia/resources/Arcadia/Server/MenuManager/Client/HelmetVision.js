var freemodeMale = 1885233650;
var freemodeFemale = -1667301416;
var maleCombatHelmetDrawables = [116, 117, 118, 119];
var femaleCombatHelmetDrawables = [115, 116, 117, 118];
var animReporterActive = false;
var curAnim = "";

API.onResourceStop.connect(function () {
    API.callNative("SET_NIGHTVISION", false);
    API.callNative("SET_SEETHROUGH", false);
});

API.onKeyUp.connect(function (e, key) {
    if (key.KeyCode == Keys.F10) {
        if (animReporterActive || API.isPlayerInAnyVehicle(API.getLocalPlayer())) return;

        var playerModel = API.getEntityModel(API.getLocalPlayer());
        if (!(playerModel == freemodeMale || playerModel == freemodeFemale)) return;

        var playerHat = API.returnNative("GET_PED_PROP_INDEX", 0, API.getLocalPlayer(), 0);
        if (playerModel == freemodeMale) {
            if (maleCombatHelmetDrawables.indexOf(playerHat) > -1) API.triggerServerEvent("ToggleHelmet");
        } else if (playerModel == freemodeFemale) {
            if (femaleCombatHelmetDrawables.indexOf(playerHat) > -1) API.triggerServerEvent("ToggleHelmet");
        }
    }
});

API.onServerEventTrigger.connect(function (eventName, args) {
    if (eventName == "ActivateAnimReporter") {
        animReporterActive = true;
        curAnim = args[0];
    }
});

API.onUpdate.connect(function () {
    if (!animReporterActive) return;

    var curAnimTime = API.getAnimCurrentTime(API.getLocalPlayer(), "anim@mp_helmets@on_foot", curAnim);
    if (curAnimTime >= 0.46) {
        animReporterActive = false;
        API.triggerServerEvent("HelmetAnimComplete");
    }
});