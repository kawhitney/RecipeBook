using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class MealController : Controller
{
    private MyContext _context;

    public MealController(MyContext context)
    {
        _context = context;
    }
}