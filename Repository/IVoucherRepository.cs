using DesafioApi.Models.Dto;

namespace DesafioApi.Repository
{
    public interface IVoucherRepository
    {
        Task<List<VoucherDto>> GetVoucher();
        Task<VoucherDto> GetVoucherById(int id);

        Task<VoucherDto> CreateUpdate(VoucherDto voucherDto);

        Task<bool> DeleteVoucher(int id);
    }
}
