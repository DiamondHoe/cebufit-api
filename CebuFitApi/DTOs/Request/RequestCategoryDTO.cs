namespace CebuFitApi.DTOs
{
    public class RequestCategoryDto : RequestWithDetailsDto
    {
        public CategoryDTO RequestedCategory { get; set; }
    }
}

