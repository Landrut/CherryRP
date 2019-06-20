using System;
using MySql.Data.MySqlClient;
using CherryMPServer;
//
//
using CherryMPServer.Constant;
using CherryMPServer;
using CherryMPShared;
//;
using System.IO;
using System.Collections.Generic;

using CustomSkin;
using System.Data;
using Arcadia.Server.Services.Doors;
using Arcadia.Server.Models;
using Newtonsoft.Json;

namespace MySQL
{
    public class Database : Script
    {


        public static void Debug(int debug_code, string text)
        {
            if (debug_code == 0)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (debug_code == 1)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (debug_code == 2)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[Отладка] " + text);
            Console.ResetColor();
        }

        public static string myConnectionString = "SERVER=localhost;" + "DATABASE=arcadia;" + "UID=root;" + "PASSWORD=qcoluk1ps3;";
        public static MySqlConnection connection;
        public static MySqlCommand command;
        public static MySqlCommand command2;
        public static MySqlDataReader Reader;

        public Database()
        {
            Init();
        }

        public static void Init()
        {
            Console.WriteLine("\n[MySQL] Подключение к Базе Данных...");
            try
            {
                connection = new MySqlConnection(myConnectionString);
                connection.Open();
                Debug(0, "[MySQL] База Данных успешно подключена!\n");
                DoorService.LoadAllDoorsFromDB();
            }
            catch (MySqlException error)
            {
                Debug(1, "[MySQL] MySQL error: (#" + error.Number + ") " + error.Message + "\n");
            }
        }

