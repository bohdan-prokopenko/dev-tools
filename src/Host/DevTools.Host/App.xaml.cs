namespace DevTools.Host
{
    using System;
    using System.Windows;

    using Unity;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        UnityContainer _container;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            
            _container = new UnityContainer();

            var mainWindow = _container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        /// <summary>
        /// Handle the Exception occurring from UI Thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            MessageBox.Show(
                e.Exception.Message,
                e.Exception.GetType().ToString(),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        /// <summary>
        /// Handle the Exception occurring from Non UI Thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            
            MessageBox.Show(
                ex.Message,
                ex.GetType().ToString(),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
