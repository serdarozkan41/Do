using Do.TikTokDownloader.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Do.TikTokDownloader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguageView : ContentPage
    {
        public LanguageView()
        {
            InitializeComponent();
            TheTheme.SetTheme();
        }
    }
}