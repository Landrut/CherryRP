using System;
using System.IO;
using System.Collections.Generic;
//
using CherryMPServer.Constant;
//
using CherryMPServer;
using CherryMPShared;
//;
using Newtonsoft.Json;
using MySQL;

namespace CustomSkin
{
    #region ParentData
    public class ParentData
    {
        public int Father;
        public int Mother;
        public float Similarity;
        public float SkinSimilarity;

        public ParentData(int father, int mother, float similarity, float skinsimilarity)
        {
            Father = father;
            Mother = mother;
            Similarity = similarity;
            SkinSimilarity = skinsimilarity;
        }
    }
    #endregion

    #region AppearanceItem
    public class AppearanceItem
    {
        public int Value;
        public float Opacity;

        public AppearanceItem(int value, float opacity)
        {
            Value = value;
            Opacity = opacity;
        }
    }
    #endregion

    #region HairData
    public class HairData
    {
        public int Hair;
        public int Color;
        public int HighlightColor;

        public HairData(int hair, int color, int highlightcolor)
        {
            Hair = hair;
            Color = color;
            HighlightColor = highlightcolor;
        }
    }
    #endregion

    #region PlayerCustomization Class
    public class PlayerCustomization
    {
        // Player
        public int Gender;

        // Parents
        public ParentData Parents;

        // Features
        public float[] Features = new float[20];

        // Appearance
        public AppearanceItem[] Appearance = new AppearanceItem[10];

        // Hair & Colors
        public HairData Hair;

        public int EyebrowColor;
        public int BeardColor;
        public int EyeColor;
        public int BlushColor;
        public int LipstickColor;
        public int ChestHairColor;

        // Clothes

        public PlayerCustomization()
        {
            Gender = 0;
            Parents = new ParentData(0, 0, 1.0f, 1.0f);
            for (int i = 0; i < Features.Length; i++) Features[i] = 0f;
            for (int i = 0; i < Appearance.Length; i++) Appearance[i] = new AppearanceItem(255, 1.0f);
            Hair = new HairData(0, 0, 0);
        }
    }
    #endregion
    public class customskin_main : Script
    {
        public static string SAVE_DIRECTORY = "Server/Data/CustomCharacters";
        public static Dictionary<NetHandle, PlayerCustomization> CustomPlayerData = new Dictionary<NetHandle, PlayerCustomization>();

        public static Vector3 CreatorCharPos = new Vector3(402.8664, -996.4108, -99.00027);
        public static Vector3 CreatorPos = new Vector3(402.8664, -997.5515, -98.5);
        public static Vector3 CameraLookAtPos = new Vector3(402.8664, -996.4108, -98.5);
        public static float FacingAngle = -185.0f;
        public static int DimensionID = 1;

        public customskin_main()
        {
            API.onResourceStart += CustomSkin_Init;

            API.onPlayerFinishedDownload += CustomSkin_PlayerJoin;
            API.onClientEventTrigger += CustomSkin_EventTrigger;
            API.onPlayerDisconnected += CustomSkin_PlayerLeave;

            API.onResourceStop += CustomSkin_Exit;
        }

        #region Methods
        public static void LoadCharacter(Client player)
        {
            if (player.getData("InGame") == 0) return;
            if (CustomPlayerData.ContainsKey(player.handle)) return;
            string player_file = SAVE_DIRECTORY + Path.DirectorySeparatorChar + player.name + ".json";

            if (File.Exists(player_file))
            {
                CustomPlayerData.Add(player.handle, JsonConvert.DeserializeObject<PlayerCustomization>(File.ReadAllText(player_file)));
            }
            else
            {
                CustomPlayerData.Add(player.handle, new PlayerCustomization());
            }

            ApplyCharacter(player);
        }

