using DesafioApi.Models.Dto;

namespace DesafioApi.Repository
{
    public interface IStoreProductsRepository
    {
        Task<List<StoreProductsDto>> GetStoreProducts();
        Task<StoreProductsDto> GetStoreProductsById(int id);

        Task<StoreProductsDto> CreateUpdate(StoreProductsDto storeProductsDto);

        Task<bool> DeleteStoreProducts(int id);
    }
}
