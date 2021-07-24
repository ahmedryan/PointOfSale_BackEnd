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
    public class FitsController : ControllerBase
    {
        private readonly IFitRepository _fitRepository;

        public FitsController(IFitRepository fitRepository)
        {
            _fitRepository = fitRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetFits()
        {
            var productFits = await _fitRepository
                .GetFits();
        
            return Ok(productFits);
        }
        
        [HttpGet("{fitId:int}")]
        public async Task<IActionResult> GetFit(int fitId)
        {
            var fit = await _fitRepository.GetFit(fitId);

            if (fit == null)
            {
                return NotFound();
            }
            
            return Ok(fit);
        }
    }
}
