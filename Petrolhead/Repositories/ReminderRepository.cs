using Petrolhead.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead.Repositories
{
    public class ReminderRepository
    {
        public static Models.Reminder Factory(string name, string description = null, string id = null, ReminderState? state = null, TimeSpan? dueDate = null, ulong? dueMileage = null)
        {
            return new Reminder()
            {
                Name = name,
                Id = id ?? Guid.NewGuid().ToString(),
                State = state ?? ReminderState.Active,
                DueDate = dueDate ?? TimeSpan.FromDays(365),
                DueMileage = dueMileage ?? 0,
                Description = description ?? "",
                


            };
        }
         
        public static Models.Reminder Clone(Reminder r)
        {
            return Factory(r.Name, r.Description, Guid.NewGuid().ToString(), r.State, r.DueDate, r.DueMileage);
        }
    }
}
