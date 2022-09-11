using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjetsController : ControllerBase
    {             

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }


    }
}
