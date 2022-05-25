using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace MauiBoilerplate
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}