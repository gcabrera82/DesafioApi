using System.ComponentModel.DataAnnotations;

namespace DesafioApi.Models
{
    public class VoucherProducts
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int IdProduct { get; set; }
        public int IdVoucher { get; set; }

    }
}
