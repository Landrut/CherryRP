var creatorMenus = [];

var creatorMainMenu = null;
var creatorParentsMenu = null;
var creatorFeaturesMenu = null;
var creatorAppearanceMenu = null;
var creatorHairMenu = null;
var creatorClothesMenu = null;

var genderItem = null;
var currentGender = 0;

var fathers = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 42, 43, 44];
var mothers = [21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 45];
var fatherNames = ["Бенджамин", "Дэниель", "Джошуа", "Ноа", "Энрдю", "Хуан", "Алекс", "Исаак", "Эван", "Итан", "Винсент", "Анхель", "Диего", "Адриан", "Гэбриель", "Майкл", "Сантьяго", "Кевин", "Луис", "Сэмюель", "Энтони", "Клод", "Нико", "Джон"];
var motherNames = ["Ханна", "Эйбри", "Жасмин", "Джизель", "Амелия", "Изабелла", "Зои", "Эва", "Камила", "Виолетта", "София", "Эвелин", "Николь", "Эшли", "Грэйси", "Брианна", "Натали", "Оливия", "Элизабет", "Шарлотта", "Эмма", "Мисти"];
var fatherItem = null;
var motherItem = null;
var similarityItem = null;
var skinSimilarityItem = null;
var angleItem = null;

var top_model = [
    // male
    [1, 12, 13, 57, 171],
    // female
    [2, 3, 9, 13, 26, 27, 39, 110, 249, 250]
];
var legs_model = [
    // male
    [0, 3, 4, 24, 12],
    // female
    [9, 51, 37, 31, 10]
];
var feet_model = [
    // male
    [1, 9, 21, 28, 48],
    // female
    [0, 1, 50, 13, 10]
];

var featureNames = ["Ширина носа", "Высота носа", "Длинна кончика носа", "Глубина переносицы", "Высота кончика носа", "Поломанность носа", "Высота бровей", "Глубина бровей", "Высота скул", "Ширина скул", "Глубина скул", "Размер глаз", "Толщина губ", "Ширина челюсти", "Форма челюсти", "Высота подбородка", "Глубина подбородка", "Ширина подбородка", "Отступ подбородка", "Шея"];
var creatorFeaturesItems = [];

var appearanceNames = ["Пятна", "Борода", "Брови", "Морщины", "Макияж", "Румянец", "Цвет лица", "Повреждение солнца", "Губная помада", "Родинки и веснушки", "Волосы на груди"];
var creatorAppearanceItems = [];
var creatorAppearanceOpacityItems = [];

var appearanceItemNames = [
    // blemishes
    ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"],
    // facial hair
    ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29"],
    // eyebrows
    ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33"],
    // ageing
    ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"],
    // makeup
    ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16"],
    // blush
    ["0", "1", "2", "3", "4", "5", "6", "7"],
    // complexion
    ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
    // sun damage
    ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"],
    // lipstick
    ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"],
    // freckles
    ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18"],
    // chest hair
    ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17"]
];

var hairIDList = [
    // male
    [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 72, 73],
    // female
    [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 76, 77]
];

var hairNameList = [
    // male
    ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38"],
    // female
    ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40"]
];

var eyeColors = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32"];
var hairItem = null;
var hairColorItem = null;
var hairHighlightItem = null;
var eyebrowColorItem = null;
var beardColorItem = null;
var eyeColorItem = null;
var blushColorItem = null;
var lipstickColorItem = null;
var chestHairColorItem = null;

var creatorCamera = null;
//var secondCamera = null;
var baseAngle = 0.0;

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function resetParentsMenu(clear_idx) {
    clear_idx = clear_idx || false;

    fatherItem.Index = 0;
    motherItem.Index = 0;
    similarityItem.Index = (currentGender == 0) ? 100 : 0;
    skinSimilarityItem.Index = (currentGender == 0) ? 100 : 0;

    updateCharacterParents();
    if (clear_idx) creatorParentsMenu.RefreshIndex();
}

function resetFeaturesMenu(clear_idx) {
    clear_idx = clear_idx || false;

    for (var i = 0; i < featureNames.length; i++) {
        creatorFeaturesItems[i].Index = 100;
        updateCharacterFeature(i);
    }

    if (clear_idx) creatorFeaturesMenu.RefreshIndex();
}

function resetAppearanceMenu(clear_idx) {
    clear_idx = clear_idx || false;

    for (var i = 0; i < appearanceNames.length; i++) {
        creatorAppearanceItems[i].Index = 0;
        creatorAppearanceOpacityItems[i].Index = 100;
        updateCharacterAppearance(i);
    }

    if (clear_idx) creatorAppearanceMenu.RefreshIndex();
}

