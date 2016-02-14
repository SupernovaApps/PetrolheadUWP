using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Petrolhead.Models
{
    public enum ReminderState { Disabled, Active, Due, Overdue, Completed }
    public class Reminder : ModelBase
    {
        private string _id = default(string);
        public string Id { get { return _id; } set { Set(ref _id, value); } }
        private ulong _MileageTrigger = default(ulong);
        public ulong DueMileage
        {
            get
            {
                return _MileageTrigger;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MileageTrigger", value, "Mileage for reminder trigger must be equal to or above zero!");

                Set(ref _MileageTrigger, value);
            }
        }

        private ReminderState _State = default(ReminderState);
        public ReminderState State { get { return _State; } set { Set(ref _State, value); } }

        private TimeSpan _TimeBasedTrigger = default(TimeSpan);
        public TimeSpan DueDate
        {
            get
            {
                return _TimeBasedTrigger;
            }
            set
            {
                
                Set(ref _TimeBasedTrigger, value);
            }
        }



      
    }
}
