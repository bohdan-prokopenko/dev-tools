namespace DevTools.Host
{

    using DevTools.Host.Properties;

    public class SettingsController
    {
        private readonly UserSettings userSettings;
        private readonly AppSettings appSettings;

        public SettingsController()
        {
            this.userSettings = new UserSettings();
            this.appSettings = new AppSettings();
        }

        internal string GetPluginsEntryCatalog() =>
            this.userSettings.Catalog;

        internal string GetPluginsSearchPattern() =>
            this.appSettings.SearchPattern;

        internal void SetPluginsEntryCatalog(string pluginsCatalog) =>
            this.userSettings.Catalog = pluginsCatalog;

        internal void SavePluginsSettings() =>
            this.userSettings.Save();
    }
}
