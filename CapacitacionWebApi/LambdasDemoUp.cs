using System;
using Microsoft.Extensions.Logging;

namespace CapacitacionWebApi
{
    public static class LambdasDemo
    {//Lambdas
        public static object DemoLambdas(ILogger logger)
        {
            Action voidLambda = () => logger.LogInformation("Void lambda ejecutada");
            voidLambda();
            Func<dynamic> returnLambda = () => new { Prop = "Valor" };
            var resultado = returnLambda();
            logger.LogInformation($"Lambda retorno: {resultado.Prop}");
            return new { Message = "Lambdas ejecutadas", Prop = resultado.Prop };
        }
        public static object DemoAction(ILogger logger)
            //Action
        {
            Action<int, string, bool, double> accion4 = (i, s, b, d) => logger.LogInformation($"Action4: {i}, {s}, {b}, {d}");
            Action<int, string, bool> accion3 = (i, s, b) => logger.LogInformation($"Action3: {i}, {s}, {b}");
            Action<int, string> accion2 = (i, s) => logger.LogInformation($"Action2: {i}, {s}");
            Action<int> accion1 = i => logger.LogInformation($"Action1: {i}");
            accion4(1, "test", true, 2.0);
            accion3(1, "test", true);
            accion2(1, "test");
            accion1(1);
            void MetodoAccion(Action<int> param) { param(5); }
            MetodoAccion(accion1);
            return new { Message = "Action ejecutada" };
        }
    }
}