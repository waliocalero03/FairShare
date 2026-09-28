namespace FairShare.API.DTOs.Participant
{
    public class ParticipantResponse
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public required string Name { get; set; }
    }
}
