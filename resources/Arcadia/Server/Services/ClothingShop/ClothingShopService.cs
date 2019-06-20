using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//
//
using CherryMPServer;
using CherryMPServer.Constant;
using CherryMPShared;
//;
using MenuManagement;
using Newtonsoft.Json;

using MySQL;
using PlayerFunctions;
using CustomSkin;
using System.Collections.Generic;
using System;
using System.IO;

namespace Arcadia.Server.Services.ClothingShop
{
    public class ClothingShopService : Script
    {
        public ClothingShopService()
        {
            API.onResourceStart += onResourceStart;
        }

        public readonly Vector3 ClothesShopBlipPos = new Vector3(77.20451f, -1393.163f, 29.37614f);
        public readonly Vector3 ClothesShopBuyMarkerPos = new Vector3(72.00786f, -1399.171f, 29.37614f);
        public readonly Vector3 ClothesShopBuyColshapePos = new Vector3(72.00786f, -1399.171f, 29.37614f);

        public readonly Vector3 ClothesShop2BuyMarkerPos = new Vector3(-163.637726f, -311.09314f, 39.733284f);
        public readonly Vector3 ClothesShop2BuyColshapePos = new Vector3(-163.637726f, -311.09314f, 39.733284f);

        public readonly Vector3 ClothesShop3BuyMarkerPos = new Vector3(-3175.36572f, 1041.8479f, 20.8632183f);
        public readonly Vector3 ClothesShop3BuyColshapePos = new Vector3(-3175.36572f, 1041.8479f, 20.8632183f);

        public readonly Vector3 ClothesShop4BuyMarkerPos = new Vector3(-715.8824f, -147.729553f, 37.41514f);
        public readonly Vector3 ClothesShop4BuyColshapePos = new Vector3(-715.8824f, -147.729553f, 37.41514f);

        public readonly Vector3 ClothesShop5BuyMarkerPos = new Vector3(121.113159f, -225.83815f, 54.5578346f);
        public readonly Vector3 ClothesShop5BuyColshapePos = new Vector3(121.113159f, -225.83815f, 54.5578346f);

        public Blip ClothesShopBlip;

        public static string SAVE_DIRECTORY = "Server/Data/CustomCharacters";
        public static Dictionary<NetHandle, PlayerCustomization> PlayerClothesData = new Dictionary<NetHandle, PlayerCustomization>();

        public ColShape ClothesShopBuyColshape;
        public ColShape ClothesShop2BuyColshape;
        public ColShape ClothesShop3BuyColshape;
        public ColShape ClothesShop4BuyColshape;
        public ColShape ClothesShop5BuyColshape;

        private MenuManager MenuManager = new MenuManager();
        private Menu savedMenu = null;

