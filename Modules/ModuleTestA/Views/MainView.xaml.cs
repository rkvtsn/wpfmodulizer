using System;
using System.Collections.Generic;
using WpfModulizer.Library;

namespace ModuleTestA.Views
{
    public partial class MainView : IView
    {
        public MainView()
        {
            InitializeComponent();
            Title = "Hello World";

            var subscription = EventAggregator.Subscribe("ClickB", SomeAction);
            //EventAggregator.UnSubscribe(subscription);
        }

        public void SomeAction(EventMessage msg)
        {
            int temp = msg.MessageAs<int>() * 2;
            this.world.Text = temp.ToString();
        }

        public INavigation Navigation { get; set; }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }

        public string Title { get; set; }

        private int _i = 0;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var em = new EventMessage();
            em.Messages["text"] = "Hello from A " + (_i++).ToString();

            EventAggregator.Publish("onClick", em);
        }


    }

}
