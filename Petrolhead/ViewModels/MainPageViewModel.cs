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
using Microsoft.WindowsAzure.MobileServices;
using Petrolhead.Repositories;

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

        private MobileServiceCollection<Vehicle, Vehicle> _items;
        public MobileServiceCollection<Vehicle, Vehicle> Items { get { return _items; } set { Set(ref _items, value); } }
        private IMobileServiceSyncTable<Vehicle> vehicles = App.MobileService.GetSyncTable<Vehicle>();

        private CancellationTokenSource cts = new CancellationTokenSource();


       
        private async Task InitLocalStoreAsync()
        {
            if (!App.MobileService.SyncContext.IsInitialized)
            {
                var store = new MobileServiceSQLiteStore("localstore.db");
                store.DefineTable<Vehicle>();
                await App.MobileService.SyncContext.InitializeAsync(store);
            }
            await SyncAsync();
        }
        private async Task SyncAsync()
        {
            try
            {
                var ct = cts.Token;
                ct.ThrowIfCancellationRequested();
                await App.MobileService.SyncContext.PushAsync();
                await vehicles.PullAsync("vehicles", vehicles.CreateQuery());
            }
            catch (MobileServicePushFailedException)
            {

            }
            finally
            {
                foreach (var vehicle in await vehicles.ToEnumerableAsync())
                {
                    Debug.WriteLine("Vehicle: " + vehicle.Name);
                }
            }
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            try
            {
                Debug.WriteLine("Inserting vehicle...");
                await vehicles.InsertAsync(vehicle);
                Debug.WriteLine("Adding vehicle to offline database...");
                Items.Add(vehicle);
                Debug.WriteLine("Syncing offline database...");
                await SyncAsync();
            }
            catch
            {

            }
        }
        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            try
            {
                await InitLocalStoreAsync();
                await RefreshVehiclesAsync();

            }
            catch (MobileServicePushFailedException)
            {

            }
            finally
            {
                
                
            }
          
        }

        public async Task InsertVehicleAsync(Vehicle v)
        {
            Debug.WriteLine("Inserting vehicle " + v.Name);
            await vehicles.InsertAsync(v);
            Items.Add(v);
            await SyncAsync();
        }

        public async Task RefreshVehiclesAsync()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems
                Items = await vehicles
                    .OrderBy(x => x.Name)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                
            }
        }

        private bool _IsLoaded = default(bool);
        public bool IsLoaded { get { return _IsLoaded; } set { Set(ref _IsLoaded, value); } }

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

