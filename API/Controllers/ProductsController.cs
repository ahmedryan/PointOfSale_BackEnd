using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var queryCollection = HttpContext.Request.Query;

            // Console.WriteLine(HttpContext.Request.QueryString);
            
            var productsWithCount = await _productRepository.GetProducts(queryCollection);
            var productsDto = new List<ProductToReturnDto>();

            foreach (var product in productsWithCount.Products)
            {
                productsDto.Add(_mapper.Map<ProductToReturnDto>(product));
            }

            var pageIndex = int.Parse(queryCollection["pageIndex"]);
            var pageSize = int.Parse(queryCollection["pageSize"]);
            var count = productsWithCount.Count;
            var pagination = new Pagination(pageIndex, pageSize, count, productsDto);

            return Ok(pagination);
        }
        
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Operator")]
        [HttpGet("{productId:int}",Name="GetProduct")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _productRepository.GetProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductToReturnDto>(product);

            return Ok(productDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(ModelState);
            }

            if (await _productRepository.ProductExists(productDto.Pid))
            {
                return StatusCode(404, "Product exists!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var product = _mapper.Map<Product>(productDto);

            if (!await _productRepository.CreateProduct(product))
            {
                ModelState.AddModelError(
                    "", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            // return CreatedAtRoute(
            //     "GetProduct",
            //     new {productId = product.Id},
            //     product
            // );
            return Ok();
        }

        [HttpPatch("{productId:int}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] EditProductDto productToEditDto)
        {
            if (productToEditDto == null || productId != productToEditDto.Id)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productToEditDto);

            if (!await _productRepository.UpdateProduct(product))
            {
                ModelState.AddModelError("", 
                    $"Something went wrong when updating the record {product.Pid}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (!await _productRepository.ProductExists(productId))
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct(productId);

            if (!await _productRepository.DeleteProduct(product))
            {
                ModelState.AddModelError("", 
                    $"Something went wrong when deleting the record {product.Pid}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
