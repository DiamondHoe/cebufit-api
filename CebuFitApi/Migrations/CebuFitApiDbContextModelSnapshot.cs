﻿// <auto-generated />
using System;
using CebuFitApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CebuFitApi.Migrations
{
    [DbContext(typeof(CebuFitApiDbContext))]
    partial class CebuFitApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
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

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CebuFitApi.Models.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("CebuFitApi.Models.Demand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Calories")
                        .HasColumnType("integer");

                    b.Property<int?>("CarbPercent")
                        .HasColumnType("integer");

                    b.Property<int?>("FatPercent")
                        .HasColumnType("integer");

                    b.Property<int?>("ProteinPercent")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Demands");
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

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("ProductId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

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

                    b.Property<bool>("Prepared")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

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

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("Packaged")
                        .HasColumnType("boolean");

                    b.Property<int>("UnitWeight")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("CebuFitApi.Models.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("CebuFitApi.Models.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ApproverId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("RequestedItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RequesterId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("RequesterId");

                    b.ToTable("Request", (string)null);
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

                    b.Property<decimal?>("ActualQuantity")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("ActualWeight")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("BoughtQuantity")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("BoughtWeight")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("DateOfPurchase")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("MealId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("StorageId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("ProductId");

                    b.HasIndex("StorageId");

                    b.HasIndex("UserId");

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

                    b.Property<int>("Height")
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

                    b.Property<int>("PhysicalActivityLevel")
                        .HasColumnType("integer");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric");

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

            modelBuilder.Entity("CebuFitApi.Models.Category", b =>
                {
                    b.HasOne("CebuFitApi.Models.User", "User")
                        .WithMany("Categories")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CebuFitApi.Models.Day", b =>
                {
                    b.HasOne("CebuFitApi.Models.User", "User")
                        .WithMany("Days")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CebuFitApi.Models.Demand", b =>
                {
                    b.HasOne("CebuFitApi.Models.User", "User")
                        .WithOne("Demand")
                        .HasForeignKey("CebuFitApi.Models.Demand", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

                    b.HasOne("CebuFitApi.Models.User", "User")
                        .WithMany("Ingredients")
                        .HasForeignKey("UserId");

                    b.Navigation("Meal");

                    b.Navigation("Product");

                    b.Navigation("Recipe");

                    b.Navigation("User");
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

                    b.HasOne("CebuFitApi.Models.User", "User")
                        .WithMany("Meals")
                        .HasForeignKey("UserId");

                    b.Navigation("Day");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CebuFitApi.Models.Product", b =>
                {
                    b.HasOne("CebuFitApi.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("CebuFitApi.Models.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CebuFitApi.Models.Recipe", b =>
                {
                    b.HasOne("CebuFitApi.Models.User", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CebuFitApi.Models.Request", b =>
                {
                    b.HasOne("CebuFitApi.Models.User", "Approver")
                        .WithMany("ApprovedRequests")
                        .HasForeignKey("ApproverId");

                    b.HasOne("CebuFitApi.Models.User", "Requester")
                        .WithMany("CreatedRequests")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approver");

                    b.Navigation("Requester");
                });

            modelBuilder.Entity("CebuFitApi.Models.StorageItem", b =>
                {
                    b.HasOne("CebuFitApi.Models.Meal", null)
                        .WithMany("StorageItems")
                        .HasForeignKey("MealId");

                    b.HasOne("CebuFitApi.Models.Product", "Product")
                        .WithMany("StorageItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CebuFitApi.Models.Storage", "Storage")
                        .WithMany("StorageItems")
                        .HasForeignKey("StorageId");

                    b.HasOne("CebuFitApi.Models.User", "User")
                        .WithMany("StorageItems")
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("Storage");

                    b.Navigation("User");
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

                    b.Navigation("StorageItems");
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

            modelBuilder.Entity("CebuFitApi.Models.User", b =>
                {
                    b.Navigation("ApprovedRequests");

                    b.Navigation("Categories");

                    b.Navigation("CreatedRequests");

                    b.Navigation("Days");

                    b.Navigation("Demand");

                    b.Navigation("Ingredients");

                    b.Navigation("Meals");

                    b.Navigation("Products");

                    b.Navigation("Recipes");

                    b.Navigation("StorageItems");
                });
#pragma warning restore 612, 618
        }
    }
}
