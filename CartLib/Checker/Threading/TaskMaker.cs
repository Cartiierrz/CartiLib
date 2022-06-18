using CartiLib.Configuration;
using System;
using System.Threading.Tasks;

namespace CartiLib.Checker.Threading
{
    public class TaskMaker
    {

        public static void Init(Action<string> target) //Creates tasks for the desired target
        {
            for (var i = 0; i <= SettingsModel.Threads; i++)
            {
                while (!CheckerVariables.Combo.IsEmpty)
                {
                    var item = new Task(delegate { Process(target); }, TaskCreationOptions.LongRunning);
                    CheckerVariables.Tasks.Add(item);
                    break;
                }
            }
            Start();
        }
        public static void Process(Action<string> target) //dequeues the combo out to the target
        {
            while (!CheckerVariables.Combo.IsEmpty)
            {
                CheckerVariables.Combo.TryDequeue(out var combo);
                target(combo);
            }
        }
        private static void Start() //Starts each task
        {
            foreach (var task in CheckerVariables.Tasks)
            {
                task.Start();
            }
        }
    }
}
