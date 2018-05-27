using Prism.Mvvm;
using Prism.Regions;

namespace Infinal.CarPc.Ui.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RequestNavigate("ContentRegion", "MenuView");
            _regionManager.RequestNavigate("BarRegion", "MenuBar");
        }

        private void BackButtonClicked()
        {
            _regionManager.RequestNavigate("ContentRegion", "MenuView");
        }
    }
}
