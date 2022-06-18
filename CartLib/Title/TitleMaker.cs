using System;

namespace CartiLib.Title
{
    public class TitleMaker
    {
        public static void CreateBasic(string appName, string item1, string Developer) //Creates a basic title for the console
        {
            Console.Title = $"{appName} - {item1} | Developed by {Developer}";
        }
        public static void CreateStats(string appName, string module, string Developer) //Creates a checker stats title for the console
        {
            Console.Title = $"{appName} - Module: {module} [Hits: {CheckerVariables.Hits} - Bads: {CheckerVariables.Bads} - Frees: {CheckerVariables.Frees}] - [Cpm: {CheckerVariables.Cpm} - Checked: {CheckerVariables.Checked}/{CheckerVariables.TotalCombo} - Errors: {CheckerVariables.Errors} - Retries: {CheckerVariables.Retries}]  | Developed by {Developer}";
        }
    }
}
