using System;
//
//
using CherryMPServer;
using System.Globalization;
using System.IO;
public class savedPositions : Script
{

    public savedPositions()
    {
        API.onResourceStart += MyResouceStart;
    }
    public void MyResouceStart()
    {
        API.consoleOutput("Сейв позиций запущен!");
    }
    [Command("save", GreedyArg = true)]
    public void SavePosition_Command(Client sender, string name = "")
    {
        var pos = API.getEntityPosition(sender.handle);
        var angle = API.getEntityRotation(sender.handle);
        if (sender.isInVehicle)
        {
            var dim = API.getEntityDimension(sender.handle);
            var playerVehicleHash = API.getEntityModel(API.getPlayerVehicle(sender));
            var playerVehicleColor1 = API.getVehiclePrimaryColor(API.getPlayerVehicle(sender));
            var playerVehicleColor2 = API.getVehicleSecondaryColor(API.getPlayerVehicle(sender));
            File.AppendAllText(@"savedpositions.txt", string.Format("API.createVehicle((VehicleHash){0}, new Vector3({1}, {2}, {3}), new Vector3({4}, {5}, {6}), {7}, {8}, {9}); // {10}\n", playerVehicleHash, pos.X.cc(), pos.Y.cc(), pos.Z.cc(), angle.X.cc(), angle.Y.cc(), angle.Z.cc(), playerVehicleColor1, playerVehicleColor2, dim, name));
            API.sendChatMessageToPlayer(sender, "~#92a079~", "-> Позиция и транспорт сохранены (" + name + ")");
        }
        else
        {
            File.AppendAllText(@"savedpositions.txt", string.Format("({0}, {1}, {2}, {3}, {4}, {5}) // {6}\n", pos.X.cc(), pos.Y.cc(), pos.Z.cc(), angle.X, angle.Y, angle.Z.cc(), name));
            API.sendChatMessageToPlayer(sender, "~#92a079~", "-> Позиция сохранена (" + name + ")");
        }
    }
}
public static class CultChange
{
    public static string cc(this float value)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }
}