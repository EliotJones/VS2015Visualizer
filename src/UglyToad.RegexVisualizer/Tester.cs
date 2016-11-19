namespace UglyToad.RegexVisualizer
{
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.DebuggerVisualizers;

    internal static class Tester
    {
        public static void Run(MatchCollection matchCollection)
        {
            var visualizerHost = new VisualizerDevelopmentHost(matchCollection, typeof(MatchCollectionVisualizer));

            visualizerHost.ShowVisualizer();
        }
    }
}
