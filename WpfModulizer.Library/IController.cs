using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfModulizer.Library
{
    public interface IController <TV> where TV : IView
    {
        NavigationContext<TV> Navigation { get; set; }
    }

    public class NavigationContext<TV> where TV : IView
    {
        private readonly Dictionary<string, IView> _views;
        private Window _window;

        public NavigationContext(Window window)
        {
            _views = new Dictionary<string, IView>();
            _window = window;
            PrepareWindow();
        }
        
        public void Destruct()
        {
            // TODO
            this._views.Clear();
        }

        private void PrepareWindow()
        {
            this._window.Content = _views["Home"];
        }

        public void GoTo(string viewName)
        {

        }

        public void GoHome()
        {
            
        }

        public void AddView<T>(string viewName) where T : IView
        {
            
        }

        public void ShowInWindow(string viewName)
        {
            
        }
    }
}
