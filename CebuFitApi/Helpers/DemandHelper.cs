using CebuFitApi.DTOs;
using CebuFitApi.DTOs.Demand;

namespace CebuFitApi.Helpers
{
    public static class DemandHelper
    {
        public static void CountDayDemand(DayWithMealsDTO? day)
        {   
            foreach (var meal in day.Meals)
            {
                foreach(var ingredient in meal.Ingredients)
                {
                    day.Demand.CaloriesEaten += ingredient.Product.Macro.Calories;
                    day.Demand.CarbEaten += ingredient.Product.Macro.Carb;
                    day.Demand.FatEaten += ingredient.Product.Macro.Fat;
                    day.Demand.ProteinEaten += ingredient.Product.Macro.Protein;
                }
            }
        }
    }
}
