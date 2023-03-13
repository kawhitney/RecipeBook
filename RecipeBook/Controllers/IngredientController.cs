using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class IngredientController : Controller
{
    private MyContext _context;

    public IngredientController(MyContext context)
    {
        _context = context;
    }

    //! CREATE
    // *** new ingredient to recipe ingredient list ***
    [HttpPost("addIngredient")]
    public IActionResult AddIngredient(Ingredient ingredient){
        if(ModelState.IsValid){
            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
        }
        return Redirect($"recipe/{ingredient.RecipeID}/edit");
    }

    //! READ

    //! UPDATE

    //! DELETE
    // *** remove step from recipe list of directions ***
    [HttpPost("{ingredientId}/deleteIngredient")]
    public IActionResult DeleteIngredient(int ingredientId){
        // Console.WriteLine($"====== Into Delete ====");
        Ingredient? itemToDelete = _context.Ingredients.SingleOrDefault(i => i.ID == ingredientId);
        if(itemToDelete != null){
            int recipeId = itemToDelete.RecipeID;
            // Console.WriteLine($"====== Deleting ====");
            _context.Ingredients.Remove(itemToDelete);
            _context.SaveChanges();
            return Redirect($"/recipe/{recipeId}/edit");
        } 
        return Redirect($"/recipe");
    }
}

