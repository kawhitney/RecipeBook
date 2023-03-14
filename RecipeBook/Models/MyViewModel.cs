#pragma warning disable
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
namespace RecipeBook.Models;
public class MyViewModel{
    //? Equipment
    public Equipment Equipment {get; set;}
    public List<Equipment> AllEquipment {get; set;}
    //? Ingredient
    public Ingredient Ingredient {get; set;}
    public List<Ingredient> AllIngredients {get; set;}
    //? Step
    public Step Step {get; set;}
    public List<Step> AllSteps {get; set;}
    //? Recipe
    public Recipe Recipe {get; set;}
    public List<Recipe> AllRecipes {get; set;}
    //? MealPlan
    public MealPlan MealPlan {get; set;}
    public List<MealPlan> AllMP {get; set;}
    //? Meal
    public Meal Meal {get; set;}
    public List<Meal> AllMeals {get; set;}
}