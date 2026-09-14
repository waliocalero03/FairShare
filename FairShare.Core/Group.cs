namespace FairShare.Core
{
    public class Group
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Participant>? Participants { get; set; }
        public List<Expense>? Expenses { get; set; }
    }
}
