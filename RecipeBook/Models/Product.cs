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

    //! internal use only
    [NotMapped] 
    private float TimesAdded {get; set;}

    //# Methods
    public Product(){}
    public Product(string name, int amt, int slID){
        this.Name = name;
        this.Amount = amt;
        this.TimesAdded = 1;
        this.ShoppingListID = slID;
    }

    // Increase times added by integer , reset when when TimesAdded > 16.0
    public void IncreaseCount(double increase){
        if(this.TimesAdded > 16.0){
            this.TimesAdded = 0;
            this.Amount++;
        }
        else {
            this.TimesAdded+= (float)increase;
        }
    }

}