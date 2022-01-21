using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Do.TikTokDownloader.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandartEntry : StackLayout
    {
        public StandartEntry()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(StandartEntry), string.Empty, propertyChanged: OnTextChanged);

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (StandartEntry)bindable;
            if ((string)newValue != null)
            {
                button.TbUrl.Text = newValue.ToString();
            }
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(StandartEntry), string.Empty, propertyChanged: OnLabelTextChanged);

        private static void OnLabelTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (StandartEntry)bindable;

            button.LbText.Text = (string)newValue;
        }

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public static readonly BindableProperty PlaceHolderTextProperty = BindableProperty.Create(nameof(PlaceHolderText), typeof(string), typeof(StandartEntry), string.Empty, propertyChanged: OnPlaceHolderTextChanged);

        private static void OnPlaceHolderTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (StandartEntry)bindable;

            button.TbUrl.Placeholder = (string)newValue;
        }

        public string PlaceHolderText
        {
            get => (string)GetValue(PlaceHolderTextProperty);
            set => SetValue(PlaceHolderTextProperty, value);
        }

       
    }
}