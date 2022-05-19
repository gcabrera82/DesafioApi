using System.ComponentModel.DataAnnotations;

namespace DesafioApi.Models
{
    public class StoreProducts
    {
        [Key]
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProduct { get; set; }
        public int IdStore { get; set; }

    }
}
