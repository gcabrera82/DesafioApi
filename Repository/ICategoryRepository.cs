using DesafioApi.Models.Dto;

namespace DesafioApi.Repository
{
    public interface ICategoryRepository
    {

        Task<List<CategoryDto>> GetCategory();
        Task<CategoryDto> GetCategoryById(int id);

        

        
    }
}
