var desblip;
var marker;

API.onKeyDown.connect(function (sender, e) {
    if (e.KeyCode === Keys.E) {

        API.triggerServerEvent("StartMission");

    }

    if (e.KeyCode === Keys.Y) {

        API.triggerServerEvent("objComplete");


    }
});

API.onServerEventTrigger.connect(function (eventName, args) {
    if (eventName == "waypoint") {

        API.setWaypoint(args[0], args[1]);
      
    }

    if (eventName == "nextChek") {

        var textLabel = API.createTextLabel("Нажмите Y для завершения", args[0], 1, 1);

        if (desblip != null)
        {
          
            API.setBlipPosition(desblip, args[0]);
            API.deleteEntity(marker);
        }
        else
        {
            desblip = API.createBlip(args[0]);
            
        }
      
        marker = API.createMarker(1, args[0], new Vector3(), new Vector3(), new Vector3(2, 2, 2), 255, 0, 0, 255);
        API.attachEntity(textLabel, marker, "0", new Vector3(), new Vector3());
      
        API.setWaypoint(args[1], args[2]);
        
    }

    if (eventName == "EndMission") {

        if (desblip != null) {

            
            API.deleteEntity(marker);
        }
    }
});