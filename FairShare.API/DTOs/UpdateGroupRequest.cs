namespace FairShare.API.DTOs
{
    public class UpdateGroupRequest
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
    }
}
