using Do.TikTokDownloader.iOS.Services;
using Do.TikTokDownloader.Services.InAppReview;

[assembly: Xamarin.Forms.Dependency(typeof(InAppReviewService))]
namespace Do.TikTokDownloader.iOS.Services
{
    public class InAppReviewService : IInAppReviewService
    {
        public void LaunchReview()
        {
            
        }
    }


}