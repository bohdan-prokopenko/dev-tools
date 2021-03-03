namespace DevTools
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using DevTools.Pages;
    using DevTools.Properties;

    using Microsoft.Extensions.DependencyInjection;


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly StartupSettings startupSettings;

        public App()
            => this.startupSettings = new StartupSettings();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new MainWindow().Show();

            if (this.startupSettings.ShowReleaseNotesWindow)
            {
                new StartupWindow().Show();
            }
        }
    }
}
