using CebuFitApi.DTOs;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;

namespace CebuFitApi.Helpers
{
    public static class DemandHelper
    {
        public static void CountDayDemand(DayWithMealsDTO? day)
        {   
            foreach (var meal in day.Meals)
            {
                if (meal.Eaten)
                {
                    foreach (var ingredient in meal.Ingredients)
                    {
                        day.Demand.CaloriesEaten += ingredient.Product.Macro.Calories;
                        day.Demand.CarbEaten += ingredient.Product.Macro.Carb;
                        day.Demand.FatEaten += ingredient.Product.Macro.Fat;
                        day.Demand.ProteinEaten += ingredient.Product.Macro.Protein;
                    }
                }
                else
                {
                    foreach (var ingredient in meal.Ingredients)
                    {
                        day.Demand.CaloriesPlanned += ingredient.Product.Macro.Calories;
                        day.Demand.CarbPlanned += ingredient.Product.Macro.Carb;
                        day.Demand.FatPlanned += ingredient.Product.Macro.Fat;
                        day.Demand.ProteinPlanned += ingredient.Product.Macro.Protein;
                    }
                }
            }
        }

        public static bool IsDemandValid(UserDemand demand)
        {
            return demand.Calories.HasValue && demand.Calories > 0
                && demand.FatPercent.HasValue && demand.FatPercent > 0
                && demand.CarbPercent.HasValue && demand.CarbPercent > 0
                && demand.ProteinPercent.HasValue && demand.ProteinPercent > 0
                && (demand.ProteinPercent + demand.CarbPercent + demand.FatPercent == 100);
        }

        public static UserDemand CalculateDemand(User user)
        {
            var userDemand = CalculateUserDemand(user);

            if (IsDemandValid(userDemand))
            {
                userDemand.Id = Guid.NewGuid();
                userDemand.UserId = user.Id;
                return userDemand;
            }
            else
            {
                var newDemand = GetDefaultUserDemand();
                newDemand.Id = Guid.NewGuid();
                newDemand.UserId = user.Id;
                return newDemand;
            }
        }

        private static UserDemand CalculateUserDemand(User user)
        {
            var bmr = CalculateBasalMetabolicRate(user);
            var activityFactor = GetActivityFactor(user.PhysicalActivityLevel);

            return new UserDemand
            {
                Calories = (int?)(bmr * activityFactor),
                CarbPercent = 40,
                FatPercent = 30,
                ProteinPercent = 30
            };
        }
        private static double CalculateBasalMetabolicRate(User user)
        {
            var weightFactor = 10 * decimal.ToInt32(user.Weight);
            var heightFactor = 6.25m * user.Height;
            var ageFactor = 5 * (DateTime.UtcNow.Year - user.BirthDate.Year);
            var genderAdjustment = user.Gender ? -161 : 5;

            return (double)(weightFactor + heightFactor - ageFactor + genderAdjustment);
        }

        private static double GetActivityFactor(PhysicalActivityLevelEnum activityLevel)
        {
            return activityLevel switch
            {
                PhysicalActivityLevelEnum.Sedentary => 1.2,
                PhysicalActivityLevelEnum.LightlyActive => 1.375,
                PhysicalActivityLevelEnum.ModeratelyActive => 1.55,
                PhysicalActivityLevelEnum.VeryActive => 1.725,
                PhysicalActivityLevelEnum.SuperActive => 1.9,
                _ => 1.0 // Default case
            };
        }

        private static UserDemand GetDefaultUserDemand()
        {
            return new UserDemand
            {
                Calories = 2000,
                CarbPercent = 40,
                FatPercent = 30,
                ProteinPercent = 30
            };
        }
    }
}
