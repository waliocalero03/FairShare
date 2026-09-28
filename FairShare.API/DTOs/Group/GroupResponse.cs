namespace FairShare.API.DTOs.Group
{
    public class GroupResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