        private void ClothesShop_Female_Menu_Builder(Client client)
        {

            Menu menu = savedMenu;

            if (menu == null)
            {
                menu = new Menu("ClothesShop", "Магазин одежды", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, false)
                {
                    BannerColor = new Color(200, 0, 0, 90),
                    Callback = ClothesShop_Female_MenuManager
                };

                //Верхняя одежда
                // 0
                List<string> ftop_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077", "078", "079", "080",
                    "081", "082", "083", "084", "085", "086", "087", "088", "089", "090",
                    "091", "092", "093", "094", "095", "096", "097", "098", "099", "100",
                    "101", "102", "103", "104", "105", "106", "107", "108", "109", "110",
                    "111", "112", "113", "114", "115", "116", "117", "118", "119", "120",
                    "121", "122", "123", "124", "125", "126", "127", "128", "129", "130",
                    "131", "132", "133", "134", "135", "136", "137", "138", "139", "140",
                    "141", "142", "143", "144", "145", "146", "147", "148", "149", "150",
                    "151", "152", "153", "154", "155", "156", "157", "158", "159", "160",
                    "161", "162", "163", "164", "165", "166", "167", "168", "169", "170",
                    "171", "172", "173", "174", "175", "176", "177", "178", "179", "180",
                    "181", "182", "183", "184", "185", "186", "187", "188", "189", "190",
                    "191", "192", "193", "194", "195", "196", "197", "198", "199", "200",
                    "201", "202", "203", "204", "205", "206", "207", "208", "209", "210",
                    "211", "212", "213", "214", "215", "216", "217", "218", "219", "220",
                    "221", "222", "223", "224", "225", "226", "227", "228", "229", "230",
                    "231", "232", "233", "234", "235", "236", "237", "238", "239", "240",
                    "241", "242", "243", "244", "245", "246", "247", "248", "249", "250",
                    "251", "252", "253", "254", "255", "256", "257", "258", "259", "260",
                    "261", "262" };
                menu.Add(new ListItem("Верхняя одежда ~g~$150", "~b~Enter~w~ для примерки", "ftop_model", ftop_model_list, client.hasData("top_id") ? client.getData("top_id") : 0)
                {
                    ExecuteCallback = true
                });
                // 1
                List<string> ftop_color_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023" };
                menu.Add(new ListItem("Цвет верхней одежды", "~b~Enter~w~ для примерки", "ftop_model_color", ftop_color_list, client.hasData("toptexture_id") ? client.getData("toptexture_id") : 0)
                {
                    ExecuteCallback = true
                });
                //2
                List<string> fundershirt_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077", "078", "079", "080",
                    "081", "082", "083", "084", "085", "086", "087", "088", "089", "090",
                    "091", "092", "093", "094", "095", "096", "097", "098", "099", "100",
                    "101", "102", "103", "104", "105", "106", "107", "108", "109", "110",
                    "111", "112", "113", "114", "115", "116", "117", "118", "119", "120",
                    "121", "122", "123", "124", "125", "126", "127", "128", "129", "130",
                    "131", "132", "133", "134", "135", "136", "137", "138", "139", "140",
                    "141", "142", "143", "144", "145", "146", "147", "148", "149", "150",
                    "151", "152", "153", "154", "155", "156", "157", "158", "159", "160",
                    "161" };
                menu.Add(new ListItem("Рубашка под верхнюю одежду ~g~$150", "~b~Enter~w~ для примерки", "fundershirt_model", fundershirt_model_list, client.hasData("undershirt_id") ? client.getData("undershirt_id") : 0)
                {
                    ExecuteCallback = true
                });
                //3
                List<string> fundershirt_color_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023" };
                menu.Add(new ListItem("Цвет рубашки", "~b~Enter~w~ для примерки", "fundershirt_color", fundershirt_color_list, client.hasData("undershirttexture_id") ? client.getData("undershirttexture_id") : 0)
                {
                    ExecuteCallback = true
                });

                //4
                List<string> ftorso_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077", "078", "079", "080",
                    "081", "082", "083", "084", "085", "086", "087", "088", "089", "090",
                    "091", "092", "093", "094", "095", "096", "097", "098", "099", "100",
                    "101", "102", "103", "104", "105", "106", "107", "108", "109", "110",
                    "111", "112", "113", "114", "115", "116", "117", "118", "119", "120",
                    "121", "122", "123", "124", "125", "126", "127", "128", "129", "130",
                    "131", "132", "133", "134", "135", "136", "137", "138", "139", "140",
                    "141", "142", "143", "144", "145", "146", "147", "148", "149", "150",
                    "151", "152", "153", "154", "155", "156", "157", "158", "159", "160",
                    "161", "162", "163", "164", "165", "166", "167", "168" };
                menu.Add(new ListItem("Модель рук ~g~$150", "~b~Enter~w~ для примерки", "ftorso_model", ftorso_model_list, client.hasData("torso_id") ? client.getData("torso_id") : 0)
                {
                    ExecuteCallback = true
                });

