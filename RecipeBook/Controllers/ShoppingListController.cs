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
    [HttpGet("/mealplan/{mpId}/shoppingList/create")]
    public IActionResult CreateShoppingList(int mpId){
        // get meal plan
        MealPlan? mealplan = _context.MealPlans
                            .Include(i=>i.Meals)
                            .ThenInclude(m=>m.Recipe)
                            .ThenInclude(r=>r.Ingredients)
                            .FirstOrDefault(i=>i.ID == mpId);
        if(mealplan != null){
            // add new shopping list 
            _context.ShoppingLists.Add(new ShoppingList(mealplan.ID));
            _context.SaveChanges();
            return Redirect($"/mealplan/{mpId}/edit");
        }
        return Redirect("/mealplan");
    }

    //! READ
    [HttpGet("/shoppingList/{slId}/view")]
    public IActionResult ViewSL(int slId){
        ShoppingList? sl = _context.ShoppingLists
                            .Include(i=>i.Products)
                            .FirstOrDefault(i=>i.ID == slId);
        if(sl == null){
            return Redirect("/mealplan");
        }     
        return View(sl);
    }

    //! UPDATE
    [HttpGet("mealplan/{mpId}/shoppingList/update")]
    //# NOTE: the action is pass in to determine which page the updateSL will redirect to (-1 is reserved to force default action)
    public IActionResult UpdateSL(int mpId){
        // get meal plan
        MealPlan? mealplan = _context.MealPlans
                            .Include(i=>i.Meals)
                            .ThenInclude(m=>m.Recipe)
                            .ThenInclude(r=>r.Ingredients)
                            .FirstOrDefault(i=>i.ID == mpId);
        int slID = 0; // meant to be used for going to slView in switch[line:91] set on [line:78]
        if(mealplan != null){
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
            slID = sl.ID;
            // create all products from recipe ingredient list
            foreach(Meal m in mealplan.Meals){
                foreach(Ingredient i in m.Recipe.Ingredients){
                    Product newProduct = new Product(i.Type, (int)Math.Ceiling(i.QuantityAmount), sl.ID);
                    _context.Products.Add(newProduct);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("ViewSL", slID);
        }
        //# For null value only
        return Redirect("/mealplan");
    }

    //! DELETE
    [HttpGet("shoppingList/{slId}/delete")]
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
        Console.WriteLine(new String('=', 20));
        Console.WriteLine($"Leaving DeleteSL");
        Console.WriteLine(new String('=', 20));
        // return to all meal plans
        return Redirect("/mealplan");
    }
}