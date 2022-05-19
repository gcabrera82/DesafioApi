using AutoMapper;
using DesafioApi.Data;
using DesafioApi.Models;
using DesafioApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdate(ProductDto productoDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productoDto);
            if (product.IdProduct > 0) { _db.Product.Update(product); }
            else { await _db.Product.AddAsync(product); }
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                Product product = await _db.Product.FindAsync(id);
                if (product == null)
                {
                    return false;
                }
                _db.Product.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<ProductDto>> GetProduct()
        {
            List<Product> productList = await _db.Product.ToListAsync();
            return _mapper.Map<List<ProductDto>>(productList);
        }

        public async Task<ProductDto> GetProductByName(int id)
        {
            Product product = await _db.Product.FindAsync(id);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
