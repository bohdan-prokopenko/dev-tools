namespace DevTools.XPath.Eval.Plugin
{
    using System.ComponentModel;
    using System.Windows.Controls;

    public partial class XPathEvalControl : UserControl
    {
        private readonly XPathEvalViewModel viewModel;
        public XPathEvalControl(XPathEvalViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = this.viewModel = viewModel;
            this.viewModel.PropertyChanged += OnFileLoaded;
        }

        private void OnFileLoaded(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(this.viewModel.FilePath).Equals(e.PropertyName))
                this.browser.Navigate(this.viewModel.FilePath);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)e.Source;
            this.viewModel.EvaluateCommand.Execute(textbox.Text);
        }

        private void OnButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            switch (btn.Tag)
            {
                case "pick":
                    this.viewModel.PickFileCommand.Execute(null);
                    break;
                case "save":
                    this.viewModel.SaveAsCommand.Execute(null);
                    break;
            }
        }
    }
}
