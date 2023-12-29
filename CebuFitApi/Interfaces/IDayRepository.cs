using CebuFitApi.Models;

public interface IDayRepository
{
    Task<List<Day>> GetAllAsync();
    Task<Day> GetByIdAsync(Guid id);
    Task CreateAsync(Day day);
    Task UpdateAsync(Day day);
    Task DeleteAsync(Guid id);
}