        public static void Register_Account(Client player, string Password)
        {
            connection = new MySqlConnection(myConnectionString);
            connection.Open();

            // accounts
            int cash = 50000;
            int bankmoney = 0;
            int rights = 1;
            int fraction = 0;
            int fractionrank = 0;
            int onduty = 0;
            int job = 0;
            int jobrank = 0;
            int gang = 0;
            int gangrank = 0;
            int gender = 0;

            // licenses
            int CarsLic = 0;
            int MotoLic = 0;
            int MavLic = 0;
            int WeaponLic = 0;
            int MedCard = 0;
            int TaxiLic = 0;

            // comp's
            //int Mask = 0;
            int Torso = 0;
            int Legs = 0;
            int Leg_Texture = 0;
            int Bag = 0;
            int Feet = 0;
            int Feet_Texture = 0;
            int Accessorie = 0;
            int Accessorie_Texture = 0;
            int Undershirt = 0;
            int Undershirt_Texture = 0;
            int Decal = 0;
            int Top = 0;
            int Top_Texture = 0;

            // prop's
            int Hat = -1;
            int Hat_Texture = -1;
            int Glasses = -1;
            int Glasses_Texture = -1;
            int Ears = -1;
            int Watches = -1;
            int Braceletes = -1;

            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO accounts (Name, Password, Gender, Cash, BankMoney, Rights, Fraction, FractionRank, Job, JobRank, OnDuty, Torso, Legs, LegTexture, Bag, Feet, FeetTexture, Accessorie, AccessorieTexture, Undershirt, UndershirtTexture, Decal, Top, TopTexture, Hat, HatTexture, Glasses, GlassesTexture, Ears, Watches, Braceletes) VALUES (?name, ?password, ?gender, ?cash, ?bankmoney, ?rights, ?fraction, ?fractionrank, ?job, ?jobrank, ?onduty, ?torso, ?legs, ?legstexture, ?bag, ?feet, ?feettexture, ?accessorie, ?accessorietexture, ?undershirt, ?undershirttexture, ?decal, ?top, ?toptexture, ?hat, ?hattexture, ?glasses, ?glassestexture, ?ears, ?watches, ?braceletes)";
            command.Parameters.AddWithValue("?name", player.name);
            command.Parameters.AddWithValue("?password", Password);
            command.Parameters.AddWithValue("?gender", gender);
            command.Parameters.AddWithValue("?cash", cash);
            command.Parameters.AddWithValue("?bankmoney", bankmoney);
            command.Parameters.AddWithValue("?rights", rights);
            command.Parameters.AddWithValue("?fraction", fraction);
            command.Parameters.AddWithValue("?fractionrank", fractionrank);
            command.Parameters.AddWithValue("?job", job);
            command.Parameters.AddWithValue("?jobrank", jobrank);
            command.Parameters.AddWithValue("?gang", gang);
            command.Parameters.AddWithValue("?gangrank", gangrank);
            command.Parameters.AddWithValue("?onduty", onduty);
            //command.Parameters.AddWithValue("?mask", Mask);
            command.Parameters.AddWithValue("?torso", Torso);
            command.Parameters.AddWithValue("?legs", Legs);
            command.Parameters.AddWithValue("?legstexture", Leg_Texture);
            command.Parameters.AddWithValue("?bag", Bag);
            command.Parameters.AddWithValue("?feet", Feet);
            command.Parameters.AddWithValue("?feettexture", Feet_Texture);
            command.Parameters.AddWithValue("?accessorie", Accessorie);
            command.Parameters.AddWithValue("?accessorietexture", Accessorie_Texture);
            command.Parameters.AddWithValue("?undershirt", Undershirt);
            command.Parameters.AddWithValue("?undershirttexture", Undershirt_Texture);
            command.Parameters.AddWithValue("?decal", Decal);
            command.Parameters.AddWithValue("?top", Top);
            command.Parameters.AddWithValue("?toptexture", Top_Texture);
            command.Parameters.AddWithValue("?hat", Hat);
            command.Parameters.AddWithValue("?hattexture", Hat_Texture);
            command.Parameters.AddWithValue("?glasses", Glasses);
            command.Parameters.AddWithValue("?glassestexture", Glasses_Texture);
            command.Parameters.AddWithValue("?ears", Ears);
            command.Parameters.AddWithValue("?watches", Watches);
            command.Parameters.AddWithValue("?braceletes", Braceletes);

            command2 = connection.CreateCommand();
            command2.CommandText = "INSERT INTO licenses (Name, Cars, Motorcycles, Mavericks, Weapons, Medcard, Taxi) VALUES (?name, ?carslic, ?motolic, ?mavlic, ?weaponlic, ?medcardlic, ?taxilic)";
            command2.Parameters.AddWithValue("?name", player.name);
            command2.Parameters.AddWithValue("?carslic", CarsLic);
            command2.Parameters.AddWithValue("?motolic", MotoLic);
            command2.Parameters.AddWithValue("?mavlic", MavLic);
            command2.Parameters.AddWithValue("?weaponlic", WeaponLic);
            command2.Parameters.AddWithValue("?medcardlic", MedCard);
            command2.Parameters.AddWithValue("?taxilic", TaxiLic);

            player.setData("cash_amount", cash);
            player.setData("bank_amount", bankmoney);
            player.setData("rights_level", rights);
            player.setData("gender_id", gender);
            player.setData("fraction_id", fraction);
            player.setData("fractionrank_id", fractionrank);
            player.setData("job_id", job);
            player.setData("jobrank_id", jobrank);
            player.setData("gang_id", gang);
            player.setData("gangrank_id", gangrank);
            player.setData("onduty_status", onduty);

            player.setData("carslic_bool", CarsLic);
            player.setData("motolic_bool", MotoLic);
            player.setData("mavlic_bool", MavLic);
            player.setData("weaponlic_bool", WeaponLic);
            player.setData("medcardlic_bool", MedCard);
            player.setData("taxilic_bool", TaxiLic);

            //player.setData("mask_id", Mask);
            player.setData("torso_id", Torso);
            player.setData("legs_id", Legs);
            player.setData("legstexture_id", Leg_Texture);
            player.setData("bag_id", Bag);
            player.setData("feet_id", Feet);
            player.setData("feettexture_id", Feet_Texture);
            player.setData("accessorie_id", Accessorie);
            player.setData("accessorietexture_id", Accessorie_Texture);
            player.setData("undershirt_id", Undershirt);
            player.setData("undershirttexture_id", Undershirt_Texture);
            player.setData("decal_id", Decal);
            player.setData("top_id", Top);
            player.setData("toptexture_id", Top_Texture);
            player.setData("hat_id", Hat);
            player.setData("hattexture_id", Hat_Texture);
            player.setData("glasses_id", Glasses);
            player.setData("glassestexture_id", Glasses_Texture);
            player.setData("ears_id", Ears);
            player.setData("watches_id", Watches);
            player.setData("braceletes_id", Braceletes);

            command.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            connection.Close();

            API.shared.triggerClientEvent(player, "UpdateMoneyHUD", Convert.ToString(cash));
            API.shared.setEntityTransparency(player, 255);
        }

