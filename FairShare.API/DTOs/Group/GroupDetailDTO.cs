using FairShare.API.DTOs.Expense;
using FairShare.API.DTOs.Participant;

namespace FairShare.API.DTOs.Group
{
    public class GroupDetailDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public List<ParticipantResponse>? Participants { get; set; }
        public List<ExpenseDetailDTO>? Expenses { get; set; }
    }
}
