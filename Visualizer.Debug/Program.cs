using Domain.Core;

namespace Visualizer.Debug
{
    using System;

    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var person = new Person("Jim", "Paul", new DateTime(1970, 5, 25), "Blue");

            DebuggerSide.TestShowVisualizer(person);

            var personOld = new Person("Jane", "Smith", new DateTime(1925, 7, 27), "Green");

            DebuggerSide.TestShowVisualizer(personOld);
        }
    }
}
