namespace DevTools.Base64Conversions.Plugin
{
    using System.Windows;

    using DevTools.Contracts;

    public class Plugin : IPlugin
    {
        public FrameworkElement CreateControl()
        {
            return new Base64ConversionsControl(new Base64ConversionsViewModel());
        }
    }
}
