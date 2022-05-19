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
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        protected ResponseDto _responseDto;
        public CartsController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _responseDto = new ResponseDto();
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart()
        {
            try
            {
                var list = await _cartRepository.GetCart();
                _responseDto.Result = list;
                _responseDto.DisplayMessage = "Cart Stores";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_responseDto);
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _cartRepository.GetCartById(id);
            if (cart == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "The cart you selected does not exist";
                return NotFound(_responseDto);
            }
            _responseDto.Result = cart;
            _responseDto.DisplayMessage = "Cart´s information";
            return Ok(cart);
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, CartDto cartDto)
        {
            try
            {
                CartDto cart = await _cartRepository.CreateUpdate(cartDto);
                _responseDto.Result = cart;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to update cart";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(CartDto cartDto)
        {
            try
            {
                CartDto cart = await _cartRepository.CreateUpdate(cartDto);
                _responseDto.Result = cart;
                return CreatedAtAction("GetCart", new { id = cart.Id }, _responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to save cart";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            try
            {
                bool isDelete = await _cartRepository.DeleteCart(id);
                if (isDelete)
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.Result = isDelete;
                    _responseDto.DisplayMessage = "Cart delete";
                    return Ok(_responseDto);
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.DisplayMessage = "error to delete Cart";
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
