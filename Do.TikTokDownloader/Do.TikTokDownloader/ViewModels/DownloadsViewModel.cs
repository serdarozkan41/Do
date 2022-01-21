using Do.TikTokDownloader.Models;
using Do.TikTokDownloader.ViewModels.Base;
using Realms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Do.TikTokDownloader.ViewModels
{
    public class DownloadsViewModel : ViewModelBase
    {
        private ObservableCollection<FoundedVideo> videos;
        private Realm realmDb;
        public ICommand PlayCommand { get; protected set; }
        public ObservableCollection<FoundedVideo> Videos
        {
            get { return videos; }
            set
            {
                videos = value;
                OnPropertyChanged();
            }
        }

        public DownloadsViewModel()
        {
            realmDb = Realm.GetInstance();
            Videos = new ObservableCollection<FoundedVideo>(realmDb.All<FoundedVideo>().ToList());

            MessagingCenter.Subscribe<HomeViewModel, FoundedVideo>(this, MessageKeys.NewDownload, (sender, arg) =>
            {
                Videos.Add(arg);
            });
            PlayCommand = new Command(PlayAsync);
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        private async void PlayAsync(object obj)
        {
            ItemTappedEventArgs args = (ItemTappedEventArgs)obj;
            FoundedVideo selectedVideo = (FoundedVideo)args.Item;
            await NavigationService.NavigateToAsync<PlayerViewModel>(selectedVideo.DownloadedPath);
        }

    }
}
