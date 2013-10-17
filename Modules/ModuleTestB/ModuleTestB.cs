
using ModuleTestB.ViewModels;
using ModuleTestB.Views;
using WpfModulizer.Library;

namespace ModuleTestB
{
    [ModuleName("Mr. Test B")]
    public class ModuleTestB : ModuleWindow<MainView, ConfigModel>
    {
        public override void Boot()
        {
            base.Boot();
            //this.Subscribe("OnBoot", OnBoot);

            this.Navigation.AddView<Page1>("Page1");

            //this.Publish("OnBoot", "Hello I'am " + ModuleName);
        }
        
        private void OnBoot(dynamic data)
        {
            this.View.ViewModel.Value = data as string;
        }
    }
}