        public static void ApplyCharacter(Client player)
        {
            if (!CustomPlayerData.ContainsKey(player.handle)) return;

            player.setSkin((CustomPlayerData[player.handle].Gender == 0) ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);
            player.setDefaultClothes();
            player.setClothes(2, CustomPlayerData[player.handle].Hair.Hair, 0);

            API.shared.sendNativeToAllPlayers(
                Hash.SET_PED_HEAD_BLEND_DATA,
                player.handle,

                CustomPlayerData[player.handle].Parents.Mother,
                CustomPlayerData[player.handle].Parents.Father,
                0,

                CustomPlayerData[player.handle].Parents.Mother,
                CustomPlayerData[player.handle].Parents.Father,
                0,

                CustomPlayerData[player.handle].Parents.Similarity,
                CustomPlayerData[player.handle].Parents.SkinSimilarity,
                0,

                false
            );

            for (int i = 0; i < CustomPlayerData[player.handle].Features.Length; i++) API.shared.sendNativeToAllPlayers(Hash._SET_PED_FACE_FEATURE, player.handle, i, CustomPlayerData[player.handle].Features[i]);
            for (int i = 0; i < CustomPlayerData[player.handle].Appearance.Length; i++) API.shared.sendNativeToAllPlayers(Hash.SET_PED_HEAD_OVERLAY, player.handle, i, CustomPlayerData[player.handle].Appearance[i].Value, CustomPlayerData[player.handle].Appearance[i].Opacity);

            // apply colors
            API.shared.sendNativeToAllPlayers(Hash._SET_PED_HAIR_COLOR, player.handle, CustomPlayerData[player.handle].Hair.Color, CustomPlayerData[player.handle].Hair.HighlightColor);
            API.shared.sendNativeToAllPlayers(Hash._SET_PED_EYE_COLOR, player.handle, CustomPlayerData[player.handle].EyeColor);

            API.shared.sendNativeToAllPlayers(Hash._SET_PED_HEAD_OVERLAY_COLOR, player.handle, 1, 1, CustomPlayerData[player.handle].BeardColor, 0);
            API.shared.sendNativeToAllPlayers(Hash._SET_PED_HEAD_OVERLAY_COLOR, player.handle, 2, 1, CustomPlayerData[player.handle].EyebrowColor, 0);
            API.shared.sendNativeToAllPlayers(Hash._SET_PED_HEAD_OVERLAY_COLOR, player.handle, 5, 2, CustomPlayerData[player.handle].BlushColor, 0);
            API.shared.sendNativeToAllPlayers(Hash._SET_PED_HEAD_OVERLAY_COLOR, player.handle, 8, 2, CustomPlayerData[player.handle].LipstickColor, 0);
            API.shared.sendNativeToAllPlayers(Hash._SET_PED_HEAD_OVERLAY_COLOR, player.handle, 10, 1, CustomPlayerData[player.handle].ChestHairColor, 0);

            // CLOTHES

            PlayerFunctions.Player.SetPlayerGender(player, CustomPlayerData[player.handle].Gender);

            //PlayerFunctions.Player.UpdatePlayerClothes(player);

            player.setSyncedData("CustomCharacter", API.shared.toJson(CustomPlayerData[player.handle]));
        }

        public static void SaveCharacter(Client player)
        {
            if (!CustomPlayerData.ContainsKey(player.handle)) return;

            string player_file = SAVE_DIRECTORY + Path.DirectorySeparatorChar + player.name + ".json";
            File.WriteAllText(player_file, JsonConvert.SerializeObject(CustomPlayerData[player.handle], Formatting.Indented));
            PlayerFunctions.Player.SetPlayerGender(player, CustomPlayerData[player.handle].Gender);
        }

        public static void SendToCreator(Client player)
        {
            //player.setData("Creator_PrevPos", player.position);

            player.dimension = DimensionID;
            player.rotation = new Vector3(0f, 0f, FacingAngle);
            player.position = CreatorCharPos;

            if (CustomPlayerData.ContainsKey(player.handle))
            {
                SetCreatorClothes(player, CustomPlayerData[player.handle].Gender);
                player.triggerEvent("UpdateCreator", API.shared.toJson(CustomPlayerData[player.handle]));
            }
            else
            {
                CustomPlayerData.Add(player.handle, new PlayerCustomization());
                SetDefaultFeatures(player, 0);
            }

            player.triggerEvent("CreatorCamera", CreatorPos, CameraLookAtPos, FacingAngle);
            DimensionID++;
        }

        public static void SendBackToWorld(Client player)
        {
            player.dimension = 0;
            PlayerFunctions.Player.SetPlayerGender(player, CustomPlayerData[player.handle].Gender);

            player.setData("InGame", 1);
            player.freeze(false);

            player.position = new Vector3(-1034.276f, -2734.014f, 20.16927f);
            API.shared.setEntityRotation(player, new Vector3(0, 0, -32.2951f));
            //API.shared.setCameraBehindPlayer(player);

            player.triggerEvent("DestroyCamera");
        }

