using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//
//
using CherryMPServer;
using CherryMPServer.Constant;
using CherryMPShared;
//;

using MySQL;
using PlayerFunctions;

public class Faction : Script
{
    public static String GetPlayerFractionInfo(Client player, int fraction)
    {
        switch (fraction)
        {
            case 0: return "~w~Гражданский ~w~";
            case 1: return "~q~EMS ~w~";
            case 2: return "~b~Police ~w~";
            case 3: return "~r~MerryWeather ~w~";
            case 4: return "~b~IAA ~w~";
            case 5: return "~q~Redneck ~w~";
            case 6: return "~q~Humane Labs ~w~";
            case 7: return "~c~Government ~w~";
            case 8: return "~c~Army ~w~";
            default: return "";
        }
    }

    public static String GetPlayerFactionRank(Client player, int fractionrank)
    {

        int faction = (player.hasData("fraction_id")) ? player.getData("fraction_id") : 0;

        if (faction == 1)
        {
            switch (fractionrank)
            {
                case 1: return "Интерн";
                case 2: return "Доктор";
                case 3: return "Нарколог";
                case 4: return "Психиатр";
                case 5: return "Хирург";
                case 6: return "Онколог";
                case 7: return "Патологонатом";
                case 8: return "Судмедэксперт";
                case 9: return "Зам глав врача";
                case 10: return "Главный Врач";
                default: return "";
            }
        }
        if (faction == 2)
        {
            switch (fractionrank)
            {
                case 1: return "Кадет";
                case 2: return "Офицер";
                case 3: return "Младший Сержант";
                case 4: return "Сержант";
                case 5: return "Стажер Детектив";
                case 6: return "Детектив";
                case 7: return "Лейтенант";
                case 8: return "Капитан";
                case 9: return "Заместитель Шефа";
                case 10: return "Шеф Полиции";
                default: return "";
            }
        }
        if (faction == 3)
        {
            switch (fractionrank)
            {
                case 1: return "Рядовой";
                case 2: return "Специалист";
                case 3: return "Капрал";
                case 4: return "Сержант";
                case 5: return "Уорент офицер";
                case 6: return "Лейтинант";
                case 7: return "Сотрудник Special Division";
                case 8: return "Капитан";
                case 9: return "Майор";
                case 10: return "Подполковник";                
                default: return "";
            }
        }
        if (faction == 4)
        {
            switch (fractionrank)
            {
                case 1: return "Стажёр";
                case 2: return "Дежурный";
                case 3: return "Младший Агент";
                case 4: return "Агент";
                case 5: return "Старший Агент";
                case 6: return "Специальный Агент ";
                case 7: return "Секретный Агент";
                case 8: return "Агент Нац. Безопасности ";
                case 9: return "Инспектор ФБР";
                case 10: return "Директор Федерального Бюро Расследований";
                default: return "";
            }
        }
        if (faction == 5)
        {
            switch (fractionrank)
            {
                case 1: return "1";
                case 2: return "2";
                case 3: return "3";
                case 4: return "4";
                case 5: return "5";
                case 6: return "6";
                case 7: return "7";
                case 8: return "8";
                case 9: return "9";
                case 10: return "10";
                default: return "";
            }
        }
        if (faction == 6)
        {
            switch (fractionrank)
            {
                case 1: return "1";
                case 2: return "2";
                case 3: return "3";
                case 4: return "4";
                case 5: return "5";
                case 6: return "6";
                case 7: return "7";
                case 8: return "8";
                case 9: return "9";
                case 10: return "10";
                default: return "";
            }
        }
        if (faction == 7)
        {
            switch (fractionrank)
            {
                case 1: return "Стажёр";
                case 2: return "Секретарь";
                case 3: return "Служба внутренней безопасности";
                case 4: return "Помошник министра";
                case 5: return "Начальник безопасности";
                case 6: return "Министр";
                case 7: return "Верховный судья";
                case 8: return "Мэр";
                case 9: return "Вице губернатор";
                case 10: return "Губернатор";
                default: return "";
            }
        }
        if (faction == 8)
        {
            switch (fractionrank)
            {
                case 1: return "Рядовой";
                case 2: return "Специалист";
                case 3: return "Капрал";
                case 4: return "Сержант";
                case 5: return "Уорент офицер";
                case 6: return "Лейтинант";
                case 7: return "Сотрудник Special Division";
                case 8: return "Капитан";
                case 9: return "Майор";
                case 10: return "Подполковник";
                case 11: return "Полковник";
                case 12: return "Генерал";
                default: return "";
            }
        }
        else return "-";
    }
    // Наборы фракционной одежды
    public static void SetPlayerFactionClothes(Client sender)
    {
        int faction = (sender.hasData("fraction_id")) ? sender.getData("fraction_id") : 0;
        int gender = (sender.hasData("gender_id")) ? sender.getData("gender_id") : 0;
        int rank = (sender.hasData("fractionrank_id")) ? sender.getData("fractionrank_id") : 0;

        // EMS
        if (faction == 1)
        {
            if (gender == 1)
            {
                if (rank == 1)
                {
                    API.shared.setPlayerClothes(sender, 11, 86, 0);
                    API.shared.setPlayerClothes(sender, 3, 109, 0);
                    API.shared.setPlayerClothes(sender, 4, 6, 2);
                    API.shared.setPlayerClothes(sender, 6, 10, 3);
                    API.shared.setPlayerClothes(sender, 7, 96, 0);
                    API.shared.setPlayerClothes(sender, 10, 66, 0);
                    API.shared.setPlayerClothes(sender, 8, 159, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                }
                else if (rank >= 2 && rank <= 6)
                {
                    API.shared.setPlayerClothes(sender, 11, 244, 4);
                    API.shared.setPlayerClothes(sender, 3, 93, 0);
                    API.shared.setPlayerClothes(sender, 4, 37, 0);
                    API.shared.setPlayerClothes(sender, 6, 6, 0);
                    API.shared.setPlayerClothes(sender, 7, 96, 0);
                    API.shared.setPlayerClothes(sender, 10, 66, 1);
                    API.shared.setPlayerClothes(sender, 8, 159, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                }
                else if (rank >= 7 && rank <= 8)
                {
                    API.shared.setPlayerClothes(sender, 11, 9, 3);
                    API.shared.setPlayerClothes(sender, 3, 96, 0);
                    API.shared.setPlayerClothes(sender, 4, 37, 1);
                    API.shared.setPlayerClothes(sender, 6, 10, 3);
                    API.shared.setPlayerClothes(sender, 7, 96, 0);
                    API.shared.setPlayerClothes(sender, 10, 66, 1);
                    API.shared.setPlayerClothes(sender, 8, 159, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                }
                else if (rank == 9)
                {
                    API.shared.setPlayerClothes(sender, 11, 28, 3);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 36, 2);
                    API.shared.setPlayerClothes(sender, 6, 6, 0);
                    API.shared.setPlayerClothes(sender, 7, 97, 0);
                    API.shared.setPlayerClothes(sender, 8, 24, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                }
                else if (rank == 10)
                {
                    API.shared.setPlayerClothes(sender, 11, 28, 3);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 36, 2);
                    API.shared.setPlayerClothes(sender, 6, 6, 0);
                    API.shared.setPlayerClothes(sender, 7, 97, 0);
                    API.shared.setPlayerClothes(sender, 8, 24, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                return;
            }
            else
            {
                if (gender == 0)
                {
                    if (rank == 1)
                    {
                        API.shared.setPlayerClothes(sender, 11, 250, 0);
                        API.shared.setPlayerClothes(sender, 3, 85, 0);
                        API.shared.setPlayerClothes(sender, 10, 58, 0);
                        API.shared.setPlayerClothes(sender, 4, 96, 0);
                        API.shared.setPlayerClothes(sender, 6, 21, 2);
                        API.shared.setPlayerClothes(sender, 7, 127, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 8, 129, 0);
                        return;
                    }
                    else if (rank >= 2 && rank <= 6)
                    {
                        API.shared.setPlayerClothes(sender, 11, 12, 0);
                        API.shared.setPlayerClothes(sender, 7, 127, 0);
                        API.shared.setPlayerClothes(sender, 4, 20, 0);
                        API.shared.setPlayerClothes(sender, 6, 21, 2);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 8, 129, 0);
                        return;
                    }
                    else if (rank >= 7 && rank <= 8)
                    {
                        API.shared.setPlayerClothes(sender, 11, 250, 1);
                        API.shared.setPlayerClothes(sender, 3, 85, 0);
                        API.shared.setPlayerClothes(sender, 10, 58, 0);
                        API.shared.setPlayerClothes(sender, 4, 96, 1);
                        API.shared.setPlayerClothes(sender, 6, 21, 2);
                        API.shared.setPlayerClothes(sender, 7, 126, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 8, 129, 0);

                        return;
                    }
                    else if (rank == 9)
                    {
                        API.shared.setPlayerClothes(sender, 11, 100, 1);
                        API.shared.setPlayerClothes(sender, 4, 23, 9);
                        API.shared.setPlayerClothes(sender, 8, 31, 2);
                        API.shared.setPlayerClothes(sender, 6, 10, 0);
                        API.shared.setPlayerClothes(sender, 7, 126, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        return;
                    }
                    else if (rank == 10)
                    {
                        API.shared.setPlayerClothes(sender, 11, 100, 2);
                        API.shared.setPlayerClothes(sender, 4, 23, 4);
                        API.shared.setPlayerClothes(sender, 8, 31, 2);
                        API.shared.setPlayerClothes(sender, 6, 10, 0);
                        API.shared.setPlayerClothes(sender, 7, 126, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        return;
                    }
                    return;
                }
                else
                {
                    return;
                }
            }
        }
        // Police
        if (faction == 2)
        {
            if (gender == 1)
            {
                if (rank >= 1 && rank <= 1)
                {
                    API.shared.setPlayerClothes(sender, 11, 48, 0);
                    API.shared.setPlayerClothes(sender, 3, 14, 0);
                    API.shared.setPlayerClothes(sender, 4, 34, 0);
                    API.shared.setPlayerClothes(sender, 6, 25, 0);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 8, 159, 0);
                    API.shared.setPlayerClothes(sender, 10, 8, 1);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                else if (rank >= 2 && rank <= 4)
                {
                    API.shared.setPlayerClothes(sender, 11, 48, 0);
                    API.shared.setPlayerClothes(sender, 3, 14, 0);
                    API.shared.setPlayerClothes(sender, 4, 34, 0);
                    API.shared.setPlayerClothes(sender, 6, 25, 0);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 8, 35, 0);
                    API.shared.setPlayerClothes(sender, 10, 8, 2);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                else if (rank >= 5 && rank <= 6)
                {
                    API.shared.setPlayerClothes(sender, 11, 9, 1);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 37, 0);
                    API.shared.setPlayerClothes(sender, 6, 0, 0);
                    API.shared.setPlayerClothes(sender, 7, 95, 0);
                    API.shared.setPlayerClothes(sender, 8, 2, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                else if (rank >= 7 && rank <= 10)
                {
                    API.shared.setPlayerClothes(sender, 11, 48, 0);
                    API.shared.setPlayerClothes(sender, 3, 14, 0);
                    API.shared.setPlayerClothes(sender, 4, 34, 0);
                    API.shared.setPlayerClothes(sender, 6, 25, 0);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 8, 35, 0);
                    API.shared.setPlayerClothes(sender, 10, 8, 3);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                return;
            }
            else
            {
                if (gender == 0)
                {
                    if (rank >= 1 && rank <= 1)
                    {
                        API.shared.setPlayerClothes(sender, 8, 129, 0);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerClothes(sender, 4, 35, 0);
                        API.shared.setPlayerClothes(sender, 11, 55, 0);
                        API.shared.setPlayerClothes(sender, 3, 0, 0);
                        API.shared.setPlayerClothes(sender, 10, 8, 1);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        return;
                    }
                    else if (rank >= 2 && rank <= 4)
                    {
                        API.shared.setPlayerClothes(sender, 8, 58, 0);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerClothes(sender, 4, 35, 0);
                        API.shared.setPlayerClothes(sender, 11, 55, 0);
                        API.shared.setPlayerClothes(sender, 3, 0, 0);
                        API.shared.setPlayerClothes(sender, 10, 8, 2);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        return;
                    }
                    else if (rank >= 5 && rank <= 6)
                    {
                        API.shared.setPlayerClothes(sender, 11, 26, 0);
                        API.shared.setPlayerClothes(sender, 7, 125, 0);
                        API.shared.setPlayerClothes(sender, 4, 10, 0);
                        API.shared.setPlayerClothes(sender, 3, 11, 0);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 8, 57, 0);
                        return;
                    }
                    else if (rank >= 7 && rank <= 10)
                    {
                        API.shared.setPlayerClothes(sender, 11, 26, 0);
                        API.shared.setPlayerClothes(sender, 7, 125, 0);
                        API.shared.setPlayerClothes(sender, 4, 10, 0);
                        API.shared.setPlayerClothes(sender, 3, 11, 0);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 8, 58, 0);
                        return;
                    }
                    return;
                }
                else
                {
                    return;
                }
            }
        }
        // Меривезер
        if (faction == 3)
        {
            if (gender == 1)
            {
                if (rank >= 1 && rank <= 1)
                {
                    API.shared.setPlayerClothes(sender, 6, 24, 0);
                    API.shared.setPlayerClothes(sender, 4, 48, 0);
                    API.shared.setPlayerClothes(sender, 3, 20, 0);
                    API.shared.setPlayerClothes(sender, 11, 49, 0);
                    API.shared.setPlayerClothes(sender, 8, 159, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                else if (rank >= 2 && rank <= 5)
                {
                    API.shared.setPlayerClothes(sender, 3, 24, 0);
                    API.shared.setPlayerClothes(sender, 11, 233, 0);
                    API.shared.setPlayerClothes(sender, 6, 24, 0);
                    API.shared.setPlayerClothes(sender, 4, 33, 0);
                    API.shared.setPlayerClothes(sender, 8, 159, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                else if (rank >= 6 && rank <= 7)
                {
                    API.shared.setPlayerClothes(sender, 11, 259, 25);
                    API.shared.setPlayerClothes(sender, 4, 101, 25);
                    API.shared.setPlayerClothes(sender, 6, 24, 0);
                    API.shared.setPlayerClothes(sender, 3, 24, 0);
                    API.shared.setPlayerClothes(sender, 8, 159, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                else if (rank >= 8 && rank <= 9)
                {
                    API.shared.setPlayerClothes(sender, 11, 259, 17);
                    API.shared.setPlayerClothes(sender, 4, 101, 17);
                    API.shared.setPlayerClothes(sender, 6, 24, 0);
                    API.shared.setPlayerClothes(sender, 3, 24, 0);
                    API.shared.setPlayerClothes(sender, 8, 159, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                else if (rank >= 10 && rank <= 10)
                {
                    API.shared.setPlayerClothes(sender, 11, 259, 21);
                    API.shared.setPlayerClothes(sender, 4, 101, 21);
                    API.shared.setPlayerClothes(sender, 6, 24, 0);
                    API.shared.setPlayerClothes(sender, 3, 24, 0);
                    API.shared.setPlayerClothes(sender, 8, 159, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                return;
            }
            else
            {
                if (gender == 0)
                {
                    if (rank >= 1 && rank <= 1)
                    {
                        API.shared.setPlayerClothes(sender, 11, 16, 0);
                        API.shared.setPlayerClothes(sender, 4, 46, 0);
                        API.shared.setPlayerClothes(sender, 6, 24, 0);
                        API.shared.setPlayerClothes(sender, 3, 19, 0);
                        API.shared.setPlayerClothes(sender, 8, 15, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        return;
                    }
                    else if (rank >= 2 && rank <= 4)
                    {

                        API.shared.setPlayerClothes(sender, 11, 50, 0);
                        API.shared.setPlayerClothes(sender, 3, 27, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 4, 9, 7);
                        API.shared.setPlayerClothes(sender, 6, 24, 0);
                        API.shared.setPlayerClothes(sender, 8, 15, 0);
                        return;
                    }
                    else if (rank >= 5 && rank <= 6)
                    {
                        API.shared.setPlayerClothes(sender, 11, 223, 0);
                        API.shared.setPlayerClothes(sender, 3, 21, 0);
                        API.shared.setPlayerClothes(sender, 4, 9, 13);
                        API.shared.setPlayerClothes(sender, 6, 24, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 8, 15, 0);
                        return;
                    }

                    else if (rank >= 7 && rank <= 7)
                    {
                        API.shared.setPlayerClothes(sender, 11, 251, 25);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 4, 98, 25);
                        API.shared.setPlayerClothes(sender, 3, 115, 0);
                        API.shared.setPlayerClothes(sender, 6, 24, 0);
                        API.shared.setPlayerClothes(sender, 8, 15, 0);
                        return;
                    }
                    else if (rank >= 8 && rank <= 8)
                    {
                        API.shared.setPlayerClothes(sender, 11, 251, 17);
                        API.shared.setPlayerClothes(sender, 4, 98, 17);
                        API.shared.setPlayerClothes(sender, 3, 115, 0);
                        API.shared.setPlayerClothes(sender, 6, 24, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 8, 15, 0);
                        return;
                    }
                    else if (rank >= 9 && rank <= 9)
                    {
                        API.shared.setPlayerClothes(sender, 11, 251, 21);
                        API.shared.setPlayerClothes(sender, 4, 98, 21);
                        API.shared.setPlayerClothes(sender, 3, 115, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 6, 24, 0);
                        API.shared.setPlayerClothes(sender, 8, 15, 0);
                        return;
                    }
                    else if (rank >= 10 && rank <= 10)
                    {
                        API.shared.setPlayerClothes(sender, 11, 31, 0);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 0);
                        API.shared.setPlayerAccessory(sender, 0, 8, 0);
                        API.shared.setPlayerClothes(sender, 6, 10, 0);
                        API.shared.setPlayerClothes(sender, 8, 75, 3);
                        return;
                    }
                    return;
                }
                else
                {
                    return;
                }
            }
        }

        // FIB
        if (faction == 4)
        {
            if (gender == 1)
            {
                if (rank == 1)
                {
                    API.shared.setPlayerClothes(sender, 4, 34, 0);
                    API.shared.setPlayerClothes(sender, 11, 27, 5);
                    API.shared.setPlayerClothes(sender, 7, 98, 0);
                    API.shared.setPlayerClothes(sender, 8, 2, 0);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerAccessory(sender, 6, 2, 3);
                    API.shared.setPlayerClothes(sender, 6, 0, 0);
                }
                else if (rank >= 2 && rank <= 4)
                {
                    API.shared.setPlayerClothes(sender, 8, 160, 0);
                    API.shared.setPlayerClothes(sender, 4, 34, 0);
                    API.shared.setPlayerClothes(sender, 11, 27, 5);
                    API.shared.setPlayerClothes(sender, 7, 98, 0);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerClothes(sender, 6, 0, 0);
                    API.shared.setPlayerAccessory(sender, 6, 2, 3);
                }
                else if (rank >= 5 && rank <= 8)
                {
                    API.shared.setPlayerClothes(sender, 11, 139, 0);
                    API.shared.setPlayerClothes(sender, 8, 37, 0);
                    API.shared.setPlayerClothes(sender, 4, 6, 0);
                    API.shared.setPlayerClothes(sender, 6, 64, 0);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerAccessory(sender, 1, 11, 2);
                }
                else if (rank >= 9)
                {
                    API.shared.setPlayerClothes(sender, 11, 57, 0);
                    API.shared.setPlayerClothes(sender, 8, 41, 2);
                    API.shared.setPlayerClothes(sender, 4, 6, 0);
                    API.shared.setPlayerClothes(sender, 6, 8, 0);
                    API.shared.setPlayerClothes(sender, 7, 98, 0);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerAccessory(sender, 1, 11, 2);
                }
                return;
            }
            else
            {
                if (gender == 0)
                {
                    if (rank >= 1 && rank <= 1)
                    {
                        API.shared.setPlayerClothes(sender, 11, 243, 2);
                        API.shared.setPlayerClothes(sender, 8, 15, 0);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 94, 2);
                        API.shared.setPlayerClothes(sender, 6, 69, 20);
                        return;
                    }
                    else if (rank >= 2 && rank <= 2)
                    {
                        API.shared.setPlayerClothes(sender, 11, 139, 2);
                        API.shared.setPlayerClothes(sender, 8, 129, 0);
                        API.shared.setPlayerClothes(sender, 3, 4, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 0);
                        API.shared.setPlayerClothes(sender, 6, 59, 20);
                    }
                    else if (rank >= 3 && rank <= 3)
                    {
                        API.shared.setPlayerClothes(sender, 11, 13, 2);
                        API.shared.setPlayerClothes(sender, 8, 130, 0);
                        API.shared.setPlayerClothes(sender, 3, 11, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 12);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerClothes(sender, 7, 128, 0);
                    }
                    else if (rank >= 4 && rank <= 4)
                    {
                        API.shared.setPlayerClothes(sender, 11, 95, 1);
                        API.shared.setPlayerClothes(sender, 8, 122, 0);
                        API.shared.setPlayerClothes(sender, 3, 11, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 4);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerClothes(sender, 7, 125, 0);
                    }
                    else if (rank >= 5 && rank <= 5)
                    {
                        API.shared.setPlayerClothes(sender, 11, 192, 5);
                        API.shared.setPlayerClothes(sender, 8, 34, 0);
                        API.shared.setPlayerClothes(sender, 3, 11, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 7);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerClothes(sender, 7, 128, 0);
                    }
                    else if (rank >= 6 && rank <= 6)
                    {
                        API.shared.setPlayerClothes(sender, 11, 139, 3);
                        API.shared.setPlayerClothes(sender, 8, 122, 1);
                        API.shared.setPlayerClothes(sender, 3, 17, 0);
                        API.shared.setPlayerClothes(sender, 4, 31, 0);
                        API.shared.setPlayerClothes(sender, 6, 24, 0);
                        API.shared.setPlayerClothes(sender, 7, 125, 2);
                        API.shared.setPlayerAccessory(sender, 0, 58, 3);
                    }
                    else if (rank >= 7 && rank <= 7)
                    {
                        API.shared.setPlayerClothes(sender, 11, 142, 6);
                        API.shared.setPlayerClothes(sender, 8, 11, 0);
                        API.shared.setPlayerClothes(sender, 3, 4, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 0);
                        API.shared.setPlayerClothes(sender, 6, 10, 0);
                        API.shared.setPlayerClothes(sender, 7, 23, 12);
                        API.shared.setPlayerAccessory(sender, 0, 5, 0);
                    }
                    else if (rank >= 8 && rank <= 8)
                    {
                        API.shared.setPlayerClothes(sender, 11, 53, 3);
                        API.shared.setPlayerClothes(sender, 8, 122, 0);
                        API.shared.setPlayerClothes(sender, 3, 17, 0);
                        API.shared.setPlayerClothes(sender, 4, 9, 0);
                        API.shared.setPlayerClothes(sender, 6, 63, 1);
                        API.shared.setPlayerClothes(sender, 7, 125, 0);
                        API.shared.setPlayerAccessory(sender, 0, 106, 20);
                        API.shared.setPlayerAccessory(sender, 1, 5, 0);
                    }
                    else if (rank >= 9 && rank <= 9)
                    {
                        API.shared.setPlayerClothes(sender, 11, 115, 0);
                        API.shared.setPlayerClothes(sender, 8, 33, 0);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 0);
                        API.shared.setPlayerClothes(sender, 6, 10, 0);
                        API.shared.setPlayerClothes(sender, 7, 26, 2);
                        API.shared.setPlayerAccessory(sender, 1, 3, 5);
                    }
                    else if (rank >= 10 && rank <= 10)
                    {
                        API.shared.setPlayerClothes(sender, 11, 27, 0);
                        API.shared.setPlayerClothes(sender, 8, 21, 0);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 0);
                        API.shared.setPlayerClothes(sender, 6, 10, 0);
                        API.shared.setPlayerClothes(sender, 7, 20, 1);
                    }
                    return;
                }
            }
        }
        // Rednecks
        if (faction == 5)
        {
            if (gender == 1)
            {
                if (rank <= 2)
                {
                    API.shared.setPlayerClothes(sender, 11, 171, 4);
                    API.shared.setPlayerClothes(sender, 3, 15, 0);
                    API.shared.setPlayerClothes(sender, 4, 25, 2);
                    API.shared.setPlayerClothes(sender, 6, 38, 1);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 8, 2, 0);
                    API.shared.setPlayerAccessory(sender, 0, 20, 0);
                    return;
                }
                else if (rank >= 3 && rank <= 4)
                {
                    API.shared.setPlayerClothes(sender, 11,171,4);
                    API.shared.setPlayerClothes(sender, 8,2,0);
                    API.shared.setPlayerClothes(sender, 4, 25, 3);
                    API.shared.setPlayerClothes(sender, 6, 45, 0);
                    API.shared.setPlayerClothes(sender, 3, 15, 0);
                    API.shared.setPlayerAccessory(sender, 0, 20, 0);
                    return;
                }
                else if (rank >= 5 && rank <= 6)
                {
                    API.shared.setPlayerClothes(sender, 11, 169, 4);
                    API.shared.setPlayerClothes(sender, 8, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 14, 1);
                    API.shared.setPlayerClothes(sender, 6, 21, 1);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerAccessory(sender, 0, 20, 6);
                    return;
                }
                else if (rank >= 7 && rank <= 7)
                {
                    API.shared.setPlayerClothes(sender, 11, 9, 7);
                    API.shared.setPlayerClothes(sender, 8, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 4, 12);
                    API.shared.setPlayerClothes(sender, 6, 3, 3);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerAccessory(sender, 0, 13, 7);
                    return;
                }
                else if (rank >= 8 && rank <= 8)
                {
                    API.shared.setPlayerClothes(sender, 11, 109, 10);
                    API.shared.setPlayerClothes(sender, 8, 17, 0);
                    API.shared.setPlayerClothes(sender, 4, 51, 4);
                    API.shared.setPlayerClothes(sender, 6, 13, 2);
                    API.shared.setPlayerClothes(sender, 3, 1, 0);
                    API.shared.setPlayerAccessory(sender, 0, 13, 7);
                    return;
                }
                else if (rank >= 9 && rank <= 9)
                {
                    API.shared.setPlayerClothes(sender, 11, 9, 1);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 37, 0);
                    API.shared.setPlayerClothes(sender, 6, 0, 0);
                    API.shared.setPlayerClothes(sender, 7, 95, 0);
                    API.shared.setPlayerClothes(sender, 8, 160, 0);
                    return;
                }
                else if (rank >= 10 && rank <= 10)
                {
                    API.shared.setPlayerClothes(sender, 11, 7, 1);
                    API.shared.setPlayerClothes(sender, 8, 16, 0);
                    API.shared.setPlayerClothes(sender, 4, 50, 0);
                    API.shared.setPlayerClothes(sender, 6, 6, 2);
                    API.shared.setPlayerClothes(sender, 3, 6, 0);                    
                    return;
                }
                return;
            }
            else
            {
                if (gender == 0)
                {
                    if (rank <= 3)
                    {
                        API.shared.setPlayerClothes(sender, 11, 43, 0);
                        API.shared.setPlayerClothes(sender, 8, 15, 0);
                        API.shared.setPlayerClothes(sender, 4, 12, 12);
                        API.shared.setPlayerClothes(sender, 6, 0, 0);
                        API.shared.setPlayerClothes(sender, 3, 11, 0);
                        
                        return;
                    }
                    else if (rank >= 4 && rank <= 7)
                    {
                        API.shared.setPlayerClothes(sender, 11, 1, 4);
                        API.shared.setPlayerClothes(sender, 8, 15, 0);
                        API.shared.setPlayerClothes(sender, 4, 90, 1);
                        API.shared.setPlayerClothes(sender, 6, 27, 0);
                        API.shared.setPlayerClothes(sender, 3, 0, 0);
                        API.shared.setPlayerAccessory(sender, 0, 13, 1);
                        return;
                    }
                    else if (rank >= 5 && rank <= 8)
                    {
                        API.shared.setPlayerClothes(sender, 11, 238, 4);
                        API.shared.setPlayerClothes(sender, 8, 15, 0);
                        API.shared.setPlayerClothes(sender, 4, 63, 2);
                        API.shared.setPlayerClothes(sender, 6, 45, 0);
                        API.shared.setPlayerClothes(sender, 3, 2, 0);                        
                        return;
                    }
                    else if (rank >= 7 && rank <= 9)
                    {
                        API.shared.setPlayerClothes(sender, 11, 41, 19);
                        API.shared.setPlayerClothes(sender, 8, 8, 15);
                        API.shared.setPlayerClothes(sender, 4, 76, 3);
                        API.shared.setPlayerClothes(sender, 6, 38, 0);
                        API.shared.setPlayerClothes(sender, 3, 5, 0);
                        API.shared.setPlayerAccessory(sender, 0, 13, 1);
                        return;
                    }
                    else if (rank >= 8 && rank <= 10)
                    {
                        API.shared.setPlayerClothes(sender, 11, 35, 6);
                        API.shared.setPlayerClothes(sender, 8, 32, 14);
                        API.shared.setPlayerClothes(sender, 4, 0, 3);
                        API.shared.setPlayerClothes(sender, 6, 21, 10);
                        API.shared.setPlayerClothes(sender, 3, 12, 0);
                        API.shared.setPlayerAccessory(sender, 0, 13, 5);
                        return;
                    }                    
                    return;
                }
            }
        }
        // HumaneLabs
        if (faction == 6)
        {
            if (gender == 1)
            {
                if (rank <= 3)
                {
                    API.shared.setPlayerClothes(sender, 11, 61, 0);
                    API.shared.setPlayerClothes(sender, 3, 88, 0);
                    API.shared.setPlayerClothes(sender, 4, 40, 0);
                    API.shared.setPlayerClothes(sender, 6, 25, 0);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 8, 43, 0);
                    API.shared.setPlayerClothes(sender, 1, 46, 0);
                    API.shared.setPlayerAccessory(sender, 0, 120, 0);
                    return;
                }
                else if (rank >= 8 && rank <= 10)
                {
                    API.shared.setPlayerClothes(sender, 11, 9, 1);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 37, 0);
                    API.shared.setPlayerClothes(sender, 6, 0, 0);
                    API.shared.setPlayerClothes(sender, 7, 95, 0);
                    API.shared.setPlayerClothes(sender, 8, 160, 0);
                    return;
                }
                return;
            }
            else
            {
                return;
            }
        }

        // Правительство
        if (faction == 7)
        {
            if (gender == 1)
            {
                if (rank >= 1 && rank <= 1)
                {
                    API.shared.setPlayerClothes(sender, 11, 27, 0);
                    API.shared.setPlayerClothes(sender, 8, 159, 0);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 37, 7);
                    API.shared.setPlayerClothes(sender, 6, 29, 2);
                    return;
                }
                else if (rank >= 2 && rank <= 2)
                {
                    API.shared.setPlayerClothes(sender, 11, 6, 1);
                    API.shared.setPlayerClothes(sender, 8, 41, 2);
                    API.shared.setPlayerClothes(sender, 3, 1, 0);
                    API.shared.setPlayerClothes(sender, 4, 7, 1);
                    API.shared.setPlayerClothes(sender, 6, 6, 0);
                    API.shared.setPlayerClothes(sender, 7, 22, 0);
                }
                else if (rank >= 3 && rank <= 3)
                {
                    API.shared.setPlayerClothes(sender, 11, 93, 2);
                    API.shared.setPlayerClothes(sender, 8, 38, 0);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 52, 0);
                    API.shared.setPlayerClothes(sender, 6, 29, 0);
                    API.shared.setPlayerClothes(sender, 7, 22, 0);
                    API.shared.setPlayerAccessory(sender, 1, 25, 9);
                }
                else if (rank >= 4 && rank <= 4)
                {
                    API.shared.setPlayerClothes(sender, 11, 91, 1);
                    API.shared.setPlayerClothes(sender, 8, 41, 2);
                    API.shared.setPlayerClothes(sender, 3, 3, 0);
                    API.shared.setPlayerClothes(sender, 4, 52, 2);
                    API.shared.setPlayerClothes(sender, 6, 6, 0);
                    API.shared.setPlayerClothes(sender, 7, 28, 0);
                }
                else if (rank >= 5 && rank <= 5)
                {
                    API.shared.setPlayerClothes(sender, 11, 137, 3);
                    API.shared.setPlayerClothes(sender, 8, 66, 3);
                    API.shared.setPlayerClothes(sender, 3, 3, 0);
                    API.shared.setPlayerClothes(sender, 4, 7, 1);
                    API.shared.setPlayerClothes(sender, 6, 6, 0);
                }
                else if (rank >= 6 && rank <= 6)
                {
                    API.shared.setPlayerClothes(sender, 11, 70, 1);
                    API.shared.setPlayerClothes(sender, 8, 41, 2);
                    API.shared.setPlayerClothes(sender, 3, 1, 0);
                    API.shared.setPlayerClothes(sender, 4, 7, 23);
                    API.shared.setPlayerClothes(sender, 6, 6, 0);
                    API.shared.setPlayerClothes(sender, 7, 22, 10);
                }
                else if (rank >= 7 && rank <= 7)
                {
                    API.shared.setPlayerClothes(sender, 11, 139, 0);
                    API.shared.setPlayerClothes(sender, 8, 41, 2);
                    API.shared.setPlayerClothes(sender, 3, 1, 0);
                    API.shared.setPlayerClothes(sender, 4, 67, 0);
                    API.shared.setPlayerClothes(sender, 6, 6, 0);
                    API.shared.setPlayerClothes(sender, 7, 22, 10);
                }
                else if (rank >= 8 && rank <= 8)
                {
                    API.shared.setPlayerClothes(sender, 11, 66, 0);
                    API.shared.setPlayerClothes(sender, 8, 38, 11);
                    API.shared.setPlayerClothes(sender, 3, 1, 0);
                    API.shared.setPlayerClothes(sender, 4, 36, 2);
                    API.shared.setPlayerClothes(sender, 6, 6, 0);
                    API.shared.setPlayerClothes(sender, 7, 86, 1);
                }
                else if (rank >= 9 && rank <= 9)
                {
                    API.shared.setPlayerClothes(sender, 11, 194, 0);
                    API.shared.setPlayerClothes(sender, 8, 38, 2);
                    API.shared.setPlayerClothes(sender, 3, 1, 0);
                    API.shared.setPlayerClothes(sender, 4, 36, 0);
                    API.shared.setPlayerClothes(sender, 6, 42, 2);
                    API.shared.setPlayerClothes(sender, 7, 86, 1);
                }
                else if (rank >= 10 && rank <= 10)
                {
                    API.shared.setPlayerClothes(sender, 11, 194, 0);
                    API.shared.setPlayerClothes(sender, 8, 38, 0);
                    API.shared.setPlayerClothes(sender, 3, 1, 0);
                    API.shared.setPlayerClothes(sender, 4, 37, 1);
                    API.shared.setPlayerClothes(sender, 6, 6, 0);
                    API.shared.setPlayerClothes(sender, 7, 86, 0);
                }
                return;
            }
            else
            {
                if (gender == 0)
                {
                    if (rank >= 1 && rank <= 1)
                    {
                        API.shared.setPlayerClothes(sender, 11, 13, 0);
                        API.shared.setPlayerClothes(sender, 8, 122, 0);
                        API.shared.setPlayerClothes(sender, 3, 11, 0);
                        API.shared.setPlayerClothes(sender, 4, 10, 0);
                        API.shared.setPlayerClothes(sender, 6, 12, 6);
                        API.shared.setPlayerClothes(sender, 7, 38, 0);
                        return;
                    }
                    else if (rank >= 2 && rank <= 2)
                    {
                        API.shared.setPlayerClothes(sender, 11, 25, 7);
                        API.shared.setPlayerClothes(sender, 8, 6, 0);
                        API.shared.setPlayerClothes(sender, 3, 11, 0);
                        API.shared.setPlayerClothes(sender, 4, 22, 4);
                        API.shared.setPlayerClothes(sender, 6, 21, 6);
                        API.shared.setPlayerClothes(sender, 7, 26, 2);
                    }
                    else if (rank >= 3 && rank <= 3)
                    {
                        API.shared.setPlayerClothes(sender, 11, 28, 0);
                        API.shared.setPlayerClothes(sender, 8, 96, 16);
                        API.shared.setPlayerClothes(sender, 3, 12, 0);
                        API.shared.setPlayerClothes(sender, 4, 10, 4);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerClothes(sender, 7, 28, 2);
                        API.shared.setPlayerClothes(sender, 1, 121, 0);
                    }
                    else if (rank >= 4 && rank <= 4)
                    {
                        API.shared.setPlayerClothes(sender, 11, 4, 0);
                        API.shared.setPlayerClothes(sender, 8, 4, 1);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 0);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerClothes(sender, 7, 25, 0);
                    }
                    else if (rank >= 5 && rank <= 5)
                    {
                        API.shared.setPlayerClothes(sender, 11, 28, 0);
                        API.shared.setPlayerClothes(sender, 8, 31, 2);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 10, 0);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerClothes(sender, 7, 10, 0);
                    }
                    else if (rank >= 6 && rank <= 6)
                    {
                        API.shared.setPlayerClothes(sender, 11, 27, 0);
                        API.shared.setPlayerClothes(sender, 8, 10, 0);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 4);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerClothes(sender, 7, 20, 5);
                    }
                    else if (rank >= 7 && rank <= 7)
                    {
                        API.shared.setPlayerClothes(sender, 11, 142, 0);
                        API.shared.setPlayerClothes(sender, 8, 10, 0);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 25, 0);
                        API.shared.setPlayerClothes(sender, 6, 21, 0);
                        API.shared.setPlayerClothes(sender, 7, 21, 2);
                    }
                    else if (rank >= 8 && rank <= 8)
                    {
                        API.shared.setPlayerClothes(sender, 11, 28, 2);
                        API.shared.setPlayerClothes(sender, 8, 28, 0);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 10, 2);
                        API.shared.setPlayerClothes(sender, 6, 20, 0);
                        API.shared.setPlayerClothes(sender, 7, 115, 1);
                    }
                    else if (rank >= 9 && rank <= 9)
                    {
                        API.shared.setPlayerClothes(sender, 11, 192, 0);
                        API.shared.setPlayerClothes(sender, 8, 26, 12);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 22, 12);
                        API.shared.setPlayerClothes(sender, 6, 20, 0);
                        API.shared.setPlayerClothes(sender, 7, 24, 5);
                    }
                    else if (rank >= 10 && rank <= 10)
                    {
                        API.shared.setPlayerClothes(sender, 11, 29, 5);
                        API.shared.setPlayerClothes(sender, 8, 31, 0);
                        API.shared.setPlayerClothes(sender, 3, 1, 0);
                        API.shared.setPlayerClothes(sender, 4, 20, 0);
                        API.shared.setPlayerClothes(sender, 6, 20, 0);
                        API.shared.setPlayerClothes(sender, 7, 115, 0);
                    }
                    return;
                }
            }
        }

        //army
        if (faction == 8)
        {
            if (gender == 1)
            {
                if (rank == 1)
                {
                    API.shared.setPlayerClothes(sender, 3, 4, 0);
                    API.shared.setPlayerClothes(sender, 11, 210, 1);
                    API.shared.setPlayerClothes(sender, 4, 89, 1);
                    API.shared.setPlayerClothes(sender, 6, 64, 2);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 8, 2, 0);
                    API.shared.setPlayerAccessory(sender, 0, 58, 0);
                }
                else if (rank >= 2 && rank <= 4)
                {
                    API.shared.setPlayerClothes(sender, 11, 230, 1);
                    API.shared.setPlayerClothes(sender, 4, 89, 1);
                    API.shared.setPlayerClothes(sender, 6, 64, 2);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 8, 2, 0);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                }
                else if (rank >= 5 && rank <= 6)
                {
                    API.shared.setPlayerClothes(sender, 11, 232, 1);
                    API.shared.setPlayerClothes(sender, 4, 90, 1);
                    API.shared.setPlayerClothes(sender, 3, 4, 0);
                    API.shared.setPlayerClothes(sender, 6, 77, 1);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 8, 2, 0);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                }
                else if (rank >= 7 && rank <= 8)
                {
                    API.shared.setPlayerClothes(sender, 11, 216, 1);
                    API.shared.setPlayerClothes(sender, 8, 128, 1);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 89, 1);
                    API.shared.setPlayerClothes(sender, 6, 64, 2);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                }
                else if (rank >= 9 && rank <= 10)
                {
                    API.shared.setPlayerClothes(sender, 11, 230, 5);
                    API.shared.setPlayerClothes(sender, 8, 2, 0);
                    API.shared.setPlayerClothes(sender, 7, 0, 0);
                    API.shared.setPlayerClothes(sender, 4, 89, 5);
                    API.shared.setPlayerClothes(sender, 6, 64, 2);
                    API.shared.setPlayerClothes(sender, 3, 0, 0);
                }
                return;
            }
            else
            {
                if (gender == 0)
                {
                    if (rank == 1)
                    {
                        API.shared.setPlayerClothes(sender, 9, 0, 0);
                        API.shared.setPlayerClothes(sender, 3, 0, 0);
                        API.shared.setPlayerClothes(sender, 4, 86, 1);
                        API.shared.setPlayerClothes(sender, 7, 0, 0);
                        API.shared.setPlayerClothes(sender, 8, 57, 0);
                        API.shared.setPlayerClothes(sender, 11, 208, 1);
                        API.shared.setPlayerAccessory(sender, 0, 58, 0);
                    }
                    else if (rank >= 2 && rank <= 4)
                    {
                        API.shared.setPlayerClothes(sender, 11, 222, 1);
                        API.shared.setPlayerClothes(sender, 4, 8, 1);
                        API.shared.setPlayerClothes(sender, 6, 35, 0);
                        API.shared.setPlayerClothes(sender, 8, 57, 0);
                        API.shared.setPlayerClothes(sender, 3, 0, 0);
                        API.shared.setPlayerClothes(sender, 7, 0, 0);
                        API.shared.setPlayerClothes(sender, 9, 0, 0);
                    }
                    else if (rank >= 5 && rank <= 8)
                    {
                        API.shared.setPlayerClothes(sender, 11, 220, 1);
                        API.shared.setPlayerClothes(sender, 4, 86, 1);
                        API.shared.setPlayerClothes(sender, 3, 12, 0);
                        API.shared.setPlayerClothes(sender, 8, 57, 0);
                        API.shared.setPlayerClothes(sender, 7, 0, 0);
                        API.shared.setPlayerClothes(sender, 6, 63, 0);
                    }
                    else if (rank >= 9 && rank <= 10)
                    {
                        API.shared.setPlayerClothes(sender, 11, 222, 5);
                        API.shared.setPlayerClothes(sender, 4, 8, 5);
                        API.shared.setPlayerClothes(sender, 6, 35, 0);
                        API.shared.setPlayerClothes(sender, 7, 0, 0);
                        API.shared.setPlayerClothes(sender, 8, 57, 0);
                        API.shared.setPlayerClothes(sender, 3, 0, 0);
                        API.shared.setPlayerClothes(sender, 9, 0, 0);
                    }
                    return;
                }
            }
        }

        else return;
    }

    [Command("invite")]
    public void FactionInviteCommand(Client sender, string idOrName)
    {
        int rank = Player.GetFractionRank(sender);
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        int factionIDOfTarget = Player.GetFractionId(target);
        int factionIDOfSender = Player.GetFractionId(sender);
        var inviteMessageTarget = "~b~" + sender.name + "~w~ принял вас во фракцию " + Faction.GetPlayerFractionInfo(sender, Player.GetFractionId(sender));
        var inviteMessageSender = "~b~" + target.name + "~w~ был принят вами во фракцию " + Faction.GetPlayerFractionInfo(sender, Player.GetFractionId(sender));

        if (Player.GetFractionRank(sender) <= 8)
        {
            API.sendChatMessageToPlayer(sender, "~r~Ваш ранг не позволяет вам использовать данную команду");
        }
        else
        {
            if (factionIDOfSender == factionIDOfTarget)
            {
                API.sendChatMessageToPlayer(sender, "~b~ " + target.name + "~y~ уже состоит в вашей фракции");
                return;
            }
            else
            {
                Player.SetFactionID(target, factionIDOfSender);
                API.sendChatMessageToPlayer(sender, inviteMessageSender);
                API.sendChatMessageToPlayer(target, inviteMessageTarget);
            }
        }
    }

    [Command("uninvite")]
    public void FactionUninviteCommand(Client sender, string idOrName)
    {
        int rank = Player.GetFractionRank(sender);
        Client target = API.exported.playerids.findPlayer(sender, idOrName);
        int FactionIDofTarget = Player.GetFractionId(target);
        int factionIDOfSender = Player.GetFractionId(sender);
        var uninviteMessageTarget = "~b~" + sender.name + "~w~ уволил вас из фракции " + Faction.GetPlayerFractionInfo(sender, Player.GetFractionId(sender));
        var uninviteMessageSender = "~b~" + target.name + "~w~ был уволен вами из фракции " + Faction.GetPlayerFractionInfo(sender, Player.GetFractionId(sender));

        if (Player.GetFractionRank(sender) <= 8)
        {
            API.sendChatMessageToPlayer(sender, "~r~Ваш ранг не позволяет вам использовать данную команду");
        }
        else
        {
            if (FactionIDofTarget == 0)
            {
                API.sendChatMessageToPlayer(sender, "~b~ " + target.name + "~y~ не состоит в вашей фракции");
                return;
            }
            else
            {
                Player.SetFactionID(target, 1);
                API.sendChatMessageToPlayer(sender, uninviteMessageSender);
                API.sendChatMessageToPlayer(target, uninviteMessageTarget);
            }
        }
    }

    [Command("giverank")]
    public void GiveRankCommand(Client sender, string idOrName, int giverank)
    {
        Client target = API.exported.playerids.findPlayer(sender, idOrName);

        int senderfaction = Player.GetFractionId(sender);
        int targetfaction = Player.GetFractionId(target);

        var rankupmessage = "~r~" + sender.name + "~w~ повысил вас до " + giverank + " ранга";
        var rankdownmessage = "~r~" + sender.name + "~w~ понизил вас до " + giverank + " ранга";

        var rankupmessagesender = "~y~Вы повысили ~b~" + target.name + " ~y~до " + giverank + " ранга";
        var rankdownmessagesender = "~y~Вы понизили ~b~" + target.name + " ~y~до " + giverank + " ранга";

        if (Player.GetFractionRank(sender) <= 8)
        {
            API.sendChatMessageToPlayer(sender, "~r~Ваш ранг не позволяет вам использовать данную команду");
        }
        else
        {
            if (targetfaction == 0)
            {
                API.sendChatMessageToPlayer(sender, "~b~ " + target.name + "~y~ не состоит в вашей фракции");
                return;
            }
            else
            {
                if (Player.GetFractionRank(target) < giverank)
                {
                    Player.SetFactionRank(target, giverank);
                    API.sendChatMessageToPlayer(target, rankupmessage);
                    return;
                }
                else
                {
                    Player.SetFactionRank(target, giverank);
                    API.sendChatMessageToPlayer(target, rankdownmessage);
                    return;
                }
            }
        }
    }

    [Command("r", GreedyArg = true)]
    public void FractionChatCommand(Client sender, string message)
    {
        int rank = (sender.hasData("fractionrank_id")) ? sender.getData("fractionrank_id") : 0;
        string groupName = "R";
        var chatMessage = "~b~[" + groupName + " ID: " + "~b~" + API.exported.playerids.getIdFromClient(sender) + "] ~b~" + GetPlayerFactionRank(sender, Player.GetFractionRank(sender)) + " | " + sender.name + ": ~w~" + message;
        if (Player.GetFractionId(sender) == 0)
        {
            API.sendChatMessageToPlayer(sender, "~r~Вы не состоите во фракции!");
            return;
        }
        if (Player.GetFractionId(sender) != 0)
        {
            foreach (Client client in API.getAllPlayers())
            {
                if (Player.GetFractionId(client) == Player.GetFractionId(sender))
                {
                    API.sendChatMessageToPlayer(client, chatMessage);
                }
            }
            return;
        }
    }
}


