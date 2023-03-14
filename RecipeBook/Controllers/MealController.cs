using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class MealController : Controller
{
    private MyContext _context;

    public MealController(MyContext context)
    {
        _context = context;
    }

    //! CREATE
    // *** new meal ***
    [HttpPost("meal/addMeal")]
    public IActionResult CreateMeal(Meal meal){
        // Console.WriteLine($"****** ModelState:{ModelState.IsValid}\n\t{meal.MealPlanID}\n\t{meal.RecipeID}\n====================");
        if(ModelState.IsValid){
            _context.Meals.Add(meal);
            _context.SaveChanges();
        }
        return Redirect($"/mealplan/{meal.MealPlanID}/edit");
    }
    //! READ
    //! UPDATE
    //! DELETE
    // *** delete meal by ID ***
    [HttpPost("meal/{mealId}/delete")]
    public IActionResult DeleteMeal(int mealId){
        Meal? itemToDelete = _context.Meals.SingleOrDefault(i=>i.ID == mealId);
        if(itemToDelete != null){
            int mpId = itemToDelete.MealPlanID;
            // Console.WriteLine($"====== Deleting ====");
            _context.Meals.Remove(itemToDelete);
            _context.SaveChanges();
            return Redirect($"/mealplan/{mpId}/edit");
        }
        return Redirect("/mealPlan");
    }
}