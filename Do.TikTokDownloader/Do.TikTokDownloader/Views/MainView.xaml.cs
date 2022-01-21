using Do.TikTokDownloader.Resources;
using Do.TikTokDownloader.ViewModels;
using Do.TikTokDownloader.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Do.TikTokDownloader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : TabbedPage
    {
        public MainView()
        {
            InitializeComponent();
            LbTitle.Text = "DoTikTok Downloader";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<MainViewModel, int>(this, MessageKeys.ChangeTab, (sender, arg) =>
            {
                switch (arg)
                {
                    case 0:
                        CurrentPage = HomeView;
                        LbTitle.Text = "DoTikTok Downloader";
                        break;
                    case 1:
                        CurrentPage = DownloadsView;
                        LbTitle.Text = AppResources.Downloads;
                        break;
                    //case 2:
                    //    CurrentPage = TrendsView;
                    //    LbTitle.Text = "TikTok";
                    //    break;
                }
            });

            await ((HomeViewModel)HomeView.BindingContext).InitializeAsync(null);
            await ((DownloadsViewModel)DownloadsView.BindingContext).InitializeAsync(null);
            //await ((TrendsViewModel)TrendsView.BindingContext).InitializeAsync(null);
        }

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            if (CurrentPage is DownloadsView)
            {
                await (HomeView.BindingContext as ViewModelBase).InitializeAsync(null);
                LbTitle.Text = AppResources.Downloads;
            }
            else if (CurrentPage is HomeView)
            {
                LbTitle.Text = "DoTikTok Downloader";
            }
            //else if (CurrentPage is TrendsView)
            //{
            //    LbTitle.Text = "TikTok";
            //}
        }
    }
}