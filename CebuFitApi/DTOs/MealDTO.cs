﻿using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class MealDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool Eaten { get; set; }
        public List<Guid> IngredientsId { get; set; }
    }
}