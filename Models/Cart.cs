using System.ComponentModel.DataAnnotations;

namespace DesafioApi.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
    }
}
