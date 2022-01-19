using Do.TikTokDownloader.Services;
using Do.TikTokDownloader.Services.Settings;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Do.TikTokDownloader.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        protected readonly ISettingsService SettingsService;

        public ICommand BackCommand { get; protected set; }

        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase()
        {
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            SettingsService = ViewModelLocator.Resolve<ISettingsService>();
            BackCommand = new Command(BackAsync);
        }

        private async void BackAsync(object args)
        {
            await NavigationService.NavigateToBackAsync();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}