        public static void SetDefaultFeatures(Client player, int gender, bool reset = false)
        {
            if (reset)
            {
                CustomPlayerData[player.handle] = new PlayerCustomization();
                CustomPlayerData[player.handle].Gender = gender;

                CustomPlayerData[player.handle].Parents.Father = 0;
                CustomPlayerData[player.handle].Parents.Mother = 21;
                CustomPlayerData[player.handle].Parents.Similarity = (gender == 0) ? 1.0f : 0.0f;
                CustomPlayerData[player.handle].Parents.SkinSimilarity = (gender == 0) ? 1.0f : 0.0f;
            }

            // will apply the resetted data
            ApplyCharacter(player);

            // clothes
            SetCreatorClothes(player, gender);
        }

        public static void SetCreatorClothes(Client player, int gender)
        {
            if (!CustomPlayerData.ContainsKey(player.handle)) return;

            // clothes
            player.setDefaultClothes();
            for (int i = 0; i < 10; i++) player.clearAccessory(i);

            if (gender == 0)
            {
                player.setClothes(3, 15, 0);
                player.setClothes(4, 21, 0);
                player.setClothes(6, 34, 0);
                player.setClothes(8, 15, 0);
                player.setClothes(11, 15, 0);
            }
            else
            {
                player.setClothes(3, 15, 0);
                player.setClothes(4, 10, 0);
                player.setClothes(6, 35, 0);
                player.setClothes(8, 15, 0);
                player.setClothes(11, 15, 0);
            }

            player.setClothes(2, CustomPlayerData[player.handle].Hair.Hair, 0);
        }
        #endregion

        #region Events
        public void CustomSkin_Init()
        {
            SAVE_DIRECTORY = API.getResourceFolder() + Path.DirectorySeparatorChar + SAVE_DIRECTORY;
            if (!Directory.Exists(SAVE_DIRECTORY)) Directory.CreateDirectory(SAVE_DIRECTORY);
        }

        public void CustomSkin_PlayerJoin(Client player)
        {
            LoadCharacter(player);
        }

        public void CustomSkin_EventTrigger(Client player, string event_name, params object[] args)
        {
            switch (event_name)
            {
                case "SetGender":
                    {
                        if (args.Length < 1 || !CustomPlayerData.ContainsKey(player.handle)) return;

                        int gender = Convert.ToInt32(args[0]);
                        player.setSkin((gender == 0) ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);
                        player.setData("ChangedGender", true);
                        PlayerFunctions.Player.SetPlayerGender(player, gender);
                        PlayerFunctions.Player.SetPlayerGender(player, CustomPlayerData[player.handle].Gender);
                        SetDefaultFeatures(player, gender, true);
                        break;
                    }

                case "SaveCharacter":
                    {
                        if (args.Length < 8 || !CustomPlayerData.ContainsKey(player.handle)) return;

                        player.setDefaultClothes();

                        // gender
                        CustomPlayerData[player.handle].Gender = Convert.ToInt32(args[0]);
                        PlayerFunctions.Player.SetPlayerGender(player, CustomPlayerData[player.handle].Gender);

                        // parents
                        CustomPlayerData[player.handle].Parents.Father = Convert.ToInt32(args[1]);
                        CustomPlayerData[player.handle].Parents.Mother = Convert.ToInt32(args[2]);
                        CustomPlayerData[player.handle].Parents.Similarity = (float)Convert.ToDouble(args[3]);
                        CustomPlayerData[player.handle].Parents.SkinSimilarity = (float)Convert.ToDouble(args[4]);

                        // features
                        float[] feature_data = JsonConvert.DeserializeObject<float[]>(args[5].ToString());
                        CustomPlayerData[player.handle].Features = feature_data;

                        // appearance
                        AppearanceItem[] appearance_data = JsonConvert.DeserializeObject<AppearanceItem[]>(args[6].ToString());
                        CustomPlayerData[player.handle].Appearance = appearance_data;

                        // hair & colors
                        int[] hair_and_color_data = JsonConvert.DeserializeObject<int[]>(args[7].ToString());
                        for (int i = 0; i < hair_and_color_data.Length; i++)
                        {
                            switch (i)
                            {
                                // Hair
                                case 0:
                                    {
                                        CustomPlayerData[player.handle].Hair.Hair = hair_and_color_data[i];
                                        break;
                                    }

                                // Hair Color
                                case 1:
                                    {
                                        CustomPlayerData[player.handle].Hair.Color = hair_and_color_data[i];
                                        break;
                                    }

                                // Hair Highlight Color
                                case 2:
                                    {
                                        CustomPlayerData[player.handle].Hair.HighlightColor = hair_and_color_data[i];
                                        break;
                                    }

                                // Eyebrow Color
                                case 3:
                                    {
                                        CustomPlayerData[player.handle].EyebrowColor = hair_and_color_data[i];
                                        break;
                                    }

                                // Beard Color
                                case 4:
                                    {
                                        CustomPlayerData[player.handle].BeardColor = hair_and_color_data[i];
                                        break;
                                    }

                                // Eye Color
                                case 5:
                                    {
                                        CustomPlayerData[player.handle].EyeColor = hair_and_color_data[i];
                                        break;
                                    }

                                // Blush Color
                                case 6:
                                    {
                                        CustomPlayerData[player.handle].BlushColor = hair_and_color_data[i];
                                        break;
                                    }

                                // Lipstick Color
                                case 7:
                                    {
                                        CustomPlayerData[player.handle].LipstickColor = hair_and_color_data[i];
                                        break;
                                    }

                                // Chest Hair Color
                                case 8:
                                    {
                                        CustomPlayerData[player.handle].ChestHairColor = hair_and_color_data[i];
                                        break;
                                    }
                            }
                        }
                        // Clothes
                        if (player.hasData("ChangedGender")) player.resetData("ChangedGender");

                        if (PlayerFunctions.Player.GetPlayerGender(player) == 1)
                        {
                            API.setPlayerAccessory(player, 0, 120, 0);
                            API.setPlayerAccessory(player, 1, 12, 0);
                        }
                        else
                        {
                            API.setPlayerAccessory(player, 0, 8, 0);
                            API.setPlayerAccessory(player, 1, 0, 0);
                        }

                        //PlayerFunctions.Player.UpdatePlayerClothes(player);
                        Database.SavePlayerClothes(player);
                        PlayerFunctions.Player.SetPlayerGender(player, CustomPlayerData[player.handle].Gender);
                        ApplyCharacter(player);
                        SaveCharacter(player);
                        SendBackToWorld(player);
                        break;
                    }

                case "LeaveCreator":
                    {
                        if (!CustomPlayerData.ContainsKey(player.handle)) return;

                        // revert back to last save data
                        if (player.hasData("ChangedGender"))
                        {
                            string player_file = SAVE_DIRECTORY + Path.DirectorySeparatorChar + player.name + ".json";
                            if (File.Exists(player_file)) CustomPlayerData[player.handle] = JsonConvert.DeserializeObject<PlayerCustomization>(File.ReadAllText(player_file));
                            player.resetData("ChangedGender");
                        }

                        ApplyCharacter(player);
                        PlayerFunctions.Player.SetPlayerGender(player, CustomPlayerData[player.handle].Gender);

                        // bye
                        SendBackToWorld(player);
                        break;
                    }
            }
        }

