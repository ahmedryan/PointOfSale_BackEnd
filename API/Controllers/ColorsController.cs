using System.Threading.Tasks;
using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorRepository _colorRepository;

        public ColorsController(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var colors = await _colorRepository
                .GetColors();
        
            return Ok(colors);
        }
        
        [HttpGet("{colorId:int}")]
        public async Task<IActionResult> GetColor(int colorId)
        {
            var color = await _colorRepository.GetColor(colorId);

            if (color == null)
            {
                return NotFound();
            }
            
            return Ok(color);
        }
    }
}
