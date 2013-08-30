using System;
using System.Collections.Generic;

namespace WpfModulizer.Library
{
    public class Config<T> where T : ConfigModel
    {
        #region @singleton

        private Config() { _configurations = new Dictionary<string, ConfigModel>(); }
        readonly private static Config<T> ConfigContext = new Config<T>();
        public static Config<T> Context { get { return ConfigContext; } }

        #endregion




        public void Save(string moduleId)
        {
            try
            {
                var model = Get(moduleId);
                // TODO
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Load(string moduleId, string dllName)
        {
            try
            {
                // TODO
                T model = null;
                // TODO
                this.Set(moduleId, model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private readonly IDictionary<string, ConfigModel> _configurations;

        public void Remove(string moduleId)
        {
            if (this._configurations.ContainsKey(moduleId)) { this._configurations.Remove(moduleId); }
        }

        public T Get(string moduleId)
        {
            if (this._configurations.ContainsKey(moduleId)) { return (T)this._configurations[moduleId]; }
            return null;
        }

        public void Set(string moduleId, T config)
        {
            if (!this._configurations.ContainsKey(moduleId)) { this._configurations[moduleId] = config; }
            this._configurations.Add(moduleId, config);
        }
    }
}

