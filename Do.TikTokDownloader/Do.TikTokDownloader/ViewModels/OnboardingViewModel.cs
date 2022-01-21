using Do.TikTokDownloader.Resources;
using Do.TikTokDownloader.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Do.TikTokDownloader.ViewModels
{
    public class OnboardingViewModel : ViewModelBase
    {
        #region PROPS
        private ObservableCollection<OnboardingDataModel> items;
        private int position;
        private string nextButtonText;
        private string skipButtonText;

        public ObservableCollection<OnboardingDataModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }
        public string NextButtonText
        {
            get => nextButtonText;
            set
            {
                nextButtonText = value;
                OnPropertyChanged();
            }
        }
        public string SkipButtonText
        {
            get => skipButtonText;
            set
            {
                skipButtonText = value;
                OnPropertyChanged();
            }
        }
        public int Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged();
                
                    UpdateNextButtonText();
                
            }
        }
        public ICommand NextCommand { get; protected set; }
        public ICommand SkipCommand { get; protected set; }
        #endregion

        public OnboardingViewModel()
        {
            LaunchNextCommand();
            LaunchSkipCommand();
        }

        public async override Task InitializeAsync(object navigationData)
        {
            SetNextButtonText(AppResources.OBV_Next);
            SetSkipButtonText(AppResources.OBV_Skip);
            Items = new ObservableCollection<OnboardingDataModel>();
            Items.Add(new OnboardingDataModel
            {
                Title = AppResources.OBV_Title_1,
                Content = AppResources.OBV_Desc_1,
                ImageUrl = "t_1.png"
            });
            Items.Add(new OnboardingDataModel
            {
                Title = AppResources.OBV_Title_2,
                Content = AppResources.OBV_Desc_2,
                ImageUrl = "t_2.png"
            });
            Items.Add(new OnboardingDataModel
            {
                Title = AppResources.OBV_Title_3,
                Content = AppResources.OBV_Desc_3,
                ImageUrl = "t_3.png"
            });
           
            await Task.Delay(10);
        }

        private void SetNextButtonText(string nextButtonText) => NextButtonText = nextButtonText;
        private void SetSkipButtonText(string skipButtonText) => SkipButtonText = skipButtonText;
        private void LaunchNextCommand()
        {

            NextCommand = new Command(() =>
            {
                if (LastPositionReached())
                {
                    ExitOnBoarding();
                }
                else
                {
                    MoveToNextPosition();
                }
            });
        }
        private void LaunchSkipCommand()
        {
            SkipCommand = new Command(() =>
            {
                ExitOnBoarding();

            });
        }

        private async void ExitOnBoarding()
        {
            var statusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (statusWrite != PermissionStatus.Granted)
            {
                statusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (statusWrite != PermissionStatus.Granted)
                {
                    DialogService.ShowToastError(AppResources.AccessDesc);
                }
            }

            await NavigationService.NavigateToAsync<MainViewModel>();
        }

        private void MoveToNextPosition()
        {
            var nextPosition = ++Position;
            Position = nextPosition;
        }

        private bool LastPositionReached()
            => Position == Items.Count - 1;

        private void UpdateNextButtonText()
        {
            if (LastPositionReached())
            {
                SetNextButtonText(AppResources.OBV_GotIt);
            }
            else
            {
                SetNextButtonText(AppResources.OBV_Next);
            }
        }
    }

    public class OnboardingDataModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }
}
