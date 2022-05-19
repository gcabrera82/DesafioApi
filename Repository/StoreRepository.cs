using AutoMapper;
using DesafioApi.Data;
using DesafioApi.Models;
using DesafioApi.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace DesafioApi.Repository
{
    public class StoreRepository : IStoreRepository

    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public StoreRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


         public async Task<StoreDto> CreateUpdate(StoreDto storeDto)
        {
            Store store = _mapper.Map<StoreDto, Store>(storeDto);
            if (store.IdStore > 0) {  _db.Store.Update(store); }
            else {await _db.Store.AddAsync(store); }
            await _db.SaveChangesAsync();
            return _mapper.Map<Store, StoreDto>(store);
        }

        public async Task<bool> DeleteStore(int id)
        {
        try
            {
                Store store = await _db.Store.FindAsync(id);
                if (store == null)
                {
                    return false;
                }
                _db.Store.Remove(store);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<StoreDto>> GetStore()
        {
            
            List<Store> storeList = await _db.Store.ToListAsync();
            return _mapper.Map<List<StoreDto>>(storeList);
            
        }

        public async Task<StoreDto> GetStoreById(int id)
        {
            Store store = await  _db.Store.FindAsync(id);
            return _mapper.Map<StoreDto>(store);
        }

        
    }
}
