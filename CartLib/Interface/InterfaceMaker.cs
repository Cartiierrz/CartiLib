using Spectre.Console;
using System;

namespace CartiLib.Interface
{
    public class InterfaceMaker
    {
        private static int _num;
        public static void ResetOptionsCount() => _num = 0; //Resets the numbers on the create options void
        public static void CreateNotification(string message) //Writes a notification to the console
        {
            AnsiConsole.Markup("[gray]{0}[/][RoyalBlue1]{1}[/][gray]{2}[/] [gray]{3}[/]", Markup.Escape("["), Markup.Escape("Notification"), Markup.Escape("]"), Markup.Escape(message));
            Console.WriteLine();
        }
        public static void CreateInput(bool addspace) //Writes an input section to the console
        {
            switch (addspace) //wheter to add newline before the input or not
            {
                case true:
                    Console.WriteLine();
                    break;
            }
            AnsiConsole.Markup("[gray]{0}[/][RoyalBlue1]{1}[/][gray]{2}[/]", Markup.Escape("["), Markup.Escape("Input"), Markup.Escape("]: "));
        }
        public static void CreateOptions(string optionname) //Writes options to the console
        {
            _num++;
            AnsiConsole.Markup("[gray]{0}[/][RoyalBlue1]{1}[/][gray]{2}[/]", Markup.Escape("["), Markup.Escape(_num.ToString()), Markup.Escape($"] {optionname}"));
            Console.WriteLine();
        }
    }
}
