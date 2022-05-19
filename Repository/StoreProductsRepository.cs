using AutoMapper;
using DesafioApi.Data;
using DesafioApi.Models;
using DesafioApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Repository
{
    public class StoreProductsRepository : IStoreProductsRepository
    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public StoreProductsRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<StoreProductsDto> CreateUpdate(StoreProductsDto storeProductsDto)
        {
            StoreProducts storeProducts = _mapper.Map<StoreProductsDto, StoreProducts>(storeProductsDto);
            if (storeProducts.Id > 0) { _db.StoreProducts.Update(storeProducts); }
            else { await _db.StoreProducts.AddAsync(storeProducts); }
            await _db.SaveChangesAsync();
            return _mapper.Map<StoreProducts, StoreProductsDto>(storeProducts);
        }

        public async Task<bool> DeleteStoreProducts(int id)
        {
            try
            {
                StoreProducts storeProducts = await _db.StoreProducts.FindAsync(id);
                if (storeProducts == null)
                {
                    return false;
                }
                _db.StoreProducts.Remove(storeProducts);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<StoreProductsDto>> GetStoreProducts()
        {
            List<StoreProducts> storeProductsList = await _db.StoreProducts.ToListAsync();
            return _mapper.Map<List<StoreProductsDto>>(storeProductsList);
        }

        public async Task<StoreProductsDto> GetStoreProductsById(int id)
        {
            StoreProducts storeProducts = await _db.StoreProducts.FindAsync(id);
            return _mapper.Map<StoreProductsDto>(storeProducts);
        }
    }
}
