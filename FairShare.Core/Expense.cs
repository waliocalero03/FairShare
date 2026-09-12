using System;
using System.Collections.Generic;
using System.Text;

namespace FairShare.Core
{
    public class Expense
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PayerId { get; set; }
        public required string Description { get; set; }    
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public List<ExpenseSplit> Splits { get; set; } = new List<ExpenseSplit>();
    }
}
