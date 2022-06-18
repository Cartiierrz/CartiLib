using CartiLib.Interface;
using Leaf.xNet;
using Spectre.Console;
using System;
using System.Threading;

namespace CartiLib.Auto_Updater
{
    public class Version
    {
        public static string ToRet;
        public static void AutoUpdater(string localversion)
        {
            var toCheck = CompareVersions(localversion); // Sends the request to the api to check versions
            AnsiConsole.Progress().AutoRefresh(true).AutoClear(true).HideCompleted(false)
                .Columns(new TaskDescriptionColumn(), new ProgressBarColumn(), new PercentageColumn(), new RemainingTimeColumn(), new SpinnerColumn()).Start(ctx =>
                {
                    var task1 = ctx.AddTask("[RoyalBlue1]Checking for new updates[/]");
                    while (!ctx.IsFinished)
                    {
                        task1.Increment(2.5);
                        Thread.Sleep(50);
                    }
                });
            switch (toCheck) // This segment will notify the user wheter they are upto date or outdated
            {
                case "true":
                    InterfaceMaker.CreateNotification("Application upto date! Redirecting...");
                    Thread.Sleep(500);
                    break;
                case "false":
                    InterfaceMaker.CreateNotification("Application outdated! Please update to the latest version to proceed");
                    Console.ReadKey();
                    break;
            }
        }
        public static string CompareVersions(string localversion)
        {
            using (var httpRequest = new HttpRequest())
            {
                var compareVersions = httpRequest.Get($"https://api.carti.wtf/api/v1/compareversion?appID={localversion}");//Replace this with your api/link if you decided to add a update checker
                switch (compareVersions.ToString().Contains("{\"error\":null,\"success\":true}"))
                {
                    case true:
                        ToRet = "true"; //Returns true if the local version matches the server version
                        break;
                    case false:
                        ToRet = "false"; //Returns false if the local version does NOT match the server version
                        break;
                }
            }
            return ToRet;
        }
    }
}
