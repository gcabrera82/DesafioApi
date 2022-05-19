using AutoMapper;
using DesafioApi.Data;
using DesafioApi.Models;
using DesafioApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Repository
{
    public class VoucherProductsRepository : IVoucherProductsRepository
    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public VoucherProductsRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<VoucherProductsDto> CreateUpdate(VoucherProductsDto voucherProductsDto)
        {
            VoucherProducts voucherProducts = _mapper.Map<VoucherProductsDto, VoucherProducts>(voucherProductsDto);
            if (voucherProducts.Id > 0) { _db.VoucherProducts.Update(voucherProducts); }
            else { await _db.VoucherProducts.AddAsync(voucherProducts); }
            await _db.SaveChangesAsync();
            return _mapper.Map<VoucherProducts, VoucherProductsDto>(voucherProducts);
        }

        public async Task<bool> DeleteVoucherProducts(int id)
        {
            try
            {
                VoucherProducts voucherProducts = await _db.VoucherProducts.FindAsync(id);
                if (voucherProducts == null)
                {
                    return false;
                }
                _db.VoucherProducts.Remove(voucherProducts);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<VoucherProductsDto>> GetVoucherProducts()
        {
            List<VoucherProducts> voucherProductsList = await _db.VoucherProducts.ToListAsync();
            return _mapper.Map<List<VoucherProductsDto>>(voucherProductsList);
        }

        public async Task<VoucherProductsDto> GetVoucherProductsById(int id)
        {
            VoucherProducts voucherProducts = await _db.VoucherProducts.FindAsync(id);
            return _mapper.Map<VoucherProductsDto>(voucherProducts);
        }
    }
}
