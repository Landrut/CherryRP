API.onKeyDown.connect(function (player, key) {
    if (key.KeyCode == Keys.E) {

        API.triggerServerEvent("Heal");
    }
});
