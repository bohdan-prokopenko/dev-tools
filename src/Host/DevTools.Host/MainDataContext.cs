namespace DevTools.Host
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    using DevTools.Contracts;
    using DevTools.Contracts.Mvvm;
    using DevTools.Host.Properties;

    public class MainDataContext : ViewModelBase
    {
        private readonly AppSettings appSettings = new();
        private readonly UserSettings userSettings = new();
        private readonly TaskFactory taskFactory = new();

        private FrameworkElement selectedControl;
        private IEnumerable<Plugin> loadedPlugins;

        public ICommand LoadPluginsCommand { get; private set; }
        public ICommand GetPluginControl { get; private set; }
        public ICommand GetSettingsControl { get; private set; }

        public FrameworkElement SelectedControl
        {
            get => selectedControl;
            set
            {
                selectedControl = value;
                RaisePropertyChanged(nameof(SelectedControl));
            }
        }

        public IEnumerable<Plugin> LoadedPlugins
        {
            get => loadedPlugins;
            set
            {
                loadedPlugins = value;
                RaisePropertyChanged(nameof(LoadedPlugins));
            }
        }

        public MainDataContext()
        {
            LoadPluginsCommand = new DelegateCommand(LoadPlugins);
            GetPluginControl = new DelegateCommand<string>(CreatePluginControl);
            GetSettingsControl = new DelegateCommand(CreateSettingsControl);
        }

        private void CreateSettingsControl()
        {
            SelectedControl = new SettingsControl();
        }

        private void CreatePluginControl(string id)
        {
            var plugin = LoadedPlugins.FirstOrDefault(p => p.Id.Equals(id));
            if (plugin is null) return;

            SelectedControl = plugin.CreateControl();
            RaisePropertyChanged(nameof(this.SelectedControl));
        }

        private async void LoadPlugins()
        {
            var paths = Directory.GetFiles(
                userSettings.PluginsEntryCatalog,
                appSettings.PluginsSearchPattern,
                SearchOption.AllDirectories)
                .ToList();

            LoadedPlugins = paths.Any()
                ? await taskFactory.StartNew(() => paths.Select(path => LoadFromPath(path))
                                                        .Where(plugin => !plugin.Equals(null)))
                : new List<Plugin>();
        }

        private static Plugin LoadFromPath(string path)
        {
            Assembly assembly;
            IPlugin pluginInterface;
            Type typeInterface;
            assembly = Assembly.LoadFrom(path);
            Plugin plugin = null;
            foreach (Type assemblyType in assembly.GetExportedTypes())
            {
                typeInterface = assemblyType.GetInterface(nameof(IPlugin));
                if (typeInterface != null
                    && (assemblyType.Attributes & TypeAttributes.Abstract)
                    != TypeAttributes.Abstract)
                {
                    pluginInterface = (IPlugin)Activator.CreateInstance(assemblyType);
                    var description = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
                    var name = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
                    var version = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
                    plugin = new Plugin(pluginInterface, name, description, version);
                }
            }

            return plugin;
        }
    }
}
