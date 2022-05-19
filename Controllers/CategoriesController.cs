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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        protected ResponseDto _responseDto;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _responseDto = new ResponseDto();
        }


        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            try
            {
                var list = await _categoryRepository.GetCategory();
                _responseDto.Result = list;
                _responseDto.DisplayMessage = "List of Categories";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_responseDto);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "The category you selected does not exist";
                return NotFound(_responseDto);
            }
            _responseDto.Result = category;
            _responseDto.DisplayMessage = "Category´s information";
            return Ok(category);


        }


    }
}
