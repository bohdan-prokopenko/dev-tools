namespace DevTools.Contracts
{
    using System;
    using System.Windows;

    public interface IPlugin
    {
        FrameworkElement CreateControl();
    }
}
