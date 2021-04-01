namespace DevTools.Host
{

    using DevTools.Host.Properties;

    public class SettingsController
    {
        private readonly PluginsSettings pluginsSettings;

        public SettingsController() =>
            this.pluginsSettings = new PluginsSettings();

        internal string GetPluginsEntryCatalog() =>
            this.pluginsSettings.Catalog;

        internal string GetPluginsSearchPattern() =>
            this.pluginsSettings.SearchPattern;

        internal void SetPluginsEntryCatalog(string pluginsCatalog) =>
            this.pluginsSettings.Catalog = pluginsCatalog;

        internal void SavePluginsSettings() =>
            this.pluginsSettings.Save();
    }
}
