using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAPI1.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
