#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models;

public class Ingredient{
    [Key]
    public int ID {get; set;}
    [Required]
    public string Type {get; set;}
    [Required]
    public string Style { get; set;}
    [Required]
    public float QuantityAmount {get; set;}
    [Required]
    public string QuantityType {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // **** RELATIONSHIPS ****
    // with Recipe
    public int RecipeID {get; set;}
    public Recipe? Recipe {get; set;}
}