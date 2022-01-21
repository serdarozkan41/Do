using Do.TikTokDownloader.Helpers;
using Do.TikTokDownloader.iOS.Services;
using Do.TikTokDownloader.Services.NativeService;
using Foundation;
using Photos;
using System;
using System.IO;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(NativeService))]
namespace Do.TikTokDownloader.iOS.Services
{
    public class NativeService : INativeService
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public bool OpenAnotherApp(string appId)
        {
            try
            {
                var profile = "tiktok://";
                var instagramUrl = new NSUrl(profile);


                if (UIApplication.SharedApplication.CanOpenUrl(instagramUrl))
                {
                    UIApplication.SharedApplication.OpenUrl(instagramUrl);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string WriteFile(byte[] dataArr, string fileName)
        {
            try
            {

                //NSData nsdata = NSData.FromArray(dataArr);
                string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                Guid id = Guid.NewGuid();
                string downloadFilePath = Path.Combine(downloadPath, fileName + ".mp4");
              
                File.WriteAllBytes(downloadFilePath, dataArr);

                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true));

                return downloadFilePath;
            }
            catch (Exception ex)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));

                return null;
            }
        }
    }
}