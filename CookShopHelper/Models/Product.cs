using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Calories { get; set; }
        public ImportanceEnum Importance { get; set; }
        public int UnitWieght { get; set; }
        public Category Category { get; set; }

    }
}
