using CebuFitApi.DTOs;

namespace CebuFitApi.Interfaces
{
    public interface IMealService
    {
        Task<List<MealDTO>> GetAllMealsAsync();
        Task<MealDTO> GetMealByIdAsync(Guid id);
        Task CreateMealAsync(MealDTO blogPostDTO);
        Task UpdateMealAsync(Guid id, MealDTO blogPostDTO);
        Task DeleteMealAsync(Guid id);
    }
}
