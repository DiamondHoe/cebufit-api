using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ImportanceEnum Importance { get; set; }
        public bool IsPublic { get; set; }
        public int UnitWeight { get; set; }
        public Guid CategoryId { get; set; }
        public Guid MacroId { get; set; }
    }
}
