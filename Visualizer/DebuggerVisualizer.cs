using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Domain.Core;

[assembly: DebuggerVisualizer(typeof(Visualizer.DebuggerSide), typeof(VisualizerObjectSource),
        Target = typeof(List<>), Description = "Person Visualizer")]
namespace Visualizer
{
    public class DebuggerSide : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            try
            {
                var person = objectProvider.GetObject() as List<Person>;

                if (person == null)
                {
                    MessageBox.Show("Cannot visualize this object, please use a different visualizer.", "Type Not Supported", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                var window = new MainWindow(person[0]);

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
