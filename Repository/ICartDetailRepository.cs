using DesafioApi.Models.Dto;

namespace DesafioApi.Repository
{
    public interface ICartDetailRepository
    {

        Task<List<CartDetailDto>> GetCartDetail();
        Task<CartDetailDto> GetCartDetailById(int id);

        Task<CartDetailDto> CreateUpdate(CartDetailDto cartDetailDto);

        Task<bool> DeleteCartDetail(int id);

    }
}
