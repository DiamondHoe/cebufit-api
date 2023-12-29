﻿using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Data
{
    public class CebuFitApiDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public CebuFitApiDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("cebufit_db"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new MacroConfiguration());
            modelBuilder.ApplyConfiguration(new MealConfiguration());

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new StorageItemConfiguration());
            modelBuilder.ApplyConfiguration(new IngredientConfiguration());

            modelBuilder.ApplyConfiguration(new StorageConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogueConfiguration());
            modelBuilder.ApplyConfiguration(new DayConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeConfiguration()); 
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Macro> Macros { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StorageItem> StorageItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
    }
}
