namespace DevTools
{
    using System.Windows;

    using DevTools.Pages;
    using DevTools.Properties;

    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        private StartupSettings settings;
        public StartupWindow()
            => this.InitializeComponent();

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.settings.ShowReleaseNotesWindow = this.showOnStartup.IsChecked ?? true;
            this.settings.Save();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ = this.NavigationFrame.Navigate(new ReleaseNotesPage());

            this.settings = new StartupSettings();
            this.showOnStartup.IsChecked = this.settings.ShowReleaseNotesWindow;
        }
    }
}
