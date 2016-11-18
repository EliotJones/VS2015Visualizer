namespace Visualizer
{
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Input;

    internal partial class MainWindow
    {
        public MainWindow(string value)
        {
            DataContext = new MainWindowViewModel(value);

            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Super important!
            Dispatcher.InvokeShutdown();

            base.OnClosing(e);
        }

        public static void DisableWpfTabletSupport()
        {
            // Get a collection of the tablet devices for this window.  
            var devices = Tablet.TabletDevices;

            if (devices.Count == 0)
            {
                return;
            }
           
            var inputManagerType = typeof(InputManager);
            
            var stylusLogic = inputManagerType.InvokeMember("StylusLogic",
                        BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                        null, InputManager.Current, null);

            if (stylusLogic == null)
            {
                return;
            }

            var stylusLogicType = stylusLogic.GetType();
            
            while (devices.Count > 0)
            {
                // Remove the first tablet device in the devices collection.
                stylusLogicType.InvokeMember("OnTabletRemoved",
                        BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
                        null, stylusLogic, new object[] { (uint)0 });
            }            
        }
    }
}
