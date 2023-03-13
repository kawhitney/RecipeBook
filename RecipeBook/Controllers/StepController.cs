using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class StepController : Controller
{
    private MyContext _context;

    public StepController(MyContext context)
    {
        _context = context;
    }

    //! CREATE
    // *** new step to recipe directions ***
    [HttpPost("addStep")]
    public IActionResult AddStep(Step step){
        if(ModelState.IsValid){
            _context.Directions.Add(step);
            _context.SaveChanges();
        }
        return Redirect($"recipe/{step.RecipeID}/edit");
    }

    
    //! READ

    //! UPDATE

    //! DELETE
    // *** remove ingredient from recipe ingredient list ***
    [HttpPost("{ingredientId}/deleteStep")]
    public IActionResult DeleteStep(int ingredientId){
        Step? itemToDelete = _context.Directions.SingleOrDefault(s => s.ID == ingredientId);
        if(itemToDelete != null){
            int recipeId = itemToDelete.RecipeID;
            _context.Directions.Remove(itemToDelete);
            _context.SaveChanges();
            return Redirect($"/recipe/{recipeId}/edit");
        } 
        return Redirect($"/recipe");
    }
}