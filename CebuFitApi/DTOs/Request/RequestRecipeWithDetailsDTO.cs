namespace CebuFitApi.DTOs
{
    public class RequestRecipeWithDetailsDto : RequestWithDetailsDto
    {
        public RecipeWithDetailsDTO RequestedRecipe { get; set; }
    }
}

