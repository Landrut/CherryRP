using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//
//
using CherryMPServer;
using CherryMPShared;
//;
using CherryMPServer.Constant;

using MenuManagement;
using PlayerFunctions;

public enum AnimationFlags
{
    Loop = 1 << 0,
    StopOnLastFrame = 1 << 1,
    OnlyAnimateUpperBody = 1 << 4,
    AllowPlayerControl = 1 << 5,
    Cancellable = 1 << 7
}

namespace Arcadia.Server.Services.AnimationService
{
    class AnimationService : Script
    {

        public AnimationService()
        {
            API.onClientEventTrigger += onClientEvent;
        }

        private MenuManager MenuManager = new MenuManager();
        private Menu savedMenu = null;

        // Конструктор основного Аним-Меню

        private void Animation_Menu_Builder(Client client)
        {

            Menu menu = savedMenu;

            if (menu == null)
            {
                menu = new Menu("Anims", "Анимации", "~y~Выберите категорию", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true)
                {
                    BannerColor = new Color(200, 0, 0, 90),
                    Callback = Anim_MenuManager
                };

                MenuItem menuItem = new MenuItem("Танцы", "Анимации танцев", "Dances");
                menuItem.ExecuteCallback = true;
                menu.Add(menuItem);

                menuItem = new MenuItem("Эмоции и состояния", "Эмоции и состояния", "Emojis");
                menuItem.ExecuteCallback = true;
                menu.Add(menuItem);
                
                MenuItem menuItem2 = new MenuItem("~g~Остановить анимацию", "", "Clear");
                menuItem2.ExecuteCallback = true;
                menu.Add(menuItem2);
                
                MenuItem menuItem3 = new MenuItem("~r~Закрыть", "Закрыть меню", "Close");
                menuItem3.ExecuteCallback = true;
                menu.Add(menuItem3);
            }
            MenuManager.OpenMenu(client, menu);
        }

        // Конструктор суб-меню танцев

        private void Sub_Dances_Menu_Builder(Client client)
        {
            Menu MenuDances = new Menu("Dances", "Танцы", "~y~Выберите танец", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true)
            {
                BannerColor = new Color(200, 0, 0, 90),
                Callback = Dances_MenuManager
            };

            MenuItem menuItem = new MenuItem("Читать рэп", "", "rap");
            menuItem.ExecuteCallback = true;
            MenuDances.Add(menuItem);

            menuItem = new MenuItem("Танец колдуна", "", "shamandance");
            menuItem.ExecuteCallback = true;
            MenuDances.Add(menuItem);

            menuItem = new MenuItem("Пританцовывать", "", "tdance");
            menuItem.ExecuteCallback = true;
            MenuDances.Add(menuItem);

            menuItem = new MenuItem("Танец стрип", "", "stripdance");
            menuItem.ExecuteCallback = true;
            MenuDances.Add(menuItem);

            menuItem = new MenuItem("Нелепый танец", "", "sillydance");
            menuItem.ExecuteCallback = true;
            MenuDances.Add(menuItem);

            menuItem = new MenuItem("Деревянный танец", "", "wooddance");
            menuItem.ExecuteCallback = true;
            MenuDances.Add(menuItem);

            MenuItem menuItem3 = new MenuItem("~r~Назад", "Вернуться назад", "Back");
            menuItem3.ExecuteCallback = true;
            MenuDances.Add(menuItem3);

            MenuManager.OpenMenu(client, MenuDances);
        }

        // Конструктор суб-меню эмоций

        private void Sub_Emojis_Menu_Builder(Client client)
        {
            Menu MenuEmojis = new Menu("Emojis", "Эмоции/Состояния", "~y~Выберите эмоцию/состояние", 0, 0, Menu.MenuAnchor.MiddleRight, false, true, true)
            {
                BannerColor = new Color(200, 0, 0, 90),
                Callback = Emojis_MenuManager
            };

            MenuItem menuItem = new MenuItem("Холодно [М]", "", "coldm");
            menuItem.ExecuteCallback = true;
            MenuEmojis.Add(menuItem);

            menuItem = new MenuItem("Холодно [Ж]", "", "coldj");
            menuItem.ExecuteCallback = true;
            MenuEmojis.Add(menuItem);

            menuItem = new MenuItem("Разминаться [М]", "", "razminkam");
            menuItem.ExecuteCallback = true;
            MenuEmojis.Add(menuItem);

            menuItem = new MenuItem("Разминаться [Ж]", "", "razminkaj");
            menuItem.ExecuteCallback = true;
            MenuEmojis.Add(menuItem);

            menuItem = new MenuItem("Фейспалм", "", "facepalm");
            menuItem.ExecuteCallback = true;
            MenuEmojis.Add(menuItem);

            menuItem = new MenuItem("Воздушный поцелуй", "", "fingerkiss");
            menuItem.ExecuteCallback = true;
            MenuEmojis.Add(menuItem);

            MenuItem menuItem3 = new MenuItem("~r~Назад", "Вернуться назад", "Back");
            menuItem3.ExecuteCallback = true;
            MenuEmojis.Add(menuItem3);

            MenuManager.OpenMenu(client, MenuEmojis);
        }

