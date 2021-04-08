namespace DevTools.Host
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    using DevTools.Contracts;
    using DevTools.Contracts.Mvvm;

    public class MainViewModel : ViewModelBase
    {
        private readonly PluginsController controller;
        private readonly SettingsControl settingsControl;
        
        public ICommand LoadPluginsCommand { get; private set; }
        public ICommand GetPluginControl { get; private set; }
        public ICommand GetSettingsControl { get; private set; }

        public FrameworkElement SelectedControl { get; set; }

        public IEnumerable<Plugin> LoadedPlugins { get; set; }

        public MainViewModel(PluginsController controller, SettingsControl settingsControl)
        {
            this.controller = controller;
            this.settingsControl = settingsControl;

            LoadPluginsCommand = new DelegateCommand(LoadPlugins);
            GetPluginControl = new DelegateCommand<Guid>(CreatePluginControl);
            GetSettingsControl = new DelegateCommand(CreateSettingsControl);
        }

        private void CreateSettingsControl()
        {
            this.SelectedControl = settingsControl;
            RaisePropertyChanged(nameof(SelectedControl));
        }

        private void CreatePluginControl(Guid id)
        {
            var plugin = this.LoadedPlugins.FirstOrDefault(p => p.Id.Equals(id));
            if (plugin is null) return;
            
            this.SelectedControl = plugin.CreateControl();
            base.RaisePropertyChanged(nameof(this.SelectedControl));
        }

        private async void LoadPlugins()
        {
            var plugins = await this.controller.LoadPlugins();
            if (plugins.Count.Equals(0)) return;
            
            this.LoadedPlugins = plugins;
            base.RaisePropertyChanged(nameof(this.LoadedPlugins));
        }
    }
}
