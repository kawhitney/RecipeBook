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
            // return Redirect($"{mp.ID}/edit");
            return Redirect($"/mealplan/{mp.ID}/shoppingList/create");
        }
        return Redirect("/mealplan");
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
                        .ToList(),
            AllMeals = _context.Meals
                        .Where(i=>i.MealPlanID == mealplanId)
                        .Include(i=>i.Recipe)
                        .ToList()
        };

        // setting viewbags to use for meal weekday
        ViewBag.M = 1;
        ViewBag.T = 2;
        ViewBag.W = 3;
        ViewBag.Th = 4;
        ViewBag.F = 5;
        ViewBag.Sa = 6;
        ViewBag.Su = 7;

        return View(model);
    }
    [HttpPost("mealplan/{mealPlanId}/update")]
    public IActionResult UpdateMP(MealPlan mp, int mealPlanId){
        Console.WriteLine($"MP: {mp.Favorite} {mp.Name} (kw)");
        if(ModelState.IsValid){
            MealPlan? item = _context.MealPlans
                            .FirstOrDefault(i => i.ID == mealPlanId);
            item.Name = mp.Name;
            item.Favorite = mp.Favorite;
            item.UpdatedAt = DateTime.Now;
            _context.MealPlans.Update(item);
            _context.SaveChanges();
            return Redirect($"/mealplan/{mealPlanId}/edit");
        }
        return RedirectToAction("MealPlans");
    }

    //! DELETE
    [HttpPost("mealplan/{mealPlanId}/delete")]
    public IActionResult DeleteMP(int mealPlanId){
        MealPlan? itemToDelete = _context.MealPlans
                                .Include(i=>i.Meals)
                                .Include(i=>i.ShoppingList)
                                .SingleOrDefault(i=>i.ID == mealPlanId);
        if(itemToDelete != null){
            int? shoppingList = itemToDelete.ShoppingList.ID;
            Console.WriteLine(new String('=', 20));
            Console.WriteLine($"shoppingListID: {shoppingList}");
            Console.WriteLine(new String('=', 20));
            // delete meal link(s)
            foreach(Meal m in itemToDelete.Meals){
                _context.Meals.Remove(m);
            }
            // delete meal plan
            _context.MealPlans.Remove(itemToDelete);
            _context.SaveChanges();
            // delete shopping list
            if(shoppingList != null){
                return Redirect($"/shoppingList/{shoppingList}/delete");
            }
        }       
        return Redirect("/mealplan"); 
    }
}