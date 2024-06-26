using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class RequestProductWithDetailsDto
    {
        public Guid Id { get; set; }
        public RequestType Type { get; set; }
        public UserPublicDto Requester { get; set; }
        public ProductWithDetailsDTO RequestedProduct { get; set; }
        public string Status { get; set; }
        public UserPublicDto? Approver { get; set; }
        public string? Description { get; set; }
    }
}

