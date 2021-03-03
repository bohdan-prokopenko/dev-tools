namespace DevTools.Infrastructure.DataContext
{
    using System.IO;
    using System.Xml;

    using DevTools.Core.Application.Extensions;

    public class XPathEvaluationContext : DataContextBase
    {
        private readonly XmlDocument xmlDocument;

        public string XPathExpression { get; set; }
        public bool? XPathExpressionResult { get; private set; }
        public bool IsFileLoaded { get; private set; } = false;
        public bool IsXPathExpressionSet => !string.IsNullOrWhiteSpace(XPathExpression);

        public XPathEvaluationContext()
        {
            this.xmlDocument = new XmlDocument();
        }

        public void LoadXml(string path)
        {
            this.xmlDocument.Load(path);
            this.IsFileLoaded = true;
            this.Notify(nameof(this.IsFileLoaded));
        }

        public void EvaluateXPathExpression()
        {
            this.XPathExpressionResult = this.xmlDocument.Evaluate(this.XPathExpression);
            this.Notify(nameof(this.XPathExpressionResult));
            this.Notify(nameof(this.IsXPathExpressionSet));
        }

        public void Reset()
        {
            this.XPathExpressionResult = null;
            this.Notify(nameof(this.XPathExpressionResult));
        }

        public void SaveAsFile(string path)
        {
            File.WriteAllText(path, this.XPathExpression);
        }
    }
}
