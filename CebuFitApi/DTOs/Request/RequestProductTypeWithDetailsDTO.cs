namespace CebuFitApi.DTOs
{
    public class RequestProductTypeWithDetailsDto : RequestWithDetailsDto
    {
        public ProductTypeDto RequestedProductType { get; set; }
    }
}

