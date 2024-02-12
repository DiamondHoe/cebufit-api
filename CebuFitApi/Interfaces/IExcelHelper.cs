using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IExcelHelper
    {
        Task<byte[]> GenerateExcel(List<Day> days); // Adjust the parameter type as per your data structure
    }
}
