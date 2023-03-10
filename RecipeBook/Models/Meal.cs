#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models;

public class Meal{
    [Key]
    public int ID {get; set;}

    // **** RELATIONSHIPS ****
    // recipe
    public int RecipeID {get; set;} // FK
    public Recipe? Recipe {get; set;} // nav prop
    // meal plan
    public int MealPlanID {get; set;} // FK
    [Required]
    [Range(1, 7)]
    public int Weekday {get; set;} // to set what weekday the meal will take place on, used for mealplan
    public MealPlan? MealPlan {get; set;}
}