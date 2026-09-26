namespace FairShare.API.DTOs.Participant
{
    public class CreateParticipantRequest
    {
        public int GroupId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
