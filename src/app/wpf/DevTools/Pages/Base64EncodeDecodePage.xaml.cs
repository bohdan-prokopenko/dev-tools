namespace DevTools.Pages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using DevTools.Infrastructure.DataContext;

    using Microsoft.Win32;

    /// <summary>
    /// Interaction logic for Base64EncodeDecodePage.xaml
    /// </summary>
    public partial class Base64EncodeDecodePage : Page
    {
        private readonly Base64EncodeDecodeContext context;

        public Base64EncodeDecodePage()
        {
            this.InitializeComponent();
            this.context = new Base64EncodeDecodeContext();
            this.DataContext = this.context;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            switch (textbox.Tag as string)
            {
                case "plainText":
                    this.btnEncode.IsEnabled = true;
                    break;
                case "base64":
                    this.btnDecode.IsEnabled = true;
                    break;
            }
            this.btnClipboard.IsEnabled = true;
            this.btnSaveAs.IsEnabled = true;
            this.btnClear.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            switch (btn.Tag as string)
            {
                case "encode":
                    this.context.ConvertToBase64();
                    break;
                case "decode":
                    TryConvertFromBase64();
                    break;
                case "clear":
                    ClearContextAndResetButtons();
                    break;
                case "clipboard":
                    Clipboard.SetText(this.context.Result);
                    break;
                case "saveas":
                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text file (*.txt)|*.txt|XML file (*.xml)|*.xml";
                    if (saveFileDialog.ShowDialog() == true)
                        this.context.SaveAsFile(saveFileDialog.FileName);
                    break;
                default:
                    break;
            }
        }

        private void TryConvertFromBase64()
        {
            try
            {
                this.context.ConvertFromBase64();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearContextAndResetButtons()
        {
            this.context.Clear();
            this.btnClipboard.IsEnabled = false;
            this.btnSaveAs.IsEnabled = false;
            this.btnClear.IsEnabled = false;
            this.btnDecode.IsEnabled = false;
            this.btnEncode.IsEnabled = false;
        }
    }
}
