using Do.TikTokDownloader.Models.Navigation;
using Do.TikTokDownloader.Services.Dependency;
using Do.TikTokDownloader.Services.NativeService;
using Do.TikTokDownloader.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Do.TikTokDownloader.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        protected readonly IDependencyService dependencyService;
        protected readonly INativeService NativeService;
        public ICommand OpenTikTokCommand { get; set; }
        public ICommand HelpCommand { get; set; }
        public ICommand ChangeLanguageCommand { get; set; }

        public MainViewModel(IDependencyService dependencyService)
        {
            OpenTikTokCommand = new Command(OpenTikTok);
            HelpCommand = new Command(OpenHelp);
            ChangeLanguageCommand = new Command(ChangeLanguage);
            this.dependencyService = dependencyService;
            this.NativeService = this.dependencyService.Get<INativeService>();
        }

        private async void ChangeLanguage(object obj)
        {
            await NavigationService.NavigateToAsync<LanguageViewModel>(true);
        }

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


        private void OpenTikTok(object obj)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                //iOS stuff
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                if (!this.NativeService.OpenAnotherApp("com.zhiliaoapp.musically"))
                {
                    Launcher.TryOpenAsync("https://www.tiktok.com");
                }
            }
        }

        private async void OpenHelp(object obj)
        {
            await NavigationService.NavigateToAsync<OnboardingViewModel>();
        }

    }
}
