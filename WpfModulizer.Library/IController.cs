namespace WpfModulizer.Library
{
    public interface IController<TV> where TV : IView, new()
    {
        NavigationContext<TV> Navigation { get; set; }
    }
}
