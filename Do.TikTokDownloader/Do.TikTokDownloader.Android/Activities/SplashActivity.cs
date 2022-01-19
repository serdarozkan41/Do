using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace Do.TikTokDownloader.Droid.Activities
{
    [Activity(
          Label = "DoTikTok Downloader",
          Icon = "@mipmap/ic_launcher",
          Theme = "@style/Theme.Splash",
          NoHistory = true,
          MainLauncher = true,
          ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            InvokeMainActivity();
        }

        private void InvokeMainActivity()
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}