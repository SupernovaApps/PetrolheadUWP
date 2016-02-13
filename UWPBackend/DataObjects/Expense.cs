using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;

namespace UWPBackend.DataObjects
{
    public class Expense : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ulong TotalCost { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public ulong Mileage { get; set; }
        

    }

 
    public class Reminder : EntityData
    {
        public string Name { get; set; }
       public DateTimeOffset DueDate { get; set; }
       public ulong DueMileage { get; set; }
        public string Description { get; set; }
        public ulong Importance { get; set; }

    }

    public class Vehicle : EntityData
    {
        
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public ulong YearOfPurchase { get; set; }
        public ulong TotalCost { get; set; }
        public ulong BudgetMaximum { get; set; }
        public DateTimeOffset LastWarrantDate { get; set; }
        public DateTimeOffset NextWarrantDate { get; set; }
        public DateTimeOffset LastRegoDate { get; set; }
        public DateTimeOffset NextRegoDate { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Reminder> Reminders { get; set; }
    }
}