using Template10.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Petrolhead.Models;
using Petrolhead.Services.DataService;
using Petrolhead.Services.AuthenticationService;
using System.Diagnostics;
using Windows.UI.Popups;
using Template10.Common;
using System;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;  // offline sync
using Microsoft.WindowsAzure.MobileServices.Sync;         // offline sync
using System.Threading;

namespace Petrolhead.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        string _Value = string.Empty;
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        private IMobileServiceSyncTable<Vehicle> vehicles = App.MobileService.GetSyncTable<Vehicle>();

        private CancellationTokenSource cts = new CancellationTokenSource();

        private async Task InitLocalStoreAsync()
        {
            if (!App.MobileService.SyncContext.IsInitialized)
            {
                var store = new MobileServiceSQLiteStore("localstore.db");
                store.DefineTable<Vehicle>();
                await App.MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            }
            await SyncAsync(cts.Token);
        }
        private async Task SyncAsync(CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();
                await App.MobileService.SyncContext.PushAsync(ct);
                await vehicles.PullAsync("vehicles", vehicles.CreateQuery(), false, ct, new PullOptions());
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {

            }
        }
        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            await InitLocalStoreAsync();
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
            {
                state[nameof(Value)] = Value;
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

