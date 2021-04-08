namespace DevTools.Host
{
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
            _container = new UnityContainer();

            var mainWindow = _container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
