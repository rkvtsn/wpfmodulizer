
using ModuleTestB.ViewModels;
using ModuleTestB.Views;
using WpfModulizer.Library;

namespace ModuleTestB
{
    [ModuleName("Mr. Test B")]
    public class ModuleTestB : ModuleWindow<MainView>
    {
        public override void Boot()
        {
            base.Boot();
            //this.Subscribe("OnBoot", OnBoot);
            
            //this.Publish("OnBoot", "Hello I'am " + ModuleName);
        }
        
        private void OnBoot(dynamic data)
        {
            this.View.ViewModel.Value = data as string;
        }
    }
}
