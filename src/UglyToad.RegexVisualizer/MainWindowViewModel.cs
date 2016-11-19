namespace UglyToad.RegexVisualizer
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows.Documents;
    using System.Windows.Media;
    using RegexViewModels;
    using WpfHelpers;

    internal class MainWindowViewModel : ViewModelBase
    {
        private const int MYSTERY_OFFSET = 2;
        private readonly Regex _regex;
        private readonly string _originalText;

        public string Pattern { get; set; }

        private FlowDocument _document;
        public FlowDocument Document
        {
            get { return _document; }
            private set { SetProperty(ref _document, value); }
        }

        public ObservableCollection<Match> Matches { get; }
        
        private Match _selectedMatch;
        public Match SelectedMatch
        {
            get { return _selectedMatch; }
            set
            {
                SetProperty(ref _selectedMatch, value);
                if (value != null)
                {
                    SelectionChanged();
                }
            }
        }

        public ObservableCollection<GroupViewModel> Groups { get; } = new ObservableCollection<GroupViewModel>(); 

        public MainWindowViewModel(MatchCollection matchCollection)
        {
            if (matchCollection == null)
            {
                throw new ArgumentNullException(nameof(matchCollection));
            }

            var matches = matchCollection.Cast<Match>().ToArray();
            _regex = GetOriginalRegex(matchCollection);
            _originalText = GetOriginalInput(matchCollection);

            Pattern = _regex?.ToString();

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

            var groups = GroupViewModel.Create(_regex, SelectedMatch);

            foreach (var groupViewModel in groups)
            {
                Groups.Add(groupViewModel);
            }
        }

        private FlowDocument PopulateDocument()
        {
            var document = new FlowDocument();
            var range = new TextRange(document.ContentStart, document.ContentEnd)
            {
                Text = _originalText
            };

            if (SelectedMatch != null)
            {
                var start = document.ContentStart.DocumentStart.GetPositionAtOffset(SelectedMatch.Index + MYSTERY_OFFSET);
                var end =
                    document.ContentStart.DocumentStart.GetPositionAtOffset(SelectedMatch.Length + SelectedMatch.Index + MYSTERY_OFFSET);

                var matchRange = new TextRange(start, end);

                matchRange.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Lime));
            }

            return document;
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
    }
}
