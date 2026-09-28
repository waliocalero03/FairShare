namespace FairShare.API.DTOs.Expense
{
    public class UpdateExpenseRequest
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PayerId { get; set; }
        public required string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
