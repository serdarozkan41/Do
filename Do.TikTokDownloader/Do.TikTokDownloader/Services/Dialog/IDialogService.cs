using System.Threading.Tasks;

namespace Do.TikTokDownloader.Services
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        void ShowToastSuccess(string message);
        void ShowToastMessage(string message);
        void ShowToastError(string message);
        void ShowToastWarning(string message);
    }
}
