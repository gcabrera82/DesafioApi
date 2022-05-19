using DesafioApi.Models.Dto;

namespace DesafioApi.Repository
{
    public interface ICartRepository
    {
        Task<List<CartDto>> GetCart();
        Task<CartDto> GetCartById(int id);

        Task<CartDto> CreateUpdate(CartDto cartlDto);

        Task<bool> DeleteCart(int id);
    }
}
