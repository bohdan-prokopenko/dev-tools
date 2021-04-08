namespace DevTools.XPath.Eval.Plugin
{
    using System.IO;
    using System.Windows.Input;
    using System.Xml;

    using DevTools.Contracts.Mvvm;

    using Microsoft.Win32;

    public class XPathEvalViewModel : ViewModelBase
    {
        private XmlDocument document { get; set; } = new XmlDocument();
        public ICommand PickFileCommand;
        public ICommand EvaluateCommand;
        public ICommand SaveAsCommand;
        public bool IsFileLoaded { get; set; }
        public string FilePath { get; set; }
        public string XpathExpression { get; set; }
        public bool EvaluationResult { get; set; }
        public bool IsExpressionSet { get; set; }

        public XPathEvalViewModel()
        {
            PickFileCommand = new DelegateCommand(PickFile);
            EvaluateCommand = new DelegateCommand<string>(Evaluate);
            SaveAsCommand = new DelegateCommand(SaveAs);
        }

        private void PickFile()
        {
            var dialog = new OpenFileDialog() { Filter = "XML file (*.xml)|*.xml" };
            var result = dialog.ShowDialog();
            if (result != true) return;
            this.FilePath = dialog.FileName;
            this.document.Load(FilePath);
            this.IsFileLoaded = true;
            RaisePropertyChanged(nameof(this.IsFileLoaded));
            RaisePropertyChanged(nameof(this.FilePath));
        }

        private void Evaluate(string expression)
        {
            this.XpathExpression = expression;
            this.IsExpressionSet = string.IsNullOrWhiteSpace(this.XpathExpression);
            base.RaisePropertyChanged(nameof(this.IsExpressionSet));
            this.EvaluationResult = this.document.Evaluate(expression);
            RaisePropertyChanged(nameof(this.EvaluationResult));
        }

        private void SaveAs()
        {
            string filePath;
            var dialog = new SaveFileDialog() { Filter = "Txt file (*.txt)|*.txt" };
            var result = dialog.ShowDialog();
            if (result != true) return;
            filePath = dialog.FileName;
            File.WriteAllText(filePath, this.XpathExpression);
        }
    }
}
