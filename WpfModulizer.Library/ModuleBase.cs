using System;
using System.Reflection;
using System.Threading;

namespace WpfModulizer.Library
{
    public abstract class ModuleBase<T> : IModule
        where T : ConfigModel
    {
        #region @ctor

        protected ModuleBase()
        {
            this.ModuleId = Guid.NewGuid();
            this._dllName = this.GetType().Name;
            var attrs = this.GetType().GetCustomAttributes(typeof(ModuleNameAttribute), false);
            ModuleNameAttribute attr = null;
            if (attrs.Length > 0 && (attr = (attrs[0] as ModuleNameAttribute)) != null && attr.Name != string.Empty)
                this._moduleName = attr.Name;
            else
                this._moduleName = this._dllName;
            
            ConfigurationLoad();
        }

        #endregion

        private readonly string _moduleName;
        public string ModuleName { get { return _moduleName; } }

        private readonly string _dllName;
        public string DllName { get { return _dllName; } }

        public Guid ModuleId { get; set; }

        public T Configuration { get; set; }

        
        public void ConfigurationRefresh()
        {
           this.Configuration = Config<T>.Context.Get(this.ModuleId);
        }

        public void ConfigurationLoad(string dllName)
        {
            Config<T>.Context.Load(this.ModuleId, this._dllName);
        }

        public void ConfigurationLoad()
        {
            this.ConfigurationLoad(this._dllName);
        }


        public void Initializer()
        {
            Boot();
            Load();
        }



        #region @abstract

        abstract public void Destruct();
        abstract public void Load();
        abstract public void Boot();
        
        #endregion
    }
}
