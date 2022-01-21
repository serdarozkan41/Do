using Android.Content;
using Do.TikTokDownloader.Droid.Services;
using Do.TikTokDownloader.Helpers;
using Do.TikTokDownloader.Services.NativeService;
using Java.IO;
using System;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(NativeService))]
namespace Do.TikTokDownloader.Droid.Services
{
    public class NativeService : INativeService
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public bool OpenAnotherApp(string appId)
        {
            try
            {
                Intent i = Platform.CurrentActivity.PackageManager.GetLaunchIntentForPackage(appId);
                Platform.CurrentActivity.StartActivity(i);

                return true;
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
                var pathToNewFolder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
                File folder = new File(pathToNewFolder, "/DoTik");
                if (!folder.Exists())
                {
                    folder.Mkdir();
                }

                File file = new File(folder, "/" + fileName + ".mp4");

                FileOutputStream fos = new FileOutputStream(file);
                fos.Write(dataArr);
                fos.Close();
                return file.AbsolutePath;
            }
            catch
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));

                return null;
            }
        }
    }
}