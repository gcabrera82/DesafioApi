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
    public class CartDetailsController : ControllerBase
    {
        private readonly ICartDetailRepository _cartDetailRepository;
        protected ResponseDto _responseDto;
        public CartDetailsController(ICartDetailRepository cartDetailRepository)
        {
            _cartDetailRepository = cartDetailRepository;
            _responseDto = new ResponseDto();
        }

        // GET: api/CartDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDetail>>> GetCartDetail()
        {
            try
            {
                var list = await _cartDetailRepository.GetCartDetail();
                _responseDto.IsSuccess = true;
                _responseDto.Result = list;
                _responseDto.DisplayMessage = "List CartDetail";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_responseDto);
        }

        // GET: api/CartDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDetail>> GetCartDetail(int id)
        {
            var cartDetail = await _cartDetailRepository.GetCartDetailById(id);
            if (cartDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "The Cart Detail that you selected does not exist";
                return NotFound(_responseDto);
            }
            _responseDto.Result = cartDetail;
            _responseDto.DisplayMessage = "CartDetail´s information";
            return Ok(cartDetail);

        }

        // PUT: api/CartDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartDetail(int id, CartDetailDto cartDetailDto)
        {
            try
            {
                CartDetailDto cartDetail = await _cartDetailRepository.CreateUpdate(cartDetailDto);
                _responseDto.Result = cartDetail;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to update Cart Detail";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // POST: api/CartDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartDetail>> PostCartDetail(CartDetailDto cartDetailDto)
        {
            try
            {
                CartDetailDto cartDetail = await _cartDetailRepository.CreateUpdate(cartDetailDto);
                _responseDto.Result = cartDetail;
                return CreatedAtAction("GetCartDetail", new { id = cartDetail.Id }, _responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to save voucher";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // DELETE: api/CartDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartDetail(int id)
        {
            try
            {
                bool isDelete = await _cartDetailRepository.DeleteCartDetail(id);
                if (isDelete)
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.Result = isDelete;
                    _responseDto.DisplayMessage = "CartDetail delete";
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
