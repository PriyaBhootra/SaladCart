using System.ComponentModel.DataAnnotations.Schema;

namespace SaladCart.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }
        public int SaladId { get; set; }
        public int Quantity { get; set; }
        public Salads? Salad { get; set; }
    }
}
