using Avalonia;
using Avalonia.Controls.Primitives;
using static System.Net.Mime.MediaTypeNames;
using Worxly.Components;
using System.Diagnostics;
using Avalonia.Controls;
using Material.Icons.Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace Worxly.Components;

public class SearchBox : TemplatedControl
{
    public static readonly StyledProperty<string> WatermarkProperty =
            AvaloniaProperty.Register<SearchBox, string>(nameof(Watermark));

    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<SearchBox, string>(nameof(Text), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    public event EventHandler<TextChangedEventArgs>? TextChanged;
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
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        TextBox searchTextBox = e.NameScope.Find<TextBox>("TextBoxSearchBox");
        searchTextBox.TextChanged += SearchTextBox_TextChanged;
        base.OnApplyTemplate(e);
    }

    private void SearchTextBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        TextChanged?.Invoke(this, e);
    }
}