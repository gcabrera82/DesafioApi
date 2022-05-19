namespace DesafioApi.Models.Dto
{
    public class ProductDto
    {

        public int IdProduct { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public int IdCategory { get; set; }
    }
}
