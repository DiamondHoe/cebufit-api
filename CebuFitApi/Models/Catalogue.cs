namespace CebuFitApi.Models
{
    public class Catalogue
    {
        public Guid Id { get; set; }
        public IEnumerable<Recipe> Recipes { get; set; }
    }
}
