using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioApi.Data;
using DesafioApi.Models;
using DesafioApi.Repository;
using DesafioApi.Models.Dto;

namespace DesafioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly IVoucherRepository _voucherRepository;
        protected ResponseDto _responseDto;
        public VouchersController(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
            _responseDto = new ResponseDto();
        }

        // GET: api/Vouchers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voucher>>> GetVoucher()
        {
            try
            {
                var list = await _voucherRepository.GetVoucher();
                _responseDto.IsSuccess = true;
                _responseDto.Result = list;
                _responseDto.DisplayMessage = "List Voucher";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_responseDto);
        }

        // GET: api/Vouchers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voucher>> GetVoucher(int id)
        {
            var voucher = await _voucherRepository.GetVoucherById(id);
            if (voucher == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "The voucher that you selected does not exist";
                return NotFound(_responseDto);
            }
            _responseDto.Result = voucher;
            _responseDto.DisplayMessage = "Voucher´s information";
            return Ok(voucher);


        }

        // PUT: api/Vouchers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoucher(int id, VoucherDto voucherDto)
        {
            try
            {
                VoucherDto voucher = await _voucherRepository.CreateUpdate(voucherDto);
                _responseDto.Result = voucher;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to update Voucher";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // POST: api/Vouchers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voucher>> PostVoucher(VoucherDto voucherDto)
        {
            try
            {
                VoucherDto voucher = await _voucherRepository.CreateUpdate(voucherDto);
                _responseDto.Result = voucher;
                return CreatedAtAction("GetVoucher", new { id = voucher.Id }, _responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to save voucher";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // DELETE: api/Vouchers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            try
            {
                bool isDelete = await _voucherRepository.DeleteVoucher(id);
                if (isDelete)
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.Result = isDelete;
                    _responseDto.DisplayMessage = "voucher delete";
                    return Ok(_responseDto);
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.DisplayMessage = "error to delete voucher";
                    return BadRequest(_responseDto);
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

       
    }
}
