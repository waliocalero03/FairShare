using FairShare.API.DTOs.Participant;

namespace FairShare.API.DTOs.Group
{
    public class GroupDetailDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public List<ParticipantDetailDTO>? Participants { get; set; }
        public List<Expense>? Expenses { get; set; }
    }
}