function resetHairColorsMenu(clear_idx) {
    clear_idx = clear_idx || false;

    hairItem.Index = 0;
    hairColorItem.Index = 0;
    hairHighlightItem.Index = 0;
    eyebrowColorItem.Index = 0;
    beardColorItem.Index = 0;
    eyeColorItem.Index = 0;
    blushColorItem.Index = 0;
    lipstickColorItem.Index = 0;
    chestHairColorItem.Index = 0;
    updateCharacterHairAndColors();

    if (clear_idx) creatorHairMenu.RefreshIndex();
}

function updateCharacterParents() {
    API.setPlayerHeadBlendData(
        API.getLocalPlayer(),

        mothers[motherItem.Index],
        fathers[fatherItem.Index],
        0,

        mothers[motherItem.Index],
        fathers[fatherItem.Index],
        0,

        similarityItem.Index * 0.01,
        skinSimilarityItem.Index * 0.01,
        0.0,

        false
    );
}

function updateCharacterFeature(index) {
    API.setPlayerFaceFeature(API.getLocalPlayer(), index, parseFloat(creatorFeaturesItems[index].IndexToItem(creatorFeaturesItems[index].Index)));
}

function updateCharacterAppearance(index) {
    var overlay_id = ((creatorAppearanceItems[index].Index == 0) ? 255 : creatorAppearanceItems[index].Index - 1);
    API.setPlayerHeadOverlay(API.getLocalPlayer(), index, overlay_id, creatorAppearanceOpacityItems[index].Index * 0.01);
}

function updateCharacterHairAndColors(idx) {
    // hair
    API.setPlayerClothes(API.getLocalPlayer(), 2, hairIDList[currentGender][hairItem.Index], 0);
    API.setPlayerHairColor(API.getLocalPlayer(), hairColorItem.Index, hairHighlightItem.Index);

    // appearance colors
    API.setPlayerHeadOverlayColor(API.getLocalPlayer(), 2, 1, eyebrowColorItem.Index, creatorAppearanceOpacityItems[2].Index * 0.01);
    API.setPlayerHeadOverlayColor(API.getLocalPlayer(), 1, 1, beardColorItem.Index, creatorAppearanceOpacityItems[1].Index * 0.01);
    API.setPlayerHeadOverlayColor(API.getLocalPlayer(), 5, 2, blushColorItem.Index, creatorAppearanceOpacityItems[5].Index * 0.01);
    API.setPlayerHeadOverlayColor(API.getLocalPlayer(), 8, 2, lipstickColorItem.Index, creatorAppearanceOpacityItems[8].Index * 0.01);
    API.setPlayerHeadOverlayColor(API.getLocalPlayer(), 10, 1, chestHairColorItem.Index, creatorAppearanceOpacityItems[10].Index * 0.01);

    // eye color
    API.setPlayerEyeColor(API.getLocalPlayer(), eyeColorItem.Index);
}

function fillHairMenu() {
    // HAIR & COLORS - Hair
    var hair_list = new List(String);
    for (var i = 0; i < hairIDList[currentGender].length; i++) hair_list.Add(hairNameList[currentGender][i]);

    hairItem = API.createListItem("Причёска", "Причёска вашего персонажа.", hair_list, 0);
    creatorHairMenu.AddItem(hairItem);

    // HAIR & COLORS - Hair Color
    var hair_color_list = new List(String);
    for (var i = 0; i < API.getNumHairColors(); i++) hair_color_list.Add(i.toString());

    hairColorItem = API.createListItem("Цвет волос", "Цвет волос вашего персонажа.", hair_color_list, 0);
    creatorHairMenu.AddItem(hairColorItem);

    hairHighlightItem = API.createListItem("Брондирование", "Брондирование волос вашего персонажа.", hair_color_list, 0);
    creatorHairMenu.AddItem(hairHighlightItem);

    // HAIR & COLORS - Eyebrow Color
    eyebrowColorItem = API.createListItem("Цвет бровей", "Цвет бровей вашего персонажа.", hair_color_list, 0);
    creatorHairMenu.AddItem(eyebrowColorItem);

    // HAIR & COLORS - Facial Hair Color
    beardColorItem = API.createListItem("Цвет бороды", "Цвет бороды вашего персонажа.", hair_color_list, 0);
    creatorHairMenu.AddItem(beardColorItem);

    // HAIR & COLORS - Eyes
    var eye_list = new List(String);
    for (var i = 0; i < 32; i++) eye_list.Add(eyeColors[i]);

    eyeColorItem = API.createListItem("Зрачки", "Зрачки вашего персонажа.", eye_list, 0);
    creatorHairMenu.AddItem(eyeColorItem);

    // HAIR & COLORS - Blush
    var blush_color_list = new List(String);
    for (var i = 0; i < 27; i++) blush_color_list.Add(i.toString());

    blushColorItem = API.createListItem("Цвет румянца", "Цвет румянца вашего персонажа.", blush_color_list, 0);
    creatorHairMenu.AddItem(blushColorItem);

    // HAIR & COLORS - Lipstick
    var lipstick_color_list = new List(String);
    for (var i = 0; i < 32; i++) lipstick_color_list.Add(i.toString());

    lipstickColorItem = API.createListItem("Цвет помады", "Цвет помады вашего персонажа.", blush_color_list, 0);
    creatorHairMenu.AddItem(lipstickColorItem);

    // HAIR & COLORS - Chest Hair
    chestHairColorItem = API.createListItem("Цвет волос на груди", "Цвет волос на груди.", hair_color_list, 0);
    creatorHairMenu.AddItem(chestHairColorItem);

    // HAIR & COLORS - Extra
    var extra_item = API.createMenuItem("Случайно", "~r~Установить случайные причёску и цвет.");
    creatorHairMenu.AddItem(extra_item);

    extra_item = API.createMenuItem("~r~Сбросить", "~r~Сбросить настройки волос и цвета.");
    creatorHairMenu.AddItem(extra_item);
}

