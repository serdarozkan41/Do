
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Do.TikTokDownloader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerView : ContentPage
    {
        public PlayerView()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}