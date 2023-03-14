#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models;

public class Recipe{
    [Key]
    public int ID {get; set;}
    [Required]
    public string Category {get; set;}
    [Required]
    [MinLength(2)]
    public string Name {get; set;}
    [Required]
    [MinLength(2)]
    public string Yield {get; set;}
    [Range(1, int.MaxValue)]
    public int? PrepHr {get; set;}
    [Required]
    [Range(0, int.MaxValue)]
    public int PrepMin {get; set;}
    [Range(1, int.MaxValue)]
    public int? CookHr {get; set;}
    [Required]
    [Range(0, int.MaxValue)]
    public int CookMin {get; set;}
    [Required]
    public string Difficulty {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // **** RELATIONSHIPS ****
    //? user
    public int UserID {get; set;}
    User? User {get; set;}
    //? ingredients
    public List<Ingredient> Ingredients {get; set;} = new List<Ingredient>();
    //? equipment
    public List<Equipment> Equipment {get; set;} = new List<Equipment>();
    //? steps
    public List<Step> Directions {get; set;} = new List<Step>();
    //? meals
    public List<Meal> Meals {get; set;} = new List<Meal>();

    public string EstTime(){
        // calculate hours
        int hr = 0;
        if(PrepHr != null){
            hr += (int)PrepHr;
        }
        if(CookHr != null){
            hr += (int)CookHr;
        }
        // calculate minutes
        int min = PrepMin + CookMin;
        while(min >= 60){
            min -= 60;
            hr++;
        }
        if(hr == 0){
            return min + " min";
        }
        return hr + " hr " + min + " min";
    }
}