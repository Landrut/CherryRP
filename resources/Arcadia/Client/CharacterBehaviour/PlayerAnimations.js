API.onKeyDown.connect(function (player, key) {
    if (key.KeyCode == Keys.F1) {
        API.triggerServerEvent("OpenAnimMenu");
    }
});