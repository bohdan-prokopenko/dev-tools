namespace DevTools.XPath.Eval.Plugin
{
    using System.Windows;

    using DevTools.Contracts;

    public class Plugin : IPlugin
    {
        public FrameworkElement CreateControl()
            => new XPathEvalControl(new XPathEvalViewModel());
    }
}
