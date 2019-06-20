"use strict";
var ver = "0.0.3";

API.onUpdate.connect(function () {
    var res = API.getScreenResolutionMaintainRatio();
    API.drawText("~b~Arcadia~w~ RolePlay ~o~Dev " + ver, (res.Width / 2) + 600, (res.Height / 2) - 545, 0.55, 255, 255, 255, 255, 4, 1, true, false, 0);
	API.drawText("~y~Горячие клавиши", (res.Width / 2), (res.Height / 2) + 477, 0.40, 255, 255, 255, 255, 4, 1, true, false, 0);
    API.drawText("~b~F1 ~w~Анимации | ~b~F3 ~w~Походки | ~b~F4 ~w~Настроение", (res.Width / 2), (res.Height / 2) + 500, 0.40, 255, 255, 255, 255, 4, 1, true, false, 0);
});

API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case "setServerVersion":
            ver = args[0];
            break
    }
});
