﻿using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class ProductWithMacroDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ImportanceEnum Importance { get; set; }
        public bool IsPublic { get; set; }
        public int UnitWeight { get; set; }
        public Guid CategoryId { get; set; }
        public MacroDTO Macro { get; set; }
    }
}
