namespace FairShare.API.DTOs.ExpenseSplit
{
    public class CreateExpenseSplitRequest
    {
        public int ExpenseId { get; set; }
        public int ParticipantId { get; set; }
        public decimal OwedAmount { get; set; }
    }
}
