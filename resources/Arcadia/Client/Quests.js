API.onServerEventTrigger.connect(function (EventName, args) {
    if (EventName == "quest1") {
        API.setWaypoint(558.3443, -1759.917);
    }
    if (EventName == "quest1p") {
        API.removeWaypoint();
    }
});