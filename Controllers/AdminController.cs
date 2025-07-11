using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaladCart.Data;
using SaladCart.Models;

namespace SaladCart.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public AdminController( ApplicationDbContext dbContext)
        {
           
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Salads> salads = _dbContext.Salads.ToList();
            return View(salads);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.SaladTypes = new SelectList(new List<string>
            {
            "Wholesome Indian Salads",
            "Fusion & Continental Salads",
            "Seasonal Fruit Fusions"            
             });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Salads salad, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    salad.Image = "/images/" + fileName;
                }

                _dbContext.Add(salad);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(salad);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var salad = _dbContext.Salads.Find(id);
            if (salad == null)
            {
                return NotFound();
            }

            // Populate Salad Types for dropdown
            ViewBag.SaladTypes = new SelectList(new List<string>
            {
                "Wholesome Indian Salads",
                 "Fusion & Continental Salads",
                 "Seasonal Fruit Fusions"
            });

            return View(salad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Salads salad, IFormFile ImageFile)
        {
            if (id != salad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(ImageFile.FileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        // Update image path
                        salad.Image = "/images/" + fileName;
                    }

                    _dbContext.Update(salad);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContext.Salads.Any(e => e.Id == salad.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdown on failure
            ViewBag.SaladTypes = new SelectList(new List<string>
            {
                "Wholesome Indian Salads",
                "Fusion & Continental Salads",
                "Seasonal Fruit Fusions"
            });

            return View(salad);
        }


        [HttpPost]        
        public async Task<IActionResult> Delete(int id)
        {
            var salad = await _dbContext.Salads.FindAsync(id);
            if (salad != null)
            {
                _dbContext.Salads.Remove(salad);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index)); // Redirect to the index or another view
        }

        [HttpGet]
        public IActionResult OrderDetails(int? month)
        {
            ViewBag.SelectedMonth = month;
            var orders = _dbContext.Orders.Include(x => x.OrderDetail).ThenInclude(x => x.Salad).ToList();

            if (orders == null)
            {
                throw new InvalidOperationException("No Records found");
            }

            //search by month 
            if (month.HasValue)
            {
                orders = orders.Where(o => o.CreateDate.Month == month.Value).ToList();
            }

            return View(orders);
        }
    }
}
