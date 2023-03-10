#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models;

public class User {
    [Key]
    public int ID {get; set;}
    [Required]
    [MinLength(2)]
    public string FamilyName {get; set;}
    [Required]
    [MinLength(5)]
    [UniqueUserName]
    public string UserName {get; set;}
    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email {get; set;}
    [Required]
    [DataType(DataType.PhoneNumber)]
    public long PhoneNumber {get; set;}
    [Required]
    [DataType(DataType.Password)]
    public string Password {get; set;}
    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage="Passwords don't match, try again.")]
    public string ConfirmPW {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // **** RELATIONSHIPS ****
    //? Recipe
    public List<Recipe> UserRecipes {get; set;} = new List<Recipe>();
    //? MealPlan
    public List<MealPlan> UserMealPlans {get; set;} = new List<MealPlan>();
}

public class UniqueEmailAttribute : ValidationAttribute {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null){
            return new ValidationResult("Email is required");
        }
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        if(_context.Users.Any(e => e.Email == value.ToString())){
            return new ValidationResult("Email is already in use.");
        }
        // return base.IsValid(value, validationContext);
        return ValidationResult.Success;
    }
}

public class UniqueUserNameAttribute : ValidationAttribute {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null){
            return new ValidationResult("Email is required");
        }
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        if(_context.Users.Any(e => e.UserName == value.ToString())){
            return new ValidationResult("UserName not avaliable.");
        }
        // return base.IsValid(value, validationContext);
        return ValidationResult.Success;
    }
}