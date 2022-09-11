using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

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
