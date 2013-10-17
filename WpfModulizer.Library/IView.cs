namespace WpfModulizer.Library
{
    public interface IView
    {
        INavigation Navigation { get; set; }

        IViewModel ViewModel { get; set; }

        string Title { get; set; }
    }
}