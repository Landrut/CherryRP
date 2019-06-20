var Svalkablip;
var Svalkamarker;
var Baseblip;
var Basemarker;

API.onKeyDown.connect(function (sender, e) {
    if (e.KeyCode === Keys.E) {

        API.triggerServerEvent("StartSvalka");

    }

    if (e.KeyCode === Keys.Y) {

        API.triggerServerEvent("SvalkaMission");
        


    }
    if (e.KeyCode === Keys.B) {

        
        API.triggerServerEvent("BaseMission");


    }
});

API.onServerEventTrigger.connect(function (eventName, args) {
    if (eventName == "waypoint") {

        API.setWaypoint(args[0], args[1]);

    }

    if (eventName == "nextChekSvalka") {

        var textLabel = API.createTextLabel("Нажмите Y для завершения", args[0], 1, 1);

        if (Svalkablip != null) {

            API.setBlipPosition(Svalkablip, args[0]);
            API.deleteEntity(Svalkamarker);
        }
        else {
            Svalkablip = API.createBlip(args[0]);

        }

        Svalkamarker = API.createMarker(1, args[0], new Vector3(), new Vector3(), new Vector3(2, 2, 2), 255, 0, 0, 255);
        API.attachEntity(textLabel, Svalkamarker, "0", new Vector3(), new Vector3());

        API.setWaypoint(args[1], args[2]);

    }
    if (eventName == "nextBaseChek") {

        var textLabel = API.createTextLabel("Нажмите Y для завершения", args[0], 1, 1);

        if (Baseblip != null) {

            API.setBlipPosition(Baseblip, args[0]);
            API.deleteEntity(Basemarker);
        }
        else {
            Baseblip = API.createBlip(args[0]);

        }

        Basemarker = API.createMarker(1, args[0], new Vector3(), new Vector3(), new Vector3(2, 2, 2), 255, 0, 0, 255);
        API.attachEntity(textLabel, Basemarker, "1", new Vector3(), new Vector3());

        API.setWaypoint(args[1], args[2]);

    }

    if (eventName == "EndMissionSvalka") {

        if (Svalkablip != null) {


            API.deleteEntity(Svalkamarker);
        }
    }

        if (eventName == "BaseMissionSvalka") {

            if (Baseblip != null) {


                API.deleteEntity(Basemarker);
            }
    }
});