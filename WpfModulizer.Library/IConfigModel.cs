namespace WpfModulizer.Library
{
    public interface IConfigModel
    {
        bool IsEnabled { get; set; }

        string Version { get; set; }
    }
}