API.onKeyDown.connect(function (player, key) {
    if (key.KeyCode == Keys.R) {

        API.triggerServerEvent("arenda");
    }
});