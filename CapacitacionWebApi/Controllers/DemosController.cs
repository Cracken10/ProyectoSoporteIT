using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CapacitacionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemosController : ControllerBase
    {
        private readonly ILogger<DemosController> _logger;

        public DemosController(ILogger<DemosController> logger)
        {
            _logger = logger;
        }

        [HttpGet("lambdas")]
        public IActionResult GetLambdas()
        {
            var result = LambdasDemo.DemoLambdas(_logger);
            return Ok(result);
        }

        [HttpGet("action")]
        public IActionResult GetAction()
        {
            var result = LambdasDemo.DemoAction(_logger);
            return Ok(result);
        }

        [HttpGet("func")]
        public IActionResult GetFunc()
        {
            var result = LambdasDemo.DemoFunc(_logger);
            return Ok(result);
        }

   //     [HttpGet("delegate")]
    //    public IActionResult GetDelegate()
     //   {
   //         var result = LambdasDemo.DemoDelegate(_logger);
  //         return Ok(result);
    //    }
    }
}