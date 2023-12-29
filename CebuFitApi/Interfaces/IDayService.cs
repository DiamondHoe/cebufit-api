using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDayService
{
    Task<DayDTO> CreateDayAsync(DayDTO day);
    Task<DayDTO> GetDayByIdAsync(Guid dayId);
    Task<List<DayDTO>> GetAllDaysAsync();
    Task UpdateDayAsync(DayDTO day);
    Task DeleteDayAsync(Guid dayId);
}
