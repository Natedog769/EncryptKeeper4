using SimpleInjector;
using System.Windows;
using TheEncryptKeeper4.Common;

namespace TheEncryptKeeper4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize the container
            Container = Bootstrapper.Initialize();

        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            // Shutdown the container
            //Bootstrapper.Shutdown(container);
        }
    }
}
