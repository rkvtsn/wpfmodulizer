using System;
using System.Windows;
using System.Windows.Controls;
using WpfModulizer.Library;

namespace ModuleTestB.Views
{
    public partial class MainView : UserControl, IView
    {
        public MainView()
        {
            InitializeComponent();

            Action<EventMessage> someAction = x => this.MyTextBlock.Text = x.Message;
            EventAggregator.Subscribe("onClick", someAction);
        }

        public INavigation Navigation { get; set; }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
        public string Title { get; set; }

        private int _i = 0;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //EventAggregator.Publish("ClickB", new EventMessage { Message = _i++ });
            this.Navigation.GoTo("Page1");
        }
    }
}
