using System;
using System.Collections.Generic;
using CebuFitApi.DTOs;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.Helpers;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;
using JetBrains.Annotations;
using Xunit;

namespace CebuFitApi.UnitTests.Helpers
{
    [TestSubject(typeof(DemandHelper))]
    public class DemandHelperTest
    {
        [Theory]
        [InlineData(100, 50, 30, 20, 10, 10, 5, 3, 2, 10, 5, 3, 2)]
        [InlineData(100, 50, 30, 20, 20, 20, 10, 6, 4, 20, 10, 6, 4)]
        public void CountDayDemand_ShouldCalculateCorrectly(int calories, int carb, int fat, int protein,
            int unitWeight, int caloriesExpected, int carbExpected, int fatExpected, int proteinExpected,
            int caloriesPlannedExpected, int carbPlannedExpected, int fatPlannedExpected, int proteinPlannedExpected)
        {
            // Arrange
            var day = new DayWithMealsDTO
            {
                Meals = new List<MealWithDetailsDTO>
                {
                    new MealWithDetailsDTO
                    {
                        Eaten = true,
                        Ingredients = new List<IngredientWithProductDTO>
                        {
                            new IngredientWithProductDTO
                            {
                                Product = new ProductWithMacroDTO
                                {
                                    UnitWeight = unitWeight,
                                    Macro = new MacroDTO
                                    {
                                        Calories = calories,
                                        Carb = carb,
                                        Fat = fat,
                                        Protein = protein
                                    }
                                }
                            }
                        }
                    },
                    new MealWithDetailsDTO
                    {
                        Eaten = false,
                        Ingredients = new List<IngredientWithProductDTO>
                        {
                            new IngredientWithProductDTO
                            {
                                Product = new ProductWithMacroDTO
                                {
                                    UnitWeight = unitWeight,
                                    Macro = new MacroDTO
                                    {
                                        Calories = calories,
                                        Carb = carb,
                                        Fat = fat,
                                        Protein = protein
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            DemandHelper.CountDayDemand(day);

            // Assert
            Assert.Equal(caloriesExpected, day.Demand.CaloriesEaten);
            Assert.Equal(carbExpected, day.Demand.CarbEaten);
            Assert.Equal(fatExpected, day.Demand.FatEaten);
            Assert.Equal(proteinExpected, day.Demand.ProteinEaten);

            Assert.Equal(caloriesPlannedExpected, day.Demand.CaloriesPlanned);
            Assert.Equal(carbPlannedExpected, day.Demand.CarbPlanned);
            Assert.Equal(fatPlannedExpected, day.Demand.FatPlanned);
            Assert.Equal(proteinPlannedExpected, day.Demand.ProteinPlanned);
        }

        [Theory]
        [InlineData(2000, 65, 20, 15, true)]
        [InlineData(0, 65, 20, 15, false)]
        [InlineData(2000, 0, 20, 15, false)]
        [InlineData(2000, 65, 0, 15, false)]
        [InlineData(2000, 65, 20, 0, false)]
        [InlineData(2000, 65, 20, 16, false)]
        public void IsDemandValid_ShouldReturnCorrectResult(int? calories, int? carbPercent, int? fatPercent,
            int? proteinPercent, bool expected)
        {
            // Arrange
            var demand = new UserDemand
            {
                Calories = calories,
                CarbPercent = carbPercent,
                FatPercent = fatPercent,
                ProteinPercent = proteinPercent
            };

            // Act
            var result = DemandHelper.IsDemandValid(demand);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDemand_ShouldReturnValidDemand()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Weight = 70,
                Height = 175,
                BirthDate = new DateTime(1990, 1, 1),
                Gender = true,
                PhysicalActivityLevel = PhysicalActivityLevelEnum.ModeratelyActive
            };

            // Act
            var demand = DemandHelper.CalculateDemand(user);

            // Assert
            Assert.NotNull(demand);
            Assert.Equal(user.Id, demand.UserId);
            Assert.Equal(user, demand.User);
        }

        [Fact]
        public void CalculateDemand_ShouldReturnDefaultDemandWhenInvalid()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Weight = 0,
                Height = 0,
                BirthDate = new DateTime(1990, 1, 1),
                Gender = true,
                PhysicalActivityLevel = PhysicalActivityLevelEnum.ModeratelyActive
            };

            // Act
            var demand = DemandHelper.CalculateDemand(user);

            // Assert
            Assert.NotNull(demand);
            Assert.Equal(2000, demand.Calories);
            Assert.Equal(65, demand.CarbPercent);
            Assert.Equal(20, demand.FatPercent);
            Assert.Equal(15, demand.ProteinPercent);
        }
    }
}