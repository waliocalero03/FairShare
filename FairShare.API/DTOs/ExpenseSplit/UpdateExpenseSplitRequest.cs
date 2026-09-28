namespace FairShare.API.DTOs.ExpenseSplit
{
    public class UpdateExpenseSplitRequest
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ParticipantId { get; set; }
        public decimal OwedAmount { get; set; }
    }
}
