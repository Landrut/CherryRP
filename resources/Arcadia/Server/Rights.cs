using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Rights
{

    public static String GetAdminRank(int rights)
    {
        switch (rights)
        {
            case 1: return "~w~Игрок ~w~";
            case 2: return "~g~VIP ~w~";
            case 3: return "~b~Helper ~w~";
            case 4: return "~b~Jr. Moderator ~w~";
            case 5: return "~q~Moderator ~w~";
            case 6: return "~q~Administrator ~w~";
            case 7: return "~p~Chief Administrator ~w~";
            case 8: return "~y~Special Administrator ~w~";
            case 9: return "~r~Management ~w~";
            case 10: return "~o~Project Lead ~w~";
            default: return "";
        }
    }
}


