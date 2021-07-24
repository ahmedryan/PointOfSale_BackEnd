using System.Threading.Tasks;
using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetReport()
        {
            var report = await _reportRepository.GetReport();
        
            return Ok(report);
        }
        
    }
}
