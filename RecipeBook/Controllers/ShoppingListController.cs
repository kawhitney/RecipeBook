using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class ShoppingListController : Controller
{
    private MyContext _context;

    public ShoppingListController(MyContext context)
    {
        _context = context;
    }

    //! CREATE
    // *** create shopping list from meal plan ***
    [HttpPost("/mealPlan/{mpId}/shoppingList")]
    public IActionResult CreateShoppingList(int mpId){
        // get meal plan
        MealPlan? mealplan = _context.MealPlans
                            .Include(i=>i.Meals)
                            .ThenInclude(m=>m.Recipe)
                            .ThenInclude(r=>r.Ingredients)
                            .FirstOrDefault(i=>i.ID == mpId);
        if(mealplan == null){
            return Redirect("/mealplan");
        }
        // check if shopping list exists for mp
        ShoppingList? sl = _context.ShoppingLists
                            .Include(i=>i.Products)
                            .FirstOrDefault(i=>i.MealPlanID == mpId); 
        // if exists, delete list
        if(sl != null){
            // delete all existing products
            foreach(Product p in sl.Products){
                _context.Products.Remove(p);
            }
            // delete shopping list
            _context.ShoppingLists.Remove(sl);
            _context.SaveChanges();
        }
        // add new shopping list 
        sl = new ShoppingList(mpId);
        _context.ShoppingLists.Add(sl);
        _context.SaveChanges();
        // create all products from recipe ingredient list
        Console.WriteLine(new String('=', 20));
        foreach(Meal m in mealplan.Meals){
            foreach(Ingredient i in m.Recipe.Ingredients){
                Console.WriteLine($"{i.Type} {i.QuantityType}");
            }
        }
        Console.WriteLine(new String('=', 20));
        // direct to view shopping list
        return RedirectToAction("ShoppingList", sl.ID);
    }
    //! READ
    [HttpGet("/shoppingList/{slId}")]
    public IActionResult ShoppingList(int slId){
        ShoppingList? sl = _context.ShoppingLists
                            .Include(i=>i.Products)
                            .FirstOrDefault(i=>i.ID == slId);
        if(sl == null){
            return Redirect("/mealplan");
        }     
        return View(sl);
    }
    //! UPDATE
    //! DELETE
    [HttpPost("shoppingList/{slId}/delete")]
    public IActionResult DeleteSL(int slId){
        // find all products in shopping list
        ShoppingList? sl = _context.ShoppingLists
                            .Include(i=>i.Products)
                            .FirstOrDefault(i=>i.ID == slId); 
        // if exists, delete list
        if(sl != null){
            // delete all existing products
            foreach(Product p in sl.Products){
                _context.Products.Remove(p);
            }
            // delete shopping list
            _context.ShoppingLists.Remove(sl);
            _context.SaveChanges();
        }
        // return to all meal plans
        return Redirect("/mealplan");
    }
}