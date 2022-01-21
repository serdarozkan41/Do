using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Do.TikTokDownloader.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandartButton : Frame
    {
        public StandartButton()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(StandartButton), string.Empty, propertyChanged: OnTextChanged);

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (StandartButton)bindable;

            button.LbText.Text = (string)newValue;
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(StandartButton), default, propertyChanged: OnCommandChanged);

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (StandartButton)bindable;
            button.GestureRecognizers.Clear();
            button.GestureRecognizers.Add(new TapGestureRecognizer { NumberOfTapsRequired = 1, Command = (Command)newValue });
        }

        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}