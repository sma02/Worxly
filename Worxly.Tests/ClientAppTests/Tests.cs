using Avalonia.Headless;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Worxly.Tests;
using Worxly.Views;
using Worxly.Components;
using Worxly.ViewModels;
using Avalonia.ReactiveUI;
using Avalonia.Controls.Templates;
using System.Diagnostics;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]
public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions()).UseReactiveUI();

    [AvaloniaFact]
    public void LoginCheck()
    {
        // Setup controls:
        var loginView = new LoginView();
        //loginView.DataContext = new LoginViewModel();
        var window = new Window { Content = loginView };

        window.Show();

        checkTextField(window, loginView, "IdentifierField", "abc@def.com");
        checkTextField(window, loginView, "PasswordField", "pass123");
    }
    void checkTextField(Window window, UserControl userControl, string name, string value)
    {
        var field = userControl.FindControl<TextField>(name);
        field.TextBox.Focus();
        window.KeyTextInput(value);
        Assert.Equal(value, field.Text);
    }
}