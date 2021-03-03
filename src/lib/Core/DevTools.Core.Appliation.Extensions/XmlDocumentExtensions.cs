namespace DevTools.Core.Application.Extensions
{
    using System.Xml;
    using System.Xml.XPath;

    public static class XmlDocumentExtensions
    {
        public static bool Evaluate(this XmlDocument xml, string xpath)
        {
            XPathNavigator navigator = xml.CreateNavigator();

            var manager = new XmlNamespaceManager(navigator.NameTable);

            _ = navigator.MoveToFollowing(XPathNodeType.Element);

            var namespaces = navigator.GetNamespacesInScope(XmlNamespaceScope.All);

            foreach (var (key, value) in namespaces)
            {
                manager.AddNamespace(key, value);
            }

            XPathExpression expression = navigator.Compile(xpath);
            expression.SetContext(manager);
            var result = expression.ReturnType switch
            {
                XPathResultType.Boolean => (bool)navigator.Evaluate(expression),
                _ => false,
            };

            return result;
        }
    }
}