        public void CustomSkin_PlayerLeave(Client player, string reason)
        {
            if (CustomPlayerData.ContainsKey(player.handle)) CustomPlayerData.Remove(player.handle);
        }

        public void CustomSkin_Exit()
        {
            foreach (Client player in API.shared.getAllPlayers())
            {
                if (player.hasData("Creator_PrevPos"))
                {
                    player.dimension = 0;
                    player.position = (Vector3)player.getData("Creator_PrevPos");

                    player.triggerEvent("DestroyCamera");
                    player.resetData("Creator_PrevPos");
                }
            }

            CustomPlayerData.Clear();
        }
        #endregion

        #region Commands

        [Command("getpos")]
        public void GetPosition(Client player)
        {
            Vector3 PlayerPos = API.getEntityPosition(player);
            API.sendChatMessageToPlayer(player, "X: " + PlayerPos.X + " Y: " + PlayerPos.Y + " Z: " + PlayerPos.Z);
        }

        [Command("creator")]
        public static void CMD_EnableCreator(Client player)
        {
            if (!(player.model == (int)PedHash.FreemodeMale01 || player.model == (int)PedHash.FreemodeFemale01))
            {
                player.sendChatMessage("You need to have a freemode character skin.");
                return;
            }
            SendToCreator(player);
        }
        [Command("test")]
        public static void CMD_Test(Client player, float x, float y, float z, float xx, float yy, float zz)
        {
            player.dimension = DimensionID;
            player.rotation = new Vector3(0f, 0f, FacingAngle);
            player.position = CreatorCharPos;
            /*
                /test 0 0.5 1 0 0 1
            */
            player.triggerEvent("ctest", new Vector3(402.8664, -997.5515 - 0.5, -98.5 - 1), new Vector3(402.8664, -996.4108, -98.5 - 1), FacingAngle);

            Vector3 PlayerPos = API.shared.getEntityPosition(player);
            API.shared.sendChatMessageToPlayer(player, "X: " + PlayerPos.X + " Y: " + PlayerPos.Y + " Z: " + PlayerPos.Z);
        }
        #endregion
    }
}
