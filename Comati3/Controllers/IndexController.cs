using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comati3.Controllers
{
    [Route("/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Comati Apis are working";
        }
    }
}
