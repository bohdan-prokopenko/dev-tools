namespace DevTools.Host
{
    using System.Windows.Forms;
    using System.Windows.Input;

    using DevTools.Contracts.Mvvm;

    public class SettingsViewModel : ViewModelBase
    {
        private readonly SettingsController settingsController;

        public string PluginsCatalog { get; set; }
        public string PluginsSearchPattern { get; set; }

        public ICommand SaveSettingsCommand { get; private set; }
        public ICommand OpenFolderDialogCommand { get; private set; }
        public ICommand LoadSettingsCommand { get; private set; }

        public SettingsViewModel(SettingsController settingsController)
        {
            this.settingsController = settingsController;
            SaveSettingsCommand = new DelegateCommand(SaveSettings);
            OpenFolderDialogCommand = new DelegateCommand(OpenFolderDialog);
            LoadSettingsCommand = new DelegateCommand(LoadSettings);
        }

        private void LoadSettings()
        {
            this.PluginsCatalog = this.settingsController.GetPluginsEntryCatalog();
            this.PluginsSearchPattern = this.settingsController.GetPluginsSearchPattern();
            base.RaisePropertyChanged(nameof(this.PluginsCatalog));
            base.RaisePropertyChanged(nameof(this.PluginsSearchPattern));
        }

        private void OpenFolderDialog()
        {
            var dialog = new FolderBrowserDialog();
            var dialogResult = dialog.ShowDialog();
            if (dialogResult.Equals(DialogResult.OK))
                this.PluginsCatalog = dialog.SelectedPath;
            base.RaisePropertyChanged(nameof(this.PluginsCatalog));
        }

        private void SaveSettings()
        {
            this.settingsController.SetPluginsEntryCatalog(this.PluginsCatalog);
            this.settingsController.SavePluginsSettings();
            MessageBox.Show(
                null,
                "The application settings have changed, please restart the application for these changes to take effect.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
