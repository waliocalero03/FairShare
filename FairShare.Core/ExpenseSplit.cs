using System;
using System.Collections.Generic;
using System.Text;

namespace FairShare.Core
{
    public class ExpenseSplit
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ParticipantId { get; set; }
        public decimal OwedAmount { get; set; }
    }
}
