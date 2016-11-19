using System.IO;
using Domain.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Newtonsoft.Json;

namespace Visualizer
{
    class PersonObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, Stream outgoingData)
        {
            var person = target as Person;

            if (person == null)
            {
                return;
            }

            var writer = new StreamWriter(outgoingData);

            var content = JsonConvert.SerializeObject(person);

            writer.WriteLine(content);

            writer.Flush();
        }
    }
}
