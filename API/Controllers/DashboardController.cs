using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public IActionResult Teste()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return Ok(assembly.GetName().Name);
        }
    }
}
