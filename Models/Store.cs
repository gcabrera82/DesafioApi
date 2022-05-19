using System.ComponentModel.DataAnnotations;

namespace DesafioApi.Models
{
    public class Store
    {
        [Key]
        public int IdStore { get; set; }

        public string Name { get; set; }

        public bool Avaiable { get; set; } = true;


    }
}
