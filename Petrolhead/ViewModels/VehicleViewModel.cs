using Petrolhead.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace Petrolhead.ViewModels
{
    public class VehicleViewModel : ViewModelBase
    {
        public VehicleViewModel(Vehicle v)
        {

        }

        private Vehicle _Vehicle = default(Vehicle);
        public Vehicle Vehicle { get { return _Vehicle; } set { Set(ref _Vehicle, value); } }

        private ObservableCollection<ReminderViewModel> _Reminders = new ObservableCollection<ReminderViewModel>();
        public ObservableCollection<ReminderViewModel> Reminders { get { return _Reminders; } set { Set(ref _Reminders, value); } }
    }
}
