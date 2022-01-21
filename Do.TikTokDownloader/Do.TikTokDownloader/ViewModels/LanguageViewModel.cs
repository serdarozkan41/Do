using Do.TikTokDownloader.Resources;
using Do.TikTokDownloader.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Do.TikTokDownloader.ViewModels
{
    public class LanguageViewModel : ViewModelBase
    {
        #region PROPS
        private ObservableCollection<LanguageDataModel> languages;
        private bool MainSender = false;
        public ObservableCollection<LanguageDataModel> Languages
        {
            get { return languages; }
            set
            {
                languages = value;
                OnPropertyChanged();
            }
        }
        public ICommand ItemTappedCommand { get; set; }
        #endregion

        #region FUNCS
        public LanguageViewModel()
        {
            ItemTappedCommand = new Command(ItemTappedAsync);
        }

        public async override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                MainSender = (bool)navigationData;
            }
            Languages = new ObservableCollection<LanguageDataModel>();
            Languages.Add(new LanguageDataModel { LanguageCode = "tr", LanguageIcon = "ic_tr", LanguageName = "Turkish" });//
            Languages.Add(new LanguageDataModel { LanguageCode = "en", LanguageIcon = "ic_uk", LanguageName = "English (US)" });//
            Languages.Add(new LanguageDataModel { LanguageCode = "ru", LanguageIcon = "ic_ru", LanguageName = "Russian" });//
            Languages.Add(new LanguageDataModel { LanguageCode = "pt", LanguageIcon = "ic_pt", LanguageName = "Portuguese" });//
            Languages.Add(new LanguageDataModel { LanguageCode = "id", LanguageIcon = "ic_id", LanguageName = "Indonesian" });
            Languages.Add(new LanguageDataModel { LanguageCode = "hi", LanguageIcon = "ic_hi", LanguageName = "Hindi" });//
            Languages.Add(new LanguageDataModel { LanguageCode = "fr", LanguageIcon = "ic_fr", LanguageName = "French" });
            Languages.Add(new LanguageDataModel { LanguageCode = "es", LanguageIcon = "ic_es", LanguageName = "Spanish" });//
            Languages.Add(new LanguageDataModel { LanguageCode = "zh-CN", LanguageIcon = "ic_ch", LanguageName = "Chinese" });//
            Languages.Add(new LanguageDataModel { LanguageCode = "ar", LanguageIcon = "ic_ar", LanguageName = "Arabic" });
            await Task.Delay(10);

        }

        private async void ItemTappedAsync(object obj)
        {
            ItemTappedEventArgs args = (ItemTappedEventArgs)obj;
            LanguageDataModel selectedLanguage = (LanguageDataModel)args.Item;
            Preferences.Set("SelectedLanguage", selectedLanguage.LanguageCode);
            CultureInfo language = new CultureInfo(Preferences.Get("SelectedLanguage", "en"));
            Thread.CurrentThread.CurrentUICulture = language;
            AppResources.Culture = language;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (selectedLanguage.LanguageCode == "ar")
                {
                    App.Current.MainPage.FlowDirection = FlowDirection.RightToLeft;
                    DialogService.ShowToastMessage("أعد تشغيل التطبيق لتصبح التغييرات سارية المفعول.");
                }
                else
                {
                    App.Current.MainPage.FlowDirection = FlowDirection.LeftToRight;
                }
            });


            if (MainSender)
                await NavigationService.NavigateToAsync<MainViewModel>();
            else
                await NavigationService.NavigateToAsync<OnboardingViewModel>();
        }
        #endregion
    }

    public class LanguageDataModel
    {
        public string LanguageIcon { get; set; }
        public string LanguageName { get; set; }
        public string LanguageCode { get; set; }

    }
}
