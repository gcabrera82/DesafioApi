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
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _storeProductsRepository;
        protected ResponseDto _responseDto;
        public StoresController(IStoreRepository storeProductsRepository)
        {
            _storeProductsRepository = storeProductsRepository;
            _responseDto = new ResponseDto();
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStore()
        {
            try
            {
                var list = await _storeProductsRepository.GetStore();
                _responseDto.Result = list;
                _responseDto.DisplayMessage="List Stores";
            }
            catch    (Exception ex)
            {
                _responseDto.IsSuccess=false;
                _responseDto.ErrorMessages=new List<string> {ex.ToString()};
            }
            return Ok(_responseDto);
        }


        


        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
            var store = await _storeProductsRepository.GetStoreById(id);
            if (store== null)
            {
                _responseDto.IsSuccess= false;
                _responseDto.DisplayMessage = "The store you selected does not exist";
                return NotFound(_responseDto);
            }
            _responseDto.Result=store;
            _responseDto.DisplayMessage = "Store´s information";
            return Ok(store);

          
        }

        // PUT: api/Stores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, StoreDto storeDto)
        {
           try
            {
                StoreDto store = await _storeProductsRepository.CreateUpdate(storeDto);
                _responseDto.Result = store;
                return Ok(_responseDto);
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess= false;
                _responseDto.DisplayMessage = "Error to update store";
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(StoreDto storeDto)
        {
            try
            {
                StoreDto store = await _storeProductsRepository.CreateUpdate(storeDto);
                _responseDto.Result = store;
                return CreatedAtAction("GetStore", new { id = store.IdStore }, _responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error to save store";
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
                bool isDelete = await _storeProductsRepository.DeleteStore(id);
                if (isDelete)
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.Result = isDelete;
                    _responseDto.DisplayMessage = "storeProducts delete";
                    return Ok(_responseDto);
                }else
                {
                    _responseDto.IsSuccess=false;
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
