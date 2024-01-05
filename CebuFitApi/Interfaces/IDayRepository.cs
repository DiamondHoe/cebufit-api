using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IDayRepository
{
    Task<List<Day>> GetAllAsync();
    Task<List<Day>> GetAllWithMealsAsync();
    Task<Day> GetByIdAsync(Guid id);
    Task<Day> GetByIdWithMealsAsync(Guid id);
    Task CreateAsync(Day day);
    Task UpdateAsync(Day day);
    Task DeleteAsync(Guid id);

    Task<Day> AddMealToDayAsync(Guid dayId, Guid mealId);
    Task<Day> RemoveMealFromDayAsync(Guid dayId, Guid mealId);

}
