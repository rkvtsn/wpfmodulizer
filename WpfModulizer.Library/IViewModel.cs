using System.ComponentModel;
using WpfModulizer.Library.Annotations;

namespace WpfModulizer.Library
{
    public interface IViewModel
    {
        string Value { get; set; }
    }

    abstract public class ViewModelNotify : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Value { get; set; }
    }
}