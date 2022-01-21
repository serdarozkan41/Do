using Do.TikTokDownloader.Services;
using Do.TikTokDownloader.Services.Settings;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Do.TikTokDownloader.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        protected readonly ISettingsService SettingsService;
        private LayoutState mainState;

        public LayoutState MainState
        {
            get { return mainState; }
            set
            {
                mainState = value;
                OnPropertyChanged();
            }
        }
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
                if (_isBusy)
                {
                    MainState = LayoutState.Loading;
                }
                else
                {
                    MainState = LayoutState.None;
                }
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