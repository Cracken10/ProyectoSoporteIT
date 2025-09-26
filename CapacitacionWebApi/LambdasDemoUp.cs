using System;
using System.Collections.Generic; // Importación necesaria para usar List<T>
using Microsoft.Extensions.Logging;

namespace CapacitacionWebApi
{
    public static class LambdasDemo
    {
        // Lambdas
        public static object DemoLambdas(ILogger logger)
        {
            logger.LogInformation("Inicio de DemoLambdas.");
            var messages = new List<string>();

            // Lambda que no devuelve un valor 
            Action voidLambda = () =>
            {
                var msg = "Void lambda ejecutada";
                logger.LogInformation(msg);
                messages.Add(msg);
            };
            voidLambda();

            // Lambda que devuelve un valor 
            Func<dynamic> returnLambda = () =>
            {
                var result = new { Prop = "Valor" };
                var msg = $"Lambda retorno: {result.Prop}";
                logger.LogInformation(msg);
                messages.Add(msg);
                return result;
            };
            var resultado = returnLambda();

            logger.LogInformation("Fin de DemoLambdas.");

            return new
            {
                Message = "Lambdas ejecutadas",
                Prop = resultado.Prop,
                LogMessages = messages
            };
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


        public static object DemoFunc(ILogger logger)
        {
            Func<int, string, bool, double, string> func4 = (i, s, b, d) => $"Func4: {i}, {s}, {b}, {d}";
            Func<int, string, bool, int> func3 = (i, s, b) => i;
            Func<int, string, string> func2 = (i, s) => s;
            Func<int, string> func1 = i => $"Func1: {i}";

            var resFunc4 = func4(1, "test", true, 2.0);
            logger.LogInformation(resFunc4);

            var resFunc3 = func3(1, "test", true);
            logger.LogInformation($"Resultado Func3: {resFunc3}"); 
            var resFunc2 = func2(1, "test");
            logger.LogInformation(resFunc2);

            var resFunc1 = func1(1);
            logger.LogInformation(resFunc1);

            // Método que recibe una Func y la ejecuta
            string MetodoFunc(Func<int, string> param)
            {
                var res = param(5);
                logger.LogInformation($"MetodoFunc ejecutado con resultado: {res}");
                return res;
            }

            var resMetodoFunc = MetodoFunc(func1);

            return new
            {
                Message = "Func ejecutada",
                Results = new
                {
                    Func4 = resFunc4,
                    Func3 = resFunc3,
                    Func2 = resFunc2,
                    Func1 = resFunc1,
                    MetodoFuncResult = resMetodoFunc
                }
            };
        }

        public delegate void MiDelegate(string mensaje);

        public static class DelegadosDemo
        {
            public static object DemoDelegate(ILogger logger)
            {
                var messages = new List<string>();

                // 1. Asignar un lambda directamente al delegado personalizado
                MiDelegate del = (m) =>
                {
                    var msg = $"Delegate (lambda): {m}";
                    logger.LogInformation(msg);
                    messages.Add(msg);
                };

                // 2. Crear una instancia de Action con la misma firma
                Action<string> accion = (m) =>
                {
                    var msg = $"Action en delegate: {m}";
                    logger.LogInformation(msg);
                    messages.Add(msg);
                };

                // 3. Crear una instancia de Func y convertirla a Action
                Func<string, string> func = (m) =>
                {
                    return $"Func (convertido): {m}";
                };

                // Se crea un Action que encapsula la llamada al Func
                Action<string> accionDesdeFunc = (m) =>
                {
                    var msg = func(m);
                    logger.LogInformation(msg);
                    messages.Add(msg);
                };

                // Mezcla.
                del += accion.Invoke; // Añadir la Action
                del += accionDesdeFunc.Invoke; // Añadir la Action que encapsula el Func

                // Invocar el delegado, que llamará a los tres métodos en orden
                del("Resultado");

                return new
                {
                    Message = "Delegate, Func y Action ejecutados",
                    LogMessages = messages
                };
            }
        }
    }

}
    } 
}