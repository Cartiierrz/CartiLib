using System.Linq;
using System.Threading;

namespace CartiLib.Checker
{
    public class CalculateCpm
    {
        public static Thread CpmThread = new Thread(Init)
        {
            Priority = ThreadPriority.Lowest,
            IsBackground = true
        };
        public static void Init()
        {
            for (; ; )
            {
                CheckerVariables.CpmList[CheckerVariables.CpmI % 60] = CheckerVariables.Checked - CheckerVariables.PreviousCheckeds;
                CheckerVariables.PreviousCheckeds = CheckerVariables.Checked;
                CheckerVariables.Cpm = CheckerVariables.CpmList.Sum();
                CheckerVariables.CpmI++;
                Thread.Sleep(1000);
            }
        }
    }
}
