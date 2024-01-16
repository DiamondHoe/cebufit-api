﻿// <auto-generated />
using System;
using CebuFitApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CebuFitApi.Migrations
{
    [DbContext(typeof(CebuFitApiDbContext))]
    [Migration("20240116171407_Users")]
    partial class Users
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CatalogueRecipe", b =>
                {
                    b.Property<Guid>("CataloguesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RecipesId")
                        .HasColumnType("uuid");

                    b.HasKey("CataloguesId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("CatalogueRecipe");
                });

            modelBuilder.Entity("CebuFitApi.Models.Catalogue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Catalogues");
                });

            modelBuilder.Entity("CebuFitApi.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CebuFitApi.Models.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("CebuFitApi.Models.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("MealId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("ProductId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("CebuFitApi.Models.Macro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Calories")
                        .HasColumnType("integer");

                    b.Property<decimal?>("Carb")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Fat")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Protein")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Salt")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("SaturatedFattyAcid")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Sugar")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Macros");
                });

            modelBuilder.Entity("CebuFitApi.Models.Meal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DayId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Eaten")
                        .HasColumnType("boolean");

                    b.Property<int>("MealTime")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("CebuFitApi.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<int>("Importance")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("UnitWeight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("CebuFitApi.Models.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("CebuFitApi.Models.Storage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("CebuFitApi.Models.StorageItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("StorageId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("expirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("StorageId");

                    b.ToTable("StorageItems");
                });

            modelBuilder.Entity("CebuFitApi.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Gender")
                        .HasColumnType("boolean");

                    b.Property<int>("KcalDemand")
                        .HasColumnType("integer");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CatalogueRecipe", b =>
                {
                    b.HasOne("CebuFitApi.Models.Catalogue", null)
                        .WithMany()
                        .HasForeignKey("CataloguesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CebuFitApi.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CebuFitApi.Models.Ingredient", b =>
                {
                    b.HasOne("CebuFitApi.Models.Meal", "Meal")
                        .WithMany("Ingredients")
                        .HasForeignKey("MealId");

                    b.HasOne("CebuFitApi.Models.Product", "Product")
                        .WithMany("Ingredients")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CebuFitApi.Models.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Meal");

                    b.Navigation("Product");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("CebuFitApi.Models.Macro", b =>
                {
                    b.HasOne("CebuFitApi.Models.Product", "Product")
                        .WithOne("Macro")
                        .HasForeignKey("CebuFitApi.Models.Macro", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CebuFitApi.Models.Meal", b =>
                {
                    b.HasOne("CebuFitApi.Models.Day", "Day")
                        .WithMany("Meals")
                        .HasForeignKey("DayId");

                    b.HasOne("CebuFitApi.Models.Recipe", null)
                        .WithMany("Meals")
                        .HasForeignKey("RecipeId");

                    b.Navigation("Day");
                });

            modelBuilder.Entity("CebuFitApi.Models.Product", b =>
                {
                    b.HasOne("CebuFitApi.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CebuFitApi.Models.StorageItem", b =>
                {
                    b.HasOne("CebuFitApi.Models.Product", "Product")
                        .WithMany("StorageItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CebuFitApi.Models.Storage", "Storage")
                        .WithMany("StorageItems")
                        .HasForeignKey("StorageId");

                    b.Navigation("Product");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("CebuFitApi.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CebuFitApi.Models.Day", b =>
                {
                    b.Navigation("Meals");
                });

            modelBuilder.Entity("CebuFitApi.Models.Meal", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("CebuFitApi.Models.Product", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Macro");

                    b.Navigation("StorageItems");
                });

            modelBuilder.Entity("CebuFitApi.Models.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Meals");
                });

            modelBuilder.Entity("CebuFitApi.Models.Storage", b =>
                {
                    b.Navigation("StorageItems");
                });
#pragma warning restore 612, 618
        }
    }
}
