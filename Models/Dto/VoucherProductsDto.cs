namespace DesafioApi.Models.Dto
{
    public class VoucherProductsDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int IdProduct { get; set; }
        public int IdVoucher { get; set; }
    }
}
