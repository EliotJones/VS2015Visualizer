namespace UglyToad.RegexVisualizer.Debug
{
    using System;
    using System.Text.RegularExpressions;

    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var regex = new Regex(@".+ id: (?<id>[\d]{7,10})");

            var matches = regex.Matches("the id: 13847345");

            Tester.Run(matches);
        }
    }
}
