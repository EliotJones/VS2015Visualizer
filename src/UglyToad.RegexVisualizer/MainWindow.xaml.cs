using System.Text.RegularExpressions;

namespace UglyToad.RegexVisualizer
{
    using System.ComponentModel;
    using System.Windows.Input;

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

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                var viewModel = (MainWindowViewModel) DataContext;

                viewModel.Deselect();
            }
        }
    }
}
