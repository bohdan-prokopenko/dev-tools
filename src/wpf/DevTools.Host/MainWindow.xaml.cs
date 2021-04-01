namespace DevTools.Host
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = this.viewModel = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.viewModel.LoadPluginsCommand.Execute(null);
        }

        private void OnPluginMenuItemClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var parameter = (Guid)menuItem.CommandParameter;
            this.viewModel.GetPluginControl.Execute(parameter);
        }
    }
}
