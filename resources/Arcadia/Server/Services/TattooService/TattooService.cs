using System.Collections.Generic;
using System.IO;
using System.Linq;
using CherryMPServer;
using CherryMPShared;
using Newtonsoft.Json;

namespace TattooAPI
{
    #region TattooDataClass
    class TattooData
    {
        [JsonIgnore] public string Collection { get; set; }
        public string Name { get; private set; }
        public string LocalizedName { get; private set; }

        public string HashNameMale { get; private set; }
        public string HashNameFemale { get; private set; }

        public string Zone { get; private set; }
        public int ZoneID { get; private set; }

        public int Price { get; private set; }

        public TattooData(string name, string localizedname, string hashnamemale, string hashnamefemale, string zone, int zoneid, int price)
        {
            Name = name;
            LocalizedName = localizedname;

            HashNameMale = hashnamemale;
            HashNameFemale = hashnamefemale;

            Zone = zone;
            ZoneID = zoneid;

            Price = price;
        }
    }
    #endregion

    #region PlayerTattooData Class
    class PlayerTattooData
    {
        public string Collection { get; set; }
        public string Name { get; set; }
        public int TattooMaleHash { get; set; }
        public int TattooFemaleHash { get; set; }

        public PlayerTattooData(string collection, string name)
        {
            Collection = collection;
            Name = name;

            TattooMaleHash = API.shared.getHashKey(TattooAPI.GetTattooMaleHash(collection, name));
            TattooFemaleHash = API.shared.getHashKey(TattooAPI.GetTattooFemaleHash(collection, name));
        }
    }
    #endregion

    public class TattooAPI : Script
    {
        const string TATTOO_DATA_DIR = "TattooData"; // Содержит информацию о татуировках
        const string SAVE_DIR = "Server/Data/PlayerTattooData"; // Содержит информацию о татуировках игрока

        List<string> TattooCollections = new List<string>();
        static List<TattooData> Tattoos = new List<TattooData>();

        Dictionary<NetHandle, List<PlayerTattooData>> PlayerTattoos = new Dictionary<NetHandle, List<PlayerTattooData>>();

        public TattooAPI()
        {
            API.onResourceStart += TattooAPI_Init;
            API.onPlayerDisconnected += TattooAPI_PlayerLeave;
            API.onResourceStop += TattooAPI_Exit;
        }

        #region Script Methods
        public string GetTattooDir()
        {
            return API.getResourceFolder() + Path.DirectorySeparatorChar + TATTOO_DATA_DIR;
        }

        public string GetSaveDir()
        {
            return API.getResourceFolder() + Path.DirectorySeparatorChar + SAVE_DIR;
        }
        #endregion

        #region Exported Methods (Player)
        public void LoadTattoos(Client player)
        {
            if (!(player.model == (int)PedHash.FreemodeMale01 || player.model == (int)PedHash.FreemodeFemale01)) return;
            if (!PlayerTattoos.ContainsKey(player.handle)) PlayerTattoos.Add(player.handle, new List<PlayerTattooData>());

            string file = GetSaveDir() + Path.DirectorySeparatorChar + player.socialClubName + ".json";

            if (File.Exists(file))
            {
                PlayerTattoos[player.handle].Clear();
                API.sendNativeToAllPlayers(Hash.CLEAR_PED_DECORATIONS, player.handle);

                List<PlayerTattooData> player_tattos = JsonConvert.DeserializeObject<List<PlayerTattooData>>(File.ReadAllText(file));
                PlayerTattoos[player.handle] = player_tattos;

                foreach (PlayerTattooData tattoo in player_tattos)
                {
                    API.sendNativeToAllPlayers(Hash._SET_PED_DECORATION, player.handle, API.getHashKey(tattoo.Collection), ((player.model == (int)PedHash.FreemodeMale01) ? tattoo.TattooMaleHash : tattoo.TattooFemaleHash));
                }

                player.setSyncedData("TattooData", API.toJson(player_tattos));
            }
        }

        public bool AddTattoo(Client player, string collection, string name)
        {
            if (!(player.model == (int)PedHash.FreemodeMale01 || player.model == (int)PedHash.FreemodeFemale01)) return false;
            if (!IsCollectionValid(collection) || !IsTattooValid(collection, name) || !PlayerTattoos.ContainsKey(player.handle)) return false;
            if (HasTattoo(player, collection, name)) return false;

            PlayerTattooData new_tattoo = new PlayerTattooData(collection, name);
            PlayerTattoos[player.handle].Add(new_tattoo);

            API.sendNativeToAllPlayers(Hash._SET_PED_DECORATION, player.handle, API.getHashKey(collection), ((player.model == (int)PedHash.FreemodeMale01) ? new_tattoo.TattooMaleHash : new_tattoo.TattooFemaleHash));
            player.setSyncedData("TattooData", API.toJson(PlayerTattoos[player.handle]));
            return true;
        }

