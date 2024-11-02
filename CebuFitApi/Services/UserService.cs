using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Sprache;
using System.Globalization;

namespace CebuFitApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStorageItemService _storageItemService;
        private readonly IDayService _dayService;
        private readonly IMealService _mealService;
        private readonly ICategoryService _categoryService;
        private readonly IUserDemandService _demandService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IStorageItemService storageItemService, IDayService dayService, IMealService mealService, ICategoryService categoryService, IUserDemandService demandService, IMapper mapper)
        {
            _userRepository = userRepository;
            _storageItemService = storageItemService;
            _dayService = dayService;
            _mealService = mealService;
            _categoryService = categoryService;
            _demandService = demandService;
            _mapper = mapper;
        }
        public async Task<User> AuthenticateAsync(UserLoginDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            var userAuthenticated = await _userRepository.AuthenticateAsync(userEntity);
            return userAuthenticated;
        }

        public async Task<(bool, User)> CreateAsync(UserCreateDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            userEntity.Id = Guid.NewGuid();
            bool isRegistered = await _userRepository.CreateAsync(userEntity);
            userEntity.Demand = new UserDemand();
            if (isRegistered
                && (userEntity.Demand?.Calories != null || userEntity.Demand?.Calories != 0)
                && (userEntity.Demand?.FatPercent != null || userEntity.Demand?.FatPercent != 0)
                && (userEntity.Demand?.CarbPercent != null || userEntity.Demand?.CarbPercent != 0)
                && (userEntity.Demand?.ProteinPercent != null || userEntity.Demand?.ProteinPercent != 0)) await _demandService.AutoCalculateDemandAsync(userEntity.Id);
            return (isRegistered, userEntity);
        }
        public Task<string> ResetPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }
        public Task<string> UpdateAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<SummaryDTO> GetSummaryAsync(Guid userId, DateTime start, DateTime end)
        {
            var SummaryType = (end - start).Days switch
            {
                var n when n < 27 => SummaryTypeEnum.Daily,     // Less than 27 days
                var n when n >= 28 && n <= 91 => SummaryTypeEnum.Weekly,  // Between 28 and 91 days
                _ => SummaryTypeEnum.Monthly   // Above 91 days
            };

            var summaryDto = new SummaryDTO
            {
                SummaryType = SummaryType.ToString(),
                Costs = new Dictionary<DateTime, decimal?>(),
                Macros = new Dictionary<DateTime, MacroDTO>(),
                AverageMacros = new MacroDTO(),
                AverageCategories = new Dictionary<string, int?>(),
                AverageImportances = new Dictionary<ImportanceEnum, int?>(),
            };

            var storageItemsDtos = await _storageItemService.GetAllStorageItemsWithProductAsync(userId)
                .ContinueWith(task => task.Result.Where(x => x.DateOfPurchase >= start && x.DateOfPurchase <= end).ToList());

            var daysDtos = await _dayService.GetAllDaysWithMealsAsync(userId)
                .ContinueWith(task => task.Result.Where(x => x.Date >= start && x.Date <= end).ToList());

            //Calculate summary for days
            if (SummaryType == SummaryTypeEnum.Daily)
            {
                //Grouped data
                var storageItemsByDay = storageItemsDtos
                    .GroupBy(x => x.DateOfPurchase.Date)
                    .OrderBy(g => g.Min(x => x.DateOfPurchase))
                    .ToDictionary(g => g.Key, g => g.ToList());

                var daysByDay = daysDtos
                    .GroupBy(x => x.Date.Date)
                    .OrderBy(g => g.Min(x => x.Date))
                    .ToDictionary(g => g.Key, g => g.ToList());

                //Costs
                foreach (var si in storageItemsByDay)
                {
                    decimal? costPerDay = si.Value.Sum(x => x.Price);
                    summaryDto.Costs.Add(si.Key, costPerDay);
                }

                foreach (var day in daysByDay)
                {
                    //Macros
                    List<MacroDTO> macrosPerDay = day.Value
                        .SelectMany(x => x.Meals)
                        .SelectMany(y => y.Ingredients)
                        .Select(z => z.Product.Macro)
                        .ToList();

                    MacroDTO macroPerDay = new MacroDTO()
                    {
                        Id = Guid.Empty,
                        Calories = macrosPerDay.Sum(macro => macro.Calories),
                        Carb = macrosPerDay.Sum(macro => macro.Carb),
                        Sugar = macrosPerDay.Sum(macro => macro.Sugar),
                        Fat = macrosPerDay.Sum(macro => macro.Fat),
                        SaturatedFattyAcid = macrosPerDay.Sum(macro => macro.SaturatedFattyAcid),
                        Protein = macrosPerDay.Sum(macro => macro.Protein),
                        Salt = macrosPerDay.Sum(macro => macro.Salt),
                    };

                    summaryDto.Macros.Add(day.Key, macroPerDay);

                    List<ProductWithMacroDTO> productsPerDay = day.Value
                        .SelectMany(x => x.Meals)
                        .SelectMany(y => y.Ingredients)
                        .Select(z => z.Product)
                        .ToList();

                    //Categories
                    foreach (var product in productsPerDay)
                    {
                        var foundCategory = await _categoryService.GetCategoryByIdAsync(product.CategoryId.GetValueOrDefault(), userId);
                        if(foundCategory != null)
                        {
                            if (summaryDto.AverageCategories.ContainsKey(foundCategory.Name))
                            {
                                summaryDto.AverageCategories[foundCategory.Name]++;
                            }
                            else summaryDto.AverageCategories.Add(foundCategory.Name, 1);
                        }
                    }

                    //Importances
                    foreach(var product in productsPerDay)
                    {
                        var importance = product.Importance;

                        if (summaryDto.AverageImportances.ContainsKey(importance))
                        {
                            summaryDto.AverageImportances[importance]++;
                        }
                        else summaryDto.AverageImportances.Add(importance, 1);
                    }
                }
            }

            //Calculate summary for weeks
            if (SummaryType == SummaryTypeEnum.Weekly)
            {
                var storageItemsByWeek = storageItemsDtos
                   .GroupBy(x => x.DateOfPurchase.StartOfWeek(DayOfWeek.Monday))
                   .OrderBy(g => g.Min(x => x.DateOfPurchase))
                   .ToDictionary(g => g.Key, g => g.ToList());

                var daysByWeek = daysDtos
                    .GroupBy(x => x.Date.StartOfWeek(DayOfWeek.Monday))
                    .OrderBy(g => g.Min(x => x.Date))
                    .ToDictionary(g => g.Key, g => g.ToList());

                //Costs
                foreach (var si in storageItemsByWeek)
                {
                    decimal? costPerWeek = si.Value.Sum(x => x.Price);
                    summaryDto.Costs.Add(si.Key, costPerWeek);
                }

                foreach (var week in daysByWeek)
                {
                    //Macros
                    List<MacroDTO> macrosPerWeek = week.Value
                        .SelectMany(x => x.Meals)
                        .SelectMany(y => y.Ingredients)
                        .Select(z => z.Product.Macro)
                        .ToList();

                    MacroDTO macroPerWeek = new MacroDTO()
                    {
                        Id = Guid.Empty,
                        Calories = macrosPerWeek.Sum(macro => macro.Calories),
                        Carb = macrosPerWeek.Sum(macro => macro.Carb),
                        Sugar = macrosPerWeek.Sum(macro => macro.Sugar),
                        Fat = macrosPerWeek.Sum(macro => macro.Fat),
                        SaturatedFattyAcid = macrosPerWeek.Sum(macro => macro.SaturatedFattyAcid),
                        Protein = macrosPerWeek.Sum(macro => macro.Protein),
                        Salt = macrosPerWeek.Sum(macro => macro.Salt),
                    };

                    summaryDto.Macros.Add(week.Key, macroPerWeek);

                    List<ProductWithMacroDTO> productsPerWeek = week.Value
                        .SelectMany(x => x.Meals)
                        .SelectMany(y => y.Ingredients)
                        .Select(z => z.Product)
                        .ToList();

                    //Categories
                    foreach (var product in productsPerWeek)
                    {
                        var foundCategory = await _categoryService.GetCategoryByIdAsync(product.CategoryId.GetValueOrDefault(), userId);
                        if (foundCategory != null)
                        {
                            if (summaryDto.AverageCategories.ContainsKey(foundCategory.Name))
                            {
                                summaryDto.AverageCategories[foundCategory.Name]++;
                            }
                            else summaryDto.AverageCategories.Add(foundCategory.Name, 1);
                        }
                    }

                    //Importances
                    foreach (var product in productsPerWeek)
                    {
                        var importance = product.Importance;

                        if (summaryDto.AverageImportances.ContainsKey(importance))
                        {
                            summaryDto.AverageImportances[importance]++;
                        }
                        else summaryDto.AverageImportances.Add(importance, 1);
                    }
                }
            }

            //Calculate summary for months
            if (SummaryType == SummaryTypeEnum.Monthly)
            {
                var storageItemsByMonth = storageItemsDtos
                    .GroupBy(x => new DateTime(x.DateOfPurchase.Year, x.DateOfPurchase.Month, 1))
                    .ToDictionary(g => g.Key, g => g.ToList());

                var daysByMonth = daysDtos
                    .GroupBy(x => new DateTime(x.Date.Year, x.Date.Month, 1))
                    .ToDictionary(g => g.Key, g => g.ToList());

                //Costs
                foreach (var si in storageItemsByMonth)
                {
                    decimal? costPerWeek = si.Value.Sum(x => x.Price);
                    summaryDto.Costs.Add(si.Key, costPerWeek);
                }

                foreach (var month in daysByMonth)
                {
                    //Macros
                    List<MacroDTO> macrosPerWeek = month.Value
                        .SelectMany(x => x.Meals)
                        .SelectMany(y => y.Ingredients)
                        .Select(z => z.Product.Macro)
                        .ToList();

                    MacroDTO macroPerMonth = new MacroDTO()
                    {
                        Id = Guid.Empty,
                        Calories = macrosPerWeek.Sum(macro => macro.Calories),
                        Carb = macrosPerWeek.Sum(macro => macro.Carb),
                        Sugar = macrosPerWeek.Sum(macro => macro.Sugar),
                        Fat = macrosPerWeek.Sum(macro => macro.Fat),
                        SaturatedFattyAcid = macrosPerWeek.Sum(macro => macro.SaturatedFattyAcid),
                        Protein = macrosPerWeek.Sum(macro => macro.Protein),
                        Salt = macrosPerWeek.Sum(macro => macro.Salt),
                    };

                    summaryDto.Macros.Add(month.Key, macroPerMonth);

                    List<ProductWithMacroDTO> productsPerMonth = month.Value
                        .SelectMany(x => x.Meals)
                        .SelectMany(y => y.Ingredients)
                        .Select(z => z.Product)
                        .ToList();

                    //Categories
                    foreach (var product in productsPerMonth)
                    {
                        var foundCategory = await _categoryService.GetCategoryByIdAsync(product.CategoryId.GetValueOrDefault(), userId);
                        if (foundCategory != null)
                        {
                            if (summaryDto.AverageCategories.ContainsKey(foundCategory.Name))
                            {
                                summaryDto.AverageCategories[foundCategory.Name]++;
                            }
                            else summaryDto.AverageCategories.Add(foundCategory.Name, 1);
                        }
                    }

                    //Importances
                    foreach (var product in productsPerMonth)
                    {
                        var importance = product.Importance;

                        if (summaryDto.AverageImportances.ContainsKey(importance))
                        {
                            summaryDto.AverageImportances[importance]++;
                        }
                        else summaryDto.AverageImportances.Add(importance, 1);
                    }
                }
            }

            summaryDto.AverageMacros = new MacroDTO()
            {
                Id = Guid.Empty,
                Calories = (int)summaryDto.Macros.Values.Average(x => x.Calories),
                Carb = (int)summaryDto.Macros.Values.Average(x => x.Carb),
                Sugar = (int)summaryDto.Macros.Values.Average(x => x.Sugar),
                Fat = (int)summaryDto.Macros.Values.Average(x => x.Fat),
                SaturatedFattyAcid = (int)summaryDto.Macros.Values.Average(x => x.SaturatedFattyAcid),
                Protein = (int)summaryDto.Macros.Values.Average(x => x.Protein),
                Salt = (int)summaryDto.Macros.Values.Average(x => x.Salt),
            };

            return summaryDto;
        }
    }
}
