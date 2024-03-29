﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeBook.Models;

#nullable disable

namespace RecipeBook.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230316234031_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RecipeBook.Models.Equipment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("RecipeID");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("RecipeBook.Models.Ingredient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("QuantityAmount")
                        .HasColumnType("float");

                    b.Property<string>("QuantityType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("RecipeID");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RecipeBook.Models.Meal", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MealPlanID")
                        .HasColumnType("int");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.Property<int>("Weekday")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MealPlanID");

                    b.HasIndex("RecipeID");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("RecipeBook.Models.MealPlan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Favorite")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("MealPlans");
                });

            modelBuilder.Entity("RecipeBook.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ShoppingListID")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("ShoppingListID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RecipeBook.Models.Recipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("CookHr")
                        .HasColumnType("int");

                    b.Property<int>("CookMin")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PrepHr")
                        .HasColumnType("int");

                    b.Property<int>("PrepMin")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("Yield")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RecipeBook.Models.ShoppingList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MealPlanID")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("MealPlanID")
                        .IsUnique();

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("RecipeBook.Models.Step", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("RecipeID");

                    b.ToTable("Directions");
                });

            modelBuilder.Entity("RecipeBook.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecipeBook.Models.Equipment", b =>
                {
                    b.HasOne("RecipeBook.Models.Recipe", "Recipe")
                        .WithMany("Equipment")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeBook.Models.Ingredient", b =>
                {
                    b.HasOne("RecipeBook.Models.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeBook.Models.Meal", b =>
                {
                    b.HasOne("RecipeBook.Models.MealPlan", "MealPlan")
                        .WithMany("Meals")
                        .HasForeignKey("MealPlanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeBook.Models.Recipe", "Recipe")
                        .WithMany("Meals")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealPlan");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeBook.Models.MealPlan", b =>
                {
                    b.HasOne("RecipeBook.Models.User", null)
                        .WithMany("UserMealPlans")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipeBook.Models.Product", b =>
                {
                    b.HasOne("RecipeBook.Models.ShoppingList", "ShoppingList")
                        .WithMany("Products")
                        .HasForeignKey("ShoppingListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("RecipeBook.Models.Recipe", b =>
                {
                    b.HasOne("RecipeBook.Models.User", null)
                        .WithMany("UserRecipes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipeBook.Models.ShoppingList", b =>
                {
                    b.HasOne("RecipeBook.Models.MealPlan", "MealPlan")
                        .WithOne("ShoppingList")
                        .HasForeignKey("RecipeBook.Models.ShoppingList", "MealPlanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealPlan");
                });

            modelBuilder.Entity("RecipeBook.Models.Step", b =>
                {
                    b.HasOne("RecipeBook.Models.Recipe", "Recipe")
                        .WithMany("Directions")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeBook.Models.MealPlan", b =>
                {
                    b.Navigation("Meals");

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("RecipeBook.Models.Recipe", b =>
                {
                    b.Navigation("Directions");

                    b.Navigation("Equipment");

                    b.Navigation("Ingredients");

                    b.Navigation("Meals");
                });

            modelBuilder.Entity("RecipeBook.Models.ShoppingList", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("RecipeBook.Models.User", b =>
                {
                    b.Navigation("UserMealPlans");

                    b.Navigation("UserRecipes");
                });
#pragma warning restore 612, 618
        }
    }
}
