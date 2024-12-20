﻿using CebuFitApi.DTOs.Demand;

namespace CebuFitApi.DTOs
{
    public class DayWithMealsDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<MealWithDetailsDTO> Meals { get; set; } = new List<MealWithDetailsDTO>();
        public DayDemandDTO Demand { get; set; } = new DayDemandDTO();
    }
}
