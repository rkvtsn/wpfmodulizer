using System.Windows;

namespace WpfModulizer.Library
{
    public abstract class ModuleWindow<TV, TC> : ModuleBase<TC>, IModuleWindow, IController<TV>
        where TV : IView, new()
        where TC : ConfigModel
    {
        protected Window Window { get; set; }

        public override void Load()
        {
            this.Window.Show();
            this.Window.Title = Window.Title ?? ModuleName;
        }

        public override void Boot()
        {
            this.Window = new Window { Width = Width, Height = Height }; //Content = View,
            this.Navigation = new NavigationContext<TV>(this.Window);
            //this.View = new TV();
        }

        public override void Destruct()
        {
            this.Window.Close();
            this.Window = null;
        }

        public IView View { get; set; }

        public NavigationContext<TV> Navigation { get; set; }

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

    public abstract class ModuleWindow<TV> : ModuleWindow<TV, ConfigModel> where TV : IView, new()
    {
        
    }
}