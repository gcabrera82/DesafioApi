using DesafioApi.Models.Dto;

namespace DesafioApi.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetProduct();
        Task<ProductDto> GetProductByName(int id);

        Task<ProductDto> CreateUpdate(ProductDto productDto);

        Task<bool> DeleteProduct(int id);


    }
}
