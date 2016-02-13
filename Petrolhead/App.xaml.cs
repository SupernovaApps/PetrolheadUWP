using System;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Petrolhead.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Utils;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using Microsoft.WindowsAzure.MobileServices;

namespace Petrolhead
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {
        
        public App()
        {

            TelemetryConfiguration.Active.InstrumentationKey = "5afcb70e-e5b7-41c5-9e57-aa6fb9f08c2a";
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session |
                Microsoft.ApplicationInsights.WindowsCollectors.PageView |
                Microsoft.ApplicationInsights.WindowsCollectors.UnhandledException
        );
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);
            #region TelemetryClient Init
            Telemetry = new TelemetryClient();
            #endregion
           MobileService =
new MobileServiceClient(
    "https://petrolheadappuwp.azurewebsites.net"
);
            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion
        }

        public static MobileServiceClient MobileService
        {
            get; set;
        }

       

        // runs even if restored from state
        public override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // content may already be shell when resuming
            if ((Window.Current.Content as Views.Shell) == null)
            {
                // setup hamburger shell
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                Window.Current.Content = new Views.Shell(nav);
            }
            return Task.CompletedTask;
        }

        private static TelemetryClient _Telemetry;
        public static TelemetryClient Telemetry { get { return _Telemetry; } set { _Telemetry = value; } }
        // runs only when not restored from state
        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {

            // set hamburger menu to full screen mode on Mobile devices.
            if (DeviceUtils.Current().IsPhone())
                Views.Shell.HamburgerMenu.IsFullScreen = true;

            NavigationService.Navigate(typeof(Views.MainPage));
            return Task.CompletedTask;
        }
    }
}

