using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SaladCart.Models;
using SaladCart.Models.DTOs;
using SaladCart.Repository;
//using Stripe.Checkout;

namespace SaladCart.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        //private readonly StripeSettings _stripeSettings;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
           //_stripeSettings = stripeOptions.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUserCart()
        {
            string? userid =  HttpContext.Session.GetString("UserId");
            

            var cart = _cartRepository.GetUserCart(userid);
            if(cart ==null)
            { return Ok(Response.StatusCode); }
            return View(cart);
        }

        [HttpGet]
        public IActionResult AddItem(int saladid, int qty = 1, int redirect = 0)
        {
            string? id = HttpContext.Session.GetString("UserId");
            var cartCount = _cartRepository.AddItem(saladid, qty,id);
            //if (redirect == 0)
               // return Ok(cartCount);           
            return RedirectToAction("GetUserCart");
        }

        [HttpGet]
        public IActionResult RemoveItem(int saladid)
        {
            string? id = HttpContext.Session.GetString("UserId");           
            var cartCount = _cartRepository.RemoveItem(saladid, id);
            //if (redirect == 0)
            // return Ok(cartCount);           
            return RedirectToAction("GetUserCart");
        }


        [HttpGet]
        public IActionResult Checkout()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Checkout(CheckoutModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            string? userid = HttpContext.Session.GetString("UserId");

            // if (model.PaymentMethod == "Online")
            //ViewBag.PublishableKey = _stripeSettings.SecretKey;
            //CreateCheckoutSession(model);            

            bool isCheckedOut = _cartRepository.DoCheckout(model, userid);
            if (!isCheckedOut)
                return RedirectToAction(nameof(OrderFailure));            
            return RedirectToAction(nameof(OrderSuccess));
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCheckoutSession(CheckoutModel model)
        {
            /*
              var options = new SessionCreateOptions
              {
                  PaymentMethodTypes = new List<string> { "card" },
                  LineItems = new List<SessionLineItemOptions>
              {
                  new SessionLineItemOptions
                  {
                      PriceData = new SessionLineItemPriceDataOptions
                      {
                          UnitAmount = 500, // $50.00 = 5000 cents
                          Currency = "usd",
                          ProductData = new SessionLineItemPriceDataProductDataOptions
                          {
                              Name = "Sample Product"
                          }
                      },
                      Quantity = 1
                  }
              },
                  Mode = "payment",
                  SuccessUrl = "https://localhost:5001/payment/success",
                  CancelUrl = "https://localhost:5001/payment/cancel"
              };

              var service = new SessionService();
              var session = service.Create(options);

              return Json(new { id = session.Id });
          }

          public IActionResult Success() => View();
          public IActionResult Cancel() => View(); */
            return View();
        }

    }
}
