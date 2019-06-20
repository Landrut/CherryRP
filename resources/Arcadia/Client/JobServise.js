var desblip;
var marker = null;
var counter = 0;

API.onKeyDown.connect(function (sender, e) {
    if (e.KeyCode === Keys.E) {

        API.triggerServerEvent("MissionTrigger");

    }
	if (e.KeyCode === Keys.Q) {

        API.triggerServerEvent("objComplete");

    }
});

API.onServerEventTrigger.connect(function (eventName, args) {
    if (eventName == "") {

        API.setWaypoint(args[0], args[1]);
      
    }


    if (eventName == "nextDes") {



        if (desblip != null) {

            API.setBlipPosition(desblip, args[0]);
            API.deleteEntity(marker);
        }
        else {
            desblip = API.createBlip(args[0]);

        }

        marker = API.createMarker(1, args[0], new Vector3(), new Vector3(), new Vector3(1, 1, 1), 255, 0, 0, 255);
        API.setWaypoint(args[1], args[2]);


    }    

    if (eventName == "NextPointPizza") {

        if (marker != null) {

            API.deleteEntity(marker);
        }

        API.setWaypoint(args[0], args[1]);
        marker = API.createMarker(0, new Vector3(args[0], args[1], args[2]), new Vector3(), new Vector3(), new Vector3(1, 1, 1), 255, 0, 0, 255);

        if (args[3] != 0) {
            counter++;
        }


        if (counter == 9) { API.sendNotificationToPlayer("Вы закончили развозку"); counter = 0; API.triggerServerEvent("SetSallaryPizzaing"); return; }

    } 

    if (eventName == "nextChekFW") {

        if (marker != null) {

            API.deleteEntity(marker);
        }

        API.setWaypoint(args[0], args[1]);
        marker = API.createMarker(0, new Vector3(args[0], args[1], args[2]), new Vector3(), new Vector3(), new Vector3(1, 1, 1), 255, 0, 0, 255);

        if (args[3] != 0) {
            counter++;
        }


        if (counter == 9) { API.sendNotificationToPlayer("Вы закончили развозку"); counter = 0; API.triggerServerEvent("SetSallaryPizzaing"); return; }

    } 


    if (eventName == "DeleteNextPoint") {

        if (marker != null) {

            API.deleteEntity(marker);
            API.removeWaypoint();
            counter = 0;
        }

    } 

});