using System;

namespace CartiLib
{
    public class LocalVariables
    {
        public static string Version { get; set; }
        public static string IsLoggedIn { get; set; }
        public static string Username { get; set; }

        public static string Date = DateTime.Now.ToString("MM-dd-yyyy H.mm");
    }
}
