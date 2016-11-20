namespace UglyToad.RegexVisualizer
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using RegexViewModels;
    using WpfHelpers;

    internal class MainWindowViewModel : ViewModelBase
    {
        private const int MysteryOffset = 2;
        private readonly Regex regex;
        private readonly string originalText;

        public string Pattern { get; set; }

        private FlowDocument document;
        public FlowDocument Document
        {
            get { return document; }
            private set { SetProperty(ref document, value); }
        }

        public ObservableCollection<Match> Matches { get; }
        
        private Match selectedMatch;
        public Match SelectedMatch
        {
            get { return selectedMatch; }
            set
            {
                SetProperty(ref selectedMatch, value);
                if (value != null)
                {
                    SelectionChanged();
                }
                else
                {
                    SelectedGroup = null;
                }
            }
        }

        public ObservableCollection<GroupViewModel> Groups { get; } = new ObservableCollection<GroupViewModel>();

        private GroupViewModel selectedGroup;
        public GroupViewModel SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                SetProperty(ref selectedGroup, value);
                if (value != null)
                {
                    GroupChanged();
                }
            }
        }

        public ObservableCollection<CaptureViewModel> Captures { get; } = new ObservableCollection<CaptureViewModel>();

        private void GroupChanged()
        {
            if (SelectedGroup == null)
            {
                return;
            }

            Captures.Clear();

            foreach (var captureViewModel in SelectedGroup.Captures)
            {
                Captures.Add(captureViewModel);
            }

            Document = PopulateDocument();
        }

        public MainWindowViewModel(MatchCollection matchCollection)
        {
            if (matchCollection == null)
            {
                throw new ArgumentNullException(nameof(matchCollection));
            }

            var matches = matchCollection.Cast<Match>().ToArray();
            regex = GetOriginalRegex(matchCollection);
            originalText = GetOriginalInput(matchCollection);

            Pattern = regex?.ToString();

            Matches = new ObservableCollection<Match>(matches);

            Document = PopulateDocument();
        }

        private void SelectionChanged()
        {
            if (SelectedMatch == null)
            {
                return;
            }

            Document = PopulateDocument();
            Groups.Clear();

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                var groups = GroupViewModel.Create(regex, SelectedMatch);

                foreach (var groupViewModel in groups)
                {
                    Groups.Add(groupViewModel);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            Mouse.OverrideCursor = null;
        }

        private FlowDocument PopulateDocument()
        {
            var updatedDocument = new FlowDocument();
            
            // ReSharper disable once UnusedVariable
            var range = new TextRange(updatedDocument.ContentStart, updatedDocument.ContentEnd)
            {
                Text = originalText
            };

            if (SelectedMatch == null)
            {
                return updatedDocument;
            }

            if (SelectedGroup != null)
            {
                var start = SelectedGroup.Index + MysteryOffset;
                var end = SelectedGroup.Index + SelectedGroup.Value.Length + MysteryOffset;

                HighlightRange(updatedDocument, start, end, Colors.Aqua);
            }
            else
            {

                var start = SelectedMatch.Index + MysteryOffset;
                var end = SelectedMatch.Length + SelectedMatch.Index + MysteryOffset;
                
                HighlightRange(updatedDocument, start, end, Colors.Lime);
            }

            return updatedDocument;
        }

        private static void HighlightRange(FlowDocument updatedDocument, int start, int end, Color color)
        {
            var startPointer = updatedDocument.ContentStart.DocumentStart.GetPositionAtOffset(start);
            var endPointer =
                updatedDocument.ContentStart.DocumentStart.GetPositionAtOffset(end);

            var matchRange = new TextRange(startPointer, endPointer);

            matchRange.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(color));
        }

        private static Regex GetOriginalRegex(MatchCollection matchCollection)
        {
            var regexFieldInfo = typeof (MatchCollection).GetField("_regex",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            var result = regexFieldInfo?.GetValue(matchCollection) as Regex;

            return result;
        }

        private static string GetOriginalInput(MatchCollection matchCollection)
        {
            var inputFieldInfo = typeof (MatchCollection).GetField("_input",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            var result = inputFieldInfo?.GetValue(matchCollection)?.ToString() ?? string.Empty;

            return result;
        }

        public void Deselect()
        {
            SelectedMatch = null;
        }
    }
}