API.onResourceStart.connect(function () {

    creatorMainMenu = API.createMenu("Редактор", " ", 0, 0, 6);
    creatorMainMenu.ResetKey(menuControl.Back);
    creatorMenus.push(creatorMainMenu);

    // GENDER
    var gender_list = new List(String);
    gender_list.Add("Мужской");
    gender_list.Add("Женский");

    genderItem = API.createListItem("Пол", "~r~Изменение пола приведёт к сбросу настроек вашего персонажа.", gender_list, 0);
    creatorMainMenu.AddItem(genderItem);

    genderItem.OnListChanged.connect(function (item, new_index) {
        currentGender = new_index;

        API.triggerServerEvent("SetGender", new_index);

        API.setEntityRotation(API.getLocalPlayer(), new Vector3(0.0, 0.0, baseAngle));
        API.callNative("CLEAR_PED_TASKS_IMMEDIATELY", API.getLocalPlayer());

        angleItem.Index = 36;

        resetParentsMenu(true);
        resetFeaturesMenu(true);
        resetAppearanceMenu(true);

        creatorHairMenu.Clear();
        fillHairMenu();
        creatorHairMenu.RefreshIndex();
    });

    // PARENTS
    creatorParentsMenu = API.createMenu("Родители", "Родители вашего персонажа.", 0, 0, 6);
    creatorMenus.push(creatorParentsMenu);

    // PARENTS - Father
    var fathers_list = new List(String);
    for (var i = 0; i < fatherNames.length; i++) fathers_list.Add(fatherNames[i]);

    fatherItem = API.createListItem("Отец", "Отец вашего персонажа.", fathers_list, 0);
    creatorParentsMenu.AddItem(fatherItem);

    // PARENTS - Mother
    var mothers_list = new List(String);
    for (var i = 0; i < motherNames.length; i++) mothers_list.Add(motherNames[i]);

    motherItem = API.createListItem("Мать", "Мать вашего персонажа.", mothers_list, 0);
    creatorParentsMenu.AddItem(motherItem);

    // PARENTS - Similarity
    var similarity_list = new List(String);
    for (var i = 0; i <= 100; i++) similarity_list.Add(i + "%");

    similarityItem = API.createListItem("Схожесть", "Схожесть на родителя. (ниже = на мать, выше = на отца)", similarity_list, 0);
    skinSimilarityItem = API.createListItem("Цвет кожи", "Баланс цвета кожи. (ниже = матери, выше = отца)", similarity_list, 0);
    creatorParentsMenu.AddItem(similarityItem);
    creatorParentsMenu.AddItem(skinSimilarityItem);

    // PARENTS - Extra
    var extra_item = API.createMenuItem("Случайно", "~r~Установить случайных родителей.");
    creatorParentsMenu.AddItem(extra_item);

    extra_item = API.createMenuItem("~r~Сбросить", "~r~Сбросить выбор родителей.");
    creatorParentsMenu.AddItem(extra_item);

    creatorParentsMenu.OnListChange.connect(function (menu, item, index) {
        updateCharacterParents();
    });

    creatorParentsMenu.OnItemSelect.connect(function (menu, item, index) {
        switch (item.Text) {
            case "Случайно":
                fatherItem.Index = getRandomInt(0, fathers.length - 1);
                motherItem.Index = getRandomInt(0, mothers.length - 1);
                similarityItem.Index = getRandomInt(0, 100);
                skinSimilarityItem.Index = getRandomInt(0, 100);

                updateCharacterParents();
                break;

            case "~r~Сбросить":
                resetParentsMenu();
                break;
        }
    });

    // FACIAL FEATURES
    creatorFeaturesMenu = API.createMenu("Черты лица", "Черты лица вашего персонажа.", 0, 0, 6);
    creatorMenus.push(creatorFeaturesMenu);

    var feature_size_list = new List(String);
    for (var i = -1.0; i <= 1.01; i += 0.01) feature_size_list.Add(i.toFixed(2));

    var temp_feature_item = null;
    for (var i = 0; i < featureNames.length; i++) {
        temp_feature_item = API.createListItem(featureNames[i], "", feature_size_list, 100);
        creatorFeaturesMenu.AddItem(temp_feature_item);

        creatorFeaturesItems.push(temp_feature_item);
    }

    // FACIAL FEATURES - Extra
    extra_item = API.createMenuItem("Случайно", "~r~Случайные черты лица персонажа.");
    creatorFeaturesMenu.AddItem(extra_item);

    extra_item = API.createMenuItem("~r~Сбросить", "~r~Сбросить черты лица персонажа.");
    creatorFeaturesMenu.AddItem(extra_item);

    creatorFeaturesMenu.OnListChange.connect(function (menu, item, index) {
        updateCharacterFeature(menu.CurrentSelection);
    });

    creatorFeaturesMenu.OnItemSelect.connect(function (menu, item, index) {
        switch (item.Text) {
            case "Случайно":
                for (var i = 0; i < featureNames.length; i++) {
                    creatorFeaturesItems[i].Index = getRandomInt(0, 199);
                    updateCharacterFeature(i);
                }
                break;

            case "~r~Сбросить":
                resetFeaturesMenu();
                break;
        }
    });

    // APPEARANCE
    creatorAppearanceMenu = API.createMenu("Внешность", "Внешность вашего персонажа.", 0, 0, 6);
    creatorMenus.push(creatorAppearanceMenu);

    var opacity_list = new List(String);
    for (var i = 0; i <= 100; i++) opacity_list.Add(i.toString() + "%");

    // APPEARANCE - Menu Items
    for (var i = 0; i < appearanceNames.length; i++) {
        // generate item list
        var items_list = new List(String);
        for (var j = 0; j <= API.getNumHeadOverlayValues(i); j++) {
            if (appearanceItemNames[i][j] === undefined) {
                items_list.Add(j.toString());
            } else {
                items_list.Add(appearanceItemNames[i][j]);
            }
        }

        // generate item
        var appearance_item = API.createListItem(appearanceNames[i], "", items_list, 0);
        creatorAppearanceMenu.AddItem(appearance_item);
        creatorAppearanceItems.push(appearance_item);

        // generate opacity item
        var appearance_opacity_item = API.createListItem("[Насыщенность] " + appearanceNames[i], "", opacity_list, 100);
        creatorAppearanceMenu.AddItem(appearance_opacity_item);
        creatorAppearanceOpacityItems.push(appearance_opacity_item);
    }

    // APPEARANCE - Extra
    extra_item = API.createMenuItem("Случайно", "~r~Случайный набор внешности.");
    creatorAppearanceMenu.AddItem(extra_item);

    extra_item = API.createMenuItem("~r~Сбросить", "~r~Сбросить вашу внешность.");
    creatorAppearanceMenu.AddItem(extra_item);

    creatorAppearanceMenu.OnListChange.connect(function (menu, item, index) {
        var overlayID = menu.CurrentSelection;

        if (menu.CurrentSelection % 2 == 0) {
            // feature
            overlayID = menu.CurrentSelection / 2;
            updateCharacterAppearance(overlayID);
        } else {
            // opacity
            var tempOverlayID = 0;

            switch (overlayID) {
                case 1:
                    {
                        // blemishes
                        tempOverlayID = 0;
                        break;
                    }

                case 3:
                    {
                        // facial hair
                        tempOverlayID = 1;
                        break;
                    }

                case 5:
                    {
                        // eyebrows
                        tempOverlayID = 2;
                        break;
                    }

                case 7:
                    {
                        // ageing
                        tempOverlayID = 3;
                        break;
                    }

                case 9:
                    {
                        // makeup
                        tempOverlayID = 4;
                        break;
                    }

                case 11:
                    {
                        // blush
                        tempOverlayID = 5;
                        break;
                    }

                case 13:
                    {
                        // complexion
                        tempOverlayID = 6;
                        break;
                    }

                case 15:
                    {
                        // sun damage
                        tempOverlayID = 7;
                        break;
                    }

                case 17:
                    {
                        // lipstick
                        tempOverlayID = 8;
                        break;
                    }

                case 19:
                    {
                        // freckles
                        tempOverlayID = 9;
                        break;
                    }

                case 21:
                    {
                        // chest hair
                        tempOverlayID = 10;
                    }
            }

            updateCharacterAppearance(tempOverlayID);
        }
    });

    creatorAppearanceMenu.OnItemSelect.connect(function (menu, item, index) {
        switch (item.Text) {
            case "Случайно":
                for (var i = 0; i < appearanceNames.length; i++) {
                    creatorAppearanceItems[i].Index = getRandomInt(0, API.getNumHeadOverlayValues(i) - 1);
                    creatorAppearanceOpacityItems[i].Index = getRandomInt(0, 100);
                    updateCharacterAppearance(i);
                }
                break;

            case "~r~Сбросить":
                resetAppearanceMenu();
                break;
        }
    });

    // HAIR & COLORS
    creatorHairMenu = API.createMenu("Волосы и цвета", "Волосы и цвета волос вашего персонажа.", 0, 0, 6);
    creatorMenus.push(creatorHairMenu);

    fillHairMenu();
    // CLOTHES
    creatorClothesMenu = API.createMenu("Одежда", "Одежда вашего персонажа.", 0, 0, 6);
    creatorMenus.push(creatorClothesMenu);

    // CLOTHES - TOP
    var clothes_top_list = new List(String);
    for (var i = 0; i < top_model[currentGender].length; i++) clothes_top_list.Add(i.toString());
    clothes_top_Item = API.createListItem("Верх", "", clothes_top_list, 0);
    creatorClothesMenu.AddItem(clothes_top_Item);

    // CLOTHES - TOP - COLOR
    var clothes_top_color_list = new List(String);
    for (var i = 0; i < 15; i++) clothes_top_color_list.Add(i.toString());
    clothes_top_color_Item = API.createListItem("Цвет верха", "", clothes_top_color_list, 0);
    creatorClothesMenu.AddItem(clothes_top_color_Item);

    // CLOTHES - LEGS
    var clothes_legs_list = new List(String);
    for (var i = 0; i < legs_model[currentGender].length; i++) clothes_legs_list.Add(i.toString());
    clothes_legs_Item = API.createListItem("Ноги", "", clothes_legs_list, 0);
    creatorClothesMenu.AddItem(clothes_legs_Item);

    // CLOTHES - LEGS - COLOR
    var clothes_legs_color_list = new List(String);
    for (var i = 0; i < 15; i++) clothes_legs_color_list.Add(i.toString());
    clothes_legs_color_Item = API.createListItem("Цвет", "", clothes_legs_color_list, 0);
    creatorClothesMenu.AddItem(clothes_legs_color_Item);

    // CLOTHES - FEET
    var clothes_feet_list = new List(String);
    for (var i = 0; i < feet_model[currentGender].length; i++) clothes_feet_list.Add(i.toString());
    clothes_feet_Item = API.createListItem("Обувь", "", clothes_feet_list, 0);
    creatorClothesMenu.AddItem(clothes_feet_Item);

    // CLOTHES - FEET - COLOR
    var clothes_feet_color_list = new List(String);
    for (var i = 0; i < 15; i++) clothes_feet_color_list.Add(i.toString());
    clothes_feet_color_Item = API.createListItem("Цвет обуви", "", clothes_feet_color_list, 0);
    creatorClothesMenu.AddItem(clothes_feet_color_Item);

    creatorClothesMenu.OnIndexChange.connect(function (sender, newIndex) {
        if (newIndex <= 1) {
            creatorCamera = API.createCamera(new Vector3(402.8664, -997.5515, -98.5), new Vector3(0, 0, 0));
            API.pointCameraAtPosition(creatorCamera, new Vector3(402.8664, -996.4108, -98.5));

            API.setActiveCamera(creatorCamera);
        }
        else {
            creatorCamera = API.createCamera(new Vector3(402.8664, -997.5515 - 0.5, -98.5 - 1), new Vector3(0, 0, 0));
            API.pointCameraAtPosition(creatorCamera, new Vector3(402.8664, -996.4108, -98.5 - 1));

            API.setActiveCamera(creatorCamera);
        }
    });

    creatorClothesMenu.OnListChange.connect(function (menu, item, index) {
        switch (menu.CurrentSelection) {
            // TOP
            case 0:
                Clothes_Top_Fix();
                API.setPlayerClothes(API.getLocalPlayer(), 11, top_model[currentGender][clothes_top_Item.Index], clothes_top_color_Item.Index);
                break;
            case 1:
                API.setPlayerClothes(API.getLocalPlayer(), 11, top_model[currentGender][clothes_top_Item.Index], clothes_top_color_Item.Index);
                break;
            // LEGS
            case 2:
                API.setPlayerClothes(API.getLocalPlayer(), 4, legs_model[currentGender][clothes_legs_Item.Index], clothes_legs_color_Item.Index);
                break;
            case 3:
                API.setPlayerClothes(API.getLocalPlayer(), 4, legs_model[currentGender][clothes_legs_Item.Index], clothes_legs_color_Item.Index);
                break;
            // FEET
            case 4:
                API.setPlayerClothes(API.getLocalPlayer(), 6, feet_model[currentGender][clothes_feet_Item.Index], clothes_feet_color_Item.Index);
                break;
            case 5:
                API.setPlayerClothes(API.getLocalPlayer(), 6, feet_model[currentGender][clothes_feet_Item.Index], clothes_feet_color_Item.Index);
                break;
        }
    });

    creatorClothesMenu.OnMenuClose.connect(function (sender) {
        creatorCamera = API.createCamera(new Vector3(402.8664, -997.5515, -98.5), new Vector3(0, 0, 0));
        API.pointCameraAtPosition(creatorCamera, new Vector3(402.8664, -996.4108, -98.5));

        API.setActiveCamera(creatorCamera);
    });

    // ANGLE
    var angle_list = new List(String);
    for (var i = -180.0; i <= 180.0; i += 5) angle_list.Add(i.toFixed(1));

    angleItem = API.createListItem("Поворот", "", angle_list, 36);
    creatorMainMenu.AddItem(angleItem);

    angleItem.OnListChanged.connect(function (item, new_index) {
        API.setEntityRotation(API.getLocalPlayer(), new Vector3(0.0, 0.0, baseAngle + parseFloat(item.IndexToItem(new_index))));
        API.callNative("CLEAR_PED_TASKS_IMMEDIATELY", API.getLocalPlayer());
    });

    // SAVE & CANCEL BUTTONS
    var save_button = API.createMenuItem("~g~Сохранить", "Сохранить все изменения и выйти из редактора.");
    creatorMainMenu.AddItem(save_button);

    save_button.Activated.connect(function (menu, item) {
        var feature_values = [];
        for (var i = 0; i < featureNames.length; i++) feature_values.push(parseFloat(creatorFeaturesItems[i].IndexToItem(creatorFeaturesItems[i].Index)));

        var appearance_values = [];
        for (var i = 0; i < appearanceNames.length; i++) appearance_values.push({ Value: ((creatorAppearanceItems[i].Index == 0) ? 255 : creatorAppearanceItems[i].Index - 1), Opacity: creatorAppearanceOpacityItems[i].Index * 0.01 });

        var hair_or_colors = [];
        hair_or_colors.push(hairIDList[currentGender][hairItem.Index]);
        hair_or_colors.push(hairColorItem.Index);
        hair_or_colors.push(hairHighlightItem.Index);
        hair_or_colors.push(eyebrowColorItem.Index);
        hair_or_colors.push(beardColorItem.Index);
        hair_or_colors.push(eyeColorItem.Index);
        hair_or_colors.push(blushColorItem.Index);
        hair_or_colors.push(lipstickColorItem.Index);
        hair_or_colors.push(chestHairColorItem.Index);

        API.triggerServerEvent("SaveCharacter",
            currentGender,
            fathers[fatherItem.Index],
            mothers[motherItem.Index],
            similarityItem.Index * 0.01,
            skinSimilarityItem.Index * 0.01,
            JSON.stringify(feature_values),
            JSON.stringify(appearance_values),
            JSON.stringify(hair_or_colors),
            top_model[currentGender][clothes_top_Item.Index],
            clothes_top_color_Item.Index,
            legs_model[currentGender][clothes_legs_Item.Index],
            clothes_legs_color_Item.Index,
            feet_model[currentGender][clothes_feet_Item.Index],
            clothes_feet_color_Item.Index);
    });

    var cancel_button = API.createMenuItem("~r~Отмена", "Отменить все изменения и выйти из редактора.");
    creatorMainMenu.AddItem(cancel_button);

    cancel_button.Activated.connect(function (menu, item) {
        API.triggerServerEvent("LeaveCreator");
    });

    creatorHairMenu.OnListChange.connect(function (menu, item, index) {
        if (menu.CurrentSelection > 0) {
            switch (menu.CurrentSelection) {
                case 1:
                    API.setPlayerHairColor(API.getLocalPlayer(), index, hairHighlightItem.Index);
                    break;

                case 2:
                    API.setPlayerHairColor(API.getLocalPlayer(), hairColorItem.Index, index);
                    break;

                case 3:
                    API.setPlayerHeadOverlayColor(API.getLocalPlayer(), 2, 1, index, creatorAppearanceOpacityItems[2].Index * 0.01);
                    break;

                case 4:
                    API.setPlayerHeadOverlayColor(API.getLocalPlayer(), 1, 1, index, creatorAppearanceOpacityItems[1].Index * 0.01);
                    break;

                case 5:
                    API.setPlayerEyeColor(API.getLocalPlayer(), index);
                    break;

                case 6:
                    API.setPlayerHeadOverlayColor(API.getLocalPlayer(), 5, 2, index, creatorAppearanceOpacityItems[5].Index * 0.01);
                    break;

                case 7:
                    API.setPlayerHeadOverlayColor(API.getLocalPlayer(), 8, 2, index, creatorAppearanceOpacityItems[8].Index * 0.01);
                    break;

                case 8:
                    API.setPlayerHeadOverlayColor(API.getLocalPlayer(), 10, 1, index, creatorAppearanceOpacityItems[10].Index * 0.01);
                    break;
            }
        } else {
            API.setPlayerClothes(API.getLocalPlayer(), 2, hairIDList[currentGender][index], 0);
        }
    });

    creatorHairMenu.OnItemSelect.connect(function (menu, item, index) {
        switch (item.Text) {
            case "Случайно":
                var hair_colors = API.getNumHairColors() - 1;

                hairItem.Index = getRandomInt(0, hairIDList[currentGender].length);
                hairColorItem.Index = getRandomInt(0, hair_colors);
                hairHighlightItem.Index = getRandomInt(0, hair_colors);
                eyebrowColorItem.Index = getRandomInt(0, hair_colors);
                beardColorItem.Index = getRandomInt(0, hair_colors);
                eyeColorItem.Index = getRandomInt(0, 31);
                blushColorItem.Index = getRandomInt(0, 26);
                lipstickColorItem.Index = getRandomInt(0, 31);
                chestHairColorItem.Index = getRandomInt(0, hair_colors);

                updateCharacterHairAndColors();
                break;

            case "~r~Сбросить":
                resetHairColorsMenu();
                break;
        }
    });

    for (var i = 0; i < creatorMenus.length; i++) creatorMenus[i].RefreshIndex();
});

