using FairShare.API.DTOs.ExpenseSplit;

namespace FairShare.API.DTOs.Expense
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PayerName { get; set; }
        public DateTime Date { get; set; }
        public List<ExpenseSplitDTO> Splits { get; set; } = new();
    }
}
