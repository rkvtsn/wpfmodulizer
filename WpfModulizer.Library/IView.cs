namespace WpfModulizer.Library
{
    public interface IView
    {
        IViewModel ViewModel { get; set; }

        string Title { get; set; }
    }
}