API.onEntityStreamIn.connect(function (ent, entType) {
    if (entType == 6 && (API.getEntityModel(ent) == 1885233650 || API.getEntityModel(ent) == -1667301416) && API.hasEntitySyncedData(ent, "CustomCharacter")) {
        var data = JSON.parse(API.getEntitySyncedData(ent, "CustomCharacter"));
        API.setPlayerHeadBlendData(
            ent,

            data.Parents.Mother,
            data.Parents.Father,
            0,

            data.Parents.Mother,
            data.Parents.Father,
            0,

            data.Parents.Similarity,
            data.Parents.SkinSimilarity,
            0.0,

            false
        );

        for (var i = 0; i < data.Features.length; i++) API.setPlayerFaceFeature(ent, i, data.Features[i]);
        for (var i = 0; i < data.Appearance.length; i++) API.setPlayerHeadOverlay(ent, i, data.Appearance[i].Value, data.Appearance[i].Opacity);

        API.setPlayerHairColor(ent, data.Hair.Color, data.Hair.HighlightColor);

        API.setPlayerHeadOverlayColor(ent, 1, 1, data.BeardColor, data.Appearance[1].Opacity);
        API.setPlayerHeadOverlayColor(ent, 2, 1, data.EyebrowColor, data.Appearance[2].Opacity);
        API.setPlayerHeadOverlayColor(ent, 5, 2, data.BlushColor, data.Appearance[5].Opacity);
        API.setPlayerHeadOverlayColor(ent, 8, 2, data.LipstickColor, data.Appearance[8].Opacity);
        API.setPlayerHeadOverlayColor(ent, 10, 1, data.ChestHairColor, data.Appearance[10].Opacity);

        API.setPlayerEyeColor(ent, data.EyeColor);
    }
});

