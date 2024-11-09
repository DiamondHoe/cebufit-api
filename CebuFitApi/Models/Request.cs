using System.ComponentModel.DataAnnotations;
using CebuFitApi.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CebuFitApi.Models;
public class Request : BaseModel
{
    public Request(){}

    public RequestType Type { get; set; }
    public User Requester { get; set; }
    public Guid RequestedItemId { get; set; }
    public RequestStatus Status { get; set; }
    public User? Approver { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }
}

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable(nameof(Request));
    }
}

