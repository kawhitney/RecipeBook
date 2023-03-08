#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models;

[NotMapped] // don't send to database
public class LoginUser{
    [Required]
    [Display(Name ="User Name")]
    public string UserName {get; set;}

    [Required]
    [DataType(DataType.Password)] // auto fills input type attr
    [Display(Name = "Password")]
    public string Password { get; set; }
}