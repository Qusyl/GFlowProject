using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GFlowApp.Services;
using GFlowApp.Services.Window;
using GFlowApp.ViewModels;
using GFlowApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace GFlowApp;

public partial class App : Avalonia.Application
{
    public static IServiceProvider? Services { get; private set; }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        services.AddHttpClient<IClientService, GFlowHttpClient>(client =>
        {
            client.BaseAddress = new System.Uri("https://localhost:7167");
        });

        services.AddSingleton<MainViewModel>();

        services.AddSingleton<MainWindow>();

        services.AddSingleton<IWindowService, WindowService>();

        Services = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainViewModel = Services.GetRequiredService<MainViewModel>();
            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
            var wService = Services.GetRequiredService<IWindowService>() as WindowService;

            wService?.SetMainWindow(mainWindow);
            
             desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}