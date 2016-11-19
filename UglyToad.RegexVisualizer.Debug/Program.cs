namespace UglyToad.RegexVisualizer.Debug
{
    using System;
    using System.Text.RegularExpressions;

    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var regex = new Regex(@"\b[a-z]+\b");

            var matches = regex.Matches("all dry river");

            Tester.Run(matches);
        }
    }
}
