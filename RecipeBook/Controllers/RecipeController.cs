using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class RecipeController : Controller
{
    private MyContext _context;

    public RecipeController(MyContext context)
    {
        _context = context;
    }

    // **** recipe main page ****
    [SessionCheck]
    [HttpGet("recipe")]
    public IActionResult Recipe(){
        return View();
    }

    //! CREATE
    // *** new recipe ***
    [HttpPost("recipe/add")]
    public IActionResult CreateRecipe(Recipe recipe){
        if(ModelState.IsValid){
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
            return Redirect($"{recipe.ID}/edit");
        }
        return Redirect($"{recipe.Category}");
    }

    //! READ
    // **** view all recipes in a category ****
    [SessionCheck]
    [HttpGet("recipe/{category}")]
    public IActionResult ViewCategory(string category){
        List<Recipe> recipes;
        switch(category){
            case "appetizers":
            case "breakfast":
            case "soupsNstews":
            case "mainDishes":
            case "sideDishes":
            case "breads":
            case "desserts":
            case "drinks":
                recipes = _context.Recipes
                        .Where(r => r.Category == category)
                        .Where(r => r.UserID == HttpContext.Session.GetInt32("uid"))
                        .ToList();
                ViewBag.Category = category;
                return View(recipes);
            default:
                return RedirectToAction("Recipe");
        }
    }
    // *** view single recipe ***
    [SessionCheck]
    [HttpGet("/recipe/{recipeId}/view")]
    public IActionResult ViewRecipe(int recipeId){
        Recipe? item = _context.Recipes
                        .Include(i => i.Ingredients)
                        .Include(i => i.Equipment)
                        .Include(i => i.Directions)
                        .FirstOrDefault(i => i.ID == recipeId);
        if(item == null){
            return RedirectToAction("Recipe");
        }
        return View(item);
    }

    //! Update
    // *** edit a recipe ***
    [SessionCheck]
    [HttpGet("recipe/{recipeId}/edit")]
    public IActionResult EditRecipe(int recipeId){
        Recipe? item = _context.Recipes
                        .Include(i => i.Ingredients)
                        .Include(i => i.Equipment)
                        .Include(i => i.Directions)
                        .FirstOrDefault(i => i.ID == recipeId);
        if(item == null){
            return RedirectToAction("Recipe");
        }
        return View(item);
    }

    [HttpPost("recipe/{recipeId}/update")]
    public IActionResult UpdateRecipe(Recipe recipe, int recipeId){
        if(ModelState.IsValid){
            Recipe? item = _context.Recipes
                            .FirstOrDefault(i => i.ID == recipeId);
            item.Category = recipe.Category;
            item.Name = recipe.Name;
            item.Yield = recipe.Yield;
            item.PrepHr = recipe.PrepHr;
            item.PrepMin = recipe.PrepMin;
            item.CookHr = recipe.CookHr;
            item.CookMin = recipe.CookMin;
            item.Difficulty = recipe.Difficulty;
            item.UpdatedAt = DateTime.Now;
            _context.Recipes.Update(item);
            _context.SaveChanges();
        }
        return Redirect("edit");
    }

    //! DELETE
    // *** remove a recipe ***

    // *** remove equipment from recipe equipment list ***

}