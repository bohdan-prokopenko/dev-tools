namespace DevTools.Base64Conversions.Plugin
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;

    using DevTools.Contracts.Mvvm;
    using DevTools.Extensions;

    using Microsoft.Win32;

    public class Base64ConversionsViewModel : ViewModelBase
    {
        private string result { get; set; }
        public string Encoded { get; set; }
        public string Decoded { get; set; }
        public ICommand EncodeCommand { get; private set; }
        public ICommand DecodeCommand { get; private set; }
        public ICommand ToClipboardCommand { get; private set; }
        public ICommand ToFileCommand { get; private set; }
        public Base64ConversionsViewModel()
        {
            EncodeCommand = new DelegateCommand<string>(ToBase64);
            DecodeCommand = new DelegateCommand<string>(FromBase64);
            ToClipboardCommand = new DelegateCommand(SaveToClipboard);
            ToFileCommand = new DelegateCommand(SaveAsFile);
        }

        private void ToBase64(string text)
        {
            this.Encoded = text.ToBase64(Encoding.Default);
            base.RaisePropertyChanged(nameof(this.Encoded));
        }

        private void FromBase64(string text)
        {
            try
            {
                this.Decoded = text.FromBase64(Encoding.Default);
                base.RaisePropertyChanged(nameof(this.Decoded));
            }
            catch(FormatException ex)
            {
                MessageBox.Show(
               ex.Message,
               ex.Source,
               MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void SaveToClipboard()
        {
            Clipboard.SetText(this.result);
            MessageBox.Show(
                $"Result succesfully copied to clipboard",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void SaveAsFile()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text file (*.txt)|*.txt|XML file (*.xml)|*.xml"
            };
            
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, this.result);
            
            MessageBox.Show(
                $"Result succesfully saved as {saveFileDialog.FileName}",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
