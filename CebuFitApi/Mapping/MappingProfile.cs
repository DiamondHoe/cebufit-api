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
            CreateMap<MacroCreateDTO, Macro>();
            CreateMap<Macro, MacroCreateDTO>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<Category, CategoryCreateDTO>();

            CreateMap<DayDTO, Day>();
            CreateMap<Day, DayDTO>();

            CreateMap<IngredientDTO, Ingredient>();
            CreateMap<Ingredient, IngredientDTO>();

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

            CreateMap<RecipeDTO, Recipe>();
            CreateMap<Recipe, RecipeDTO>();

            CreateMap<StorageItemDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemDTO>();
            CreateMap<StorageItemCreateDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemCreateDTO>();
            CreateMap<StorageItemWithProductDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemWithProductDTO>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .AfterMap((src, dest) => dest.Product.CategoryId = src.Product.Category.Id);


        }
    }
}
