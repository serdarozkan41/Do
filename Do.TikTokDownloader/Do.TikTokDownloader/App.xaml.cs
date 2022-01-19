using Do.TikTokDownloader.Helpers;
using Do.TikTokDownloader.Resources;
using Do.TikTokDownloader.Services;
using Do.TikTokDownloader.Services.Settings;
using Do.TikTokDownloader.ViewModels.Base;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Do.TikTokDownloader
{
    public partial class App : Application
    {
        ISettingsService _settingsService;
        public App()
        {
            InitializeComponent();
            if (Preferences.ContainsKey("SelectedLanguage"))
            {
                CultureInfo language = new CultureInfo(Preferences.Get("SelectedLanguage", "en"));
                Thread.CurrentThread.CurrentUICulture = language;
                AppResources.Culture = language;
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InstalledUICulture;
                AppResources.Culture = Thread.CurrentThread.CurrentUICulture;
            }
            InitApp();
        }

        private void InitApp()
        {
            _settingsService = ViewModelLocator.Resolve<ISettingsService>();
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected async override void OnStart()
        {
            base.OnStart();
            
            await InitNavigation();


            base.OnResume();
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
