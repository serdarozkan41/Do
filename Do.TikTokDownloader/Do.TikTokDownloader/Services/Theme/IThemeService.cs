using System.Drawing;

namespace Do.TikTokDownloader.Services.Enviroment
{
    public interface IThemeService
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}