        // Менеджер основного меню анимаций

        private void Anim_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            if (itemIndex == 0)
            {
                savedMenu = menu;
                Sub_Dances_Menu_Builder(client);
            }
            else if (itemIndex == 1)
            {
                savedMenu = menu;
                Sub_Emojis_Menu_Builder(client);
            }
            else if (menu.Id == "Anims" && menuItem.Id == "Clear")
            {
                API.stopPlayerAnimation(client);
            }
            else if (menu.Id == "Anims")
            {
                Animation_Menu_Builder(client);
                savedMenu = menu;
                MenuManager.CloseMenu(client);
            }
            else if (menu.Id == "Anims" && menuItem.Id == "Close")
            {
                savedMenu = menu;
                MenuManager.CloseMenu(client);
            }
        }

        // Менеджер суб-меню танцев

        private void Dances_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            if (menu.Id == "Dances" && menuItem.Id == "rap")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, 1, "missfbi3_sniping", "dance_m_default");
            }
            else if (menu.Id == "Dances" && menuItem.Id == "shamandance")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, 1, "misschinese2_crystalmazemcs1_ig", "dance_loop_tao");
            }
            else if (menu.Id == "Dances" && menuItem.Id == "tdance")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, 1, "mini@strip_club@drink@idle_a", "idle_a");
            }
            else if (menu.Id == "Dances" && menuItem.Id == "stripdance")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, 1, "mp_am_stripper", "lap_dance_girl");
            }
            else if (menu.Id == "Dances" && menuItem.Id == "sillydance")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, 1, "special_ped@mountain_dancer@monologue_2@monologue_2a", "mnt_dnc_angel");
            }
            else if (menu.Id == "Dances" && menuItem.Id == "wooddance")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, 1, "move_clown@p_m_zero_idles@", "fidget_short_dance");
            }
            else if (menu.Id == "Dances")
            {
                Animation_Menu_Builder(client);
            }
            else if (menu.Id == "Dances" && menuItem.Id == "Back")
            {
                Animation_Menu_Builder(client);
            }
        }

        // Менеджер суб-меню эмоций

        private void Emojis_MenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, bool forced, dynamic data)
        {
            if (menu.Id == "Emojis" && menuItem.Id == "coldm")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, (int)(AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), "amb@code_human_wander_rain@male_a@base", "static");
            }
            else if (menu.Id == "Emojis" && menuItem.Id == "coldj")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, (int)(AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), "amb@code_human_wander_rain@female@base", "static");
            }
            else if (menu.Id == "Emojis" && menuItem.Id == "razminkam")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, 1, "amb@world_human_jog_standing@male@idle_a", "idle_a");
            }
            else if (menu.Id == "Emojis" && menuItem.Id == "razminkaj")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, 1, "amb@world_human_jog_standing@female@idle_a", "idle_a");
            }
            else if (menu.Id == "Emojis" && menuItem.Id == "facepalm")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, (int)(AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), "anim@mp_player_intcelebrationfemale@face_palm", "face_palm");
            }
            else if (menu.Id == "Emojis" && menuItem.Id == "fingerkiss")
            {
                API.stopPlayerAnimation(client);
                API.playPlayerAnimation(client, 0, "anim@mp_player_intcelebrationfemale@blow_kiss", "blow_kiss");
            }
            else if (menu.Id == "Emojis")
            {
                Animation_Menu_Builder(client);
            }
            else if (menu.Id == "Emojis" && menuItem.Id == "Back")
            {
                Animation_Menu_Builder(client);
            }
        }


        private void onClientEvent(Client player, string EventName, params object[] arguments)
        {
            if (EventName == "OpenAnimMenu")
            {
                Animation_Menu_Builder(player);
            }
            else return;
        }
    }
}
