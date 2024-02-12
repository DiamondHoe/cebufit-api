using System.Collections.Generic;
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
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("ShoppingList");

            AddHeaders(worksheet);

            int row = 2;
            foreach (var day in days)
            {
                foreach (var meal in day.Meals)
                {
                    foreach (var ingredient in meal.Ingredients)
                    {
                        FillRow(worksheet, row, ingredient);
                        row++;
                    }
                }
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

        private void FillRow(ExcelWorksheet worksheet, int row, Ingredient ingredient)
        {
            worksheet.Cells[row, 1].Value = ingredient.Product.Name;
            worksheet.Cells[row, 3].Value = ingredient.Product.Name;
            worksheet.Cells[row, 4].Value = ingredient.Product.Importance;
            worksheet.Cells[row, 5].Value = ingredient.Product.Category?.Name;

            // Check if Quantity is null, then use Weight; vice versa
            if (ingredient.Quantity != null)
            {
                worksheet.Cells[row, 2].Value = ingredient.Quantity;
            }
            else if (ingredient.Weight != null)
            {
                worksheet.Cells[row, 2].Value = ingredient.Weight;
            }
            else
            {
                worksheet.Cells[row, 2].Value = ""; // Handle case when both Quantity and Weight are null
            }
        }
    }
}
