using log4net;
using System.Windows;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ILog log = LogManager.GetLogger(typeof(App));

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            log.Debug("Initialising ...");
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            log.Debug("Ended");
        }
    }
}
