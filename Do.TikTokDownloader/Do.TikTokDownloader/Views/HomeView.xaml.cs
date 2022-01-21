using MarcTron.Plugin;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Do.TikTokDownloader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();

            CrossMTAdmob.Current.OnInterstitialLoaded += (s, args) => {
                CrossMTAdmob.Current.ShowInterstitial();
            };

            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-1670197314603951/7193226632");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var text = await Clipboard.GetTextAsync();
            if (!string.IsNullOrEmpty(text))
            {
                if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
                {
                    LbUrl.Text = text;
                }
            }
        }
    }
}