using CebuFitApi.DTOs;

namespace CebuFitApi.Interfaces
{
    public interface IMealService
    {
        Task<List<MealDTO>> GetAllMealsAsync(Guid userIdClaim);
        Task<List<MealWithDetailsDTO>> GetAllMealsWithDetailsAsync(Guid userIdClaim);
        Task<MealDTO> GetMealByIdAsync(Guid id, Guid userIdClaim);
        Task<MealWithDetailsDTO> GetMealByIdWithDetailsAsync(Guid id, Guid userIdClaim);
        Task<Guid> CreateMealAsync(MealCreateDTO mealDTO, Guid userIdClaim);
        Task UpdateMealAsync(MealUpdateDTO mealDTO, Guid userIdClaim);
        Task DeleteMealAsync(Guid id, Guid userIdClaim);
        Task PrepareMealAsync(MealPrepareDTO mealPrepareDTO, Guid userIdClaim);
        Task EatMealAsync(Guid mealId, Guid userIdClaim);
    }
}
