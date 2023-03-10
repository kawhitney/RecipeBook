#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models;

public class Product{
    [Key]
    public int ID {get; set;}
    [Required]
    public string Name {get; set;}
    [Required]
    public int Amount {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // **** RELATIONSHIPS ****
    // shopping list
    public int ShoppingListID {get; set;} // FK
    public ShoppingList? ShoppingList {get; set;} // nav prop
}