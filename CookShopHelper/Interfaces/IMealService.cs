using CebuFitApi.DTOs;

namespace CebuFitApi.Interfaces
{
    public interface IMealService
    {
        Task<List<MealDTO>> GetAllMealsAsync();
        Task<MealDTO> GetMealByIdAsync(int id);
        Task CreateMealAsync(MealDTO blogPostDTO);
        Task UpdateMealAsync(int id, MealDTO blogPostDTO);
        Task DeleteMealAsync(int id);
    }
}
