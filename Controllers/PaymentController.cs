using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SaladCart.Models;
using Stripe.Checkout;

namespace SaladCart.Controllers
{
    public class PaymentController : Controller
    {
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }

        [HttpPost]
        public IActionResult CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 5000, // $50.00 = 5000 cents
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
        public IActionResult Cancel() => View();
    }
}
