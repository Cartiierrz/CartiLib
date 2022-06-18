using CartiLib.Configuration.Files;
using CartiLib.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CartiLib.Configuration
{
    public class SettingsModel
    {
        // General Settings
        public static bool PrintInvalid { get; set; }
        public static bool PrintCustoms { get; set; }

        //Proxy Settings
        public static int ProxyTimeout { get; set; }
        public static bool UseProxyAuth { get; set; }
        public static string ProxyAuthType { get; set; }

        //Checker Settings
        public static int Threads { get; set; }
        public static int Retries { get; set; }
        public static bool UseGui { get; set; }
        public enum Settings //Enumerations for the different settings for the edit settings void
        {
            PrintInvalid,
            PrintCustoms,
            ProxyTimeout,
            UseProxyAuth,
            ProxyAuthType,
            Threads,
            Retries,
            UseGui
        }
        public static void ReadSettings()
        {
            try
            {
                var jsonContents = File.ReadAllText("./Data/settings.json");
                var cfg = JsonConvert.DeserializeObject<Root>(jsonContents);
                PrintInvalid = cfg.Settings.PrintInvalid;
                PrintCustoms = cfg.Settings.PrintCustoms;
                ProxyTimeout = cfg.Proxies.ProxyTimeout;
                UseProxyAuth = cfg.Proxies.UseProxyAuth;
                ProxyAuthType = cfg.Proxies.ProxyAuthType;
                Threads = cfg.Checker.Threads;
                Retries = cfg.Checker.Retries;
                UseGui = cfg.Checker.UseGui;
            }
            catch (Exception)
            {
                InterfaceMaker.CreateNotification("Settings.json is corrupted! Reverting to defaults");
                Thread.Sleep(1500);
                switch (File.Exists("./Data/settings.json"))
                {
                    case true:
                        var mainDictionary = new Dictionary<string, object>();
                        var settingsDictionary = new Dictionary<string, object>();
                        var proxyDictionary = new Dictionary<string, object>();
                        var checkerDictionary = new Dictionary<string, object>();
                        mainDictionary["Settings"] = settingsDictionary;
                        settingsDictionary["PrintInvalid"] = false;
                        settingsDictionary["PrintCustoms"] = true;
                        mainDictionary["Proxies"] = proxyDictionary;
                        proxyDictionary["ProxyTimeout"] = 5000;
                        proxyDictionary["UseProxyAuth"] = false;
                        proxyDictionary["ProxyAuthType"] = "IP:PORT:USER:PASS";
                        mainDictionary["Checker"] = checkerDictionary;
                        checkerDictionary["Threads"] = 150;
                        checkerDictionary["Retries"] = 0;
                        checkerDictionary["UseGui"] = true;
                        var contents = JsonConvert.SerializeObject(mainDictionary, Formatting.Indented);
                        File.WriteAllText("./Data/settings.json", contents);
                        ReadSettings();
                        break;
                }
            }
        }
        public static void EditSettings(Settings ToEdit) //Simply and quickly edits settings
        {
            var value = File.ReadAllText("./Data/settings.json");
            var cfg = JsonConvert.DeserializeObject<Root>(value);
            var mainDictionary = new Dictionary<string, object>();
            var settingsDictionary = new Dictionary<string, object>();
            var proxyDictionary = new Dictionary<string, object>();
            var checkerDictionary = new Dictionary<string, object>();
            mainDictionary["Settings"] = settingsDictionary;
            mainDictionary["Proxies"] = proxyDictionary;
            mainDictionary["Checker"] = checkerDictionary;
            switch (ToEdit)
            {
                case Settings.PrintInvalid: //boolean
                    switch (cfg.Settings.PrintInvalid)
                    {
                        case true:
                            settingsDictionary["PrintInvalid"] = false;
                            settingsDictionary["PrintCustoms"] = PrintCustoms;
                            proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                            proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                            proxyDictionary["ProxyAuthType"] = ProxyAuthType;
                            checkerDictionary["Threads"] = Threads;
                            checkerDictionary["Retries"] = Retries;
                            checkerDictionary["UseGui"] = UseGui;
                            break;
                        case false:
                            settingsDictionary["PrintInvalid"] = true;
                            settingsDictionary["PrintCustoms"] = PrintCustoms;
                            proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                            proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                            proxyDictionary["ProxyAuthType"] = ProxyAuthType;
                            checkerDictionary["Threads"] = Threads;
                            checkerDictionary["Retries"] = Retries;
                            checkerDictionary["UseGui"] = UseGui;
                            break;
                    }
                    break;
                case Settings.PrintCustoms: //boolean
                    switch (cfg.Settings.PrintCustoms)
                    {
                        case true:
                            settingsDictionary["PrintInvalid"] = PrintInvalid;
                            settingsDictionary["PrintCustoms"] = false;
                            proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                            proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                            proxyDictionary["ProxyAuthType"] = ProxyAuthType;
                            checkerDictionary["Threads"] = Threads;
                            checkerDictionary["Retries"] = Retries;
                            checkerDictionary["UseGui"] = UseGui;
                            break;
                        case false:
                            settingsDictionary["PrintInvalid"] = PrintInvalid;
                            settingsDictionary["PrintCustoms"] = true;
                            proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                            proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                            proxyDictionary["ProxyAuthType"] = ProxyAuthType;
                            checkerDictionary["Threads"] = Threads;
                            checkerDictionary["Retries"] = Retries;
                            checkerDictionary["UseGui"] = UseGui;
                            break;
                    }
                    break;
                case Settings.ProxyTimeout: //int
                    InterfaceMaker.CreateNotification("Enter the new proxy timeout");
                    InterfaceMaker.CreateInput(false);
                    var newTimeout = Convert.ToInt32(Console.ReadLine());
                    settingsDictionary["PrintInvalid"] = PrintInvalid;
                    settingsDictionary["PrintCustoms"] = PrintCustoms;
                    proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                    proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                    proxyDictionary["ProxyAuthType"] = ProxyAuthType;
                    checkerDictionary["Threads"] = newTimeout;
                    checkerDictionary["Retries"] = Retries;
                    checkerDictionary["UseGui"] = UseGui;
                    break;
                case Settings.UseProxyAuth: //boolean
                    switch (cfg.Proxies.UseProxyAuth)
                    {
                        case true:
                            settingsDictionary["PrintInvalid"] = PrintInvalid;
                            settingsDictionary["PrintCustoms"] = PrintCustoms;
                            proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                            proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                            proxyDictionary["ProxyAuthType"] = false;
                            checkerDictionary["Threads"] = Threads;
                            checkerDictionary["Retries"] = Retries;
                            checkerDictionary["UseGui"] = UseGui;
                            break;
                        case false:
                            settingsDictionary["PrintInvalid"] = PrintInvalid;
                            settingsDictionary["PrintCustoms"] = PrintCustoms;
                            proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                            proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                            proxyDictionary["ProxyAuthType"] = true;
                            checkerDictionary["Threads"] = Threads;
                            checkerDictionary["Retries"] = Retries;
                            checkerDictionary["UseGui"] = UseGui;
                            break;
                    }
                    break;
                case Settings.ProxyAuthType: //string
                    switch (cfg.Proxies.ProxyAuthType)
                    {
                        case "IP:PORT:USER:PASS":
                            settingsDictionary["PrintInvalid"] = PrintInvalid;
                            settingsDictionary["PrintCustoms"] = PrintCustoms;
                            proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                            proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                            proxyDictionary["ProxyAuthType"] = "USER:PASS:IP:PORT";
                            checkerDictionary["Threads"] = Threads;
                            checkerDictionary["Retries"] = Retries;
                            checkerDictionary["UseGui"] = UseGui;
                            break;
                        case "USER:PASS:IP:PORT":
                            settingsDictionary["PrintInvalid"] = PrintInvalid;
                            settingsDictionary["PrintCustoms"] = PrintCustoms;
                            proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                            proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                            proxyDictionary["ProxyAuthType"] = "IP:PORT:USER:PASS";
                            checkerDictionary["Threads"] = Threads;
                            checkerDictionary["Retries"] = Retries;
                            checkerDictionary["UseGui"] = UseGui;
                            break;
                    }
                    break;
                case Settings.Threads: //int
                    InterfaceMaker.CreateNotification("Enter the new threads amount");
                    InterfaceMaker.CreateInput(false);
                    var newThreads = Convert.ToInt32(Console.ReadLine());
                    settingsDictionary["PrintInvalid"] = PrintInvalid;
                    settingsDictionary["PrintCustoms"] = PrintCustoms;
                    proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                    proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                    proxyDictionary["ProxyAuthType"] = ProxyAuthType;
                    checkerDictionary["Threads"] = newThreads;
                    checkerDictionary["Retries"] = Retries;
                    checkerDictionary["UseGui"] = UseGui; break;
                case Settings.Retries: //int
                    InterfaceMaker.CreateNotification("Enter the new retries amount");
                    InterfaceMaker.CreateInput(false);
                    var neRetries = Convert.ToInt32(Console.ReadLine());
                    settingsDictionary["PrintInvalid"] = PrintInvalid;
                    settingsDictionary["PrintCustoms"] = PrintCustoms;
                    proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                    proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                    proxyDictionary["ProxyAuthType"] = ProxyAuthType;
                    checkerDictionary["Threads"] = Threads;
                    checkerDictionary["Retries"] = neRetries;
                    checkerDictionary["UseGui"] = UseGui;
                    break;
                case Settings.UseGui: //boolean
                    switch (cfg.Checker.UseGui)
                    {
                        case true:
                            settingsDictionary["PrintInvalid"] = PrintInvalid;
                            settingsDictionary["PrintCustoms"] = PrintCustoms;
                            proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                            proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                            proxyDictionary["ProxyAuthType"] = ProxyAuthType;
                            checkerDictionary["Threads"] = Threads;
                            checkerDictionary["Retries"] = Retries;
                            checkerDictionary["UseGui"] = false;
                            break;
                        case false:
                            settingsDictionary["PrintInvalid"] = PrintInvalid;
                            settingsDictionary["PrintCustoms"] = PrintCustoms;
                            proxyDictionary["ProxyTimeout"] = ProxyTimeout;
                            proxyDictionary["UseProxyAuth"] = UseProxyAuth;
                            proxyDictionary["ProxyAuthType"] = ProxyAuthType;
                            checkerDictionary["Threads"] = Threads;
                            checkerDictionary["Retries"] = Retries;
                            checkerDictionary["UseGui"] = true;
                            break;
                    }
                    break;
            }
        }
    }
    public static class SettingsMaker
    {
        private static readonly string JsonFolderpath = "./Data/";
        private static readonly string JsonPath = "./Data/settings.json";
        private static SettingsModel Settings;

        public static void Save(this SettingsModel Model)
        {
            switch (!Directory.Exists(JsonFolderpath)) //Checks if the Data directory does not exist and creates it 
            {
                case true:
                    Directory.CreateDirectory(JsonFolderpath);
                    break;
            }
            Settings = Model;
            var mainDictionary = new Dictionary<string, object>();    //Formats the settings.json file and the settings within the file
            var settingsDictionary = new Dictionary<string, object>();
            var proxyDictionary = new Dictionary<string, object>();
            var checkerDictionary = new Dictionary<string, object>();
            mainDictionary["Settings"] = settingsDictionary;
            settingsDictionary["PrintInvalid"] = false;
            settingsDictionary["PrintCustoms"] = true;
            mainDictionary["Proxies"] = proxyDictionary;
            proxyDictionary["ProxyTimeout"] = 5000;
            proxyDictionary["UseProxyAuth"] = false;
            proxyDictionary["ProxyAuthType"] = "IP:PORT:USER:PASS";
            mainDictionary["Checker"] = checkerDictionary;
            checkerDictionary["Threads"] = 150;
            checkerDictionary["Retries"] = 0;
            checkerDictionary["UseGui"] = true;
            var contents = JsonConvert.SerializeObject(mainDictionary, Formatting.Indented);
            File.WriteAllText(JsonPath, contents);
        }
        public static SettingsModel Load()
        {
            SettingsModel result;
            switch (Settings != null) // checks if the settings does NOT equals null 
            {
                case true:
                    result = Settings;
                    break;
                default:
                    {
                        switch (!Directory.Exists(JsonFolderpath)) // creates the data folder
                        {
                            case true:
                                Directory.CreateDirectory(JsonFolderpath);
                                break;
                        }
                        switch (!File.Exists(JsonPath)) // creates the json file 
                        {
                            case true:
                                {
                                    var model = new SettingsModel();
                                    model.Save();
                                    break;
                                }
                        }
                        var text = File.ReadAllText(JsonPath);
                        var settingsModel = JsonConvert.DeserializeObject<SettingsModel>(text);
                        Settings = settingsModel;
                        result = settingsModel;
                        break;
                    }
            }
            return result;
        }
    }
}
