using Do.TikTokDownloader.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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
            SetNextButtonText("NEXT");
            SetSkipButtonText("SKIP");
            Items = new ObservableCollection<OnboardingDataModel>();
            Items.Add(new OnboardingDataModel
            {
                Title = "Copy it if you want",
                Content = "Open TikTok and copy and paste the link to DoTik using the share button in the right corner.",
                ImageUrl = "t_1.png"
            });
            Items.Add(new OnboardingDataModel
            {
                Title = "Use the share button if you want",
                Content = "You can start an automatic download by opening TikTok and selecting DoTik on the share button in the right corner.",
                ImageUrl = "t_2.png"
            });
            Items.Add(new OnboardingDataModel
            {
                Title = "Easily share",
                Content = "From the Downloads tab, you can watch or share their videos.",
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
                SetNextButtonText("GOT IT");
            }
            else
            {
                SetNextButtonText("NEXT");
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
