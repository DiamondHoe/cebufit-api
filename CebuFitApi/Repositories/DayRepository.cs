using CebuFitApi.Data;
using CebuFitApi.DTOs;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class DayRepository : IDayRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public DayRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Day>> GetAllAsync()
        {
            var days = await _dbContext.Days
                .Include(d => d.Meals)
                .ToListAsync();
            return days;
        }

        public async Task<List<Day>> GetAllWithMealsAsync()
        {
            var days = await _dbContext.Days
                 .Include(d => d.Meals)
                 .ThenInclude(m => m.Ingredients)
                 .ThenInclude(i => i.Product)
                 .ThenInclude(p => p.Macro)
                 .Include(d => d.Meals)
                 .ThenInclude(m => m.Ingredients)
                 .ThenInclude(i => i.Product)
                 .ThenInclude(p => p.Category)
                 .ToListAsync();
            return days;
        }

        public async Task<Day> GetByIdAsync(Guid dayId)
        {
            var day = await _dbContext.Days
    .           Include(d => d.Meals)
                .Where(p => p.Id == dayId)
                .FirstOrDefaultAsync();
            return day;
        }

        public async Task<Day> GetByIdWithMealsAsync(Guid dayId)
        {
            var day = await _dbContext.Days
                 .Include(d => d.Meals)
                 .ThenInclude(m => m.Ingredients)
                 .ThenInclude(i => i.Product)
                 .ThenInclude(p => p.Macro)
                 .Include(d => d.Meals)
                 .ThenInclude(m => m.Ingredients)
                 .ThenInclude(i => i.Product)
                 .ThenInclude(p => p.Category)
                 .Where(p => p.Id == dayId)
                 .FirstOrDefaultAsync();
            return day;
        }

        public async Task CreateAsync(Day day)
        {
            await _dbContext.AddAsync(day);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Day day)
        {
            var existingDay = await _dbContext.Days
                 .Include(d => d.Meals)
                 .ThenInclude(m => m.Ingredients)
                 .ThenInclude(i => i.Product)
                 .ThenInclude(p => p.Macro)
                 .Where(p => p.Id == day.Id)
                 .FirstOrDefaultAsync();

            if(existingDay != null)
            {
                //NP TODO Implement - for now not in use
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            var dayToDelete = await _dbContext.Days.FindAsync(id);
            if(dayToDelete != null)
            {
                _dbContext.Days.Remove(dayToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Day> AddMealToDayAsync(Guid dayId, Guid mealId)
        {
            var day = await _dbContext.Days
                .Include(d => d.Meals)
                .Where(p => p.Id == dayId)
                .FirstOrDefaultAsync(); ;
            var meal = await _dbContext.Meals.FindAsync(mealId);

            if (day != null && meal != null)
            {
                if (!_dbContext.Entry(day).IsKeySet)
                {
                    _dbContext.Attach(day);
                }

                if (!_dbContext.Entry(meal).IsKeySet)
                {
                    _dbContext.Attach(meal);
                }
                day.Meals.Add(meal);

                await _dbContext.SaveChangesAsync();
                return day;
            }

            return null;
        }

        public async Task<Day> RemoveMealFromDayAsync(Guid dayId, Guid mealId)
        {
            var day = await _dbContext.Days.FindAsync(dayId);
            var meal = await _dbContext.Meals.FindAsync(mealId);

            if (day != null && meal != null)
            {
                if (!_dbContext.Entry(day).IsKeySet)
                {
                    _dbContext.Attach(day);
                }

                if (!_dbContext.Entry(meal).IsKeySet)
                {
                    _dbContext.Attach(meal);
                }

                day.Meals.Remove(meal);

                await _dbContext.SaveChangesAsync();
                return day;
            }

            return null;
        }
    }
}
