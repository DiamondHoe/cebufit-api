using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class RequestCreateDto
    {
        public RequestType Type { get; set; }
        public Guid RequestedItemId { get; set; }
    }
}

