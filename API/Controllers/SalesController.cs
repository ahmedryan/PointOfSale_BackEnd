using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public SalesController(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _saleRepository
                .GetSales();
        
            return Ok(sales);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleDto createSaleDto)
        {
            if (createSaleDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var existingCustomerId = await _saleRepository.CustomerExists(createCustomerDto.ContactNumber);
            //
            // if (existingCustomerId != 0)
            // {
            //     return Ok(existingCustomerId);
            // }
            
            var sale = _mapper.Map<Sale>(createSaleDto);
            
            // Console.WriteLine(sale.Id);
            
            if (!await _saleRepository.CreateSale(sale))
            {
                ModelState.AddModelError(
                    "", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            // Console.WriteLine(sale.Id);
            
            // return CreatedAtRoute(
            //     "GetProduct",
            //     new {productId = product.Id},
            //     product
            // );
            return Ok(sale.Id);
        }
    }
}