API.onServerEventTrigger.connect(function (event, args) {
    switch (event) {
        case "CreatorCamera":
            if (creatorCamera == null) {
                creatorCamera = API.createCamera(args[0], new Vector3(0, 0, 0));
                API.pointCameraAtPosition(creatorCamera, args[1]);

                API.setActiveCamera(creatorCamera);
                API.setCanOpenChat(false);
                API.setHudVisible(false);
                API.setChatVisible(false);

                baseAngle = args[2];
                creatorMainMenu.Visible = true;
            }
            break;

        case "ctest":
            creatorCamera = API.createCamera(new Vector3(402.8664, -997.5515 - 0.5, -98.5 - 1), new Vector3(0, 0, 0));
            API.pointCameraAtPosition(creatorCamera, new Vector3(402.8664, -996.4108, -98.5 - 1));

            API.setActiveCamera(creatorCamera);

            baseAngle = args[2];

            API.sendChatMessage("Camera X: " + API.getCameraPosition(creatorCamera).X + " Y: " + API.getCameraPosition(creatorCamera).Y + " Z: " + API.getCameraPosition(creatorCamera).Z);
            break;

        case "DestroyCamera":
            API.setActiveCamera(null);
            API.setCanOpenChat(true);
            API.setHudVisible(true);
            API.setChatVisible(true);

            for (var i = 0; i < creatorMenus.length; i++) creatorMenus[i].Visible = false;
            creatorCamera = null;
            break;

        case "UpdateCreator":
            var data = JSON.parse(args[0]);

            currentGender = data.Gender;
            genderItem.Index = data.Gender;

            creatorHairMenu.Clear();
            fillHairMenu();

            fatherItem.Index = fathers.indexOf(data.Parents.Father);
            motherItem.Index = mothers.indexOf(data.Parents.Mother);
            similarityItem.Index = parseInt(data.Parents.Similarity * 100);
            skinSimilarityItem.Index = parseInt(data.Parents.SkinSimilarity * 100);

            // probably sucks lul
            var float_values = [];
            for (var i = -1.0; i <= 1.01; i += 0.01) float_values.push(i.toFixed(2));
            for (var i = 0; i < data.Features.length; i++) creatorFeaturesItems[i].Index = float_values.indexOf(data.Features[i].toFixed(2));

            float_values = [];
            for (var i = 0; i <= 100; i++) float_values.push((i * 0.01).toFixed(2));
            for (var i = 0; i < data.Appearance.length; i++) {
                creatorAppearanceItems[i].Index = (data.Appearance[i].Value == 255) ? 0 : data.Appearance[i].Value + 1;
                creatorAppearanceOpacityItems[i].Index = float_values.indexOf(data.Appearance[i].Opacity.toFixed(2));
            }

            hairItem.Index = hairIDList[currentGender].indexOf(data.Hair.Hair);
            hairColorItem.Index = data.Hair.Color;
            hairHighlightItem.Index = data.Hair.HighlightColor;
            eyebrowColorItem.Index = data.EyebrowColor;
            beardColorItem.Index = data.BeardColor;
            eyeColorItem.Index = data.EyeColor;
            blushColorItem.Index = data.BlushColor;
            lipstickColorItem.Index = data.LipstickColor;
            chestHairColorItem.Index = data.ChestHairColor;

            clothes_top_Item.Index = data.Clothes.Top;
            clothes_top_color_Item.Index = data.Clothes.Top_Color;
            clothes_legs_Item.Index = data.Clothes.Legs;
            clothes_legs_color_Item.Index = data.Clothes.Legs_Color;
            clothes_feet_Item.Index = data.Clothes.Feet;
            clothes_feet_Item.Index = data.Clothes.Feet_Color;
            break;
    }
});

