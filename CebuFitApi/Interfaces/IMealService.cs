using CebuFitApi.DTOs;

namespace CebuFitApi.Interfaces
{
    public interface IMealService
    {
        Task<List<MealDTO>> GetAllMealsAsync();
        Task<List<MealWithDetailsDTO>> GetAllMealsWithDetailsAsync();
        Task<MealDTO> GetMealByIdAsync(Guid id);
        Task<MealWithDetailsDTO> GetMealByIdWithDetailsAsync(Guid id);
        Task CreateMealAsync(MealCreateDTO mealDTO);
        Task UpdateMealAsync(MealUpdateDTO mealDTO);
        Task DeleteMealAsync(Guid id);
    }
}
