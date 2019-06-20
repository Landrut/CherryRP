var menu = new Menu();


function openPlayerMenu() {
    menu
        .createMenu("ATM", "ATM")
        .addMenuItem("Ballance", "Show your ballance", true, true, "callServerTrigger", "testServerEvent", 2, false)
        .addCloseButton();
}

function callServerTrigger(args) {
    API.triggerServerEvent(args[0], args[1] /*2*/, args[2] /*false*/);
}

function Menu() {
    var mainMenu = null;

    this.createMenu = function (title, subTitle, isResetKey, isDisableAllControls, posX, posY, anchor) {

        this.destroyMenu();

        title = typeof title !== "undefined" ? title : "Menu";
        subTitle = typeof subTitle !== "undefined" ? subTitle : "";
        isResetKey = typeof isResetKey !== "undefined" ? isResetKey : false;
        anchor = typeof anchor !== "undefined" ? anchor : 6;
        posX = typeof posX !== "undefined" ? posX : 0;
        posY = typeof posY !== "undefined" ? posY : 0;

        mainMenu = API.createMenu(title, subTitle, posX, posY, anchor);
        mainMenu.Visible = true;
        mainMenu.RefreshIndex();

        if (isResetKey === true)
            mainMenu.ResetKey(menuControl.Back);

        if (isDisableAllControls === true)
            disableAllControls = true;

        return this;
    };

    this.addMenuItem = function (title, subTitle, isActivateInvisibleMenu, isActivated, callFunction) {

        title = typeof title !== "undefined" ? title : "Menu";
        subTitle = typeof subTitle !== "undefined" ? subTitle : "";
        isActivateInvisibleMenu = typeof isActivateInvisibleMenu !== "undefined" ? isActivateInvisibleMenu : true;
        isActivated = typeof isActivated !== "undefined" ? isActivated : false;
        callFunction = typeof callFunction !== "undefined" ? callFunction : "";

        var args = [].slice.call(arguments).splice(5);
        var menuItem = API.createMenuItem(title, subTitle);

        if (isActivated === true) {
            menuItem.Activated.connect(function (menu, item) {
                if (isActivateInvisibleMenu === true)
                    mainMenu.Visible = false;

                eval(callFunction)(args);
            });
        }

        mainMenu.AddItem(menuItem);
        return this;
    };

    this.addMenuItemList = function (subTitle, isActivateInvisibleMenu, isActivated, list, callFunction) {

        subTitle = typeof subTitle !== "undefined" ? subTitle : "";
        isActivateInvisibleMenu = typeof isActivateInvisibleMenu !== "undefined" ? isActivateInvisibleMenu : true;
        isActivated = typeof isActivated !== "undefined" ? isActivated : false;
        callFunction = typeof callFunction !== "undefined" ? callFunction : "";

        Object.keys(list).map(function (objectKey, index) {

            var value = list[objectKey];

            if (typeof value === "string") {

                var args = [].slice.call(arguments).splice(5);
                var menuItem = API.createMenuItem(value, subTitle);

                if (isActivated === true) {
                    menuItem.Activated.connect(function (menu, item) {
                        if (isActivateInvisibleMenu === true)
                            mainMenu.Visible = false;

                        eval(callFunction)(args);
                    });
                }

                mainMenu.AddItem(menuItem);
            }
        });
        return this;
    };

    this.addCheckBoxItem = function (title, subTitle, defaultValue, isActivateInvisibleMenu, isActivated, callFunction) {

        title = typeof title !== "undefined" ? title : "Menu";
        subTitle = typeof subTitle !== "undefined" ? subTitle : "";
        defaultValue = typeof defaultValue !== "undefined" ? defaultValue : true;
        isActivateInvisibleMenu = typeof isActivateInvisibleMenu !== "undefined" ? isActivateInvisibleMenu : true;
        isActivated = typeof isActivated !== "undefined" ? isActivated : false;
        callFunction = typeof callFunction !== "undefined" ? callFunction : "";

        var args2 = [].slice.call(arguments).splice(6);
        var checkBoxItem = API.createCheckboxItem(title, subTitle, defaultValue);

        if (isActivated === true) {
            checkBoxItem.CheckboxEvent.connect(function (item, callBack) {
                if (isActivateInvisibleMenu === true)
                    mainMenu.Visible = false;

                var args = args2;
                var args1 = [callBack];
                args = args.concat(args1);

                eval(callFunction)(args);
            });
        }

        mainMenu.AddItem(checkBoxItem);
        return this;
    };

    this.addListItem = function (title, subTitle, list, isActivateInvisibleMenu, isActivated, isListChanged, callChangeFunction, callFunction) {

        title = typeof title !== "undefined" ? title : "Menu";
        subTitle = typeof subTitle !== "undefined" ? subTitle : "";
        list = typeof list !== "undefined" ? list : "undefined";
        isActivateInvisibleMenu = typeof isActivateInvisibleMenu !== "undefined" ? isActivateInvisibleMenu : true;
        isActivated = typeof isActivated !== "undefined" ? isActivated : false;
        isListChanged = typeof isListChanged !== "undefined" ? isListChanged : false;
        callChangeFunction = typeof callChangeFunction !== "undefined" ? callChangeFunction : "";
        callFunction = typeof callFunction !== "undefined" ? callFunction : "";

        var listItem = API.createListItem(title, subTitle, list, 0);
        var args2 = [].slice.call(arguments).splice(8);
        var selectItem = 0;

        if (isListChanged === true) {
            listItem.OnListChanged.connect(function (menu, callBack) {
                selectItem = callBack;
                API.triggerServerEvent(callChangeFunction, selectItem);
            });
        }
        else {
            listItem.OnListChanged.connect(function (menu, callBack) {
                selectItem = callBack;
            });
        }

        if (isActivated === true) {

            listItem.Activated.connect(function (item, callBack) {
                if (isActivateInvisibleMenu === true)
                    mainMenu.Visible = false;
                var args = args2;
                var args1 = (callChangeFunction === 'selectInList') ? [list[selectItem]] : [selectItem];
                args = args.concat(args1);
                eval(callFunction)(args);
                //API.triggerServerEvent(callFunction, args);
            });
        }

        mainMenu.AddItem(listItem);
        return this;
    };

    this.addCloseButton = function (closeTriggerEvent) {
        var destroyMenu = this.destroyMenu;
        var menuItem = API.createMenuItem("~r~Close", "");

        menuItem.Activated.connect(function (menu, item) {

            if (typeof closeTriggerEvent !== "undefined")
                API.triggerServerEvent(closeTriggerEvent);

            mainMenu.Visible = false;
            destroyMenu();
        });

        mainMenu.AddItem(menuItem);
        return true;
    };

    this.callClientFunction = function (functionName /*, args */) {
        var args = [].slice.call(arguments).splice(1);
        eval(functionName)(args);
    };

    this.callServerFunction = function (functionName /*, args */) {
        var args = [].slice.call(arguments).splice(1);
        API.triggerServerEvent(functionName, args);
    };

    this.destroyMenu = function () {
        if (mainMenu !== null)
            mainMenu.Visible = false;

        disableAllControls = false;

        mainMenu = null;

        return true;
    };
}