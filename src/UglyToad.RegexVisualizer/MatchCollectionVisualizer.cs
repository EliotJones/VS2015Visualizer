using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;
using UglyToad.RegexVisualizer;

[assembly: DebuggerVisualizer(typeof(MatchCollectionVisualizer), typeof(VisualizerObjectSource),
    Description = "Regex Match Collection Visualizer", Target = typeof(MatchCollection))]
namespace UglyToad.RegexVisualizer
{
    using System;

    public class MatchCollectionVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            if (objectProvider == null)
            {
                return;
            }

            try
            {
                ShowInternal(objectProvider);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Could not show Visualizer due to error: " + ex);
            }
        }

        private static void ShowInternal(IVisualizerObjectProvider objectProvider)
        {
            var data = objectProvider.GetObject() as MatchCollection;

            if (data == null)
            {
                MessageBox.Show("Cannot Visualize this object type.", "Object Not Supported", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            var window = new MainWindow(data);

            window.ShowDialog();
        }
    }
}
