using AutoMapper;
using DesafioApi.Data;
using DesafioApi.Models;
using DesafioApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Repository
{
    public class CategoryRepository :ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetCategory()
        {
            List<Category> categoryList = await _db.Category.ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categoryList);
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            Category category = await _db.Category.FindAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
