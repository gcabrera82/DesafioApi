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
    public class StoreProductsController : ControllerBase
    {
        private readonly IStoreProductsRepository _storeProductsRepository;
        protected ResponseDto _responseDto;
        public StoreProductsController(IStoreProductsRepository storeProductsRepository)
        {
            _storeProductsRepository = storeProductsRepository;
            _responseDto = new ResponseDto();
        }


        // GET: api/StoresProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreProducts>>> GetStoreProducts()
        {
            try
            {
                var list = await _storeProductsRepository.GetStoreProducts();
                _responseDto.Result = list;
                _responseDto.DisplayMessage = "List  Products of the Stores";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_responseDto);
        }



        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreProducts>> GetStoreProducts(int id)
        {
            var storeProducts = await _storeProductsRepository.GetStoreProductsById(id);
            if (storeProducts == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "The Store Products you selected does not exist";
                return NotFound(_responseDto);
            }
            _responseDto.Result = storeProducts;
            _responseDto.DisplayMessage = "StoreProducts´s information";
            return Ok(storeProducts);


        }





        // PUT: api/Stores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, StoreProductsDto storeProductsDto)
        {
            try
            {
                StoreProductsDto storeProducts = await _storeProductsRepository.CreateUpdate(storeProductsDto);
                _responseDto.Result = storeProducts;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to update store";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }


        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoreProducts>> PostStoreProducts(StoreProductsDto storeProductsDto)
        {
            try
            {
                StoreProductsDto storeProducts = await _storeProductsRepository.CreateUpdate(storeProductsDto);
                _responseDto.Result = storeProducts;
                return CreatedAtAction("GetStoreProducts", new { id = storeProducts.Id }, _responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to save storeProducts";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }

        }


        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreProducts(int id)
        {
            try
            {
                bool isDelete = await _storeProductsRepository.DeleteStoreProducts(id);
                if (isDelete)
                {
                    _responseDto.Result = isDelete;
                    _responseDto.DisplayMessage = "store delete";
                    return Ok(_responseDto);
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.DisplayMessage = "error to delete store";
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
