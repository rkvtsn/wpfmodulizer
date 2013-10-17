using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;

namespace WpfModulizer.Library
{

    /// <summary>
    /// Ядро системы
    /// </summary>
    public class Modulizer
    {
        private static readonly Modulizer Instance = new Modulizer();

        static Modulizer() { }
        private Modulizer() { }
        static public Modulizer Pool { get { return Instance; } }
        public IDictionary<Guid, IModule> CurrentModules { get; set; }

        public Modulizer Boot(string directory)
        {
            string[] dlls = Directory.GetFiles(directory, "*.dll");
            CurrentModules = new Dictionary<Guid, IModule>();
            Assembly assembly = null;
            Type type = null;
            foreach (var dll in dlls)
            {
                assembly = Assembly.LoadFrom(dll);
                string name = Path.GetFileNameWithoutExtension(dll);
                type = assembly.GetType(name + "." + name);
                var module = this.CreateModuleByType(type);
                // Клонируем модули если необходимо !
                //CloneModule(module);
            }
            return Instance;
        }
        #region @old
        //IModule instance = null;
        //{if (type == null || type.GetInterface("IModule") == null) continue;
        //instance = (IModule)type.InvokeMember(String.Empty, BindingFlags.CreateInstance, null, null, null);
        //CurrentModules.Add(instance.ModuleId.ToString(), instance);
        //instance.Initializer();}
        #endregion

        private IModule CreateModuleByType(Type type)
        {
            if (type == null || type.GetInterface("IModule") == null) return null;
            var instance = (IModule)type.InvokeMember(String.Empty, BindingFlags.CreateInstance, null, null, null);
            CurrentModules.Add(instance.ModuleId, instance);
            instance.Initializer();
            return instance;
        }

        public Modulizer Boot(string directory, Window mainWindow)
        {
            mainWindow.Closing += (sender, args) => this.Destruct();
            return this.Boot(directory);
        }

        #region @public

        public void CloneModule(Guid id)
        {
            if (CurrentModules.ContainsKey(id)) this.CreateModuleByType(CurrentModules[id].GetType());
        }

        public void CloneModule(IModule module)
        {
            if (module != null) this.CloneModule(module.ModuleId);
        }
        public void UpdateModule(Guid id)
        {
            if (CurrentModules.ContainsKey(id)) CurrentModules[id].Load();

        }

        public void RemoveModule(Guid id)
        {
            if (!CurrentModules.ContainsKey(id)) return;
            CurrentModules[id].Destruct();
            CurrentModules.Remove(id);
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
