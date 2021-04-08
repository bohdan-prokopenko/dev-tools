namespace DevTools.Host
{
    using System.Windows.Controls;

    public partial class SettingsControl : UserControl
    {
        private readonly SettingsViewModel viewModel;
        public SettingsControl(SettingsViewModel viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();
            this.DataContext = this.viewModel;
        }

        private void OnControlLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.viewModel.LoadSettingsCommand.Execute(null);
        }
    }
}
