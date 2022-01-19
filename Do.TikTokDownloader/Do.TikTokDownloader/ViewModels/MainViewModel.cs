using Do.TikTokDownloader.Models.Navigation;
using Do.TikTokDownloader.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Do.TikTokDownloader.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public override Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            if (navigationData is TabParameter)
            {
                var tabIndex = ((TabParameter)navigationData).TabIndex;
                MessagingCenter.Send(this, MessageKeys.ChangeTab, tabIndex);
            }
            SettingsService.IsFirstLogin = false;
            return base.InitializeAsync(navigationData);
        }
    }
}
