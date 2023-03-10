using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RecipeBook.Models;

namespace RecipeBook.Controllers;

public class UserController : Controller
{

    private MyContext _context;

    public UserController( MyContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index(){
        if(HttpContext.Session.GetInt32("uid") != null){
            return RedirectToAction("Dashboard", "Home");
        }
        else{
            return View();
        }
    }

    [HttpGet("register")]
    public IActionResult Register(){
        return View();
    }

    [HttpPost("register")]
    public IActionResult Register(User newUser){
        if(ModelState.IsValid){
            PasswordHasher<User> hash = new PasswordHasher<User>(); // create new instance of the password hasher so that we can use it on the next line
            newUser.Password = hash.HashPassword(newUser, newUser.Password);
            // let newUser.Password = hashed version of the password
            _context.Users.Add(newUser);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("uid", newUser.ID);
            HttpContext.Session.SetString("FamilyName", newUser.FamilyName);
            return RedirectToAction("Dashboard", "Home");
        }
        return View();
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUser login){
        if(ModelState.IsValid){
            User? user = _context.Users.FirstOrDefault(u => u.UserName == login.UserName);
            if(user == null){
                ModelState.AddModelError("LoginUser", "Invalid User");
            }
            else{
                PasswordHasher<LoginUser> hash = new PasswordHasher<LoginUser>();
                var result = hash.VerifyHashedPassword(login, user.Password, login.Password);
                if(result == 0){// not a match
                    ModelState.AddModelError("LoginUser", "Invalid Password");
                }
                else{
                    HttpContext.Session.SetInt32("uid", user.ID);
                    HttpContext.Session.SetString("FamilyName", user.FamilyName);
                    return RedirectToAction("Dashboard", "Home");
                }
            }
        }
        return View("Index");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}