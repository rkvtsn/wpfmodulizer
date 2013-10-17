using System;

namespace WpfModulizer.Library
{
    public interface IModule //<T> where T : ConfigModel
    {
        string ModuleName { get; }
        string DllName { get; }
        
        Guid ModuleId { get; set; }
        //T Configuration { get; set; }
        

        #region @methods

        void ConfigurationRefresh();
        void ConfigurationLoad(string dllName);
        void ConfigurationLoad();

        void Initializer();

        void Boot();

        void Load();

        void Destruct();

        #endregion
    }
}