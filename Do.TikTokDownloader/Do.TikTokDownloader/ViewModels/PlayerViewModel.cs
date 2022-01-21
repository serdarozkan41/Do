using Do.TikTokDownloader.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Do.TikTokDownloader.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        private string videoPath;

        public string VideoPath
        {
            get { return videoPath; }
            set
            {
                videoPath = value;
                OnPropertyChanged();
            }
        }
        public ICommand ShareCommand { get; set; }

        public PlayerViewModel()
        {
            ShareCommand = new Command(ShareAsync);
        }
        public override Task InitializeAsync(object navigationData)
        {
            VideoPath = navigationData.ToString();
            return base.InitializeAsync(navigationData);
        }

        private async void ShareAsync(object obj)
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                File = new ShareFile(VideoPath)
            });
        }
    }

}
