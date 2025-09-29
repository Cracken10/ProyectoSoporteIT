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

        [HttpGet("delegate")]
        public IActionResult GetDelegate()
        {
            var result = LambdasDemo.DemoDelegate(_logger);
            return Ok(result);
        }

        [HttpGet("stringformat")]
        public IActionResult GetStringFormat()
        {
            var result = LambdasDemo.DemoStringFormat(_logger);
            return Ok(result);
        }

        [HttpGet("attributes")]
        public IActionResult GetAttributes()
        {//
            var result = AttributesDemo.DemoAttributes(_logger);
            return Ok(result);
        }

        [HttpGet("anonymousemployees")]
        public IActionResult GetAnonymousEmployees()
        {
            var result = AnonymousEmployeesDemo.DemoAnonymousEmployees(_logger);
            return Ok(result);
        }
        [HttpGet("json")]
        public IActionResult GetJson()
        {
            var result = JsonSingletonDemo.DemoJson(_logger);
            return Ok(result);
        }
        [HttpGet("reflection")]
        public IActionResult GetReflection()
        {
            _logger.LogInformation("Llamando a DemoReflection...");
            var result = JsonSingletonDemo.DemoReflection(_logger);
            return Ok(result);
        }
        [HttpGet("extensionmethods")]
        public IActionResult GetExtensionMethods()
        {
            var result = JsonSingletonDemo.DemostradorDeExtensiones.RunDemo(_logger);
            return Ok(result);
        }
        [HttpGet("NullableT")]
        public IActionResult GetNullableT()
        {
            var result = JsonSingletonDemo.DemostradorDeExtensiones.NullableTypeD.NullTDemo(_logger);
            return Ok(result);
        }
    }
}