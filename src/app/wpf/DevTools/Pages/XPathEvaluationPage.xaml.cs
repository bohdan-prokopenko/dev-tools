namespace DevTools.Pages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using DevTools.Infrastructure.DataContext;

    using Microsoft.Win32;

    /// <summary>
    /// Interaction logic for XPathEvaluationPage.xaml
    /// </summary>
    public partial class XPathEvaluationPage : Page
    {
        private readonly XPathEvaluationContext context;
        private readonly OpenFileDialog openFileDialog;
        private readonly SaveFileDialog saveFileDialog;

        public XPathEvaluationPage()
        {
            InitializeComponent();
            this.DataContext = this.context = new XPathEvaluationContext();
            this.openFileDialog = new OpenFileDialog
            {
                CheckPathExists = true,
                Filter = "XML file (*.xml)|*.xml"
            };

            this.saveFileDialog = new SaveFileDialog
            {
                Filter = "Text file (*.txt)|*.txt"
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            switch (btn.Tag)
            {
                case "pickFile":
                    var openResult = this.openFileDialog.ShowDialog() ?? false;
                    if (openResult) this.context.LoadXml(this.openFileDialog.FileName);
                    this.webBrowser.Navigate(this.openFileDialog.FileName);
                    break;
                case "evaluate":
                    try
                    {
                        this.context.EvaluateXPathExpression();
                        if (this.context.XPathExpressionResult ?? false) this.textBlockResult.Background = Brushes.Green;
                        else this.textBlockResult.Background = Brushes.Red;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case "reset":
                    this.context.Reset();
                    this.textBlockResult.Background = null;
                    break;
                case "saveas":
                    var saveResult = this.saveFileDialog.ShowDialog() ?? false;
                    if (saveResult) this.context.SaveAsFile(saveFileDialog.FileName);
                    break;
            }
        }
    }
}
