using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SaladCart.Data;
using SaladCart.Models;
using System.Diagnostics;

namespace SaladCart.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        /* POST: /Account/Login
        [HttpPost]
        public IActionResult Login(User user)
        {
            // Dummy user check (in production, use database)

            var UserDetails = _dbContext.Users.FirstOrDefault(u => u.UserName == user.UserName);

            if (UserDetails != null)
            {

                if (user.UserName == UserDetails.UserName && user.Password == UserDetails.Password)
                {
                    // TempData or Claims-based auth can be used here
                    user.Id = UserDetails.Id;
                    TempData["Message"] = "Login successful!";
                    HttpContext.Session.SetString("UserId", user.Id.ToString());                  

                    return RedirectToAction("Index", "Home");
                }
                //store user id in session
                HttpContext.Session.SetString("UserId", user.Id.ToString());
            }

            ViewBag.Message = "Invalid credentials";
            return View();
        }

        */


        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var normalizedEmail = email?.Trim().ToUpper();
           var user = await _userManager.FindByEmailAsync(email);

            var result = await _signInManager.PasswordSignInAsync(email.Trim(), password, false, false);

            if (result.Succeeded)
            {
                var UserDetails = _dbContext.Users.FirstOrDefault(u => u.UserName == email);
                HttpContext.Session.SetString("UserId", UserDetails.Id.ToString());

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login.");
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            using var transaction = _dbContext.Database.BeginTransaction();            
            var user = new ApplicationUser {
                Email = email,
                FullName= email,
                Address = "",
                EmailAddress= email,
                UserName = email,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };
            var result = new IdentityResult();
            if (email == "admin@saladcart.com")
            {
                 result = await _userManager.CreateAsync(user, "Admin@123");
                await _userManager.AddToRoleAsync(user, "Admin");
                password = "Admin@123";
            }
            else
            {

                 result = await _userManager.CreateAsync(user, password);
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            var cunstomuser = new User
                {
                    UserName = email,
                    Password = "",//password,
                    Email = email

                };

              
                _dbContext.Users.Add(cunstomuser);
                _dbContext.SaveChanges();
            transaction.Commit();

            if (result.Succeeded)
                {
                    HttpContext.Session.SetString("UserId", cunstomuser.Id.ToString());
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View();
            
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        


    }
}
