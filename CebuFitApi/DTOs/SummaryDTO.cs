using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CebuFitApi.DTOs
{
    public class SummaryDTO
    {
        public string SummaryType { get; set; }
        public Dictionary<DateTime,decimal? > Costs { get; set; }
        public Dictionary<DateTime, MacroDTO> Macros { get; set; }
        public MacroDTO AverageMacros {  get; set; }
        public Dictionary<string, int?> AverageCategories { get; set; }
        public Dictionary<ImportanceEnum, int?> AverageImportances { get; set; }
    }
}
