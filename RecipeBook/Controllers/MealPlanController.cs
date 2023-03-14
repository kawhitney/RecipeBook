using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class MealPlanController : Controller
{
    private MyContext _context;

    public MealPlanController(MyContext context)
    {
        _context = context;
    }

    //! CREATE
    // *** new mealplan ***
    [HttpPost("mealplan/add")]
    public IActionResult CreateMealPlan(MealPlan mp){
        
        if(ModelState.IsValid){
            _context.MealPlans.Add(mp);
            _context.SaveChanges();
            return Redirect($"{mp.ID}/edit");
        }
        return Redirect("/mealplan");
    }

    // *** new meal ***
    [HttpPost("mealplan/{mpId}/addMeal")]
    public IActionResult CreateMeal(Meal meal, int mpId){

        Console.WriteLine($"*** ModelState:\n\t {meal.MealPlanID}\n\t{meal.RecipeID}");
        if(ModelState.IsValid){
            _context.Meals.Add(meal);
            _context.SaveChanges();
        }
        return Redirect($"/mealplan/{mpId}/edit");
    }

    //! READ
    // *** view all meal plans ***
    [SessionCheck]
    [HttpGet("/mealplan")]
    public IActionResult MealPlans(){
        List<MealPlan> mealPlans = _context.MealPlans
                                    .Where(i=>i.UserID == HttpContext.Session.GetInt32("uid"))
                                    .Include(i=>i.Meals)
                                    .ThenInclude(m=>m.Recipe).ToList();
        return View(mealPlans);
    }

    // *** view favorite meal plans ***
    [SessionCheck]
    [HttpGet("/mealplan/favs")]
    public IActionResult MPFavorites(){
        List<MealPlan> mealPlans = _context.MealPlans
                                    .Where(i=>i.UserID == HttpContext.Session.GetInt32("uid"))
                                    .Where(i=>i.Favorite == true)
                                    .Include(i=>i.Meals)
                                    .ThenInclude(m=>m.Recipe).ToList();
        return View(mealPlans);
    }

    //! UPDATE
    // *** edit specific meal plan ***
    [SessionCheck]
    [HttpGet("mealplan/{mealplanId}/edit")]
    public IActionResult EditMP(int mealplanId){
        MealPlan? item = _context.MealPlans
                        .Include(i=>i.Meals)
                        .ThenInclude(m=>m.Recipe)
                        .FirstOrDefault(i=>i.ID == mealplanId);
        if(item == null){
            return RedirectToAction("MealPlans");
        }
        MyViewModel model = new MyViewModel{
            MealPlan = item,
            AllRecipes = _context.Recipes
                        .Where(i=>i.UserID == @HttpContext.Session.GetInt32("uid"))
                        .Include(r => r.Ingredients)
                        .ToList()
        };

        return View(model);
    }
    [HttpPost("mealplan/{mealPlanId}/update")]
    public IActionResult UpdateMP(MealPlan mp, int mealPlanId){
        // Console.WriteLine($"\n=====HERE=====\n");
        if(ModelState.IsValid){
            MealPlan? item = _context.MealPlans
                            .FirstOrDefault(i => i.ID == mealPlanId);
            item.Name = mp.Name;
            item.Favorite = mp.Favorite;
            item.UpdatedAt = DateTime.Now;
            _context.MealPlans.Update(item);
            _context.SaveChanges();
        }
        return Redirect("edit");
    }

    //! DELETE
    [HttpPost("mealplan/{mealPlanId}/delete")]
    public IActionResult DeleteMP(int mealPlanId){
        // delete shopping list
        // delete meal link(s)
        // delete mealplan
        return View(); //! CHANGE ME
    }
}