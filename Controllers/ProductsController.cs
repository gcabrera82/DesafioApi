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
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        protected ResponseDto _responseDto;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _responseDto = new ResponseDto();
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            try
            {
                var list = await _productRepository.GetProduct();
                _responseDto.IsSuccess = true;
                _responseDto.Result = list;
                _responseDto.DisplayMessage = "List of Products";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_responseDto);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {

            var product = await _productRepository.GetProductByName(id);
            if (product == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "The product you selected does not exist";
                return NotFound(_responseDto);
            }
            _responseDto.Result = product;
            _responseDto.DisplayMessage = "Products´s information";
            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto product)
        {
            try
            {
                ProductDto productDto = await _productRepository.CreateUpdate(product);
                _responseDto.Result = productDto;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to update products";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto product)
        {
            try
            {
                ProductDto productDto = await _productRepository.CreateUpdate(product);
                _responseDto.Result = productDto;
                return CreatedAtAction("GetProduct", new { id = productDto.IdProduct }, _responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to save product";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                bool isDelete = await _productRepository.DeleteProduct(id);
                if (isDelete)
                {
                    _responseDto.Result = isDelete;
                    _responseDto.DisplayMessage = "product delete";
                    return Ok(_responseDto);
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.DisplayMessage = "error to delete product";
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
