﻿using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class ProductCreateDTO
    {
        public string? Name { get; set; }
        public ImportanceEnum Importance { get; set; }
        public int UnitWeight { get; set; }
        public Guid CategoryId { get; set; }
        public MacroCreateDTO Macro { get; set; }
    }
}
