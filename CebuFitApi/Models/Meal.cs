namespace CebuFitApi.Models
{
    public class Meal
    {
        public Guid Id { get; set; }
        public string? Name {  get; set; }
        public bool Eaten { get; set; }
        public bool Doable { get; set; }
        public IEnumerable<Ingredient>? Ingredients { get; set;}
    }
}
