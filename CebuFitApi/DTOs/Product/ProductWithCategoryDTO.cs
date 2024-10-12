using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class ProductWithCategoryDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ImportanceEnum Importance { get; set; }
        public bool IsPublic { get; set; }
        public bool Packaged { get; set; }
        public int UnitWeight { get; set; }
        public CategoryDTO Category { get; set; }
        public Guid MacroId { get; set; }
    }
}
