namespace DevTools.Host
{
    using System.Windows.Forms;
    using System.Windows.Input;

    using DevTools.Contracts.Mvvm;
    using DevTools.Host.Properties;

    public class SettingsDataContext : ViewModelBase
    {
        private readonly UserSettings userSettings = new();
        private readonly AppSettings appSettings = new();

        public string PluginsCatalog
        {
            get { return userSettings.PluginsEntryCatalog; }
            set
            {
                userSettings.PluginsEntryCatalog = value;
                userSettings.Save();
                RaisePropertyChanged(nameof(PluginsCatalog));
            }
        }
        public string PluginsSearchPattern { get => appSettings.PluginsSearchPattern; }

        public ICommand SaveSettingsCommand => new DelegateCommand(SaveSettings);
        public ICommand OpenFolderDialogCommand => new DelegateCommand(OpenFolderDialog);

        private void OpenFolderDialog()
        {
            var dialog = new FolderBrowserDialog();
            var dialogResult = dialog.ShowDialog();
            if (dialogResult.Equals(DialogResult.OK))
                PluginsCatalog = dialog.SelectedPath;
        }

        private void SaveSettings()
        {
            userSettings.Save();
            MessageBox.Show(
                null,
                "The application settings have changed, please restart the application for these changes to take effect.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
