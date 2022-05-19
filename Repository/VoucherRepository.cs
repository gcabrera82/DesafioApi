using AutoMapper;
using DesafioApi.Data;
using DesafioApi.Models;
using DesafioApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Repository
{
    public class VoucherRepository : IVoucherRepository

    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public VoucherRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<VoucherDto> CreateUpdate(VoucherDto voucherDto)
        {
            Voucher voucher = _mapper.Map<VoucherDto, Voucher>(voucherDto);
            if (voucher.Id > 0) { _db.Voucher.Update(voucher); }
            else { await _db.Voucher.AddAsync(voucher); }
            await _db.SaveChangesAsync();
            return _mapper.Map<Voucher, VoucherDto>(voucher);
        }

        public async Task<bool> DeleteVoucher(int id)
        {
            try
            {
                Voucher voucher = await _db.Voucher.FindAsync(id);
                if (voucher == null)
                {
                    return false;
                }
                _db.Voucher.Remove(voucher);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<VoucherDto>> GetVoucher()
        {
            List<Voucher> voucherList = await _db.Voucher.ToListAsync();
            return _mapper.Map<List<VoucherDto>>(voucherList);
        }

        public async Task<VoucherDto> GetVoucherById(int id)
        {
            Voucher voucher = await _db.Voucher.FindAsync(id);
            return _mapper.Map<VoucherDto>(voucher);
        }
    }
}
