using Acr.UserDialogs;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Do.TikTokDownloader.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public void ShowToastSuccess(string message)
        {
            var tc = new ToastConfig(message)
            {
                BackgroundColor = Color.FromHex("#28a745"),
                MessageTextColor = Color.White,
                Position = ToastPosition.Top,
                Message = message
            };
            UserDialogs.Instance.Toast(tc);
        }

        public void ShowToastMessage(string message)
        {
            var tc = new ToastConfig(message)
            {
                BackgroundColor = Color.FromHex("#585858"),
                MessageTextColor = Color.White,
                Position = ToastPosition.Top,
                Message = message
            };
            UserDialogs.Instance.Toast(tc);
        }

        public void ShowToastWarning(string message)
        {
            var tc = new ToastConfig(message)
            {
                BackgroundColor = Color.FromHex("#ffc107"),
                MessageTextColor = Color.White,
                Position = ToastPosition.Top,
                Message = message
            };
            UserDialogs.Instance.Toast(tc);
        }

        public void ShowToastError(string message)
        {
            var tc = new ToastConfig(message)
            {
                BackgroundColor = Color.FromHex("#dc3545"),
                MessageTextColor = Color.White,
                Position = ToastPosition.Top,
                Message = message
            };
        }
    }
}
