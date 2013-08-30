using System;

namespace WpfModulizer.Library
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ModuleNameAttribute : System.Attribute
    {
        private readonly string _name;

        public string Name { get { return _name; } }

        public ModuleNameAttribute(string name)
        {
            this._name = name;
        }
    }
}
