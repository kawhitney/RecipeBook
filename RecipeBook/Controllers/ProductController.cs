using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class ProductController : Controller
{
    private MyContext _context;

    public ProductController(MyContext context)
    {
        _context = context;
    }

    //! CREATE
    //! READ
    //! UPDATE
    [HttpGet("shoppingList/{slId}/updateProducts")]
    public IActionResult UpdateProducts(int slId){
        // *** get shopping list
        //# INCLUDE: product list and mealplan
        ShoppingList? sl = _context.ShoppingLists
                            .Include(s=>s.Products)
                            .Include(s=>s.MealPlan)
                            .ThenInclude(mp => mp.Meals)
                            .FirstOrDefault(s => s.ID == slId);
        // if shopping list is not null
        if(sl != null){
            //? delete all products currently in list
            foreach(Product p in sl.Products){
                _context.Products.Remove(p);
            }
            //? Create new list of products from current mealplan
            // *** get mealplan
            //# INCLUDE: (meals-> [meal) -> {Recipe] -> Ingredients}
            MealPlan? mp = _context.MealPlans
                            .Include(i=>i.Meals)
                            .ThenInclude(m=>m.Recipe)
                            .ThenInclude(r=>r.Ingredients)
                            .FirstOrDefault(i=>i.ID == sl.MealPlanID);
            // if meal plan is not null
            if(mp != null){
                //# LOOP: each ingredient in (mp-> [meal->) recipe-> ingredient] list - add as product to list
                foreach(Meal m in mp.Meals){
                    foreach(Ingredient i in m.Recipe.Ingredients){
                        // grab product if it exists
                        Product? product = _context.Products
                                            .SingleOrDefault(p=> p.Name == i.Type);
                        // if product name does not exist
                        if(product == null){
                            // make new product
                            if(i.QuantityType == " "){
                                product = new Product(
                                        i.Type,
                                        0,
                                        sl.ID);
                            }
                            else {
                                product = new Product(
                                        i.Type,
                                        1,
                                        sl.ID);
                            }
                            product = sl.UpdateProduct(product, i.QuantityType, i.QuantityAmount);
                            _context.Products.Add(product);
                            _context.SaveChanges();
                        }
                        // otherwise if the product name does exist
                        else {
                            product = sl.UpdateProduct(product, i.QuantityType, i.QuantityAmount);
                            _context.Products.Update(product);
                            _context.SaveChanges();
                        }
                        
                    }
                }
            } 
            // redirect to shopping list
            return Redirect($"/shoppingList/{sl.ID}/view");
        }
                Console.WriteLine(new String('=', 20));
        Console.WriteLine($"ABORTING PRODUCTS");
        Console.WriteLine(new String('=', 20));
        return Redirect("/mealplan");
    }

    //! DELETE
    [HttpGet("product/{productId}/delete")]
    public IActionResult DeleteProd(int productId){
        Console.WriteLine(new String('=', 20));
        Console.WriteLine($"MADE IT TO DELETE");
        Console.WriteLine(new String('=', 20));
        Product? itemToDelete = _context.Products.SingleOrDefault(i=>i.ID == productId);
        if(itemToDelete != null){
            int shoppingList = itemToDelete.ShoppingListID;
            // delete product
            _context.Products.Remove(itemToDelete);
            _context.SaveChanges();
            // return to shopping list
            return Redirect($"/shoppingList/{shoppingList}/view");
        }
        return Redirect("/mealplan");
    }

}