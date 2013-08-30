namespace WpfModulizer.Library
{
    abstract public class ModuleService<T, TC> : ModuleBase<TC>
        where T : IService, new() where TC : ConfigModel
    {

        public override void Boot()
        {
            this.Service = new T();
            Service.Run();
        }

        public override void Destruct()
        {
            this.Service.Close();
        }

        public override void Load()
        {
            this.Service.Run();
        }

        public T Service { get; set; }
    }
}