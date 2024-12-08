using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.DTOs.User;
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
        private readonly IUserDemandRepository _demandRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,
            IStorageItemService storageItemService,
            IDayService dayService,
            IMealService mealService,
            ICategoryService categoryService,
            IUserDemandService demandService,
            IUserDemandRepository demandRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _storageItemService = storageItemService;
            _dayService = dayService;
            _mealService = mealService;
            _categoryService = categoryService;
            _demandService = demandService;
            _demandRepository = demandRepository;
            _mapper = mapper;
        }
        public async Task<User> AuthenticateAsync(UserLoginDTO userDTO)
        {
            var userEntity = _mapper.Map<User>(userDTO);
            var userAuthenticated = await _userRepository.AuthenticateAsync(userEntity);
            return userAuthenticated;
        }
        public async Task<(bool, User)> CreateAsync(UserCreateDTO userDTO)
        {
            var userEntity = _mapper.Map<User>(userDTO);
            userEntity.Id = Guid.NewGuid();

            if (userEntity.Demand == null
                || userEntity.Height == 0
                || userEntity.Weight == 0
                || !DemandHelper.IsDemandValid(userEntity.Demand)
                )
            {
                userEntity.Demand = DemandHelper.CalculateDemand(userEntity);
            }

            bool isRegistered = await _userRepository.CreateAsync(userEntity);

            return (isRegistered, userEntity);
        }
        public async Task<UserDetailsDTO> GetDetailsAsync(Guid userIdClaim)
        {
            var userEntity = await _userRepository.GetByIdAsync(userIdClaim);
            var userDTO = _mapper.Map<UserDetailsDTO>(userEntity);
            return userDTO;
        }
        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var userEntity = await _userRepository.GetByEmailAsync(email);
            var userDTO = _mapper.Map<UserDTO>(userEntity);
            return userDTO;
        }
        public async Task<string> ResetPasswordAsync(string email)
        {
            var foundUser = await _userRepository.GetByEmailAsync(email);
            if (foundUser == null)
            {
                return ("");
            }

            string generatedPassword = PasswordGenerator.GenerateRandomPassword(12);
            foundUser.Password = BCrypt.Net.BCrypt.HashPassword(generatedPassword);
            var userEntity = _mapper.Map<User>(foundUser);
            await _userRepository.UpdateAsync(userEntity);
            EmailService.SendEmail(email, foundUser.Name, generatedPassword);

            return generatedPassword;
        }
        public async Task UpdateAsync(Guid userIdClaim, UserUpdateDTO userDTO)
        {
            var foundUser = await _userRepository.GetByIdAsync(userIdClaim);

            if (foundUser != null)
            {
                foundUser.Name = userDTO.Name ?? foundUser.Name;
                foundUser.Gender = userDTO.Gender ?? foundUser.Gender;
                foundUser.Height = userDTO.Height ?? foundUser.Height;
                foundUser.Weight = userDTO.Weight ?? foundUser.Weight;
                foundUser.BirthDate = userDTO.BirthDate ?? foundUser.BirthDate;
                foundUser.PhysicalActivityLevel = userDTO.PhysicalActivityLevel ?? foundUser.PhysicalActivityLevel;
                foundUser.Demand = _mapper.Map<UserDemand>(userDTO.Demand) ?? foundUser.Demand;

                await _userRepository.UpdateAsync(foundUser);
            }
        }

        public Task<bool> DeleteAsync(Guid userIdClaim)
        {
            throw new NotImplementedException();
        }

        public async Task<SummaryDTO> GetSummaryAsync(Guid userIdClaim, DateTime start, DateTime end)
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

            var storageItemsDtos = await _storageItemService.GetAllStorageItemsWithProductAsync(userIdClaim)
                .ContinueWith(task => task.Result.Where(x => x.DateOfPurchase >= start && x.DateOfPurchase <= end).ToList());

            var daysDtos = await _dayService.GetAllDaysWithMealsAsync(userIdClaim)
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
                        var foundCategory = await _categoryService.GetCategoryByIdAsync(product.CategoryId.GetValueOrDefault(), userIdClaim);
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
                    foreach (var product in productsPerDay)
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
                        var foundCategory = await _categoryService.GetCategoryByIdAsync(product.CategoryId.GetValueOrDefault(), userIdClaim);
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
                        var foundCategory = await _categoryService.GetCategoryByIdAsync(product.CategoryId.GetValueOrDefault(), userIdClaim);
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
