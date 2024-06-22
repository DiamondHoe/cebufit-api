namespace CebuFitApi.DTOs
{
    public class RequestDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Guid Requester { get; set; }
        public Guid RequestedItemId { get; set; }
        public string Status { get; set; }
        public Guid? Approver { get; set; }
        public string? Description { get; set; }
    }
}

