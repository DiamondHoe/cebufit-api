using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IDayRepository
{
    Task<List<Day>> GetAllAsync(Guid userIdClaim);
    Task<List<Day>> GetAllWithMealsAsync(Guid userIdClaim);
    Task<Day> GetByIdAsync(Guid id, Guid userIdClaim);
    Task<Day> GetByIdWithMealsAsync(Guid id, Guid userIdClaim);
    Task CreateAsync(Day day, Guid userIdClaim);
    Task UpdateAsync(Day day, Guid userIdClaim);
    Task DeleteAsync(Guid id, Guid userIdClaim);

    Task<Day> AddMealToDayAsync(Guid dayId, Guid mealId);
    Task<Day> RemoveMealFromDayAsync(Guid dayId, Guid mealId);

}
