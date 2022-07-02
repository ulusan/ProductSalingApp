using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductSaling.Core.DTOs;
using ProductSaling.Core.Models;
using ProductSaling.Core.UnitOfWork.Abstract;
using ProductSaling.Data.Entities;

namespace ProductSaling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, ILogger<CategoriesController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        // Can be used to override global caching on a particular endpoint at any point. 
        ////[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        ////[HttpCacheValidation(MustRevalidate = false)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories([FromQuery] RequestParams requestParams)
        {
            var categories = await _unitOfWork.Categories.GetPagedList(requestParams);
            var results = _mapper.Map<IList<CategoryDTO>>(categories);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        ////[ResponseCache(CacheProfileName = "120SecondsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategory(int id)
        {
            throw new Exception("Error message");
            var category = await _unitOfWork.Categories.Get(q => q.CategoryId == id, include: q => q.Include(x => x.Products));
            var result = _mapper.Map<CategoryDTO>(category);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateCategory)}");
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(categoryDTO);
            await _unitOfWork.Categories.Insert(category);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetCategory", new { id = category.CategoryId }, category);

        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCategory)}");
                return BadRequest(ModelState);
            }

            var category = await _unitOfWork.Categories.Get(q => q.CategoryId == id);
            if (category == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCategory)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(categoryDTO, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.Save();

            return NoContent();

        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCategory)}");
                return BadRequest();
            }

            var category = await _unitOfWork.Categories.Get(q => q.CategoryId == id);
            if (category == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCategory)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Categories.Delete(id);
            await _unitOfWork.Save();

            return NoContent();

        }
    }
}
