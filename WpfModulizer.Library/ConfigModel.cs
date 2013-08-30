using System;

namespace WpfModulizer.Library
{
    [Serializable]
    public class ConfigModel : IConfigModel
    {
        private string _version;
        private bool _isEnabled;

        public ConfigModel()
        {
            
        }
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }
    }
}