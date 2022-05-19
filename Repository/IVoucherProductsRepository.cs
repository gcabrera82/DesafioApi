using DesafioApi.Models.Dto;

namespace DesafioApi.Repository
{
    public interface IVoucherProductsRepository
    {
        Task<List<VoucherProductsDto>> GetVoucherProducts();
        Task<VoucherProductsDto> GetVoucherProductsById(int id);

        Task<VoucherProductsDto> CreateUpdate(VoucherProductsDto voucherProductsDto);

        Task<bool> DeleteVoucherProducts(int id);

    }
}
