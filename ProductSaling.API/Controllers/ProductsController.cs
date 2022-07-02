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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        public ProductsController(IUnitOfWork unitOfWork, ILogger<ProductsController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts([FromQuery] RequestParams requestParams)
        {
            var products = await _unitOfWork.Products.GetPagedList(requestParams);
            var results = _mapper.Map<IList<ProductDTO>>(products);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _unitOfWork.Products.Get(q => q.ProductId == id, include: q => q.Include(x => x.Category));
            var result = _mapper.Map<ProductDTO>(product);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateProduct)}");
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productDTO);
            await _unitOfWork.Products.Insert(product);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO productDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProduct)}");
                return BadRequest(ModelState);
            }

            var product = await _unitOfWork.Products.Get(q => q.ProductId == id);
            if (product == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProduct)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(productDTO, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();

            return NoContent();

        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProduct)}");
                return BadRequest();
            }

            var product = await _unitOfWork.Products.Get(q => q.ProductId == id);
            if (product == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProduct)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Products.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }

}
