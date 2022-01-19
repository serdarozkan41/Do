using Do.TikTokDownloader.Services.Dependency;
using Do.TikTokDownloader.Services.Enviroment;
using Do.TikTokDownloader.ViewModels.Base;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Do.TikTokDownloader.Helpers
{
    public static class TheTheme
    {
        const int theme = 0;
        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }

        public static void SetTheme()
        {
            switch (Theme)
            {
                //default
                case 0:
                    App.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                //light
                case 1:
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                //dark
                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }

            var nav = App.Current.MainPage as Xamarin.Forms.NavigationPage;

            var dependencyService = ViewModelLocator.Resolve<IDependencyService>();
            var e = dependencyService.Get<IThemeService>();

            if (App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(Color.Black, false);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.Black;
                    nav.BarTextColor = Color.White;
                }
            }
            else
            {
                Debug.WriteLine("Light Mood");
                e?.SetStatusBarColor(Color.White, true);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.White;
                    nav.BarTextColor = Color.Black;
                }
            }


        }
    }
}
