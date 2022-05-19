using AutoMapper;
using DesafioApi.Data;
using DesafioApi.Models;
using DesafioApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Repository
{
    public class CartDetailRepository : ICartDetailRepository
    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public CartDetailRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CartDetailDto> CreateUpdate(CartDetailDto cartDetailDto)
        {
            CartDetail cartDetail = _mapper.Map<CartDetailDto, CartDetail>(cartDetailDto);
            if (cartDetail.Id > 0) { _db.CartDetail.Update(cartDetail); }
            else { await _db.CartDetail.AddAsync(cartDetail); }
            await _db.SaveChangesAsync();
            return _mapper.Map<CartDetail, CartDetailDto>(cartDetail);
        }

        public async Task<bool> DeleteCartDetail(int id)
        {
            try
            {
                CartDetail cartDetail = await _db.CartDetail.FindAsync(id);
                if (cartDetail == null)
                {
                    return false;
                }
                _db.CartDetail.Remove(cartDetail);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<CartDetailDto>> GetCartDetail()
        {
            List<CartDetail> cartDetailList = await _db.CartDetail.ToListAsync();
            return _mapper.Map<List<CartDetailDto>>(cartDetailList);
        }

        public async Task<CartDetailDto> GetCartDetailById(int id)
        {
            CartDetail cartDetail = await _db.CartDetail.FindAsync(id);
            return _mapper.Map<CartDetailDto>(cartDetail);
        }
    }
}
