using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Domain.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Visualizer;

[assembly: DebuggerVisualizer(typeof(DebuggerSide), typeof(PersonObjectSource),
        Target = typeof(Person), Description = "Person Visualizer")]
namespace Visualizer
{
    public class DebuggerSide : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            try
            {
                using (var reader = new StreamReader(objectProvider.GetData()))
                {
                    var result = reader.ReadToEnd();



                    var person = JsonConvert.DeserializeObject<Person>(result, new JsonSerializerSettings
                    {
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                        ContractResolver = new PrivatePropertyResolver()
                    });

                    if (person == null)
                    {
                        MessageBox.Show("Cannot visualize this object, please use a different visualizer.", "Type Not Supported", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }

                    var window = new MainWindow(person);

                    window.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Trace.Fail(ex.Message, ex.StackTrace);
            }
                
        }

        public static void TestShowVisualizer(object thingToVisualize)
        {
            var visualizerHost = new VisualizerDevelopmentHost(thingToVisualize, typeof(DebuggerSide), typeof(PersonObjectSource));
            visualizerHost.ShowVisualizer();
        }
    }
}
