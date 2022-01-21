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

            if (Device.RuntimePlatform == Device.iOS)
            {
                CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-1670197314603951/4958849494");
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-1670197314603951/7193226632");
            }
            
        }
    }
}