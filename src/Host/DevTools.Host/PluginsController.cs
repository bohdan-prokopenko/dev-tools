namespace DevTools.Host
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    using DevTools.Contracts;

    public class PluginsController
    {
        private readonly List<Plugin> plugins;
        private readonly SettingsController settingsController;
        private readonly TaskFactory factory;

        public PluginsController(SettingsController settingsController)
        {
            this.settingsController = settingsController;
            this.plugins = new();
            this.factory = new();
        }

        public async Task<List<Plugin>> LoadPlugins()
        {
            string[] paths;
            try
            {
                paths = Directory.GetFiles(this.settingsController.GetPluginsEntryCatalog(),
                                                           this.settingsController.GetPluginsSearchPattern(),
                                                           SearchOption.AllDirectories);
            }
            catch (ArgumentException e)
            {
                paths = new string[] { };
            }

            if (paths.Length > 0)
            {
                await this.factory.StartNew(() =>
                        {
                            foreach (var path in paths) this.LoadFromPath(path);
                        });
            }
            return this.plugins;
        }

        private void LoadFromPath(string path)
        {
            Assembly asm;
            IPlugin iplugin;
            Type tInterface;
            asm = Assembly.LoadFrom(path);
            foreach (Type t in asm.GetExportedTypes())
            {
                tInterface = t.GetInterface(nameof(IPlugin));
                if (tInterface != null && (t.Attributes & TypeAttributes.Abstract) !=
                    TypeAttributes.Abstract)
                {
                    iplugin = (IPlugin)Activator.CreateInstance(t);
                    var description = asm.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
                    var name = asm.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
                    var version = asm.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
                    var plugin = new Plugin(iplugin, name, description, version);
                    plugins.Add(plugin);
                }
            }
        }
    }
}
