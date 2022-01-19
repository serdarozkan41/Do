using Do.TikTokDownloader.ViewModels.Base;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Do.TikTokDownloader.Services
{
    public interface INavigationService
    {
        ViewModelBase PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToBackAsync();
        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}