namespace FairShare.API.DTOs.ExpenseSplit
{
    public class ExpenseSplitDTO
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ParticipantId { get; set; }
        public string? ParticipantName { get; set; }
        public decimal OwedAmount { get; set; }
    }
}
