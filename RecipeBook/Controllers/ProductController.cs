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
    //! DELETE
    [HttpPost("product/{productId}/delete")]
    public IActionResult DeleteProd(int productId){
        Product? itemToDelete = _context.Products.SingleOrDefault(i=>i.ID == productId);
        if(itemToDelete != null){
            int shoppingList = itemToDelete.ShoppingListID;
            // delete product
            _context.Products.Remove(itemToDelete);
            _context.SaveChanges();
            // return to shopping list
            return Redirect($"/shoppingList/{shoppingList}");
        }
        return Redirect("/mealplan");
    }

}