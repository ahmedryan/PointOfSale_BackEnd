using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeRepository _sizeRepository;

        public SizesController(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSizes()
        {
            var sizes = await _sizeRepository
                .GetSizes();
        
            return Ok(sizes);
        }
        
        [HttpGet("{sizeId:int}")]
        public async Task<IActionResult> GetSize(int sizeId)
        {
            var size = await _sizeRepository.GetSize(sizeId);

            if (size == null)
            {
                return NotFound();
            }
            
            return Ok(size);
        }
    }
}
