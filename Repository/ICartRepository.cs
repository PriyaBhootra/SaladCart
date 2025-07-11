using SaladCart.Models;
using SaladCart.Models.DTOs;

namespace SaladCart.Repository
{
    public interface ICartRepository
    {
        public int AddItem(int SaladId, int qty,string userid);
        public int RemoveItem(int SaladId,string userid);
        public ShoppingCart GetUserCart(string? userid);
        public ShoppingCart GetCart(string UserId);
        public int GetCartItemCount(string userId = "");
        public bool DoCheckout(CheckoutModel model,string userid);
    }
}
