var resMR = API.getScreenResolutionMaintainRatio();
var moneyText = null;
var moneyNegative = false;

var changeText = null;
var changeNegative = false;
var changeTime = null;

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

API.onServerEventTrigger.connect(function (name, args) {
    if (name == "UpdateMoneyHUD") {
        var money = parseInt(args[0]); // disgusting hack for long support

        if (money < 0) {
            money = Math.abs(money);

            moneyNegative = true;
            moneyText = "~#4d0909~-~#094d09~$~w~" + numberWithCommas(money);
        } else {
            moneyNegative = false;
            moneyText = "~#094d09~$~w~" + numberWithCommas(money);
        }

        // money difference if received
        if (args.Length > 1) {
            var diff = parseInt(args[1]); // disgusting hack for long support

            if (diff < 0) {
                diff = Math.abs(diff);

                changeNegative = true;
                changeText = "~#4d0909~-~#094d09~$~w~" + numberWithCommas(diff);
            } else {
                changeNegative = false;
                changeText = "~#094d09~+$~w~" + numberWithCommas(diff);
            }

            changeTime = API.getGlobalTime();
        }
    }
});

API.onUpdate.connect(function (sender, args) {
    if (!API.getHudVisible()) return;

    // money text
    if (moneyText == null) return;
    if (moneyNegative) {
        API.drawText(moneyText, resMR.Width - 19, 50, 0.8, 140, 0, 0, 255, 4, 2, false, true, 0);
    } else {
        API.drawText(moneyText, resMR.Width - 19, 50, 0.8, 0, 120, 0, 255, 4, 2, false, true, 0);
    }

    // change text
    if (changeText == null || changeTime == null) return;
    if (API.getGlobalTime() - changeTime <= 3500) {
        if (changeNegative) {
            API.drawText(changeText, resMR.Width - 19, 90, 0.8, 140, 0, 0, 255, 4, 2, false, true, 0);
        } else {
            API.drawText(changeText, resMR.Width - 19, 90, 0.8, 0, 120, 0, 255, 4, 2, false, true, 0);
        }
    }
});