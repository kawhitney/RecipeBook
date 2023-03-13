using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class MealPlanController : Controller
{
    private MyContext _context;

    public MealPlanController(MyContext context)
    {
        _context = context;
    }
}