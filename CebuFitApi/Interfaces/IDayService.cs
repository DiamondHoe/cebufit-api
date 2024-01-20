﻿using CebuFitApi.DTOs;
using CebuFitApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDayService
{
    Task<List<DayDTO>> GetAllDaysAsync(Guid userIdClaim);
    Task<List<DayWithMealsDTO>> GetAllDaysWithMealsAsync(Guid userIdClaim);
    Task<DayDTO> GetDayByIdAsync(Guid dayId, Guid userIdClaim);
    Task<DayWithMealsDTO> GetDayByIdWithMealsAsync(Guid dayId, Guid userIdClaim);
    Task<Guid> CreateDayAsync(DayCreateDTO day, Guid userIdClaim);
    Task UpdateDayAsync(DayUpdateDTO day, Guid userIdClaim);
    Task DeleteDayAsync(Guid dayId, Guid userIdClaim);

    Task<DayDTO> AddMealToDayAsync(Guid dayId, Guid mealId);
    Task<DayDTO> RemoveMealFromDayAsync(Guid dayId, Guid mealId);
}
