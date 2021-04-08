namespace DevTools.Host
{
    using System;
    using System.Windows;

    using DevTools.Contracts;

    public class Plugin : IPlugin
    {
        private readonly IPlugin plugin;

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Version { get; private set; }

        public Plugin(IPlugin plugin, string name, string description, string version)
        {
            this.Id = Guid.NewGuid();
            this.plugin = plugin ?? throw new ArgumentNullException(nameof(plugin));
            this.Name = name ?? this.Id.ToString();
            this.Description = description ?? string.Empty;
            this.Version = version ?? this.Id.ToString();
        }

        public FrameworkElement CreateControl()
        {
            return this.plugin.CreateControl();
        }
    }
}
