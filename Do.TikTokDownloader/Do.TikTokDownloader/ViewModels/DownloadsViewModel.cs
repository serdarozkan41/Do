using Do.TikTokDownloader.Models;
using Do.TikTokDownloader.Resources;
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
        public ICommand DeleteCommand { get; protected set; }
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
            Videos = new ObservableCollection<FoundedVideo>(realmDb.All<FoundedVideo>().OrderByDescending(s=>s.Id));

            MessagingCenter.Subscribe<HomeViewModel, FoundedVideo>(this, MessageKeys.NewDownload, (sender, arg) =>
            {
                Videos.Add(arg);
                OnPropertyChanged(nameof(Videos));
            });
            PlayCommand = new Command(PlayAsync);
            DeleteCommand = new Command(DeleteAsync);
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

        private void DeleteAsync(object obj)
        {
            FoundedVideo selectedVideo = (FoundedVideo)obj;
            Videos.Remove(selectedVideo);
            
            realmDb.Write(() =>
            {
                realmDb.Remove(selectedVideo);
            });
            OnPropertyChanged(nameof(Videos));
            DialogService.ShowToastSuccess(AppResources.SuccessDelete);
        }
    }
}
