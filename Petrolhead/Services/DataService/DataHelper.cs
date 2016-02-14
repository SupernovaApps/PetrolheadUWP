using Petrolhead.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Threading;

namespace Petrolhead.Services.DataService
{
    public class DataHelper
    {
        public static async Task AddVehicleAsync(Vehicle v)
        {
          
            await App.MobileService.GetTable<Vehicle>().InsertAsync(v);
        }

        public static async Task RemoveVehicleAsync(Vehicle v)
        {
            await App.MobileService.GetTable<Vehicle>().DeleteAsync(v);
        }

        public static async Task UpdateVehicleAsync(Vehicle v)
        {
            await App.MobileService.GetTable<Vehicle>().UpdateAsync(v);
        }

       public static async Task BatchAddAsync(IEnumerable<Vehicle> list)
        {
            foreach (var item in list)
            {
                await AddVehicleAsync(item);
            }
        }

        public static async Task<ObservableCollection<Vehicle>> GetObservableCollectionAsync()
        {
            var enumerable = await App.MobileService.GetTable<Vehicle>().ToListAsync();
            var collection = new ObservableCollection<Vehicle>();

            if (enumerable.Count < 1)
                return new ObservableCollection<Vehicle>();

            foreach (var item in enumerable)
            {
                collection.Add(item);
            }
            return collection;
        }

    }
}
