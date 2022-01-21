using Do.TikTokDownloader.Models;
using Do.TikTokDownloader.Services.RequestProvider;
using Do.TikTokDownloader.ViewModels.Base;
using MarcTron.Plugin;
using Realms;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Do.TikTokDownloader.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        #region PROPS
        private string tiktokVideoUrl;
        private string DownloadBaseUrl = "https://savett.cc/en/download?url=";
        private string InfoBaseUrl = "https://www.tiktok.com/oembed?url=";
        private FoundedVideo foundedVideo;
        private FoundedVideo lastVideo;
        private bool showFounded;
        private bool isFounded;
        private Realm realmDb;

        public string TikTokVideoUrl
        {
            get { return tiktokVideoUrl; }
            set
            {
                tiktokVideoUrl = value;
                OnPropertyChanged();
            }
        }
        public FoundedVideo FoundedVideo
        {
            get { return foundedVideo; }
            set
            {
                foundedVideo = value;
                OnPropertyChanged();
            }
        }
        public FoundedVideo LastVideo
        {
            get { return lastVideo; }
            set
            {
                lastVideo = value;
                OnPropertyChanged();
            }
        }
        public bool IsFounded
        {
            get { return isFounded; }
            set
            {
                isFounded = value;
                OnPropertyChanged();
            }
        }
        public bool ShowFounded
        {
            get { return showFounded; }
            set
            {
                showFounded = value;
                OnPropertyChanged();
            }
        }
        public ICommand PasteCommand { get; set; }
        public ICommand DownloadCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand PlayCommand { get; protected set; }
        protected readonly IRequestProvider _requestProvider;
        #endregion

        public HomeViewModel(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
            PasteCommand = new Command(PasteAsync);
            DownloadCommand = new Command(DownloadAsync);
            PlayCommand = new Command(PlayAsync);
            ShareCommand = new Command(ShareAsync);
            realmDb = Realm.GetInstance();
            var statusWrite = Permissions.CheckStatusAsync<Permissions.StorageWrite>().Result;
            if (statusWrite != PermissionStatus.Granted)
            {
                statusWrite = Permissions.RequestAsync<Permissions.StorageWrite>().Result;
                if (statusWrite != PermissionStatus.Granted)
                {
                    DialogService.ShowToastError("İndirme işlemi için izne ihtiyaç vardır. Akti durumda uygulama düzgün çalışmayacaktır.");
                }
            }

            LastVideo = realmDb.All<FoundedVideo>().OrderByDescending(s => s.Id).FirstOrDefault();
            if (LastVideo != null)
            {
                FoundedVideo = LastVideo;
                ShowFounded = true;
            }

            CrossMTAdmob.Current.OnInterstitialLoaded += (s, args) =>
            {
                CrossMTAdmob.Current.ShowInterstitial();
            };
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        private async void PasteAsync(object obj)
        {
            var text = await Clipboard.GetTextAsync();
            if (!string.IsNullOrEmpty(text))
            {
                Uri uriResult;
                bool result = Uri.TryCreate(text, UriKind.Absolute, out uriResult)
                    && uriResult.Scheme == Uri.UriSchemeHttps;
                if (result)
                {
                    TikTokVideoUrl = text;
                }
                else
                {
                    DialogService.ShowToastWarning("Geçersiz URL");
                }
            }
        }

        private async void DownloadAsync(object obj)
        {

            if (!string.IsNullOrEmpty(TikTokVideoUrl))
            {
                if (Uri.IsWellFormedUriString(TikTokVideoUrl, UriKind.Absolute))
                {
                    var statusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                    if (statusWrite != PermissionStatus.Granted)
                    {
                        statusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
                        if (statusWrite != PermissionStatus.Granted)
                        {
                            DialogService.ShowToastError("İndirme işlemi için izne ihtiyaç vardır. Akti durumda uygulama düzgün çalışmayacaktır.");
                            return;
                        }
                    }


                    await DownloadVideoAsync();
                }
                else
                {
                    DialogService.ShowToastWarning("Geçersiz URL");
                }
            }
            else
            {
                DialogService.ShowToastWarning("Lütfen geçerli bir url giriniz ve ardından indir tuşuna basınız.");
            }
        }

        private async Task DownloadVideoAsync()
        {
            IsBusy = true;
            try
            {
                var cleanUrl = await _requestProvider.GetUrlAsync(TikTokVideoUrl);

                if (string.IsNullOrEmpty(cleanUrl))
                {
                    DialogService.ShowToastError("Çok sık işlem yapıyorsunuz lütfen tekrar deneyiniz.");
                    IsBusy = false;
                    return;
                }
                else
                {
                    var _record = realmDb.All<FoundedVideo>().Where(s => s.CleanUrl == cleanUrl).FirstOrDefault();
                    if (_record != null)
                    {
                        DialogService.ShowToastError("Daha önce indirilmiş kayıt.");
                        IsBusy = false;
                        return;
                    }
                }
                var videoInfo = await _requestProvider.GetAsync<VideoInfo>($"{InfoBaseUrl}{cleanUrl}");
                if (videoInfo is null)
                {
                    DialogService.ShowToastError("Çok sık işlem yapıyorsunuz lütfen tekrar deneyiniz.");
                    IsBusy = false;
                    return;
                }

                var resultHtml = await _requestProvider.GetHtmlAsync($"{DownloadBaseUrl}{cleanUrl}");
                if (string.IsNullOrEmpty(resultHtml))
                {
                    DialogService.ShowToastError("Çok sık işlem yapıyorsunuz lütfen tekrar deneyiniz.");
                    IsBusy = false;
                    return;
                }

                var cleanHtml = WebUtility.UrlDecode(resultHtml);
                Regex linkFilter = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)");
                var linkList = linkFilter.Matches(cleanHtml);

                foreach (Match linkItem in linkList)
                {
                    if (linkItem.Value.Contains("tiktokcdn.com") && !linkItem.Value.Contains("jpeg"))
                    {
                        if (cleanUrl != linkItem.Value)
                        {
                            FoundedVideo = new FoundedVideo()
                            {
                                Caption = videoInfo.author_name,
                                Description = videoInfo.title,
                                CoverImgUrl = videoInfo.thumbnail_url,
                                DownloadUrl = linkItem.Value.Replace("&amp", String.Empty),
                                CleanUrl = cleanUrl
                            };
                            ShowFounded = true;
                            IsFounded = true;
                            break;
                        }
                    }
                }

                if (!IsFounded)
                {
                    DialogService.ShowToastError("Lütfen tekrar deneyin bir hata oluştu!");
                    IsBusy = false;
                    return;
                }

                var downloadedPath = await _requestProvider.DownloadByteAsync(FoundedVideo.DownloadUrl);

                if (downloadedPath is null)
                {
                    DialogService.ShowToastError("Lütfen tekrar deneyin bir hata oluştu!");
                    IsBusy = false;
                    return;
                }

                FoundedVideo.DownloadedPath = downloadedPath;
                foundedVideo.DownloadedDate = DateTime.Now;

                var maxVideoId = 0;
                if (LastVideo != null)
                {
                    maxVideoId = LastVideo.Id;
                }

                FoundedVideo.Id = maxVideoId + 1;
                realmDb.Write(() =>
                {
                    realmDb.Add(FoundedVideo);
                });
                LastVideo = realmDb.All<FoundedVideo>().OrderByDescending(s => s.Id).FirstOrDefault();
                FoundedVideo = LastVideo;
                MessagingCenter.Send(this, MessageKeys.NewDownload,FoundedVideo);
                DialogService.ShowToastSuccess("İndirme başarılı indirilenler klasöründen erişebilirsiniz.");
                TikTokVideoUrl = string.Empty;
                IsBusy = false;
                CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-1670197314603951/7193226632");
            }
            catch (Exception ex)
            {
                DialogService.ShowToastError("Lütfen daha sonra tekrar deneyiniz.");
            }

            IsBusy = false;
        }

        private async void PlayAsync(object obj)
        {
            await NavigationService.NavigateToAsync<PlayerViewModel>(FoundedVideo.DownloadedPath);
        }

        private async void ShareAsync(object obj)
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = FoundedVideo.Caption,
                File = new ShareFile(FoundedVideo.DownloadedPath)
            });
        }
    }
}

public class VideoInfo
{
    public string title { get; set; }
    public string author_name { get; set; }
    public string thumbnail_url { get; set; }
}
//Puanla bizi
//var dependencyService = ViewModelLocator.Resolve<IDependencyService>();
//var e = dependencyService.Get<IInAppReviewService>();
//e.LaunchReview();


//İndirme sonrası reklam açma
//CrossMTAdmob.Current.OnInterstitialLoaded += (s, args) => {
//    CrossMTAdmob.Current.ShowInterstitial();
//};

//CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-1670197314603951/7193226632");