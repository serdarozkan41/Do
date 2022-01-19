
using Android.Content;
using Android.Views;
using Do.TikTokDownloader.Droid.Renderers;
using Google.Android.Material.BottomNavigation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace Do.TikTokDownloader.Droid.Renderers
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        public CustomTabbedPageRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            //if (e.OldElement == null && e.NewElement != null)
            //{
            //    for (int i = 0; i <= this.ViewGroup.ChildCount - 1; i++)
            //    {
            //        var childView = this.ViewGroup.GetChildAt(i);
            //        if (childView is ViewGroup viewGroup)
            //        {
            //            for (int j = 0; j <= viewGroup.ChildCount - 1; j++)
            //            {
            //                var childRelativeLayoutView = viewGroup.GetChildAt(j);
            //                if (childRelativeLayoutView is BottomNavigationView)
            //                {
            //                    ((BottomNavigationView)childRelativeLayoutView).ItemTextAppearanceActive = Resource.Style.MyBottomNavigationView;
            //                    ((BottomNavigationView)childRelativeLayoutView).ItemTextAppearanceInactive = Resource.Style.MyBottomNavigationView;
            //                }
            //            }
            //        }
            //    }
            //}

        }
    }
}