                // Нижняя одежда
                //5
                List<string> flegs_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077", "078", "079", "080",
                    "081", "082", "083", "084", "085", "086", "087", "088", "089", "090",
                    "091", "092", "093", "094", "095", "096", "097", "098", "099", "100",
                    "101", "102" };
                menu.Add(new ListItem("Нижняя одежда ~g~$150", "~b~Enter~w~ для примерки", "flegs_model", flegs_model_list, client.hasData("legs_id") ? client.getData("legs_id") : 0)
                {
                    ExecuteCallback = true
                });
                //6
                List<string> flegs_color_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023" };
                menu.Add(new ListItem("Цвет нижней одежды", "~b~Enter~w~ для примерки", "flegs_model_color", flegs_color_list, client.hasData("legstexture_id") ? client.getData("legstexture_id") : 0)
                {
                    ExecuteCallback = true
                });

                // Обувь
                //7
                List<string> fshoes_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077" };
                menu.Add(new ListItem("Обувь ~g~$150", "~b~Enter~w~ для примерки", "fshoes_model", fshoes_model_list, client.hasData("feet_id") ? client.getData("feet_id") : 0)
                {
                    ExecuteCallback = true
                });
                //8
                List<string> fshoes_color_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023" };
                menu.Add(new ListItem("Цвет обуви", "~b~Enter~w~ для примерки", "fshoes_model_color", fshoes_color_list, client.hasData("feettexture_id") ? client.getData("feettexture_id") : 0)
                {
                    ExecuteCallback = true
                });

                // Аксессуар
                //9
                List<string> faccessory_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077", "078", "079", "080",
                    "081", "082", "083", "084", "085", "086", "087", "088", "089", "090",
                    "091", "092", "093", "094", "095", "096", "097", "098" };
                menu.Add(new ListItem("Аксессуар ~g~$150", "~b~Enter~w~ для примерки", "faccessory_model", faccessory_model_list, client.hasData("accessorie_id") ? client.getData("accessorie_id") : 0)
                {
                    ExecuteCallback = true
                });
                //10
                List<string> faccessory_color_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019",
                    "020", "021", "022", "023" };
                menu.Add(new ListItem("Цвет аксессуара", "~b~Enter~w~ для примерки", "faccessory_model_color", faccessory_color_list, client.getData("accessorietexture_id"))
                {
                    ExecuteCallback = true
                });

                //11
                menu.Add(new MenuItem("~g~Совершить покупку", "Совершить покупку и сохранить одежду", "BuyClothes")
                {
                    ExecuteCallback = true
                });
                //12
                menu.Add(new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                });
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void ClothesShop_Male_Menu_Builder(Client client)
        {

            Menu menu = savedMenu;

            if (menu == null)
            {
                menu = new Menu("ClothesShop", "Магазин одежды", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true)
                {
                    BannerColor = new Color(200, 0, 0, 90),
                    Callback = ClothesShop_Male_MenuManager
                };

                //Верхняя одежда
                // 0
                List<string> mtop_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077", "078", "079", "080",
                    "081", "082", "083", "084", "085", "086", "087", "088", "089", "090",
                    "091", "092", "093", "094", "095", "096", "097", "098", "099", "100",
                    "101", "102", "103", "104", "105", "106", "107", "108", "109", "110",
                    "111", "112", "113", "114", "115", "116", "117", "118", "119", "120",
                    "121", "122", "123", "124", "125", "126", "127", "128", "129", "130",
                    "131", "132", "133", "134", "135", "136", "137", "138", "139", "140",
                    "141", "142", "143", "144", "145", "146", "147", "148", "149", "150",
                    "151", "152", "153", "154", "155", "156", "157", "158", "159", "160",
                    "161", "162", "163", "164", "165", "166", "167", "168", "169", "170",
                    "171", "172", "173", "174", "175", "176", "177", "178", "179", "180",
                    "181", "182", "183", "184", "185", "186", "187", "188", "189", "190",
                    "191", "192", "193", "194", "195", "196", "197", "198", "199", "200",
                    "201", "202", "203", "204", "205", "206", "207", "208", "209", "210",
                    "211", "212", "213", "214", "215", "216", "217", "218", "219", "220",
                    "221", "222", "223", "224", "225", "226", "227", "228", "229", "230",
                    "231", "232", "233", "234", "235", "236", "237", "238", "239", "240",
                    "241", "242", "243", "244", "245", "246", "247", "248", "249", "250",
                    "251", "252", "253" };
                menu.Add(new ListItem("Верхняя одежда ~g~$150", "~b~Enter~w~ для примерки | Цена: ~g~$150", "mtop_model", mtop_model_list, client.hasData("top_id") ? client.getData("top_id") : 0)
                {
                    ExecuteCallback = true
                });
                // 1
                List<string> mtop_color_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019",
                    "020", "021", "022", "023" };
                menu.Add(new ListItem("Цвет верхней одежды", "~b~Enter~w~ для примерки", "mtop_model_color", mtop_color_list, client.hasData("toptexture_id") ? client.getData("toptexture_id") : 0)
                {
                    ExecuteCallback = true
                });
                //2
                List<string> mundershirt_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077", "078", "079", "080",
                    "081", "082", "083", "084", "085", "086", "087", "088", "089", "090",
                    "091", "092", "093", "094", "095", "096", "097", "098", "099", "100",
                    "101", "102", "103", "104", "105", "106", "107", "108", "109", "110",
                    "111", "112", "113", "114", "115", "116", "117", "118", "119", "120",
                    "121", "122", "123", "124", "125", "126", "127", "128", "129", "130",
                    "131" };
                menu.Add(new ListItem("Рубашка под верхнюю одежду ~g~$150", "~b~Enter~w~ для примерки | Цена: ~g~$150", "mundershirt_model", mundershirt_model_list, client.hasData("undershirt_id") ? client.getData("undershirt_id") : 0)
                {
                    ExecuteCallback = true
                });
                //3
                List<string> mundershirt_color_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023" };
                menu.Add(new ListItem("Цвет рубашки", "~b~Enter~w~ для примерки", "mundershirt_color", mundershirt_color_list, client.hasData("undershirttexture_id") ? client.getData("undershirttexture_id") : 0)
                {
                    ExecuteCallback = true
                });

                //4
                List<string> mtorso_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077", "078", "079", "080",
                    "081", "082", "083", "084", "085", "086", "087", "088", "089", "090",
                    "091", "092", "093", "094", "095", "096", "097", "098", "099", "100",
                    "101", "102", "103", "104", "105", "106", "107", "108", "109", "110",
                    "111" };
                menu.Add(new ListItem("Модель рук ~g~$150", "~b~Enter~w~ для примерки | Цена: ~g~$150", "mtorso_model", mtorso_model_list, client.hasData("torso_id") ? client.getData("torso_id") : 0)
                {
                    ExecuteCallback = true
                });

                // Нижняя одежда
                //5
                List<string> mlegs_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077", "078", "079", "080",
                    "081", "082", "083", "084", "085", "086", "087", "088", "089", "090",
                    "091", "092", "093", "094", "095", "096", "097", "098" };
                menu.Add(new ListItem("Нижняя одежда ~g~$150", "~b~Enter~w~ для примерки | Цена: ~g~$150", "mlegs_model", mlegs_model_list, client.hasData("legs_id") ? client.getData("legs_id") : 0)
                {
                    ExecuteCallback = true
                });
                //6
                List<string> mlegs_color_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023" };
                menu.Add(new ListItem("Цвет нижней одежды", "~b~Enter~w~ для примерки", "mlegs_model_color", mlegs_color_list, client.hasData("legstexture_id") ? client.getData("legstexture_id") : 0)
                {
                    ExecuteCallback = true
                });

                // Обувь
                //7
                List<string> mshoes_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073" };
                menu.Add(new ListItem("Обувь", "~b~Enter~w~ для примерки | Цена: ~g~$150", "mshoes_model", mshoes_model_list, client.hasData("feet_id") ? client.getData("feet_id") : 0)
                {
                    ExecuteCallback = true
                });
                //8
                List<string> mshoes_color_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023" };
                menu.Add(new ListItem("Цвет обуви", "~b~Enter~w~ для примерки", "mshoes_model_color", mshoes_color_list, client.hasData("feettexture_id") ? client.getData("feettexture_id") : 0)
                {
                    ExecuteCallback = true
                });    

                // Аксессуар
                //9
                List<string> maccessory_model_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019", "020",
                    "021", "022", "023", "024", "025", "026", "027", "028", "029", "030",
                    "031", "032", "033", "034", "035", "036", "037", "038", "039", "040",
                    "041", "042", "043", "044", "045", "046", "047", "048", "049", "050",
                    "051", "052", "053", "054", "055", "056", "057", "058", "059", "060",
                    "061", "062", "063", "064", "065", "066", "067", "068", "069", "070",
                    "071", "072", "073", "074", "075", "076", "077", "078", "079", "080",
                    "081", "082", "083", "084", "085", "086", "087", "088", "089", "090",
                    "091", "092", "093", "094", "095", "096", "097", "098", "099", "100",
                    "101", "102", "103", "104", "105", "106", "107", "108", "109", "110",
                    "111", "112", "113", "114", "115", "116", "117", "118", "119", "120",
                    "121", "122", "123", "124", "125", "126", "127", "128" };
                menu.Add(new ListItem("Аксессуар ~g~$150", "~b~Enter~w~ для примерки", "maccessory_model", maccessory_model_list, client.hasData("accessorie_id") ? client.getData("accessorie_id") : 0)
                {
                    ExecuteCallback = true
                });
                //10
                List<string> maccessory_color_list = new List<string>() { "000",
                    "001", "002", "003", "004", "005", "006", "007", "008", "009", "010",
                    "011", "012", "013", "014", "015", "016", "017", "018", "019",
                    "020", "021", "022", "023" };
                menu.Add(new ListItem("Цвет аксессуара", "~b~Enter~w~ для примерки", "maccessory_model_color", maccessory_color_list, client.hasData("accessorietexture_id") ? client.getData("accessorietexture_id") : 0)
                {
                    ExecuteCallback = true
                });

                // 11
                menu.Add(new MenuItem("~g~Совершить покупку", "Совершить покупку и сохранить одежду", "BuyClothes")
                {
                    ExecuteCallback = true
                });
                // 12
                menu.Add(new MenuItem("~r~Закрыть", "Закрыть меню", "Close")
                {
                    ExecuteCallback = true
                });
            }
            MenuManager.OpenMenu(client, menu);
        }

        private void ClothesShop_Female_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            // верхняя одежда
            if (itemIndex == 0)
            {
                int top_model = Convert.ToInt32((string)data["ftop_model"]["Value"]);

                ChangePlayerClothes(client, 11, top_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            if (itemIndex == 1)
            {
                int top_model_color = Convert.ToInt32((string)data["ftop_model_color"]["Value"]);
                int top_model = Convert.ToInt32((string)data["ftop_model"]["Value"]);

                int current_top = API.getPlayerClothesDrawable(client, 11);
                ChangePlayerClothes(client, 11, top_model, top_model_color);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            // модель undershirt
            if (itemIndex == 2)
            {
                int undershirt_model = Convert.ToInt32((string)data["fundershirt_model"]["Value"]);

                ChangePlayerClothes(client, 8, undershirt_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            // цвет undershirt
            if (itemIndex == 3)
            {
                int undershirt_color = Convert.ToInt32((string)data["fundershirt_color"]["Value"]);
                int undershirt_model = Convert.ToInt32((string)data["fundershirt_model"]["Value"]);

                int current_top = API.getPlayerClothesDrawable(client, 11);
                ChangePlayerClothes(client, 8, undershirt_model, undershirt_color);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            // нижняя одежда

            if (itemIndex == 4)
            {
                int torso_model = Convert.ToInt32((string)data["ftorso_model"]["Value"]);

                ChangePlayerClothes(client, 3, torso_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }

            if (itemIndex == 5)
            {
                int legs_model = Convert.ToInt32((string)data["flegs_model"]["Value"]);

                ChangePlayerClothes(client, 4, legs_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            if (itemIndex == 6)
            {
                int legs_model_color = Convert.ToInt32((string)data["flegs_model_color"]["Value"]);
                int legs_model = Convert.ToInt32((string)data["flegs_model"]["Value"]);

                int current_top = API.getPlayerClothesDrawable(client, 11);
                ChangePlayerClothes(client, 4, legs_model, legs_model_color);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            // обувь
            if (itemIndex == 7)
            {
                int shoes_model = Convert.ToInt32((string)data["fshoes_model"]["Value"]);

                ChangePlayerClothes(client, 6, shoes_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            if (itemIndex == 8)
            {
                int shoes_model_color = Convert.ToInt32((string)data["fshoes_model_color"]["Value"]);
                int shoes_model = Convert.ToInt32((string)data["fshoes_model"]["Value"]);

                int current_top = API.getPlayerClothesDrawable(client, 11);
                ChangePlayerClothes(client, 6, shoes_model, shoes_model_color);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }

            // аксессуар
            if (itemIndex == 9)
            {
                int faccessory_model = Convert.ToInt32((string)data["faccessory_model"]["Value"]);

                ChangePlayerClothes(client, 7, faccessory_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            if (itemIndex == 10)
            {
                int faccessory_model_color = Convert.ToInt32((string)data["faccessory_model_color"]["Value"]);
                int faccessory_model = Convert.ToInt32((string)data["faccessory_model"]["Value"]);

                ChangePlayerClothes(client, 7, faccessory_model, faccessory_model_color);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }

            if (itemIndex == 11)
            {

                int top_model = Convert.ToInt32((string)data["ftop_model"]["Value"]);
                int undershirt_model = Convert.ToInt32((string)data["fundershirt_model"]["Value"]);
                int torso_model = Convert.ToInt32((string)data["ftorso_model"]["Value"]);
                int legs_model = Convert.ToInt32((string)data["flegs_model"]["Value"]);
                int shoes_model = Convert.ToInt32((string)data["fshoes_model"]["Value"]);
                int faccessory_model = Convert.ToInt32((string)data["faccessory_model"]["Value"]);

                int price_top = 0;
                int price_undershirt = 0;
                int price_torso = 0;
                int price_legs = 0;
                int price_feet = 0;
                int price_hat = 0;
                int price_accessory = 0;
                int price_glasses = 0;

                if (top_model != Convert.ToInt32(client.hasData("top_id") ? client.getData("top_id") : 0))
                {
                    price_top = +150;
                }
                if(undershirt_model != Convert.ToInt32(client.hasData("undershirt_id") ? client.getData("undershirt_id") : 0))
                {
                    price_undershirt = +150;
                }
                if(torso_model != Convert.ToInt32(client.hasData("torso_id") ? client.getData("torso_id") : 0))
                {
                    price_torso = +150;
                }
                if(legs_model != Convert.ToInt32(client.hasData("legs_id") ? client.getData("legs_id") : 0))
                {
                    price_legs = +150;
                }
                if(shoes_model != Convert.ToInt32(client.hasData("feet_id") ? client.getData("feet_id") : 0))
                {
                    price_feet = +150;
                }
                if (faccessory_model != Convert.ToInt32(client.hasData("accessorie_id") ? client.getData("accessorie_id") : 0))
                {
                    price_accessory = +150;
                }

                int summary_price = price_top + price_undershirt + price_torso + price_legs + price_feet + price_accessory;

                if (summary_price == 0)
                {
                    API.sendNotificationToPlayer(client, "~b~Вы ничего не выбрали для покупки");
                    return;
                }

                if (PlayerFunctions.Player.GetMoney(client) < summary_price)
                {
                        API.sendNotificationToPlayer(client, "~r~У вас недостаточно денег для покупки");
                        return;
                }

                else
                {
                    Player.ChangeMoney(client, -summary_price);
                    Player.UpdatePlayerClothes(client);
                    Database.SavePlayerClothes(client);
                    API.sendNotificationToPlayer(client, "~g~Вы совершили покупку, вам идёт новый образ");
                    // MenuManager.CloseMenu(client);
                }
            }

            if (itemIndex == 12)
            {
                Player.LoadPlayerClothes(client);
                MenuManager.CloseMenu(client);
                return;
            }
        }

        private void ClothesShop_Male_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            // верхняя одежда
            if (itemIndex == 0)
            {
                int top_model = Convert.ToInt32((string)data["mtop_model"]["Value"]);

                ChangePlayerClothes(client, 11, top_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            if (itemIndex == 1)
            {
                int top_model_color = Convert.ToInt32((string)data["mtop_model_color"]["Value"]);
                int top_model = Convert.ToInt32((string)data["mtop_model"]["Value"]);

                int current_top = API.getPlayerClothesDrawable(client, 11);
                ChangePlayerClothes(client, 11, top_model, top_model_color);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            // модель undershirt
            if (itemIndex == 2)
            {
                int undershirt_model = Convert.ToInt32((string)data["mundershirt_model"]["Value"]);

                ChangePlayerClothes(client, 8, undershirt_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            // цвет undershirt
            if (itemIndex == 3)
            {
                int undershirt_color = Convert.ToInt32((string)data["mundershirt_color"]["Value"]);
                int undershirt_model = Convert.ToInt32((string)data["mundershirt_model"]["Value"]);

                int current_top = API.getPlayerClothesDrawable(client, 11);
                ChangePlayerClothes(client, 8, undershirt_model, undershirt_color);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            // нижняя одежда

            if (itemIndex == 4)
            {
                int torso_model = Convert.ToInt32((string)data["mtorso_model"]["Value"]);

                ChangePlayerClothes(client, 3, torso_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }

            if (itemIndex == 5)
            {
                int legs_model = Convert.ToInt32((string)data["mlegs_model"]["Value"]);

                ChangePlayerClothes(client, 4, legs_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            if (itemIndex == 6)
            {
                int legs_model_color = Convert.ToInt32((string)data["mlegs_model_color"]["Value"]);
                int legs_model = Convert.ToInt32((string)data["mlegs_model"]["Value"]);

                int current_top = API.getPlayerClothesDrawable(client, 11);
                ChangePlayerClothes(client, 4, legs_model, legs_model_color);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            // обувь
            if (itemIndex == 7)
            {
                int shoes_model = Convert.ToInt32((string)data["mshoes_model"]["Value"]);

                ChangePlayerClothes(client, 6, shoes_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            if (itemIndex == 8)
            {
                int shoes_model_color = Convert.ToInt32((string)data["mshoes_model_color"]["Value"]);
                int shoes_model = Convert.ToInt32((string)data["mshoes_model"]["Value"]);

                int current_top = API.getPlayerClothesDrawable(client, 11);
                ChangePlayerClothes(client, 6, shoes_model, shoes_model_color);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }

            // аксессуар
            if (itemIndex == 9)
            {
                int maccessory_model = Convert.ToInt32((string)data["maccessory_model"]["Value"]);

                ChangePlayerClothes(client, 7, maccessory_model, 0);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }
            if (itemIndex == 10)
            {
                int maccessory_model_color = Convert.ToInt32((string)data["maccessory_model_color"]["Value"]);
                int maccessory_model = Convert.ToInt32((string)data["maccessory_model"]["Value"]);

                ChangePlayerClothes(client, 7, maccessory_model, maccessory_model_color);
                // API.setPlayerClothes(client, 11, top_model, 0);
            }

            if (itemIndex == 11)
            {
                int mtop_model = Convert.ToInt32((string)data["mtop_model"]["Value"]);
                int mundershirt_model = Convert.ToInt32((string)data["mundershirt_model"]["Value"]);
                int mtorso_model = Convert.ToInt32((string)data["mtorso_model"]["Value"]);
                int mlegs_model = Convert.ToInt32((string)data["mlegs_model"]["Value"]);
                int mshoes_model = Convert.ToInt32((string)data["mshoes_model"]["Value"]);
                int maccessory_model = Convert.ToInt32((string)data["maccessory_model"]["Value"]);

                int price_top = 0;
                int price_undershirt = 0;
                int price_torso = 0;
                int price_legs = 0;
                int price_feet = 0;
                int price_hat = 0;
                int price_accessory = 0;
                int price_glasses = 0;

                if (mtop_model != Convert.ToInt32(client.hasData("top_id") ? client.getData("top_id") : 0))
                {
                    price_top = +150;
                }
                if (mundershirt_model != Convert.ToInt32(client.hasData("undershirt_id") ? client.getData("undershirt_id") : 0))
                {
                    price_undershirt = +150;
                }
                if (mtorso_model != Convert.ToInt32(client.hasData("torso_id") ? client.getData("torso_id") : 0))
                {
                    price_torso = +150;
                }
                if (mlegs_model != Convert.ToInt32(client.hasData("legs_id") ? client.getData("legs_id") : 0))
                {
                    price_legs = +150;
                }
                if (mshoes_model != Convert.ToInt32(client.hasData("feet_id") ? client.getData("feet_id") : 0))
                {
                    price_feet = +150;
                }
                if (maccessory_model != Convert.ToInt32(client.hasData("accessorie_id") ? client.getData("accessorie_id") : 0))
                {
                    price_accessory = +150;
                }

                int summary_price = price_top + price_undershirt + price_torso + price_legs + price_feet + price_accessory;

                if (summary_price == 0)
                {
                    API.sendNotificationToPlayer(client, "~b~Вы ничего не выбрали для покупки");
                    return;
                }

                if (PlayerFunctions.Player.GetMoney(client) < summary_price)
                {
                    API.sendNotificationToPlayer(client, "~r~У вас недостаточно денег для покупки");
                    return;
                }

                else
                {
                    PlayerFunctions.Player.ChangeMoney(client, -summary_price);
                    PlayerFunctions.Player.UpdatePlayerClothes(client);
                    Database.SavePlayerClothes(client);
                    API.sendNotificationToPlayer(client, "~g~Вы совершили покупку, вам идёт новый образ");
                    // MenuManager.CloseMenu(client);
                }
            }

            else if (itemIndex == 12)
            {
                Player.LoadPlayerClothes(client);
                MenuManager.CloseMenu(client);
                return;
            }
        }

        [Command("clothes")]
        public void ClothesCommand(Client sender)
        {
            if (PlayerFunctions.Player.GetPlayerGender(sender) == 1)
            {
                ClothesShop_Female_Menu_Builder(sender);
                return;
            }
            else if (PlayerFunctions.Player.GetPlayerGender(sender) == 0)
            {
                ClothesShop_Male_Menu_Builder(sender);
                return;
            }
        }

        private void ChangePlayerClothes(Client client, int slot, int drawable, int texture)
        {
            API.setPlayerClothes(client, slot, drawable, texture);
        }

        private void ChangePlayerAccessory(Client client, int slot, int drawable, int texture)
        {
            API.setPlayerAccessory(client, slot, drawable, texture);
        }

        private void Clothes_Shop_MenuClose(Client client)
        {
            MenuManager.CloseMenu(client);
        }

        private void onResourceStart()
        {
            ClothesShopBlip = API.createBlip(ClothesShopBlipPos);
            API.setBlipSprite(ClothesShopBlip, 73);
            API.setBlipScale(ClothesShopBlip, 1.0f);
            API.setBlipColor(ClothesShopBlip, 3);
            API.setBlipName(ClothesShopBlip, "Магазин одежды");
            API.setBlipShortRange(ClothesShopBlip, true);

            ClothesShopBuyColshape = API.createCylinderColShape(ClothesShopBuyColshapePos, 1.50f, 1f);
            ClothesShop2BuyColshape = API.createCylinderColShape(ClothesShop2BuyColshapePos, 1.50f, 1f);
            ClothesShop3BuyColshape = API.createCylinderColShape(ClothesShop3BuyColshapePos, 1.50f, 1f);
            ClothesShop4BuyColshape = API.createCylinderColShape(ClothesShop4BuyColshapePos, 1.50f, 1f);
            ClothesShop5BuyColshape = API.createCylinderColShape(ClothesShop5BuyColshapePos, 1.50f, 1f);

            API.createMarker(1, ClothesShopBuyMarkerPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.50f, 1.50f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ClothesShop2BuyMarkerPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.50f, 1.50f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ClothesShop3BuyMarkerPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.50f, 1.50f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ClothesShop4BuyMarkerPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.50f, 1.50f, 0.85f), 100, 50, 220, 90);
            API.createMarker(1, ClothesShop5BuyMarkerPos - new Vector3(0, 0, 1f), new Vector3(), new Vector3(), new Vector3(1.50f, 1.50f, 0.85f), 100, 50, 220, 90);

            ClothesShopBuyColshape.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                if (PlayerFunctions.Player.GetPlayerGender(player) == 1)
                {
                    ClothesShop_Female_Menu_Builder(player);
                    return;
                }
                else if (PlayerFunctions.Player.GetPlayerGender(player) == 0)
                {
                    ClothesShop_Male_Menu_Builder(player);
                    return;
                }
            };

            ClothesShopBuyColshape.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                Clothes_Shop_MenuClose(player);
                Player.LoadPlayerClothes(player);
            };

            ClothesShop2BuyColshape.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                if (PlayerFunctions.Player.GetPlayerGender(player) == 1)
                {
                    ClothesShop_Female_Menu_Builder(player);
                    return;
                }
                else if (PlayerFunctions.Player.GetPlayerGender(player) == 0)
                {
                    ClothesShop_Male_Menu_Builder(player);
                    return;
                }
            };

            ClothesShop2BuyColshape.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                Clothes_Shop_MenuClose(player);
                Player.LoadPlayerClothes(player);
            };

            ClothesShop3BuyColshape.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                if (PlayerFunctions.Player.GetPlayerGender(player) == 1)
                {
                    ClothesShop_Female_Menu_Builder(player);
                    return;
                }
                else if (PlayerFunctions.Player.GetPlayerGender(player) == 0)
                {
                    ClothesShop_Male_Menu_Builder(player);
                    return;
                }
            };

            ClothesShop3BuyColshape.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                Clothes_Shop_MenuClose(player);
                Player.LoadPlayerClothes(player);
            };

            ClothesShop4BuyColshape.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                if (PlayerFunctions.Player.GetPlayerGender(player) == 1)
                {
                    ClothesShop_Female_Menu_Builder(player);
                    return;
                }
                else if (PlayerFunctions.Player.GetPlayerGender(player) == 0)
                {
                    ClothesShop_Male_Menu_Builder(player);
                    return;
                }
            };

            ClothesShop4BuyColshape.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                Clothes_Shop_MenuClose(player);
                Player.LoadPlayerClothes(player);
            };

            ClothesShop5BuyColshape.onEntityEnterColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                if (PlayerFunctions.Player.GetPlayerGender(player) == 1)
                {
                    ClothesShop_Female_Menu_Builder(player);
                    return;
                }
                else if (PlayerFunctions.Player.GetPlayerGender(player) == 0)
                {
                    ClothesShop_Male_Menu_Builder(player);
                    return;
                }
            };

            ClothesShop5BuyColshape.onEntityExitColShape += (shape, Entity) =>
            {
                Client player;
                player = API.getPlayerFromHandle(Entity);
                Clothes_Shop_MenuClose(player);
                Player.LoadPlayerClothes(player);
            };
        }
    }
}
