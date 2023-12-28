namespace CebuFitApi.Models
{
    public class Day
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Meal>? Meals { get; set;}
    }
}
