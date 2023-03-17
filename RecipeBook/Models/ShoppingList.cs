#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models;

public class ShoppingList{
    [Key]
    public int ID {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // **** RELATIONSHIPS ****
    //? mealplan
    public int MealPlanID {get; set;}
    public MealPlan? MealPlan {get; set;}

    //? product
    public List<Product> Products {get; set;} = new List<Product>();

    public ShoppingList(){}
    public ShoppingList(int mpId){
        this.MealPlanID = mpId;
    }

    //# Returns ID of product if it exists in list 
    public int? MatchingProductID(string name){
        foreach(Product p in this.Products){
            if(p.Name == name){
                return p.ID;
            }
        }
        return null;
    }

    //# pass in Existing Product and ingredient type and ingredient amount
    public Product UpdateProduct(Product product, string type, double amt){
        switch(type){
            case " ": // just add the amount
                product.Amount += (int)Math.Ceiling(amt);
                break;
            case "lb": // convert lbs to cup
                product.IncreaseCount((amt*1.917223)/0.7);
                break;
            case "c.": // pass in cup amount
                product.IncreaseCount(amt);
                break;
            case "ml":
            case "g": // convert grams to cup
                product.IncreaseCount(amt*0.00423);
                break;
            default: // TBSP, tsp, pinch - just needs to exist on shopping cart
                break;
        }
        product.UpdatedAt = DateTime.Now;
        return product;
    }
}