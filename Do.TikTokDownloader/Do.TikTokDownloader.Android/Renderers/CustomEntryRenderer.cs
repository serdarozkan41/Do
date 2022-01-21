using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using Do.TikTokDownloader.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace Do.TikTokDownloader.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                this.SetForegroundGravity(Android.Views.GravityFlags.Center);

                if (e.NewElement.Keyboard == Keyboard.Numeric)
                {
                    this.Control.InputType = InputTypes.ClassNumber | InputTypes.NumberFlagSigned | InputTypes.NumberFlagDecimal;
                }
                if (e.NewElement.Keyboard == Keyboard.Plain)
                {
                    this.Control.InputType = Android.Text.InputTypes.TextFlagCapCharacters;
                }
            }
        }
    }
}