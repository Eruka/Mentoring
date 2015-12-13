using Helpers.PluginsDiscover;
using NotePad.Plugins.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;

namespace NotePadWinForms
{
    [Serializable]
    [SecurityPermission(SecurityAction.Demand,
                    Flags = SecurityPermissionFlag.AllFlags)]
    public partial class NotePad : Form
    {
        public NotePad()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);
            AppDomain.CurrentDomain.UnhandledException += (o, e) => {
                MessageBox.Show(((Exception)e.ExceptionObject).Message); }
            ;

           var names =
                PluginDiscover.GetAvailablePlagins(AppDomain.CurrentDomain.BaseDirectory + "\\Plugins", typeof(INotePadPlagin));

           List<NotePadPlugin> plugins = new List<NotePadPlugin>();
            foreach (var name in names)
            {
                var plugin = new NotePadPlugin(name);
                //plugin.Domain.UnhandledException += (o, e) =>
                //{
                //   MessageBox.Show(plugin.Domain.FriendlyName);
                //    AppDomain.Unload(plugin.Domain);
                //};

                //plugin.Domain.DomainUnload += (o, e) => MessageBox.Show("Unloaded");
                plugins.Add(plugin);
            }

            PluginsCmbBox.DataSource = plugins;
            PluginsCmbBox.DisplayMember = "Name";


        }

        private void TransformBtn_Click(object sender, EventArgs e)
        {
            NotePadPlugin selectedPlugin = (NotePadPlugin)PluginsCmbBox.SelectedItem;
            ResultTextBox.Text = selectedPlugin.Instanse.TransformText(SourseTextBox.Text);
        }

        private void PluginsCmbBox_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
        }
    }

    [Serializable]
    public class NotePadPlugin
    {
        public NotePadPlugin(string name)
        {
            Name = name;
            Domain = AppDomain.CreateDomain(name + " AppDomain");
            Instanse = (INotePadPlagin)Domain
                    .CreateInstanceAndUnwrap(name, name);
        }
        public string Name { get; set; }

        public INotePadPlagin Instanse { get; private set; }

        public AppDomain Domain { get; private set; }
    }
}
