namespace Visualizer.Debug
{
    using System;

    public class Program
    {
        [STAThread]
        public static void Main()
        {
            const string data = "test";

            DebuggerSide.TestShowVisualizer(data);
        }
    }
}
