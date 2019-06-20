API.onUpdate.connect(function () {
    if (!API.getHudVisible()) return;

    var res = API.getScreenResolutionMaintainRatio();
    API.drawText(API.getWorldSyncedData("DAYNIGHT_TEXT"), (res.Width / 2) - 560, res.Height - 50, 0.65, 255, 255, 255, 255, 4, 0, true, true, 0);

    if (!API.getWorldSyncedData("DAYNIGHT_RENDER_ICON")) {
        API.drawText(API.getWorldSyncedData("DAYNIGHT_DAY_STRING"), (res.Width / 2) - 560, res.Height - 71, 0.40, 241, 196, 15, 255, 4, 0, true, true, 0);
    } else {
        API.drawText(API.getWorldSyncedData("DAYNIGHT_DAY_STRING"), (res.Width / 2) - 535, res.Height - 71, 0.40, 241, 196, 15, 255, 4, 0, true, true, 0);

        var serverHour = API.getWorldSyncedData("DAYNIGHT_HOUR");
        API.dxDrawTexture(((serverHour >= 6 && serverHour <= 20) ? "Client/icons/iconDay" : "Client/icons/iconNight") + ".png", new Point(Math.round((res.Width / 2) - 560), Math.round(res.Height - 69)), new Size(24, 24));
    }
});