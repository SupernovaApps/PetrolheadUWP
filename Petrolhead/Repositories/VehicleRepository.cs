using Petrolhead.Services.DataService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead.Repositories
{
    public class VehicleRepository
    {
       public static Models.Vehicle Factory(string name, string id = null, string description = null, string manufacturer = null, string make = null, string model = null, ulong? purchaseYear = null, ulong? total = null, ulong? budgetMax = null, DateTimeOffset? nextWarrantDate = null, DateTimeOffset? nextRegoDate = null)
        {
            return new Models.Vehicle()
            {
                Name = name,
                Id = id ?? Guid.NewGuid().ToString(),
                Manufacturer = manufacturer ?? "",
                Make = make ?? "",
                Model = model ?? "",
                YearOfPurchase = purchaseYear ?? 2000,
                Description = description ?? "",
                TotalCost = total ?? 0,
                BudgetMaximum = budgetMax ?? 0,
                NextWarrantDate = nextWarrantDate ?? DateTime.Today,
                NextRegoDate = nextRegoDate ?? DateTime.Today,
            };
        }

        public static Models.Vehicle Clone(Models.Vehicle v)
        {
            return Factory(v.Name, Guid.NewGuid().ToString(), v.Description, v.Manufacturer, v.Make, v.Model, v.YearOfPurchase, v.TotalCost, v.BudgetMaximum, v.NextWarrantDate, v.NextRegoDate);
        }
    } 
}
