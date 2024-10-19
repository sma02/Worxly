using Avalonia;
using Avalonia.Controls.Primitives;

namespace Worxly.Components
{
    public partial class TextField : TemplatedControl
    {
        public static readonly StyledProperty<string> LabelTextProperty =
            AvaloniaProperty.Register<TextField, string>(nameof(LabelText));

        public static readonly StyledProperty<string> WatermarkProperty =
            AvaloniaProperty.Register<TextField, string>(nameof(Watermark));

        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<TextField, string>(nameof(Text), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

        public string LabelText
        {
            get => GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public string Watermark
        {
            get => GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        public string Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}