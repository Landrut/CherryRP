using System.Collections.Concurrent;
using CherryMPServer;
using CherryMPServer.Constant;
using Newtonsoft.Json;

namespace MenuManagement
{
    class MenuManager : Script
    {
        #region Private static properties
        private static ConcurrentDictionary<Client, Menu> _clientMenus = new ConcurrentDictionary<Client, Menu>();
        #endregion

        #region Constructor
        public MenuManager()
        {
            API.onClientEventTrigger += OnClientEventTriggerHandler;
            API.onPlayerDisconnected += OnPlayerDisconnectedHandler;
        }
        #endregion

        #region Private API triggers
        private static void OnClientEventTriggerHandler(Client sender, string eventName, params object[] arguments)
        {
            if (eventName == "MenuManager_ExecuteCallback")
            {
                Menu menu = null;
                _clientMenus.TryGetValue(sender, out menu);

                if (menu != null && menu.Callback != null)
                {
                    string menuId = (string)arguments[0];
                    string itemId = (string)arguments[1];
                    int itemIndex = (int)arguments[2];
                    bool forced = (bool)arguments[3];
                    dynamic data = API.shared.fromJson((string)arguments[4]);

                    foreach (MenuItem menuItem in menu.Items)
                    {
                        if (menuItem.Type == MenuItemType.CheckboxItem)
                            ((CheckboxItem)menuItem).Checked = data[menuItem.Id];
                        else if (menuItem.Type == MenuItemType.ListItem)
                            ((ListItem)menuItem).SelectedItem = data[menuItem.Id]["Index"];
                        else if (menuItem.InputMaxLength > 0)
                            menuItem.InputValue = data[menuItem.Id];
                    }

                    menu.Callback(sender, menu, menu.Items[itemIndex], itemIndex, forced, data);
                }
            }
            else if (eventName == "MenuManager_ClosedMenu")
            {
                Menu menu = null;
                _clientMenus.TryGetValue(sender, out menu);

                if (menu != null && !menu.BackCloseMenu)
                    menu.Callback(sender, menu, null, -1, false, null);
                else if (menu != null)
                    _clientMenus.TryRemove(sender, out menu);
            }
        }

        private static void OnPlayerDisconnectedHandler(Client player, string reason)
        {
            Menu menu;
            _clientMenus.TryRemove(player, out menu);
        }
        #endregion

        #region Public static methods
        public static void CloseMenu(Client client)
        {
            Menu menu;

            if (_clientMenus.TryRemove(client, out menu))
            {
                menu.Finalizer?.Invoke(client, menu);
                API.shared.triggerClientEvent(client, "MenuManager_CloseMenu");
            }
        }

        public static void ForceCallback(Client client)
        {
            Menu menu = null;
            _clientMenus.TryGetValue(client, out menu);

            if (menu == null || menu.Callback == null)
                return;

            API.shared.triggerClientEvent(client, "MenuManager_ForceCallback");
        }

        public static Menu GetMenu(Client client)
        {
            Menu menu = null;
            _clientMenus.TryGetValue(client, out menu);

            return menu;
        }

        public static bool HasOpenMenu(Client client)
        {
            return _clientMenus.ContainsKey(client);
        }

        public static void OpenMenu(Client client, Menu menu)
        {
            Menu oldMenu = null;
            _clientMenus.TryRemove(client, out oldMenu);

            if (oldMenu != null)
            {
                oldMenu.Finalizer?.Invoke(client, menu);
                API.shared.triggerClientEvent(client, "MenuManager_CloseMenu");
            }

            _clientMenus.TryAdd(client, menu);
            string json = JsonConvert.SerializeObject(menu, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            API.shared.triggerClientEvent(client, "MenuManager_OpenMenu", json);
        }
        #endregion
    }
}
