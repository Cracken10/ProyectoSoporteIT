using System;
using Microsoft.Extensions.Logging;

namespace CapacitacionWebApi
{
    //Prueba 
    public static class LambdasDemo
    {
        public static object DemoLambdas(ILogger logger)
        {
            Action voidLambda = () => logger.LogInformation("Void lambda ejecutada");
            voidLambda();
            Func<dynamic> returnLambda = () => new { Prop = "Valor" };
            var resultado = returnLambda();
            logger.LogInformation($"Lambda retorno: {resultado.Prop}");
            return new { Message = "Lambdas ejecutadas", Prop = resultado.Prop };
        }

    }
}