using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using OfficeOpenXml;

namespace CebuFitApi.Helpers
{
    public class ExcelHelper : IExcelHelper
    {
        public async Task<byte[]> GenerateExcel(List<Day> days)
        {
            if (days == null)
            {
                throw new ArgumentNullException(nameof(days));
            }
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("ShoppingList");

            AddHeaders(worksheet);

            int row = 2;
            Dictionary<Guid, double> ingredientSum = new Dictionary<Guid, double>(); // Dictionary to store summed quantities by base product ID

            foreach (var day in days)
            {
                foreach (var meal in day.Meals)
                {
                    foreach (var ingredient in meal.Ingredients)
                    {
                        // Check if the base product ID exists in the dictionary
                        if (ingredientSum.ContainsKey(ingredient.Product.Id))
                        {
                            // If yes, add the quantity or weight to the existing sum
                            ingredientSum[ingredient.Product.Id] += (double)(ingredient.Quantity ?? ingredient.Weight ?? 0);
                        }
                        else
                        {
                            // If no, initialize the sum with the current quantity or weight
                            ingredientSum[ingredient.Product.Id] = (double)(ingredient.Quantity ?? ingredient.Weight ?? 0);
                        }
                    }
                }
            }

            // Write rows with summed quantities
            foreach (var (productId, sum) in ingredientSum)
            {
                var ingredient = days.SelectMany(day => day.Meals.SelectMany(meal => meal.Ingredients))
                    .FirstOrDefault(ing => ing.Product.Id == productId);

                FillRow(worksheet, row, ingredient, sum);
                row++;
            }

            worksheet.Cells.AutoFitColumns();
            return package.GetAsByteArray();
        }

        private void AddHeaders(ExcelWorksheet worksheet)
        {
            worksheet.Cells[1, 1].Value = "Ingredient Name";
            worksheet.Cells[1, 2].Value = "Quantity/Weight";
            worksheet.Cells[1, 3].Value = "Product Name";
            worksheet.Cells[1, 4].Value = "Importance";
            worksheet.Cells[1, 5].Value = "Category";
        }

        private void FillRow(ExcelWorksheet worksheet, int row, Ingredient ingredient, double sum)
        {
            worksheet.Cells[row, 1].Value = ingredient.Product.Name;
            worksheet.Cells[row, 3].Value = ingredient.Product.Name;
            worksheet.Cells[row, 4].Value = ingredient.Product.Importance;
            worksheet.Cells[row, 5].Value = ingredient.Product.Category?.Name;

            // Set the summed quantity/weight
            worksheet.Cells[row, 2].Value = sum;
        }
    }
}