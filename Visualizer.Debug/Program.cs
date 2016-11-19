using System.Collections.Generic;
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

            var personOld = new Person("Jane", "Smith", new DateTime(1925, 7, 27), "Green");
            
            var numberList = new List<int>
            {
                1, 2, 3
            };

            var list = new List<Person>
            {
                personOld,
                person
            };

            DebuggerSide.TestShowVisualizer(person);
        }
    }
}
