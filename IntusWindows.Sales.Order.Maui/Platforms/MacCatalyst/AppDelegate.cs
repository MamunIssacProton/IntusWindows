using Foundation;
using UIKit;

namespace IntusWindows.Sales.Order.Maui;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        var services = new ServiceCollection();
        services.AddHttpClients();
        services.AddHttpProgressBarService();
        var serviceProvider = services.BuildServiceProvider();
        DependencyService.RegisterSingleton<IServiceProvider>(serviceProvider);

        return base.FinishedLaunching(application, launchOptions);
    }
}

