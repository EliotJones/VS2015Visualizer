using System.Collections.ObjectModel;
using System.Linq;

namespace Visualizer
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public string Value { get; }

        public ObservableCollection<CharacterViewModel> Characters { get; } = new ObservableCollection<CharacterViewModel>();

        public MainWindowViewModel(string value)
        {
            Value = value;

            if (value != null)
            {
                Characters = new ObservableCollection<CharacterViewModel>(value.Select(x => new CharacterViewModel(x)));
            }
        }
    }
}
