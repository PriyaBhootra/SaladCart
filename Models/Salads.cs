using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SaladCart.Models
{
    [Table("Salads")]
    public class Salads
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(40)]
        public string? Type { get; set; }
        [Required]
       
        public string? Description { get; set; }

        public double Price { get; set; }
        public string? Image { get; set; }
       
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        public Stock Stock { get; set; }
        public int Quantity { get; set; }
    }
}
