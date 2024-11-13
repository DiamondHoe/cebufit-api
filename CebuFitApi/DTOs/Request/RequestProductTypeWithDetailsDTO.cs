namespace CebuFitApi.DTOs
{
    public class RequestProductTypeWithDetailsDto : RequestWithDetailsDto
    {
        // public Guid Id { get; set; }
        // public RequestType Type { get; set; }
        // public UserPublicDto Requester { get; set; }
        public ProductTypeDto RequestedProductType { get; set; }
        // public string Status { get; set; }
        // public UserPublicDto? Approver { get; set; }
        // public string? Description { get; set; }
    }
}

