using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SaladCart.Data;
using SaladCart.Models;
using SaladCart.Models.DTOs;
using System.Net;

namespace SaladCart.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
       

        public CartRepository(ApplicationDbContext db)
        {
            _db = db;       

        }
         
        public int AddItem(int SaladId, int qty,string userId)
        {
           
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (userId == null)
                    throw new UnauthorizedAccessException("user is not logged-in");
                var cart = GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                }
                _db.SaveChanges();
                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.SaladId == SaladId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var salad = _db.Salads.Find(SaladId);
                    cartItem = new CartDetail
                    {
                        SaladId = SaladId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice = salad.Price  // it is a new line after update
                    };
                    _db.CartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            var cartItemCount = GetCartItemCount(userId);
            return cartItemCount;
        }

        //private string GetUserId()
        // {
            //var sessionvalue = _httpContextAccessor.HttpContext.User;
            //string userId = _userManager.GetUserId(sessionvalue);     
           // return userId;
        // }

        public ShoppingCart GetCart(string userId)
        {
            var cart = _db.ShoppingCarts.FirstOrDefault(x => x.UserId == userId);
            return cart;
        }

        public int GetCartItemCount(string userId)
        {
           
            var data = (from cart in _db.ShoppingCarts
                        join cartDetail in _db.CartDetails
                        on cart.Id equals cartDetail.ShoppingCartId
                        where cart.UserId == userId
                        select new { cartDetail.Id }
                        ).ToList();
            return data.Count;
        }


        public ShoppingCart GetUserCart(string? userId)
        {


            if (userId == null)
                userId = "1";
            //throw new InvalidOperationException("Invalid userid");

            //var shoppingCart = _db.ShoppingCarts.Where(a => a.UserId == userId).FirstOrDefault();


            var shoppingCart = _db.ShoppingCarts
                                .Include(a => a.CartDetails)
                                    .ThenInclude(cd => cd.Salad)
                                        .ThenInclude(s => s.Stock)
                                .Where(a => a.UserId == userId)
                                .FirstOrDefault();


            return shoppingCart;


        }

        int ICartRepository.RemoveItem(int SaladId, string userId)
        {
            
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new UnauthorizedAccessException("user is not logged-in");
                var cart = GetCart(userId);
                if (cart is null)
                    throw new InvalidOperationException("Invalid cart");
                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.SaladId == SaladId);
                if (cartItem is null)
                    throw new InvalidOperationException("Not items in cart");
                else if (cartItem.Quantity == 1)
                    _db.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            var cartItemCount = GetCartItemCount(userId);
            return cartItemCount;
        }

        ShoppingCart ICartRepository.GetCart(string UserId)
        {
            throw new NotImplementedException();
        }


        public bool DoCheckout(CheckoutModel model,string userId)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                // logic
                // move data from cartDetail to order and order detail then we will remove cart detail
               int orederstatus = 0;
                if (string.IsNullOrEmpty(userId))                   
                    throw new UnauthorizedAccessException("User is not logged-in");


                var shoppingcart =  GetCart(userId);


                if (shoppingcart is null)
                    throw new InvalidOperationException("Invalid cart");


                var cartDetail = _db.CartDetails
                                    .Where(a => a.ShoppingCartId == shoppingcart.Id).ToList();

                if (cartDetail.Count == 0)
                    throw new InvalidOperationException("Cart is empty");

                //adding payment logic 
                if (model.PaymentMethod is not null)
                {
                    Ipayment ipayment = PaymentFactory.getPaymentMethod(model.PaymentMethod.ToString());
                    double amount = cartDetail.Sum(item => item.Salad.Price * item.Quantity);
                    ipayment.Pay(amount);
                    orederstatus = 1;

                }

                //var pendingRecord = _db.orderStatuses.FirstOrDefault(s => s.StatusName == "Pending");
                //if (pendingRecord is null)
                //throw new InvalidOperationException("Order status does not have Pending status");


                var order = new Order
                {
                    UserId = userId,
                    CreateDate = DateTime.UtcNow,
                    Name = model.Name,
                    Email = model.Email,
                    MobileNumber = model.MobileNumber,
                    PaymentMethod = model.PaymentMethod,
                    Address = model.Address,
                    IsPaid = false,
                    OrderStatusId = orederstatus//pendingRecord.Id
                };

                _db.Orders.Add(order);
                _db.SaveChanges();

                foreach (var item in cartDetail)
                {
                    var orderDetail = new OrderDetail
                    {
                        SaladId = item.SaladId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    _db.OrderDetails.Add(orderDetail);

                    // update stock here

                    //var stock = await _db.Stocks.FirstOrDefaultAsync(a => a.BookId == item.BookId);
                    //if (stock == null)
                    //{
                        //throw new InvalidOperationException("Stock is null");
                    //}

                    //if (item.Quantity > stock.Quantity)
                    //{
                        //throw new InvalidOperationException($"Only {stock.Quantity} items(s) are available in the stock");
                   // }
                    // decrease the number of quantity from the stock table
                   //stock.Quantity -= item.Quantity;
                }
                //_db.SaveChanges();

                // removing the cartdetails
                _db.CartDetails.RemoveRange(cartDetail);
                _db.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


    }
}
