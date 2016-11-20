namespace UglyToad.RegexVisualizer.RegexViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using WpfHelpers;

    internal class GroupViewModel : ViewModelBase
    {
        public IReadOnlyList<CaptureViewModel> Captures { get; }

        public int MatchIndex { get; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int Index { get; set; }

        public GroupViewModel(string name, int matchIndex, Group group)
        {
            Index = group.Index;
            MatchIndex = matchIndex;
            Name = name;
            Value = group.Value;
            Captures = group.Captures.Cast<Capture>()
                .Select((x, i) => new CaptureViewModel(i, x)).ToList();
        }

        public static IReadOnlyList<GroupViewModel> Create(Regex regex, Match match)
        {
            var groups = match.Groups.Cast<Group>()
                .Select((x, i) => new GroupViewModel(regex.GroupNameFromNumber(i), i, x))
                .ToList();

            return groups;
        } 
    }
}
