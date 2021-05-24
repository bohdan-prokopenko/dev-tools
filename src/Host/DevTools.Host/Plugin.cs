namespace DevTools.Host
{
    using System;
    using System.Windows;

    using DevTools.Contracts;

    public class Plugin : IPlugin
    {
        private readonly IPlugin plugin;

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Version { get; private set; }

        public Plugin(IPlugin plugin, string name, string description, string version)
        {
            this.plugin = plugin ?? throw new ArgumentNullException(nameof(plugin));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Version = version ?? throw new ArgumentNullException(nameof(version));
            Id = name + version;
        }

        public FrameworkElement CreateControl()
        {
            return plugin.CreateControl();
        }
    }
}
