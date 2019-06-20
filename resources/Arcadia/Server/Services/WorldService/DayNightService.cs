/*using CherryMPServer;
//
//
using CherryMPServer.Constant;
using CherryMPServer;
using CherryMPShared;
//;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace TimeScript
{
    public class DayNightService : Script
    {
        string[] dayNames = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
        Thread timeThread = null;

        public DayNightService()
        {
            API.onResourceStart += DayNightInit;
            API.onResourceStop += DayNightExit;
        }

        public void DayNightInit()
        {
            API.setWorldSyncedData("DAYNIGHT_DAY", 0);
            API.setWorldSyncedData("DAYNIGHT_DAY_STRING", dayNames[API.getWorldSyncedData("DAYNIGHT_DAY")]);
            API.setWorldSyncedData("DAYNIGHT_HOUR", 12);
            API.setWorldSyncedData("DAYNIGHT_MINUTE", 0);
            API.setWorldSyncedData("DAYNIGHT_RENDER_ICON", true);
            DayNightPrepareText();

            foreach (var player in API.getAllPlayers()) API.freezePlayerTime(player, true);

            timeThread = new Thread(UpdateTime);
            timeThread.Start();
        }

        public void DayNightExit()
        {
            if (timeThread != null) timeThread.Abort();
            timeThread = null;
        }

        public void DayNightPrepareText()
        {
            API.setWorldSyncedData("DAYNIGHT_TEXT", API.getWorldSyncedData("DAYNIGHT_HOUR").ToString("D2") + ":" + API.getWorldSyncedData("DAYNIGHT_MINUTE").ToString("D2"));
        }

        public void UpdateTime()
        {
            while (true)
            {
                API.setWorldSyncedData("DAYNIGHT_MINUTE", API.getWorldSyncedData("DAYNIGHT_MINUTE") + 1);

                if (API.getWorldSyncedData("DAYNIGHT_MINUTE") == 60)
                {
                    API.setWorldSyncedData("DAYNIGHT_MINUTE", 0);
                    API.setWorldSyncedData("DAYNIGHT_HOUR", API.getWorldSyncedData("DAYNIGHT_HOUR") + 1);

                    if (API.getWorldSyncedData("DAYNIGHT_HOUR") == 24)
                    {
                        API.setWorldSyncedData("DAYNIGHT_HOUR", 0);

                        API.setWorldSyncedData("DAYNIGHT_DAY", API.getWorldSyncedData("DAYNIGHT_DAY") + 1);
                        if (API.getWorldSyncedData("DAYNIGHT_DAY") == dayNames.Length) API.setWorldSyncedData("DAYNIGHT_DAY", 0);
                        API.setWorldSyncedData("DAYNIGHT_DAY_STRING", dayNames[API.getWorldSyncedData("DAYNIGHT_DAY")]);
                    }
                }

                API.setTime(API.getWorldSyncedData("DAYNIGHT_HOUR"), API.getWorldSyncedData("DAYNIGHT_MINUTE"));
                DayNightPrepareText();
                Thread.Sleep(1000);
            }
        }

        [Command("toggletimeicon")]
        public void CMDToggleIcon(Client sender)
        {
            API.setWorldSyncedData("DAYNIGHT_RENDER_ICON", !API.getWorldSyncedData("DAYNIGHT_RENDER_ICON"));
        }
    }
}*/