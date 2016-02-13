using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead.Models
{
    public class Vehicle : ModelBase
    {
        private string _id = Guid.NewGuid().ToString();
        public string Id
        {
            get { return _id; }
            set
            {
                Set(ref _id, value);
            }
        }
        private string _Manufacturer = default(string);
        public string Manufacturer
        {
            get { return _Manufacturer; }
            set { Set(ref _Manufacturer, value); }
        }
        private string _Make = default(string);
        public string Make
        {
            get { return _Make; }
            set { Set(ref _Make, value); }
        }

        private string _Model = default(string);
        public string Model
        {
            get
            {
                return _Model;
            }
            set
            {
                Set(ref _Model, value);
            }
        }

        private ulong _YearOfPurchase = default(ulong);
        public ulong YearOfPurchase
        {
            get
            {
                return _YearOfPurchase;
            }
            set
            {
                Set(ref _YearOfPurchase, value);
            }
        }
        private ulong _Total = default(ulong);
        public ulong TotalCost
        {
            get
            {
                return _Total;
            }
            set
            {
                Set(ref _Total, value);
            }
        }
        private ulong _BudgetMax = default(ulong);
        public ulong BudgetMaximum
        {
            get
            {
                return _BudgetMax;
            }
            set
            {
                Set(ref _BudgetMax, value);
            }
        }

        private DateTimeOffset _LastWarrantDate = default(DateTimeOffset);
        public DateTimeOffset LastWarrantDate
        {
            get
            {
                return _LastWarrantDate;
            }
            set
            {
                Set(ref _LastWarrantDate, value);
            }
        }

        private DateTimeOffset _NextWarrantDate = default(DateTimeOffset);
        public DateTimeOffset NextWarrantDate
        {
            get
            {
                return _NextWarrantDate;
            }
            set
            {
                Set(ref _NextWarrantDate, value);
            }
        }
        private DateTimeOffset _LastRegoDate = default(DateTimeOffset);
        public DateTimeOffset LastRegoDate
        {
            get
            {
                return _LastRegoDate;
            }
            set
            {
                Set(ref _LastRegoDate, value);
            }
        }
        private DateTimeOffset _NextRegoDate = default(DateTimeOffset);
        public DateTimeOffset NextRegoDate
        {
            get { return _NextWarrantDate; }
            set { Set(ref _NextWarrantDate, value); }
        }
        private List<Expense> _Expenses = default(List<Expense>);
        public List<Expense> Expenses
        {
            get
            {
                return _Expenses;
            }
            set
            {
                Set(ref _Expenses, value);
            }
        }
        private List<Reminder> _Reminders = default(List<Reminder>);
        public List<Reminder> Reminders
        {
            get
            {
                return _Reminders;

            }
            set
            {
                Set(ref _Reminders, value);
            }
        }
    }
}
