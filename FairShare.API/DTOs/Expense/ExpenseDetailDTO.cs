using FairShare.API.DTOs.ExpenseSplit;

namespace FairShare.API.DTOs.Expense
{
    public class ExpenseDetailDTO
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PayerId { get; set; }
        public required string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public List<ExpenseSplitDTO> Splits { get; set; } = new();
    }
}
