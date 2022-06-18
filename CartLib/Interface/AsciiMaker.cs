using Spectre.Console;
using System;

namespace CartiLib.Interface
{
    public class AsciiMaker
    {
        public static void Init(string fontFile) //Creates the ascii with the Big figlet font 
        {
            Console.Clear();
            var fontLoad = FigletFont.Load(fontFile); //Download to various font files https://github.com/cmatsuoka/figlet/tree/master/fonts
            AnsiConsole.Write(new FigletText(fontLoad, AsciiVariables.AppName).Centered().Color(Color.RoyalBlue1));
        }
    }
}
