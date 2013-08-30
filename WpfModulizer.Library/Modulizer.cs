using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;

namespace WpfModulizer.Library
{
    public class Modulizer
    {
        private static readonly Modulizer Instance = new Modulizer();

        static Modulizer() { }
        private Modulizer() { }
        static public Modulizer Pool { get { return Instance; } }
        public IDictionary<string, IModule> CurrentModules { get; set; }

        public Modulizer Boot(string directory)
        {
            string[] dlls = Directory.GetFiles(directory, "*.dll");
            CurrentModules = new Dictionary<string, IModule>();
            Assembly assembly = null;
            Type type = null;
            //IModule instance = null;
            foreach (var dll in dlls)
            {
                assembly = Assembly.LoadFrom(dll);
                string name = Path.GetFileNameWithoutExtension(dll);
                type = assembly.GetType(name + "." + name);
                //if (type == null || type.GetInterface("IModule") == null) continue;
                //instance = (IModule)type.InvokeMember(String.Empty, BindingFlags.CreateInstance, null, null, null);
                //CurrentModules.Add(instance.ModuleId.ToString(), instance);
                //instance.Initializer();
                var module = this.CreateModuleByType(type);
                CloneModule(module);
            }
            return Instance;
        }

        private IModule CreateModuleByType(Type type)
        {
            if (type == null || type.GetInterface("IModule") == null) return null;
            var instance = (IModule)type.InvokeMember(String.Empty, BindingFlags.CreateInstance, null, null, null);
            CurrentModules.Add(instance.ModuleId.ToString(), instance);
            instance.Initializer();
            return instance;
        }

        public Modulizer Boot(string directory, Window mainWindow)
        {
            mainWindow.Closing += (sender, args) => this.Destruct();
            return this.Boot(directory);
        }

        #region @public

        public void CloneModule(string guidstr)
        {
            if (CurrentModules.ContainsKey(guidstr)) this.CreateModuleByType(CurrentModules[guidstr].GetType());
        }

        public void CloneModule(IModule module)
        {
            if (module != null) this.CloneModule(module.ModuleId.ToString());
        }
        public void UpdateModule(string guidstr)
        {
            if (CurrentModules.ContainsKey(guidstr)) CurrentModules[guidstr].Load();

        }

        public void RemoveModule(string guidstr)
        {
            if (!CurrentModules.ContainsKey(guidstr)) return;
            CurrentModules[guidstr].Destruct();
            CurrentModules.Remove(guidstr);
        }

        public void Destruct()
        {
            foreach (var currentModule in CurrentModules)
                currentModule.Value.Destruct();
            CurrentModules.Clear();
        }

        #endregion @public
    }
}
