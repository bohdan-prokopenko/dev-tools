namespace DevTools.Host
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public partial class MainWindow : Window
    {
        private readonly MainDataContext dataContext;

        public MainWindow()
        {
            InitializeComponent();
            dataContext = (MainDataContext)DataContext;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataContext.LoadPluginsCommand.Execute(null);
        }

        private void OnPluginMenuItemClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var parameter = (string)menuItem.CommandParameter;
            dataContext.GetPluginControl.Execute(parameter);
        }
    }
}
