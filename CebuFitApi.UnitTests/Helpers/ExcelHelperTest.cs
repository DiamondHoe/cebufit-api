using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CebuFitApi.Helpers;
using CebuFitApi.Models;
using JetBrains.Annotations;
using OfficeOpenXml;
using Xunit;

namespace CebuFitApi.UnitTests.Helpers
{
    [TestSubject(typeof(ExcelHelper))]
    public class ExcelHelperTest
    {
        [Fact]
        public async Task GenerateExcel_ShouldReturnNonEmptyByteArray_WhenDaysListIsProvided()
        {
            // Arrange
            var excelHelper = new ExcelHelper();
            var days = new List<Day>
            {
                new Day
                {
                    Meals = new List<Meal>
                    {
                        new Meal
                        {
                            Ingredients = new List<Ingredient>
                            {
                                new Ingredient
                                {
                                    Product = new Product { Id = Guid.NewGuid(), Name = "Product1" },
                                    Quantity = 2
                                },
                                new Ingredient
                                {
                                    Product = new Product { Id = Guid.NewGuid(), Name = "Product2" },
                                    Weight = 3
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = await excelHelper.GenerateExcel(days);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GenerateExcel_ShouldHandleEmptyDaysList()
        {
            // Arrange
            var excelHelper = new ExcelHelper();
            var days = new List<Day>();

            // Act
            var result = await excelHelper.GenerateExcel(days);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GenerateExcel_ShouldHandleNullDaysList()
        {
            // Arrange
            var excelHelper = new ExcelHelper();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => excelHelper.GenerateExcel(null));
        }

        [Fact]
        public async Task GenerateExcel_ShouldHandleDaysWithNoMeals()
        {
            // Arrange
            var excelHelper = new ExcelHelper();
            var days = new List<Day>
            {
                new Day
                {
                    Meals = new List<Meal>()
                }
            };

            // Act
            var result = await excelHelper.GenerateExcel(days);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GenerateExcel_ShouldHandleMealsWithNoIngredients()
        {
            // Arrange
            var excelHelper = new ExcelHelper();
            var days = new List<Day>
            {
                new Day
                {
                    Meals = new List<Meal>
                    {
                        new Meal
                        {
                            Ingredients = new List<Ingredient>()
                        }
                    }
                }
            };

            // Act
            var result = await excelHelper.GenerateExcel(days);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GenerateExcel_ShouldSumQuantitiesCorrectly()
        {
            // Arrange
            var excelHelper = new ExcelHelper();
            var productId = Guid.NewGuid();
            var days = new List<Day>
            {
                new Day
                {
                    Meals = new List<Meal>
                    {
                        new Meal
                        {
                            Ingredients = new List<Ingredient>
                            {
                                new Ingredient
                                {
                                    Product = new Product { Id = productId, Name = "Product1" },
                                    Quantity = 2
                                },
                                new Ingredient
                                {
                                    Product = new Product { Id = productId, Name = "Product1" },
                                    Weight = 3
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = await excelHelper.GenerateExcel(days);

            // Assert
            using var package = new ExcelPackage(new System.IO.MemoryStream(result));
            var worksheet = package.Workbook.Worksheets["ShoppingList"];
            Assert.Equal(5.0, worksheet.Cells[2, 2].Value);
        }
    }
}