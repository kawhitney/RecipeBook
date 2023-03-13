using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class EquipmentController : Controller
{
    private MyContext _context;

    public EquipmentController(MyContext context)
    {
        _context = context;
    }

    //! CREATE
    // *** new equipment to recipe equipment list ***
    [HttpPost("addEquipment")]
    public IActionResult AddEquipment(Equipment equipment){
        if(ModelState.IsValid){
            _context.Equipment.Add(equipment);
            _context.SaveChanges();
        }
        return Redirect($"recipe/{equipment.RecipeID}/edit");
    }

    //! READ

    //! UPDATE

    //! DELETE
    // *** remove equipment from recipe list of equipment ***
    [HttpPost("{equipmentId}/deleteEquipment")]
    public IActionResult DeleteEquipment(int equipmentId){
        Console.WriteLine($"====== Into Delete ====");
        Equipment? itemToDelete = _context.Equipment.SingleOrDefault(i => i.ID == equipmentId);
        if(itemToDelete != null){
            int recipeId = itemToDelete.RecipeID;
            // Console.WriteLine($"====== Deleting ====");
            _context.Equipment.Remove(itemToDelete);
            _context.SaveChanges();
            return Redirect($"/recipe/{recipeId}/edit");
        } 
        return Redirect($"/recipe");
    }
}