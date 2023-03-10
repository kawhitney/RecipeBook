#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models;

public class MealPlan{
    [Key]
    public int ID {get; set;}
    [Required]
    public string Name {get; set;}
    public bool Favorite {get; set;} = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // **** RELATIONSHIPS ****
    //? user
    public int UserID {get; set;}
    User? User {get; set;}
    //? meal
    public List<Meal> Meals {get; set;} = new List<Meal>();
    // shopping list
    public ShoppingList? ShoppingList {get; set;}
}