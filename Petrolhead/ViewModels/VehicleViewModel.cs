using Petrolhead.Models;
using Petrolhead.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.System.Threading;

namespace Petrolhead.ViewModels
{
    public class VehicleViewModel : ViewModelBase
    {
        private ExpenseRepository _expenseRepository = new ExpenseRepository();
        private ReminderRepository _reminderRepository = new ReminderRepository();
        public VehicleViewModel(Vehicle v)
        {

        }

        private Vehicle _Vehicle = default(Vehicle);
        public Vehicle Vehicle { get { return _Vehicle; } set { Set(ref _Vehicle, value); } }

        private ObservableCollection<ReminderViewModel> _Reminders = new ObservableCollection<ReminderViewModel>();
        public ObservableCollection<ReminderViewModel> Reminders { get { return _Reminders; } set { Set(ref _Reminders, value); } }

        private ReminderViewModel _SelectedReminder = default(ReminderViewModel);
        public ReminderViewModel SelectedReminder { get { return _SelectedReminder; } set { Set(ref _SelectedReminder, value); } }

        private ObservableCollection<ExpenseViewModel> _Expenses = new ObservableCollection<ExpenseViewModel>();
        public ObservableCollection<ExpenseViewModel> Expenses { get { return _Expenses; } set { Set(ref _Expenses, value); } }

        private ExpenseViewModel _SelectedExpense = default(ExpenseViewModel);
        public ExpenseViewModel SelectedExpense { get { return _SelectedExpense; } set { Set(ref _SelectedExpense, value); } }

        public async Task AddExpense(string name)
        {
            try
            {
                await ThreadPool.RunAsync((workItem) =>
                {
                    try
                    {
                        var expense = ExpenseRepository.Factory(name);
                        var vm = new ExpenseViewModel(expense);
                        Vehicle.Expenses.Add(expense);
                        Expenses.Add(vm);
                        SelectedExpense = vm;
                    }
                    catch (Exception ex)
                    {
                        App.Telemetry.TrackException(ex);
                        SelectedExpense = null;
                    }
                    finally
                    {
                        Vehicle.Expenses.OrderBy(x => x.Name);
                        Expenses.OrderBy(x => x.Expense.Name);
                    }
                });
            }
            catch (Exception ex)
            {
                App.Telemetry.TrackException(ex);
            }
        }

        public async Task RemoveExpense(ExpenseViewModel e)
        {
            try
            {
                await ThreadPool.RunAsync((workItem) =>
                {
                    try
                    {
                        Vehicle.Expenses.Remove(e.Expense);
                        Expenses.Remove(e);
                    }
                    catch
                    {

                    }
                    finally
                    {
                        Vehicle.Expenses.OrderBy(x => x.Name);
                        Expenses.OrderBy(x => x.Expense.Name);
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
