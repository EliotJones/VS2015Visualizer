using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Domain.Core;

[assembly: DebuggerVisualizer(typeof(Visualizer.DebuggerSide), typeof(VisualizerObjectSource),
        Target = typeof(Domain.Core.Person), Description = "Person Visualizer")]
namespace Visualizer
{
    public class DebuggerSide : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            try
            {
                var person = objectProvider.GetObject() as Person;

                if (person == null)
                {
                    MessageBox.Show("Cannot visualize this object", "Failed");

                    return;
                }

                var window = new MainWindow(person);

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
