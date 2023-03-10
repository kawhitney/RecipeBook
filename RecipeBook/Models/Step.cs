#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models;

public class Step{
    [Key]
    public int ID {get; set;}
    [Required]
    public int Number {get; set;}
    [Required]
    public string Direction {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // **** RELATIONSHIP ****
    // with Recipe
    public int RecipeID {get; set;} // FK
    public Recipe? Recipe {get; set;} // Nav prop
}