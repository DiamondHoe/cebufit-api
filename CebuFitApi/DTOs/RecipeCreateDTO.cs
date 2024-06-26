﻿using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class RecipeCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<IngredientCreateDTO> Ingredients { get; set; }
    }
}
