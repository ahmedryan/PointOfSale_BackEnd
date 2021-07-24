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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository
                .GetCustomers();
        
            return Ok(customers);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            if (createCustomerDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCustomerId = await _customerRepository.CustomerExists(createCustomerDto.ContactNumber);

            if (existingCustomerId != 0)
            {
                return Ok(existingCustomerId);
            }
            
            var customer = _mapper.Map<Customer>(createCustomerDto);
            
            // Console.WriteLine(customer.Id);
            
            if (!await _customerRepository.CreateCustomer(customer))
            {
                ModelState.AddModelError(
                    "", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            // Console.WriteLine(customer.Id);
            
            // return CreatedAtRoute(
            //     "GetProduct",
            //     new {productId = product.Id},
            //     product
            // );
            return Ok(customer.Id);
        }
    }
}
