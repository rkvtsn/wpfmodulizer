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

namespace ModuleVesySoft
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl , IView
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel { get; set; }
        public string Title { get; set; }
    }
}
