using AutoMapper;
using DesafioApi.Data;
using DesafioApi.Models;
using DesafioApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<CartDto> CreateUpdate(CartDto cartDto)
        {
            Cart cart = _mapper.Map<CartDto, Cart>(cartDto);
            if (cart.Id > 0) { _db.Cart.Update(cart); }
            else { await _db.Cart.AddAsync(cart); }
            await _db.SaveChangesAsync();
            return _mapper.Map<Cart, CartDto>(cart);
        }

        public async Task<bool> DeleteCart(int id)
        {
            try
            {
                Cart cart = await _db.Cart.FindAsync(id);
                if (cart == null)
                {
                    return false;
                }
                _db.Cart.Remove(cart);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<CartDto>> GetCart()
        {
            List<Cart> cartList = await _db.Cart.ToListAsync();
            return _mapper.Map<List<CartDto>>(cartList);
        }

        public async Task<CartDto> GetCartById(int id)
        {
            Cart cart = await _db.Cart.FindAsync(id);
            return _mapper.Map<CartDto>(cart);
        }
    }
}
