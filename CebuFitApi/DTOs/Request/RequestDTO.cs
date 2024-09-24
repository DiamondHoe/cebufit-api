using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs;

public class RequestDto
{
    public Guid Id { get; set; }
    public RequestType Type { get; set; }
    public Guid Requester { get; set; }
    public Guid RequestedItemId { get; set; }
    public RequestStatus Status { get; set; }
    public Guid? Approver { get; set; }
    public string? Description { get; set; }
}

