using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserCreateDTO>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<User, UserLoginDTO>();

            CreateMap<MealDTO, Meal>();
            CreateMap<Meal, MealDTO>()
                .ForMember(dest => dest.IngredientsId, opt => opt.MapFrom(src => src.Ingredients.Select(i => i.Id).ToList()));
            CreateMap<MealCreateDTO, Meal>();
            CreateMap<Meal, MealCreateDTO>();
            CreateMap<MealUpdateDTO, Meal>();
            CreateMap<Meal, MealUpdateDTO>();
            CreateMap<MealWithDetailsDTO, Meal>();
            CreateMap<Meal, MealWithDetailsDTO>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients));
            CreateMap<MealPrepareDTO, Meal>();
            CreateMap<Meal, MealPrepareDTO>()
                .ForMember(dest => dest.StorageItems, opt => opt.MapFrom(src => src.StorageItems));
            CreateMap<MealPrepareDTO, Meal>();
            CreateMap<Meal, MealPrepareDTO>();
            
            CreateMap<MacroDTO, Macro>();
            CreateMap<Macro, MacroDTO>();
            CreateMap<MacroCreateDTO, Macro>();
            CreateMap<Macro, MacroCreateDTO>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<Category, CategoryCreateDTO>();

            CreateMap<DayDTO, Day>();
            CreateMap<Day, DayDTO>()
                .ForMember(dest => dest.MealsId, opt => opt.MapFrom(src => src.Meals.Select(meal => meal.Id).ToList()));
            CreateMap<DayWithMealsDTO, Day>();
            CreateMap<Day, DayWithMealsDTO>();
            CreateMap<DayCreateDTO, Day>();
            CreateMap<Day, DayCreateDTO>();
            CreateMap<DayUpdateDTO, Day>();
            CreateMap<Day, DayUpdateDTO>();

            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<Product, ProductCreateDTO>();
            CreateMap<ProductUpdateDTO, Product>();
            CreateMap<Product, ProductUpdateDTO>()
                .ForMember(dest => dest.Macro, opt => opt.MapFrom(src => src.Macro));
            CreateMap<ProductWithMacroDTO, Product>();
            CreateMap<Product, ProductWithMacroDTO>()
                .ForMember(dest => dest.Macro, opt => opt.MapFrom(src => src.Macro))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
            CreateMap<ProductWithCategoryDTO, Product>();
            CreateMap<Product, ProductWithCategoryDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.MacroId, opt => opt.MapFrom(src => src.Macro.Id));
            CreateMap<ProductWithDetailsDTO, Product>();
            CreateMap<Product, ProductWithDetailsDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Macro, opt => opt.MapFrom(src => src.Macro));

            CreateMap<StorageItemDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemDTO>();
            CreateMap<StorageItemCreateDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemCreateDTO>();
            CreateMap<StorageItemWithProductDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemWithProductDTO>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .AfterMap((src, dest, context) => dest.Product.Category = context.Mapper.Map<CategoryDTO>(src.Product.Category));
            CreateMap<StorageItemPrepareDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemPrepareDTO>();
            CreateMap<StorageItemPrepareDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemPrepareDTO>();
            
            CreateMap<IngredientDTO, Ingredient>();
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<IngredientCreateDTO, Ingredient>();
            CreateMap<Ingredient, IngredientCreateDTO>();
            CreateMap<IngredientWithProductDTO, Ingredient>();
            CreateMap<Ingredient, IngredientWithProductDTO>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .AfterMap((src, dest) => dest.Product.CategoryId = src.Product.Category.Id);

            CreateMap<RecipeDTO, Recipe>();
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.IngredientsId, opt => opt.MapFrom(src => src.Ingredients.Select(i => i.Id)));
            CreateMap<RecipeCreateDTO, Recipe>();
            CreateMap<Recipe, RecipeCreateDTO>();
            CreateMap<RecipeUpdateDTO, Recipe>()
                 .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients));
            CreateMap<Recipe, RecipeUpdateDTO>()
                 .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients));
            CreateMap<RecipeWithDetailsDTO, Recipe>();
            CreateMap<Recipe, RecipeWithDetailsDTO>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .AfterMap((src, dest, context) =>
                {
                    foreach (var ingredient in dest.Ingredients)
                    {
                        ingredient.Product = context.Mapper.Map<ProductWithMacroDTO>(src.Ingredients.FirstOrDefault(i => i.Id == ingredient.Id)?.Product);
                    }
                });
        }
    }
}
