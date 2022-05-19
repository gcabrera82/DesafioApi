using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioApi.Data;
using DesafioApi.Models;
using DesafioApi.Models.Dto;
using DesafioApi.Repository;

namespace DesafioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherProductsController : ControllerBase
    {
        private readonly IVoucherProductsRepository _voucherProductsRepository;
        protected ResponseDto _responseDto;
        public VoucherProductsController(IVoucherProductsRepository voucherProductsRepository)
        {
            _voucherProductsRepository = voucherProductsRepository;
            _responseDto = new ResponseDto();
        }

        // GET: api/VoucherProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherProducts>>> GetVoucherProducts()
        {
            try
            {
                var list = await _voucherProductsRepository.GetVoucherProducts();
                _responseDto.IsSuccess = true;
                _responseDto.Result = list;
                _responseDto.DisplayMessage = "List Voucher Products";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_responseDto);
        }

        // GET: api/VoucherProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherProducts>> GetVoucherProducts(int id)
        {
            var voucherProducts = await _voucherProductsRepository.GetVoucherProductsById(id);
            if (voucherProducts == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "The voucher Products that you selected does not exist";
                return NotFound(_responseDto);
            }
            _responseDto.Result = voucherProducts;
            _responseDto.DisplayMessage = "Voucher Products´s information";
            return Ok(voucherProducts);
        }

        // PUT: api/VoucherProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoucherProducts(int id, VoucherProductsDto voucherProductsDto)
        {
            try
            {
                VoucherProductsDto voucherProducts = await _voucherProductsRepository.CreateUpdate(voucherProductsDto);
                _responseDto.Result = voucherProducts;
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

        // POST: api/VoucherProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VoucherProducts>> PostVoucherProducts(VoucherProductsDto voucherProductsDto)
        {
            try
            {
                VoucherProductsDto voucherProducts = await _voucherProductsRepository.CreateUpdate(voucherProductsDto);
                _responseDto.Result = voucherProducts;
                return CreatedAtAction("GetVoucherProducts", new { id = voucherProducts.Id }, _responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to save voucher Products";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // DELETE: api/VoucherProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucherProducts(int id)
        {
            try
            {
                bool isDelete = await _voucherProductsRepository.DeleteVoucherProducts(id);
                if (isDelete)
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.Result = isDelete;
                    _responseDto.DisplayMessage = "voucher Products delete";
                    return Ok(_responseDto);
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.DisplayMessage = "error to delete voucher Products";
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
