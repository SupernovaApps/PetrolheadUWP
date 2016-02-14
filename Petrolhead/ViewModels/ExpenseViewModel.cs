using Petrolhead.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace Petrolhead.ViewModels
{
    public class ExpenseViewModel : ViewModelBase
    {
        public ExpenseViewModel(Expense e)
        {
            Expense = e;
        }

        private Expense _Expense = default(Expense);
        public Expense Expense { get { return _Expense; } set { Set(ref _Expense, value); } }
    }
}
