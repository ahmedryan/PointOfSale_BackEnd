using System.Threading.Tasks;
using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProductCategories()
        {
            var categories = await _categoryRepository
                .GetCategories();
        
            return Ok(categories);
        }
        
        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> GetProduct(int categoryId)
        {
            var category = await _categoryRepository.GetCategory(categoryId);

            if (category == null)
            {
                return NotFound();
            }
            
            return Ok(category);
        }
    }
}
