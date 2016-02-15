using Petrolhead.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace Petrolhead.ViewModels
{
    public class ReminderViewModel : ViewModelBase
    {
        public ReminderViewModel(Reminder r)
        {
            Reminder = r;
        }

        private Reminder _Reminder = default(Reminder);
        public Reminder Reminder { get { return _Reminder; } set { Set(ref _Reminder, value); } }
    }
}
