using CartiLib;
using CartiLib.Interface;
using CartiLib.Title;
using System.Threading;
using CartiLib.Configuration;

namespace Test
{
    internal class Program
    {
        private static void Main()
        {
            SettingsMaker.Load();
            SettingsModel.ReadSettings();
            AsciiVariables.AppName = "CartiLib";
            TitleMaker.CreateBasic("CartiLib", "Home", "Carti#0145");
            AsciiMaker.Init("Big.flf");
            InterfaceMaker.CreateNotification("Developed by Carti#0145");
            Thread.Sleep(-1);
        }
    }
}
