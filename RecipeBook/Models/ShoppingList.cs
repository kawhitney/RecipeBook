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
    // mealplan
    public int MealPlanID {get; set;}
    public MealPlan? MealPlan {get; set;}

    // product
    public List<Product> Products {get; set;} = new List<Product>();
}