        public static void SavePlayerClothes(Client player)
        {
            connection = new MySqlConnection(myConnectionString);
            connection.Open();

            command2 = connection.CreateCommand();
            command2.CommandText = "UPDATE accounts SET Torso = ?torso, Legs = ?legs, LegTexture = ?legstexture, Bag = ?bag, Feet = ?feet, FeetTexture = ?feettexture, Accessorie = ?accessorie, AccessorieTexture = ?accessorietexture, Undershirt = ?undershirt, UndershirtTexture = ?undershirttexture, Decal = ?decal, Top = ?top, TopTexture = ?toptexture, Hat = ?hat, HatTexture = ?hattexture, Glasses = ?glasses, GlassesTexture = ?glassestexture, Ears = ?ears, Watches = ?watches, Braceletes = ?braceletes WHERE Name = ?name";

            command2.Parameters.AddWithValue("?name", player.name);
            //command2.Parameters.AddWithValue("?mask", player.getData("mask_id"));
            command2.Parameters.AddWithValue("?torso", player.getData("torso_id"));
            command2.Parameters.AddWithValue("?legs", player.getData("legs_id"));
            command2.Parameters.AddWithValue("?legstexture", player.getData("legstexture_id"));
            command2.Parameters.AddWithValue("?bag", player.getData("bag_id"));
            command2.Parameters.AddWithValue("?feet", player.getData("feet_id"));
            command2.Parameters.AddWithValue("?feettexture", player.getData("feettexture_id"));
            command2.Parameters.AddWithValue("?accessorie", player.getData("accessorie_id"));
            command2.Parameters.AddWithValue("?accessorietexture", player.getData("accessorietexture_id"));
            command2.Parameters.AddWithValue("?undershirt", player.getData("undershirt_id"));
            command2.Parameters.AddWithValue("?undershirttexture", player.getData("undershirttexture_id"));
            command2.Parameters.AddWithValue("?decal", player.getData("decal_id"));
            command2.Parameters.AddWithValue("?top", player.getData("top_id"));
            command2.Parameters.AddWithValue("?toptexture", player.getData("toptexture_id"));
            command2.Parameters.AddWithValue("?hat", player.getData("hat_id"));
            command2.Parameters.AddWithValue("?hattexture", player.getData("hattexture_id"));
            command2.Parameters.AddWithValue("?glasses", player.getData("glasses_id"));
            command2.Parameters.AddWithValue("?glassestexture", player.getData("glassestexture_id"));
            command2.Parameters.AddWithValue("?ears", player.getData("ears_id"));
            command2.Parameters.AddWithValue("?watches", player.getData("watches_id"));
            command2.Parameters.AddWithValue("?braceletes", player.getData("braceletes_id"));

            command2.ExecuteNonQuery();
            connection.Close();
        }

        public static void Save_Account(Client player)
        {
            if (player.getData("InGame") == 0) return;

            connection = new MySqlConnection(myConnectionString);
            connection.Open();
            command = connection.CreateCommand();
            command2 = connection.CreateCommand();
            command.CommandText = "UPDATE accounts SET Cash = ?cash, BankMoney = ?bankmoney, Rights = ?rights, Gender = ?gender, Fraction = ?fraction, FractionRank = ?fractionrank, Job = ?job, JobRank = ?jobrank WHERE Name = ?name";
            command2.CommandText = "UPDATE licenses SET Cars = ?carslic, Motorcycles = ?motolic, Mavericks = ?mavlic, Weapons = ?weaponlic, Medcard = ?medcardlic, Taxi = ?taxilic WHERE Name = ?name";
            command.Parameters.AddWithValue("?cash", player.getData("cash_amount"));
            command.Parameters.AddWithValue("?bankmoney", player.getData("bank_amount"));
            command.Parameters.AddWithValue("?rights", player.getData("rights_level"));
            command.Parameters.AddWithValue("?gender", player.getData("gender_id"));
            command.Parameters.AddWithValue("?name", player.name);
            command.Parameters.AddWithValue("?fraction", player.getData("fraction_id"));
            command.Parameters.AddWithValue("?fractionrank", player.getData("fractionrank_id"));
            command.Parameters.AddWithValue("?job", player.getData("job_id"));
            command.Parameters.AddWithValue("?jobrank", player.getData("jobrank_id"));
            command.Parameters.AddWithValue("?gang", player.getData("gang_id"));
            command.Parameters.AddWithValue("?gangrank", player.getData("gangrank_id"));

            command2.Parameters.AddWithValue("?name", player.name);
            command2.Parameters.AddWithValue("?carslic", player.getData("carslic_bool"));
            command2.Parameters.AddWithValue("?motolic", player.getData("motolic_bool"));
            command2.Parameters.AddWithValue("?mavlic", player.getData("mavlic_bool"));
            command2.Parameters.AddWithValue("?weaponlic", player.getData("weaponlic_bool"));
            command2.Parameters.AddWithValue("?medcardlic", player.getData("medcardlic_bool"));
            command2.Parameters.AddWithValue("?taxilic", player.getData("taxilic_bool"));

            Debug(0, "Данные пользователя " + player.name + " обновлены;\n" + player.name + " Общее: Наличные = " + player.getData("cash_amount") + " | " + "Банк = " + player.getData("bank_amount") + " | " + "Права = " + player.getData("rights_level") + " | " + "Пол = " + player.getData("gender_id") + " | " + "Фракция = " + player.getData("fraction_id") + " | " + "Ранг = " + player.getData("fractionrank_id") + " | " + "Работа = " + player.getData("job_id") + " | " + "Должность = " + player.getData("jobrank_id") + "\n" + player.name + " Лицензии: " + "Легковые = " + player.getData("carslic_bool") + " | " + "Мотоциклы = " + player.getData("motolic_bool") + " | " + "Вертолёты = " + player.getData("mavlic_bool") + " | " + "Оружие = " + player.getData("weaponlic_bool") + " | " + "Медкарта = " + player.getData("medcardlic_bool") + " | " + "Такси = " + player.getData("taxilic_bool") + "\n" + player.name + " Одежда: ");

            command.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            connection.Close();
        }

