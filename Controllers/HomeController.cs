using System.Diagnostics;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using SaladCart.Data;
using SaladCart.Models;

namespace SaladCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;     
        }

        public IActionResult Index()
        {

            List<Salads> salads = _dbContext.Salads.ToList();
            return View(salads);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

    }
}