        public bool HasTattoo(Client player, string collection, string name)
        {
            if (!(player.model == (int)PedHash.FreemodeMale01 || player.model == (int)PedHash.FreemodeFemale01)) return false;
            if (!IsCollectionValid(collection) || !IsTattooValid(collection, name) || !PlayerTattoos.ContainsKey(player.handle)) return false;
            return (PlayerTattoos[player.handle].Count(t => t.Collection == collection && t.Name == name) > 0);
        }

        public bool RemoveTattoo(Client player, string collection, string name)
        {
            if (!(player.model == (int)PedHash.FreemodeMale01 || player.model == (int)PedHash.FreemodeFemale01)) return false;
            if (!IsCollectionValid(collection) || !IsTattooValid(collection, name) || !PlayerTattoos.ContainsKey(player.handle)) return false;

            PlayerTattooData tattoo_obj = PlayerTattoos[player.handle].FirstOrDefault(t => t.Collection == collection && t.Name == name);
            if (tattoo_obj == null) return false;

            PlayerTattoos[player.handle].Remove(tattoo_obj);

            API.sendNativeToAllPlayers(Hash.CLEAR_PED_DECORATIONS, player.handle);

            foreach (PlayerTattooData tattoo in PlayerTattoos[player.handle])
            {
                API.sendNativeToAllPlayers(Hash._SET_PED_DECORATION, player.handle, API.getHashKey(tattoo.Collection), ((player.model == (int)PedHash.FreemodeMale01) ? tattoo.TattooMaleHash : tattoo.TattooFemaleHash));
            }

            player.setSyncedData("TattooData", API.toJson(PlayerTattoos[player.handle]));
            return true;
        }

        public bool RemoveAllTattoos(Client player)
        {
            if (!(player.model == (int)PedHash.FreemodeMale01 || player.model == (int)PedHash.FreemodeFemale01)) return false;
            if (!PlayerTattoos.ContainsKey(player.handle)) return false;

            PlayerTattoos[player.handle].Clear();
            API.sendNativeToAllPlayers(Hash.CLEAR_PED_DECORATIONS, player.handle);
            player.setSyncedData("TattooData", API.toJson(PlayerTattoos[player.handle]));
            return true;
        }

        public void SaveTattoos(Client player)
        {
            if (!(player.model == (int)PedHash.FreemodeMale01 || player.model == (int)PedHash.FreemodeFemale01)) return;

            if (PlayerTattoos.ContainsKey(player.handle))
            {
                string file = GetSaveDir() + Path.DirectorySeparatorChar + player.socialClubName + ".json";
                File.WriteAllText(file, JsonConvert.SerializeObject(PlayerTattoos[player.handle]));
            }
        }

        public string[] GetTattoos(Client player)
        {
            if (!PlayerTattoos.ContainsKey(player.handle)) return new string[] { };

            List<string> player_tattoos = new List<string>();
            foreach (PlayerTattooData tattoo in PlayerTattoos[player.handle]) player_tattoos.Add(tattoo.Collection + "|" + tattoo.Name);
            return player_tattoos.ToArray();
        }
        #endregion

        #region Exported Methods (Collections)
        public bool IsCollectionValid(string collection)
        {
            return TattooCollections.Contains(collection);
        }

        public string[] GetTattooCollections()
        {
            return TattooCollections.ToArray();
        }

        public string[] GetCollectionTattoos(string collection_name)
        {
            if (!IsCollectionValid(collection_name)) return new string[] { };
            return Tattoos.Where(t => t.Collection == collection_name).Select(t => t.Name).ToArray();
        }
        #endregion

        #region Exported Methods (Tattoos)
        public static bool IsTattooValid(string tattoo_collection, string tattoo_name)
        {
            return (Tattoos.FirstOrDefault(t => t.Collection == tattoo_collection && t.Name == tattoo_name) != null);
        }

        public bool IsTattooNameValid(string tattoo_name)
        {
            return (Tattoos.FirstOrDefault(t => t.Name == tattoo_name) != null);
        }

        public string[] GetAllTattoos()
        {
            return Tattoos.Select(t => t.Name).ToArray();
        }

