using MahApps.Metro.Controls.Dialogs;
using SimpleInjector;
using System;
using System.Windows;
using TheEncryptKeeper4.Services;
using TheEncryptKeeper4.ViewModel;

namespace TheEncryptKeeper4.Common
{
    public static class Bootstrapper
    {

        public static Container Initialize()
        {
            var container = new Container();

            //register service dependencies
            container.Register<IJSONService, JSONService>(Lifestyle.Singleton);
            container.Register<IEncryptService, EncryptorService>(Lifestyle.Singleton);
            container.Register<IDialogCoordinator, DialogCoordinator>(Lifestyle.Singleton);

            //register viewmodels
            container.Register<HomeViewModel>(Lifestyle.Singleton);
            container.Register<NewEntryViewModel>(Lifestyle.Singleton);
            container.Register<ManageLoginEntryViewModel>(Lifestyle.Singleton);

            try
            {
                container.Verify();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error container missing a dependency \n" + e.Message, "Bootstrapper failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            return container;
        }

        public static void Shutdown(Container container)
        {
            // dispose any disposable objects if needed
        }
    }
}
