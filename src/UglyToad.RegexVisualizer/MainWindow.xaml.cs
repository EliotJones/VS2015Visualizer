using System.Text.RegularExpressions;

namespace UglyToad.RegexVisualizer
{
    using System.ComponentModel;

    internal partial class MainWindow
    {
        public MainWindow(MatchCollection matchCollection)
        {
            DataContext = new MainWindowViewModel(matchCollection);
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Dispatcher.InvokeShutdown();

            base.OnClosing(e);
        }
    }
}
