using CebuFitApi.Data;
using CebuFitApi.Interfaces;
using CebuFitApi.Mapping;
using CebuFitApi.Repositories;
using CebuFitApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region repositories and services
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IMealService, MealService>();

builder.Services.AddScoped<CebuFitApi.Interfaces.ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryService>();

builder.Services.AddScoped<IDayRepository, DayRepository>();
builder.Services.AddScoped<IDayService, DayService>();

builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IIngredientService, IngredientService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

builder.Services.AddScoped<IStorageItemRepository, StorageItemRepository>();
builder.Services.AddScoped<IStorageItemService, StorageItemService>();

builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
#endregion

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<CebuFitApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("cebufit_db")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
