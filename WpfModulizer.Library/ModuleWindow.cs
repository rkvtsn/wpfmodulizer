using System.Windows;

namespace WpfModulizer.Library
{
    public abstract class ModuleWindow<TV> : ModuleBase, IModuleWindow
        where TV : IView, new()
    {
        protected Window Window { get; set; }

        public override void Load()
        {
            this.Window = new Window {Content = View, Width = Width, Height = Height};
            this.Window.Show();
            this.Window.Title = View.Title ?? ModuleName;
        }

        public override void Boot()
        {
            this.View = new TV();
        }

        public override void Destruct()
        {
            this.Window.Close();
            this.Window = null;
        }

        public IView View { get; set; }


        private int _width = 500;
        private int _height = 300;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
    }
}