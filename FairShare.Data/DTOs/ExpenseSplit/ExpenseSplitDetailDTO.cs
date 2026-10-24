using System;
using System.Collections.Generic;
using System.Text;

namespace FairShare.Data.DTOs.ExpenseSplit
{
    public class ExpenseSplitDetailDTO
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ParticipantId { get; set; }
        public string? ParticipantName { get; set; }
        public decimal OwedAmount { get; set; }
    }
}
