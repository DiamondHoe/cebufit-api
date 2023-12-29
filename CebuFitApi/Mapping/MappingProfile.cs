using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MealDTO, Meal>();
            CreateMap<Meal, MealDTO>();

            CreateMap<MacroDTO, Macro>();
            CreateMap<Macro, MacroDTO>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<Category, CategoryCreateDTO>();

            CreateMap<DayDTO, Day>();
            CreateMap<Day, DayDTO>();

            CreateMap<IngredientDTO, Ingredient>();
            CreateMap<Ingredient, IngredientDTO>();

            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();

            CreateMap<RecipeDTO, Recipe>();
            CreateMap<Recipe, RecipeDTO>();

            CreateMap<StorageItemDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemDTO>();
        }
    }
}
