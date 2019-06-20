var FWblip;
var FWmarker;

API.onKeyDown.connect(function (sender, e) {
    if (e.KeyCode === Keys.E) {

        API.triggerServerEvent("StartFW");

    }

    if (e.KeyCode === Keys.Y) {

        API.triggerServerEvent("FWMission");


    }
});

API.onServerEventTrigger.connect(function (eventName, args) {
    if (eventName == "waypoint") {

        API.setWaypoint(args[0], args[1]);

    }

    if (eventName == "nextChekFW") {

        var textLabel = API.createTextLabel("Нажмите Y для завершения", args[0], 1, 1);

        if (FWblip != null) {

            API.setBlipPosition(FWblip, args[0]);
            API.deleteEntity(FWmarker);
        }
        else {
            FWblip = API.createBlip(args[0]);

        }

        FWmarker = API.createMarker(6, args[0], new Vector3(), new Vector3(), new Vector3(15, 15, 15), 255, 0, 0, 255);
        API.attachEntity(textLabel, FWmarker, "0", new Vector3(), new Vector3());

        API.setWaypoint(args[1], args[2]);

    }

    if (eventName == "EndMissionFW") {

        if (FWblip != null) {


            API.deleteEntity(FWmarker);
        }
    }
});