using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using DialogHostAvalonia;
using System.Threading.Tasks;

namespace Worxly;

public partial class ConfirmationDialog : UserControl
{
    public static readonly StyledProperty<string> MessageProperty =
    AvaloniaProperty.Register<ConfirmationDialog, string>(nameof(Message));

    public string Message
    {
        get { return GetValue(MessageProperty); }
        set { SetValue(MessageProperty, value); }
    }

    public static readonly StyledProperty<string> PositiveTextProperty =
    AvaloniaProperty.Register<ConfirmationDialog, string>(nameof(PositiveText));

    public string PositiveText
    {
        get { return GetValue(PositiveTextProperty); }
        set { SetValue(PositiveTextProperty, value); }
    }

    public static readonly StyledProperty<string> NegativeTextProperty =
    AvaloniaProperty.Register<ConfirmationDialog, string>(nameof(NegativeText));

    public string NegativeText
    {
        get { return GetValue(NegativeTextProperty); }
        set { SetValue(NegativeTextProperty, value); }
    }

    public static readonly StyledProperty<HorizontalAlignment> HorizontalButtonAlignmentProperty =
    AvaloniaProperty.Register<ConfirmationDialog, HorizontalAlignment>(nameof(HorizontalButtonAlignment), HorizontalAlignment.Right);

    public HorizontalAlignment HorizontalButtonAlignment
    {
        get { return GetValue(HorizontalButtonAlignmentProperty); }
        set { SetValue(HorizontalButtonAlignmentProperty, value); }
    }

    public static readonly StyledProperty<Thickness> ButtonMarginProperty =
    AvaloniaProperty.Register<ConfirmationDialog, Thickness>(nameof(ButtonMargin), new Thickness(8));


    public static readonly StyledProperty<bool> NegativeButtonVisibilityProperty =
    AvaloniaProperty.Register<ConfirmationDialog, bool>(nameof(NegativeButtonVisibility));

    public bool NegativeButtonVisibility
    {
        get { return GetValue(NegativeButtonVisibilityProperty); }
        set { SetValue(NegativeButtonVisibilityProperty, value); }

    }

    public Thickness ButtonMargin
    {
        get { return GetValue(ButtonMarginProperty); }
        set { SetValue(ButtonMarginProperty, value); }
    }
    public async Task<object?> Show()
    {
        return await DialogHost.Show(this);
    }

    public ConfirmationDialog()
    {
        InitializeComponent();
        DataContext = this;
    }

    public void PositiveButtonClicked(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }
    public void Close(bool value)
    {
        DialogHost.Close(null, value);
    }
    public void NegativeButtonClicked(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }




}