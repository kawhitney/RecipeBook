using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class ProductController : Controller
{
    private MyContext _context;

    public ProductController(MyContext context)
    {
        _context = context;
    }
}