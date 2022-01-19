using System.Threading.Tasks;

namespace Do.TikTokDownloader.Services
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
    }
}
