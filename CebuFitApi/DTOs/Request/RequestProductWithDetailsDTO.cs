namespace CebuFitApi.DTOs
{
    public class RequestProductWithDetailsDto: RequestWithDetailsDto
    {
        public ProductWithDetailsDTO RequestedProduct { get; set; }
    }
}

