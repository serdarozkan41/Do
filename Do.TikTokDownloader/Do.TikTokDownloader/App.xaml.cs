using Do.TikTokDownloader.Helpers;
using Do.TikTokDownloader.Resources;
using Do.TikTokDownloader.Services;
using Do.TikTokDownloader.Services.Settings;
using Do.TikTokDownloader.ViewModels.Base;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Do.TikTokDownloader
{
    public partial class App : Application
    {
        private static FlowDirection globalFlow;

        public static FlowDirection GlobalFlow
        {
            get { return globalFlow; }
            set { globalFlow = value; }
        }


        ISettingsService _settingsService;
        public App()
        {
            InitializeComponent();

            AppCenter.Start("android=6cd5f294-3003-44e0-a08a-57d48cca1d0a;", typeof(Analytics), typeof(Crashes));

            if (Preferences.ContainsKey("SelectedLanguage"))
            {
                CultureInfo language = new CultureInfo(Preferences.Get("SelectedLanguage", "en"));
                Thread.CurrentThread.CurrentUICulture = language;
                AppResources.Culture = language;

                if (Preferences.Get("SelectedLanguage", "en") == "ar")
                {
                    GlobalFlow = FlowDirection.RightToLeft;
                }
                else
                {
                    GlobalFlow = FlowDirection.MatchParent;
                }
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InstalledUICulture;
                AppResources.Culture = Thread.CurrentThread.CurrentUICulture;
                GlobalFlow = FlowDirection.MatchParent;
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
