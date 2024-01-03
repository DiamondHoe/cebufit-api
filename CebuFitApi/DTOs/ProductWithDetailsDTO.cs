using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class ProductWithDetailsDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ImportanceEnum Importance { get; set; }
        public int UnitWeight { get; set; }
        public CategoryDTO Category { get; set; }
        public MacroDTO Macro { get; set; }
    }
}
