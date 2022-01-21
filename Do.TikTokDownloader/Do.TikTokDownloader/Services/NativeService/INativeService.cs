using Do.TikTokDownloader.Helpers;
using System;

namespace Do.TikTokDownloader.Services.NativeService
{
    public interface INativeService
    {
        bool OpenAnotherApp(string appId);
        
        string WriteFile(byte[] dataArr, string fileName);

        event EventHandler<DownloadEventArgs> OnFileDownloaded;
    }
}
