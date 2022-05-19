using System.ComponentModel.DataAnnotations;

namespace DesafioApi.Models
{
    public class CartDetail
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }

        public int Total { get; set; }
        public int Subtotal { get; set; }
        public int IdProduct { get; set; }
        public int IdCart { get; set; }



    }
}
