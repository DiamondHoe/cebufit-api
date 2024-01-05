using CebuFitApi.DTOs;
using CebuFitApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDayService
{
    Task<List<DayDTO>> GetAllDaysAsync();
    Task<List<DayWithMealsDTO>> GetAllDaysWithMealsAsync();
    Task<DayDTO> GetDayByIdAsync(Guid dayId);
    Task<DayWithMealsDTO> GetDayByIdWithMealsAsync(Guid dayId);
    Task<Guid> CreateDayAsync(DayCreateDTO day);
    Task UpdateDayAsync(DayUpdateDTO day);
    Task DeleteDayAsync(Guid dayId);

    Task<DayDTO> AddMealToDayAsync(Guid dayId, Guid mealId);
    Task<DayDTO> RemoveMealFromDayAsync(Guid dayId, Guid mealId);
}
