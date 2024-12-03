using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;

namespace CebuFitApi.Helpers
{
    public static class DemandHelper
    {
        public static void CountDayDemand(DayWithMealsDTO day)
        {
            foreach (var meal in day.Meals)
            {


                if (meal.Eaten)
                {
                    foreach (var ingredient in meal.Ingredients)
                    {
                        day.Demand.CaloriesEaten += ingredient.Product.Macro.Calories * ingredient.Product.UnitWeight / 100;
                        day.Demand.CarbEaten += ingredient.Product.Macro.Carb * ingredient.Product.UnitWeight / 100;
                        day.Demand.FatEaten += ingredient.Product.Macro.Fat * ingredient.Product.UnitWeight / 100;
                        day.Demand.ProteinEaten += ingredient.Product.Macro.Protein * ingredient.Product.UnitWeight / 100;
                    }
                }
                else
                {
                    foreach (var ingredient in meal.Ingredients)
                    {
                        day.Demand.CaloriesPlanned += ingredient.Product.Macro.Calories * ingredient.Product.UnitWeight / 100;
                        day.Demand.CarbPlanned += ingredient.Product.Macro.Carb * ingredient.Product.UnitWeight / 100;
                        day.Demand.FatPlanned += ingredient.Product.Macro.Fat * ingredient.Product.UnitWeight / 100;
                        day.Demand.ProteinPlanned += ingredient.Product.Macro.Protein * ingredient.Product.UnitWeight / 100;
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

        public static UserDemand CalculateDemand(User user, Guid? demandId = null)
        {
            var userDemand = CalculateUserDemand(user);
            
            if (IsDemandValid(userDemand))
            {
                userDemand.Id = demandId ?? Guid.NewGuid();
                userDemand.User = user;
                userDemand.UserId = user.Id;
                return userDemand;
            }
            
            var newDemand = GetDefaultUserDemand();
            userDemand.Id = demandId ?? Guid.NewGuid();
            userDemand.User = user;
            newDemand.UserId = user.Id;
            return newDemand;
        }

        private static UserDemand CalculateUserDemand(User user)
        {
            var bmr = CalculateBasalMetabolicRate(user);
            var activityFactor = GetActivityFactor(user.PhysicalActivityLevel);

            return new UserDemand
            {
                Calories = (int?)(bmr * activityFactor),
                CarbPercent = 65,
                FatPercent = 20,
                ProteinPercent = 15
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
                CarbPercent = 65,
                FatPercent = 20,
                ProteinPercent = 15
            };
        }
    }
}
