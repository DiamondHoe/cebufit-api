﻿using CebuFitApi.Data;
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

        public async Task<List<Day>> GetAllAsync(Guid userIdClaim)
        {
            var days = await _dbContext.Days
                 .Where(x => x.User.Id == userIdClaim)
                .Include(d => d.Meals)
                .ToListAsync();
            return days;
        }

        public async Task<List<Day>> GetAllWithMealsAsync(Guid userIdClaim)
        {
            var days = await _dbContext.Days
                 .Where(x => x.User.Id == userIdClaim)
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

        public async Task<Day> GetByIdAsync(Guid dayId, Guid userIdClaim)
        {
            var day = await _dbContext.Days
                .Where(x => x.User.Id == userIdClaim && x.Id == dayId)
                .Include(d => d.Meals)
                .FirstAsync();
            return day;
        }

        public async Task<Day> GetByIdWithMealsAsync(Guid dayId, Guid userIdClaim)
        {
            var day = await _dbContext.Days
                 .Where(x => x.User.Id == userIdClaim && x.Id == dayId)
                 .Include(d => d.Meals)
                     .ThenInclude(m => m.Ingredients)
                     .ThenInclude(i => i.Product)
                     .ThenInclude(p => p.Macro)
                 .Include(d => d.Meals)
                     .ThenInclude(m => m.Ingredients)
                     .ThenInclude(i => i.Product)
                     .ThenInclude(p => p.Category)
                 .FirstAsync();
            return day;
        }
        
        public async Task<Day?> GetByDateWithMealsAsync(DateTime date, Guid userIdClaim)
        {
            var day = await _dbContext.Days
                .Where(x => x.User.Id == userIdClaim && x.Date.ToLocalTime().Date == date.ToLocalTime().Date)
                .Include(d => d.Meals)
                    .ThenInclude(m => m.Ingredients)
                    .ThenInclude(i => i.Product)
                    .ThenInclude(p => p.ProductType)
                .Include(d => d.Meals)
                    .ThenInclude(m => m.Ingredients)
                    .ThenInclude(i => i.Product)
                    .ThenInclude(p => p.Macro)
                .Include(d => d.Meals)
                    .ThenInclude(m => m.Ingredients)
                    .ThenInclude(i => i.Product)
                    .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync();
            return day;
        }

        public async Task CreateAsync(Day day, Guid userIdClaim)
        {
            await _dbContext.AddAsync(day);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Day day, Guid userIdClaim)
        {
            var existingDay = await _dbContext.Days
                 .Where(x => x.User.Id == userIdClaim && x.Id == day.Id)
                 .Include(d => d.Meals)
                     .ThenInclude(m => m.Ingredients)
                     .ThenInclude(i => i.Product)
                     .ThenInclude(p => p.Macro)
                 .FirstAsync();

            if(existingDay != null)
            {
                //NP TODO Implement - for now not in use
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id, Guid userIdClaim)
        {
            var dayToDelete = await _dbContext.Days
                                .Where(x => x.Id == id && x.User.Id == userIdClaim)
                                .FirstAsync();
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
                .FirstAsync(); ;
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

        public async Task<decimal?> GetCostsForDateRangeAsync(DateTime start, DateTime end, Guid userIdClaim)
        {
            var totalCosts = await _dbContext.Days
                .Where(x => x.User.Id == userIdClaim && x.Date >= start && x.Date <= end)
                .SelectMany(day => day.Meals.SelectMany(meal => meal.StorageItems))
                .SumAsync(si => si.Price);

            return totalCosts;
        }

        public async Task<List<Day>> GetShoppingListForDateRangeAsync(DateTime start, DateTime end, Guid userIdClaim)
        {
            var days = await _dbContext.Days
                .Where(x => x.User.Id == userIdClaim && x.Date >= start && x.Date <= end)
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
    }
}
