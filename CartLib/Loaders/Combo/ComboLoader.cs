using CartiLib.Checker.Module_Tools;
using CartiLib.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace CartiLib.Loaders.Combo
{
    public class ComboLoader
    {
        public static List<string> Combo = new List<string>();
        public static string FileName;
        public static string EscapedDirectory;
        public static void Init(ConcurrentQueue<string> combo)
        {
            var thread = new Thread(delegate ()
            {
                AsciiMaker.Init("Big.flf");
                InterfaceMaker.CreateNotification("Drag & Drop your combolist");
                InterfaceMaker.CreateInput(false);
                var comboDirectory = Console.ReadLine();
                switch (comboDirectory.StartsWith("\"")) // if the directory starts with a " it will auto remove the " to prevent errors in the loading 
                {
                    case true:
                        EscapedDirectory = Functions.Parse(comboDirectory, "\"", "\"");
                        var fileEscaped = File.ReadAllLines(EscapedDirectory);
                        File.WriteAllLines(EscapedDirectory, fileEscaped.Distinct().ToArray()); //Removes exact duplicates of lines in the combos
                        Combo = new List<string>(File.ReadAllLines(EscapedDirectory));
                        foreach (var wombo in Combo.Where(wombo => wombo.Contains(":") || wombo.Contains(";") || wombo.Contains("|")))
                        {
                            combo.Enqueue(wombo); //adds combos to the concurrent queue 
                        }
                        break;
                    case false:
                        var file = File.ReadAllLines(comboDirectory);
                        File.WriteAllLines(comboDirectory, file.Distinct().ToArray());
                        Combo = new List<string>(File.ReadAllLines(comboDirectory));
                        foreach (var wombo in Combo.Where(wombo => wombo.Contains(":") || wombo.Contains(";") || wombo.Contains("|")))
                        {
                            combo.Enqueue(wombo);
                        }
                        break;
                }
                CheckerVariables.TotalCombo = CheckerVariables.Combo.Count;
                AsciiMaker.Init("Big.flf");
                InterfaceMaker.CreateNotification($"Loaded {CheckerVariables.Combo.Count} total lines");
                Thread.Sleep(1000);
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}
