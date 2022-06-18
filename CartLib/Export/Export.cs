using System;
using System.IO;
using System.Threading;

namespace CartiLib.Export
{
    public class Export
    {
        public enum AccType //Enumerations for the different account types
        {
            Hit,
            Bad,
            Custom,
            TwoFa,
            Free,
            Expired
        }
        public static void DirectoryCreator(string name) //Dir creator for saving
        {
            switch (Directory.Exists(name))
            {
                case false:
                    Directory.CreateDirectory(name);
                    break;
            }
        }
        public static void Save(string location, string credentials) //Writes the acc to the location
        {
            try
            {
                using (var streamWriter = new StreamWriter(location, true))
                {
                    streamWriter.WriteLine(credentials);
                }
            }
            catch
            {
                Thread.Sleep(100);
            }
        }
        public static void Init(AccType saveType, string moduleName, string credentials, string remainingPath)
        {
            DirectoryCreator($"./Results/{moduleName}/{moduleName}_{LocalVariables.Date}/");
            var saveLocation = $"./Results/{moduleName}/{moduleName}_{LocalVariables.Date}/{remainingPath}";
            switch (saveType)
            {
                case AccType.Hit:
                    CheckerVariables.Checked++;
                    CheckerVariables.Hits++;
                    Save(saveLocation, credentials);
                    break;
                case AccType.Bad:
                    CheckerVariables.Checked++;
                    CheckerVariables.Bads++;
                    Save(saveLocation, credentials);
                    break;
                case AccType.Custom:
                    CheckerVariables.Checked++;
                    CheckerVariables.Customs++;
                    Save(saveLocation, credentials);
                    break;
                case AccType.TwoFa:
                    CheckerVariables.Checked++;
                    CheckerVariables.Twofa++;
                    Save(saveLocation, credentials);
                    break;
                case AccType.Free:
                    CheckerVariables.Checked++;
                    CheckerVariables.Frees++;
                    Save(saveLocation, credentials);
                    break;
                case AccType.Expired:
                    CheckerVariables.Checked++;
                    CheckerVariables.Expired++;
                    Save(saveLocation, credentials);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(saveType), saveType, null);
            }
        }
    }
}
