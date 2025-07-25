using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaladCart.Data;
using SaladCart.Models;

namespace SaladCart.Controllers
{


    public class UserOrderController : Controller
    {
       
        private readonly ApplicationDbContext _dbContext;
        public UserOrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult UserOrders()
        {
            string userid = HttpContext.Session.GetString("UserId");
           
            var orders = _dbContext.Orders.Include(x => x.OrderDetail).ThenInclude(x=> x.Salad).Where(a => a.UserId == userid).ToList();       


          
            if (orders == null)
            {
                throw new InvalidOperationException("No Records found");
            }

            

            return View(orders);
        }
    }
}
