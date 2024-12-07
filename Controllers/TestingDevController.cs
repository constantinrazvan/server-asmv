using Microsoft.AspNetCore.Mvc;

namespace ServerAsmv.Controllers
{
    [ApiController]
    [Route("api/welcome")]
    public class TestingDevController : Controller
    { 
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("The app is working!");
        }
    }
}
