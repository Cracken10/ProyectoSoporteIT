using System;
using System.Collections.Generic; // **Importación necesaria para usar List<T>**
using Microsoft.Extensions.Logging;

namespace CapacitacionWebApi
{
    public static class LambdasDemo
    {
        // Lambdas
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
        {
            var messages = new List<string>();

            Action<int, string, bool, double> accion4 = (i, s, b, d) =>
            {
                var message = $"Action4: {i}, {s}, {b}, {d}";
                logger.LogInformation(message);
                messages.Add(message);
            };

            Action<int, string, bool> accion3 = (i, s, b) =>
            {
                var message = $"Action3: {i}, {s}, {b}";
                logger.LogInformation(message);
                messages.Add(message);
            };

            Action<int, string> accion2 = (i, s) =>
            {
                var message = $"Action2: {i}, {s}";
                logger.LogInformation(message);
                messages.Add(message);
            };

            Action<int> accion1 = i =>
            {
                var message = $"Action1: {i}";
                logger.LogInformation(message);
                messages.Add(message);
            };

            accion4(1, "test", true, 2.0);
            accion3(1, "test", true);
            accion2(1, "test");

            void MetodoAccion(Action<int> param)
            {
                param(5);
                messages.Add("MetodoAccion ejecutado.");
            }

            MetodoAccion(accion1);

            return new
            {
                Message = "Action ejecutada",
                LogMessages = messages
            };
        }
    }
}
