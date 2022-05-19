using System.ComponentModel.DataAnnotations;

namespace DesafioApi.Models
{
    public class Voucher
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

    }
}
