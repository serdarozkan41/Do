using System;

namespace Do.TikTokDownloader.Helpers
{
    public class DownloadEventArgs:EventArgs
    {
        public bool FileSaved = false;
        public DownloadEventArgs(bool fileSaved)
        {
            FileSaved = fileSaved;
        }
    }
}
