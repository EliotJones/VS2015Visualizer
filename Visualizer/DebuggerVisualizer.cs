using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Diagnostics;

[assembly: DebuggerVisualizer(typeof(Visualizer.DebuggerSide), typeof(VisualizerObjectSource),
        Target = typeof(string), Description = "Visualizer")]
namespace Visualizer
{
    public class DebuggerSide : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            try
            {
                var window = new MainWindow(objectProvider.GetObject().ToString());

                window.ShowDialog();
            }
            catch (Exception ex)
            {
                Trace.Fail(ex.Message, ex.StackTrace);
            }
                
        }

        public static void TestShowVisualizer(object thingToVisualize)
        {
            var visualizerHost = new VisualizerDevelopmentHost(thingToVisualize, typeof(DebuggerSide));
            visualizerHost.ShowVisualizer();
        }
    }
}
