using System.Windows;
using WpfModulizer.Library;

namespace WpfModulizer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Modulizer.Pool.Boot(
                directory: @".\Modules",
                mainWindow: this
            );
        }

    }
}