API.onResourceStop.connect(function () {
    API.setActiveCamera(null);
    API.setCanOpenChat(true);
    API.setHudVisible(true);
    API.setChatVisible(true);

    creatorCamera = null;
});

API.onUpdate.connect(function () {
    if (creatorCamera != null) API.disableAllControlsThisFrame();
});

/*
var top_model = [
	// male
	[1,12,13,57,171],
	// female
	[2,3,5,16,30]
];
*/
function Clothes_Top_Fix() {
    switch (currentGender) {
        case 0: {
            if (top_model[currentGender][clothes_top_Item.Index] == 1) API.setPlayerClothes(API.getLocalPlayer(), 3, 0, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 12) API.setPlayerClothes(API.getLocalPlayer(), 3, 12, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 13) API.setPlayerClothes(API.getLocalPlayer(), 3, 11, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 57) API.setPlayerClothes(API.getLocalPlayer(), 3, 12, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 171) API.setPlayerClothes(API.getLocalPlayer(), 3, 12, 0);
        }
        case 1: {
            if (top_model[currentGender][clothes_top_Item.Index] == 2) API.setPlayerClothes(API.getLocalPlayer(), 3, 2, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 3) API.setPlayerClothes(API.getLocalPlayer(), 3, 3, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 9) API.setPlayerClothes(API.getLocalPlayer(), 3, 9, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 13) API.setPlayerClothes(API.getLocalPlayer(), 3, 15, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 26) API.setPlayerClothes(API.getLocalPlayer(), 3, 21, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 27) API.setPlayerClothes(API.getLocalPlayer(), 3, 14, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 39) API.setPlayerClothes(API.getLocalPlayer(), 3, 1, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 110) API.setPlayerClothes(API.getLocalPlayer(), 3, 3, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 249) API.setPlayerClothes(API.getLocalPlayer(), 3, 14, 0);
            else if (top_model[currentGender][clothes_top_Item.Index] == 250) API.setPlayerClothes(API.getLocalPlayer(), 3, 14, 0);
        }
    }
}