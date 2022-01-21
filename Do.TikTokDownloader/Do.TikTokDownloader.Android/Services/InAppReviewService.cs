using Do.TikTokDownloader.Droid.Services;
using Do.TikTokDownloader.Services.InAppReview;
using System;
using Xamarin.Essentials;
using Xamarin.Google.Android.Play.Core.Review;
using Xamarin.Google.Android.Play.Core.Review.Testing;
using Xamarin.Google.Android.Play.Core.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(InAppReviewService))]
namespace Do.TikTokDownloader.Droid.Services
{
    public class InAppReviewService: IInAppReviewService
    {
        public void LaunchReview()
        {
#if DEBUG
            var manager = new FakeReviewManager(Platform.CurrentActivity);
#else
            var manager = ReviewManagerFactory.Create(Platform.CurrentActivity);
#endif
            var request = manager.RequestReviewFlow();
            request.AddOnCompleteListener(new OnCompleteListener(manager));
        }

        public class OnCompleteListener : Java.Lang.Object, IOnCompleteListener
        {
            FakeReviewManager _fakeReviewManager;
            IReviewManager _reviewManager;
            bool _usesFakeManager;
            void IOnCompleteListener.OnComplete(Task p0)
            {
                if (!p0.IsSuccessful)
                    return;
                // Canceling the review raises an exception
                try
                {
                    LaunchReview(p0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            private void LaunchReview(Task p0)
            {
                var review = p0.GetResult(Java.Lang.Class.FromType(typeof(ReviewInfo)));
                if (_usesFakeManager)
                {
                    var x = _fakeReviewManager.LaunchReviewFlow(Platform.CurrentActivity, (ReviewInfo)review);
                    x.AddOnCompleteListener(new OnCompleteListener(_fakeReviewManager));
                }
                else
                {
                    var x = _reviewManager.LaunchReviewFlow(Platform.CurrentActivity, (ReviewInfo)review);
                    x.AddOnCompleteListener(new OnCompleteListener(_reviewManager));
                }
            }

            public OnCompleteListener(FakeReviewManager fakeReviewManager)
            {
                _fakeReviewManager = fakeReviewManager;
                _usesFakeManager = true;
            }
            public OnCompleteListener(IReviewManager reviewManager)
            {
                _reviewManager = reviewManager;
            }
        }
    }

    
}