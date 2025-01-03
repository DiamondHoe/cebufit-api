using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.DTOs.User;
using CebuFitApi.Models;

namespace CebuFitApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserCreateDTO>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<User, UserLoginDTO>();
            CreateMap<UserPublicDto, User>();
            CreateMap<User, UserPublicDto>();
            CreateMap<UserDetailsDTO, User>();
            CreateMap<User, UserDetailsDTO>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<User, UserUpdateDTO>();
            #endregion

            #region Meal
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
            #endregion

            #region Macro
            CreateMap<MacroDTO, Macro>();
            CreateMap<Macro, MacroDTO>();
            CreateMap<MacroCreateDTO, Macro>();
            CreateMap<Macro, MacroCreateDTO>();
            #endregion

            #region Category
            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<Category, CategoryCreateDTO>();
            #endregion

            #region Day
            CreateMap<DayDTO, Day>();
            CreateMap<Day, DayDTO>()
                .ForMember(dest => dest.MealsId, opt => opt.MapFrom(src => src.Meals.Select(meal => meal.Id).ToList()));
            CreateMap<DayWithMealsDTO, Day>();
            CreateMap<Day, DayWithMealsDTO>();
            CreateMap<DayCreateDTO, Day>();
            CreateMap<Day, DayCreateDTO>();
            CreateMap<DayUpdateDTO, Day>();
            CreateMap<Day, DayUpdateDTO>();
            #endregion

            #region Product
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.ProductTypeId, opt => opt.MapFrom(src => src.ProductType.Id));
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
            #endregion

            #region ProductType
            CreateMap<ProductTypeDto, ProductType>();
            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<ProductType, ProductTypeCreateDto>();
            CreateMap<ProductTypeCreateDto, ProductType>();
            #endregion
            
            #region StorageItem
            CreateMap<StorageItemDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemDTO>();
            CreateMap<StorageItemCreateDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemCreateDTO>();
            CreateMap<StorageItemWithProductDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemWithProductDTO>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
                // .AfterMap((src, dest, context) => dest.Product.Category = context.Mapper.Map<CategoryDTO>(src.Product.Category));
            CreateMap<StorageItemPrepareDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemPrepareDTO>();
            CreateMap<StorageItemPrepareDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemPrepareDTO>();
            #endregion

            #region Ingredients
            CreateMap<IngredientDTO, Ingredient>();
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<IngredientCreateDTO, Ingredient>();
            CreateMap<Ingredient, IngredientCreateDTO>();
            CreateMap<IngredientWithProductDTO, Ingredient>();
            CreateMap<Ingredient, IngredientWithProductDTO>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .AfterMap((src, dest) => dest.Product.CategoryId = src.Product.Category?.Id);
            #endregion

            #region Recipes
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
            #endregion

            #region Request
            CreateMap<RequestCreateDto, Request>();
            CreateMap<Request, RequestCreateDto>();
            CreateMap<RequestDto, Request>();
            CreateMap<Request, RequestDto>()
                .ForMember(dest => dest.Requester, opt => opt.MapFrom(src => src.Requester.Id))
                .ForMember(dest => dest.Approver, opt => opt.MapFrom(src => src.Approver == null ? (Guid?)null : src.Approver.Id));
            CreateMap<Request, RequestProductWithDetailsDto>();
                // .ForMember(dest => dest.RequestedProduct, opt => object);
            CreateMap<RequestProductWithDetailsDto, Request>();
            CreateMap<Request, RequestRecipeWithDetailsDto>();
            CreateMap<RequestRecipeWithDetailsDto, Request>();
            CreateMap<Request, RequestProductTypeDto>();
            CreateMap<RequestProductTypeDto, Request>();
            CreateMap<Request, RequestCategoryDto>();
            CreateMap<RequestCategoryDto, Request>();
            #endregion

            #region Demand
            CreateMap<UserDemandDTO, UserDemand>();
            CreateMap<UserDemand, UserDemandDTO>();

            CreateMap<UserDemandCreateDTO, UserDemand>();
            CreateMap<UserDemand, UserDemandCreateDTO>();

            CreateMap<UserDemandUpdateDTO, UserDemand>();
            CreateMap<UserDemand, UserDemandUpdateDTO>();
            #endregion
        }
    }
}
