using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            return null;
        }


    }
}