        public string GetTattooLocalizedName(string tattoo_collection, string tattoo_name)
        {
            if (!IsTattooValid(tattoo_collection, tattoo_name)) return string.Empty;
            return Tattoos.FirstOrDefault(t => t.Collection == tattoo_collection && t.Name == tattoo_name).LocalizedName;
        }

        public static string GetTattooMaleHash(string tattoo_collection, string tattoo_name)
        {
            if (!IsTattooValid(tattoo_collection, tattoo_name)) return string.Empty;
            return Tattoos.FirstOrDefault(t => t.Collection == tattoo_collection && t.Name == tattoo_name).HashNameMale;
        }

        public static string GetTattooFemaleHash(string tattoo_collection, string tattoo_name)
        {
            if (!IsTattooValid(tattoo_collection, tattoo_name)) return string.Empty;
            return Tattoos.FirstOrDefault(t => t.Collection == tattoo_collection && t.Name == tattoo_name).HashNameFemale;
        }

        public string GetTattooZone(string tattoo_collection, string tattoo_name)
        {
            if (!IsTattooValid(tattoo_collection, tattoo_name)) return string.Empty;
            return Tattoos.FirstOrDefault(t => t.Collection == tattoo_collection && t.Name == tattoo_name).Zone;
        }

        public int GetTattooZoneID(string tattoo_collection, string tattoo_name)
        {
            if (!IsTattooValid(tattoo_collection, tattoo_name)) return -1;
            return Tattoos.FirstOrDefault(t => t.Collection == tattoo_collection && t.Name == tattoo_name).ZoneID;
        }

        public int GetTattooPrice(string tattoo_collection, string tattoo_name)
        {
            if (!IsTattooValid(tattoo_collection, tattoo_name)) return 0;
            return Tattoos.FirstOrDefault(t => t.Collection == tattoo_collection && t.Name == tattoo_name).Price;
        }

        public string GetTattooCollection(string tattoo_name)
        {
            TattooData tattoo = Tattoos.FirstOrDefault(t => t.Name == tattoo_name);
            return (tattoo == null) ? string.Empty : tattoo.Collection;
        }
        #endregion

        #region Events
        public void TattooAPI_Init()
        {
            if (!Directory.Exists(GetTattooDir()))
            {
                API.consoleOutput("TattooAPI: Tattoo data folder doesn't exist, creating one but it will be empty.");
                Directory.CreateDirectory(GetTattooDir());
            }

            if (!Directory.Exists(GetSaveDir()))
            {
                API.consoleOutput("TattooAPI: Player data folder doesn't exist, creating one.");
                Directory.CreateDirectory(GetSaveDir());
            }

            foreach (string file in Directory.EnumerateFiles(GetTattooDir()))
            {
                string collection_name = Path.GetFileNameWithoutExtension(file);
                TattooCollections.Add(collection_name);

                TattooData[] tattoo_data = JsonConvert.DeserializeObject<TattooData[]>(File.ReadAllText(file));
                foreach (TattooData single_tattoo in tattoo_data)
                {
                    single_tattoo.Collection = collection_name;
                    Tattoos.Add(single_tattoo);
                }
            }
        }

        public void TattooAPI_PlayerLeave(Client player, string reason)
        {
            if (PlayerTattoos.ContainsKey(player.handle)) PlayerTattoos.Remove(player.handle);
        }

        [Command("loadtattoos")]
        public void CMD_LoadTattoos(Client player)
        {
            // use this command before doing anything like adding or removing tattoos
            LoadTattoos(player);
        }
        [Command("addtattoo")]
        public void CMD_AddTattoo(Client player, string collection, string name)
        {
            /*
                try mpgunrunning_overlays TAT_GR_008
                you can get collection/tattoo names with provided methods or read the TattooData files
            */
            if (AddTattoo(player, collection, name))
            {
                player.sendChatMessage("Татуировка добавлена.");
            }
            else
            {
                player.sendChatMessage("Не удалось добавить тату.");
            }
        }
        [Command("removetattoo")]
        public void CMD_RemoveTattoo(Client player, string collection, string name)
        {
            // use mpgunrunning_overlays TAT_GR_008 again, this time it will be removed
            if (RemoveTattoo(player, collection, name))
            {
                player.sendChatMessage("Татуировка убрана.");
            }
            else
            {
                player.sendChatMessage("Не удалось убрать тату.");
            }
        }
        [Command("savetattoos")]
        public void CMD_SaveTattoos(Client player)
        {
            // use this command to save your tattoos so you can load them later
            SaveTattoos(player);
        }

        public void TattooAPI_Exit()
        {
            PlayerTattoos.Clear();
        }
        #endregion
    }
}