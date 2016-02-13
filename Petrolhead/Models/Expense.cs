using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead.Models
{
    public class Expense : ModelBase
    {
        private DateTimeOffset _TransactionDate = default(DateTimeOffset);
        public DateTimeOffset TransactionDate { get { return _TransactionDate; } set { Set(ref _TransactionDate, value); } }

        private ulong _TotalCost = default(ulong);
        public ulong TotalCost { get { return _TotalCost; } set { Set(ref _TotalCost, value); } }

        private ulong _Mileage = default(ulong);
        public ulong Mileage { get { return _Mileage; } set { Set(ref _Mileage, value); } }

    }
}
