using System;
using System.Collections.Generic;

namespace WpfModulizer.Library
{
    /// <summary>
    /// Конфигуратор
    /// </summary>
    /// <typeparam name="T">XML модель конфига</typeparam>
    public class Config<T> where T : ConfigModel
    {
        #region @singleton

        private Config() { _configurations = new Dictionary<Guid, ConfigModel>(); }
        readonly private static Config<T> ConfigContext = new Config<T>();
        public static Config<T> Context { get { return ConfigContext; } }

        #endregion
        
        /// <summary>
        /// Сохранить модуль
        /// </summary>
        /// <param name="moduleId">Guid модуля</param>
        public void Save(Guid moduleId)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId">Guid модуля</param>
        /// <param name="dllName"></param>
        public void Load(Guid moduleId, string dllName)
        {
            try
            {
                // TODO
                //this.Set(moduleId, model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SetDefault(string moduleId)
        {
            //TODO
        }


        private readonly IDictionary<Guid, ConfigModel> _configurations;

        public void Remove(Guid moduleId)
        {
            if (this._configurations.ContainsKey(moduleId)) { this._configurations.Remove(moduleId); }
        }

        public T Get(Guid moduleId)
        {
            if (this._configurations.ContainsKey(moduleId)) { return (T)this._configurations[moduleId]; }
            return null;
        }

        public void Set(Guid moduleId, T config)
        {
            if (!this._configurations.ContainsKey(moduleId)) { this._configurations[moduleId] = config; }
            this._configurations.Add(moduleId, config);
        }
    }
}

