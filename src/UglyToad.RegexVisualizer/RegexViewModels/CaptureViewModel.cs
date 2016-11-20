namespace UglyToad.RegexVisualizer.RegexViewModels
{
    using System.Text.RegularExpressions;
    using WpfHelpers;

    internal class CaptureViewModel : ViewModelBase
    {
        public string Value { get; }

        public int Index { get; }

        public int Length { get; }

        public int CaptureIndex { get; }

        public CaptureViewModel(int index, Capture capture)
        {
            Value = capture.Value;
            Index = capture.Index;
            Length = capture.Length;
            CaptureIndex = index;
        }
    }
}
