using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfModulizer.Library;

namespace ModuleTestB.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page, IView
    {
        public Page1()
        {
            InitializeComponent();
        }


        public INavigation Navigation { get; set; }
        public IViewModel ViewModel { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Navigation.GoHome();
        }
    }
}