        public static void Read_Licenses(Client player)
        {
            connection = new MySqlConnection(myConnectionString);
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM licenses";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string name = Reader.GetString("Name");

                int CarsLic = Reader.GetInt32("Cars");
                int MotoLic = Reader.GetInt32("Motorcycles");
                int MavLic = Reader.GetInt32("Mavericks");
                int WeaponLic = Reader.GetInt32("Weapons");
                int MedCard = Reader.GetInt32("Medcard");
                int TaxiLic = Reader.GetInt32("Taxi");

                player.setData("carslic_bool", CarsLic);
                player.setData("motolic_bool", MotoLic);
                player.setData("mavlic_bool", MavLic);
                player.setData("weaponlic_bool", WeaponLic);
                player.setData("medcardlic_bool", MedCard);
                player.setData("taxilic_bool", TaxiLic);
                return;
            }
            Reader.Close();
            connection.Close();
        }

        public static bool Login_Account(Client player, string inputPassword)
        {
            connection = new MySqlConnection(myConnectionString);
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM accounts";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string name = Reader.GetString("Name");
                string password = Reader.GetString("Password");
                int cash = Reader.GetInt32("Cash");
                int bank = Reader.GetInt32("BankMoney");
                int rights = Reader.GetInt32("Rights");
                int gender = Reader.GetInt32("Gender");
                int fraction = Reader.GetInt32("Fraction");
                int fractionrank = Reader.GetInt32("FractionRank");
                int job = Reader.GetInt32("Job");
                int jobrank = Reader.GetInt32("JobRank");
                int gang = Reader.GetInt32("Gang");
                int gangrank = Reader.GetInt32("GangRank");
                int onduty = Reader.GetInt32("OnDuty");

                //int Mask = Reader.GetInt32("Mask");
                int Torso = Reader.GetInt32("Torso");
                int Legs = Reader.GetInt32("Legs");
                int Leg_Texture = Reader.GetInt32("LegTexture");
                int Bag = Reader.GetInt32("Bag");
                int Feet = Reader.GetInt32("Feet");
                int Feet_Texture = Reader.GetInt32("FeetTexture");
                int Accessorie = Reader.GetInt32("Accessorie");
                int Accessorie_Texture = Reader.GetInt32("AccessorieTexture");
                int Undershirt = Reader.GetInt32("Undershirt");
                int Undershirt_Texture = Reader.GetInt32("UndershirtTexture");
                int Decal = Reader.GetInt32("Decal");
                int Top = Reader.GetInt32("Top");
                int Top_Texture = Reader.GetInt32("TopTexture");

                // prop's
                int Hat = Reader.GetInt32("Hat");
                int Hat_Texture = Reader.GetInt32("HatTexture");
                int Glasses = Reader.GetInt32("Glasses");
                int Glasses_Texture = Reader.GetInt32("GlassesTexture");
                int Ears = Reader.GetInt32("Ears");
                int Watches = Reader.GetInt32("Watches");
                int Braceletes = Reader.GetInt32("Braceletes");

                if (player.name == name && inputPassword == password)
                {
                    Debug(0, "Авторизация пользователя [" + name + "]" + " [" + player.address + "] " + "прошла успешно | " + password);

                    connection.Close();
                    player.setData("InGame", 1);
                    customskin_main.LoadCharacter(player);
                    Read_Licenses(player);

                    API.shared.setEntityPosition(player, new Vector3(-1037.747f, -2737.992f, 20.16927f));
                    API.shared.setEntityRotation(player, new Vector3(0, 0, -32.2951f));
                    //API.shared.setCameraBehindPlayer(player);

                    player.setData("cash_amount", cash);
                    player.setData("bank_amount", bank);
                    player.setData("rights_level", rights);
                    player.setData("gender_id", gender);
                    player.setData("fraction_id", fraction);
                    player.setData("fractionrank_id", fractionrank);
                    player.setData("job_id", job);
                    player.setData("jobrank_id", jobrank);
                    player.setData("gang_id", gang);
                    player.setData("gangrank_id", gangrank);
                    player.setData("onduty_status", onduty);

                    //player.setData("mask_id", Mask);
                    player.setData("torso_id", Torso);
                    player.setData("legs_id", Legs);
                    player.setData("legstexture_id", Leg_Texture);
                    player.setData("bag_id", Bag);
                    player.setData("feet_id", Feet);
                    player.setData("feettexture_id", Feet_Texture);
                    player.setData("accessorie_id", Accessorie);
                    player.setData("accessorietexture_id", Accessorie_Texture);
                    player.setData("undershirt_id", Undershirt);
                    player.setData("undershirttexture_id", Undershirt_Texture);
                    player.setData("decal_id", Decal);
                    player.setData("top_id", Top);
                    player.setData("toptexture_id", Top_Texture);
                    player.setData("hat_id", Hat);
                    player.setData("hattexture_id", Hat_Texture);
                    player.setData("glasses_id", Glasses);
                    player.setData("glassestexture_id", Glasses_Texture);
                    player.setData("ears_id", Ears);
                    player.setData("watches_id", Watches);
                    player.setData("braceletes_id", Braceletes);

                    API.shared.triggerClientEvent(player, "UpdateMoneyHUD", Convert.ToString(cash));
                    player.freeze(false);
                    API.shared.setEntityTransparency(player, 255);
                    return true;
                }
            }
            player.setData("InGame", 0);
            Debug(1, "Неудачная попытка авторизации [" + player.name + "]" + " [" + player.address + "] " + "| неверный пароль");
            PlayerFunctions.Player.Login(player);
            API.shared.sendNotificationToPlayer(player, "~r~Неверный пароль!");
            Save_Account(player);
            connection.Close();
            return false;
        }
        public static bool playerExists(Client player)
        {
            connection = new MySqlConnection(myConnectionString);
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM accounts";

            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string name = Reader.GetString("Name");
                if (name == player.name.ToString())
                {
                    Reader.Close();
                    connection.Close();
                    return true;
                }
            }
            connection.Close();
            return false;
        }

        public static int CreateGasStation(string name, Vector3 pos, int blip)
        {
            connection.Open();
            string query = "INSERT INTO tb_gasstations (`name`, `posX`, `posY`, `posZ`, `blip`) VALUES (?name, ?posX, ?posY, ?posZ, ?blip)";
            MySqlCommand commandInsert = new MySqlCommand(query, connection);
            commandInsert.Parameters.AddWithValue("@name", name);
            commandInsert.Parameters.AddWithValue("@posX", pos.X);
            commandInsert.Parameters.AddWithValue("@posY", pos.Y);
            commandInsert.Parameters.AddWithValue("@posZ", pos.Z);
            commandInsert.Parameters.AddWithValue("@blip", blip);
            commandInsert.ExecuteNonQuery();
            connection.Close();
            return (int)commandInsert.LastInsertedId;
        }

        public static DataTable ExecutePreparedStatement(string sql, Dictionary<string, string> parameters)
        {
            MySqlConnection conn = new MySqlConnection(myConnectionString);
            using (conn)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    if (conn.State != ConnectionState.Open && conn.State != ConnectionState.Connecting)
                    {
                        conn.Open();
                    }

                    foreach (KeyValuePair<string, string> entry in parameters)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }

                    MySqlDataReader rdr = cmd.ExecuteReader();
                    DataTable results = new DataTable();
                    results.Load(rdr);
                    rdr.Close();
                    return results;
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    API.shared.consoleOutput(LogCat.Error, "DATABASE: [ERROR] " + ex.ToString());
                    Console.ResetColor();
                    return null;
                }
            }
        }

        public void LoadAllMySQLInformations()
        {
            DoorService.LoadAllDoorsFromDB();
            Arcadia.Server.Services.StorageService.LoadAllStoragesFromDB();
        }
    }
}
