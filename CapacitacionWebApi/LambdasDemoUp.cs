using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace CapacitacionWebApi
{
    public static class LambdasDemo
    {
        // Delegado personalizado utilizado en DemoDelegate
        public delegate void MiDelegate(string mensaje);

        public static object DemoLambdas(ILogger logger)
        {
            logger.LogInformation("Inicio de DemoLambdas.");
            var messages = new List<string>();

            // Lambda que no devuelve un valor (Action)
            Action voidLambda = () =>
            {
                var msg = "Void lambda ejecutada";
                logger.LogInformation(msg);
                messages.Add(msg);
            };
            voidLambda();

            // Lambda que devuelve un valor (Func)
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
            logger.LogInformation("Inicio de DemoAction.");
            var messages = new List<string>();

            // Definiciones de las Actions
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

            // Ejecución de las Actions
            accion4(1, "test", true, 2.0);
            accion3(1, "test", true);
            accion2(1, "test");
            accion1(1);

            // Método local que invoca un Action
            void MetodoAccion(Action<int> param)
            {
                param(5);
                messages.Add("MetodoAccion ejecutado.");
            }

            MetodoAccion(accion1);

            logger.LogInformation("Fin de DemoAction.");

            return new
            {
                Message = "Action ejecutada",
                LogMessages = messages
            };
        }

        public static object DemoFunc(ILogger logger)
        {
            logger.LogInformation("Inicio de DemoFunc.");
            var messages = new List<string>();
            var results = new Dictionary<string, object>();

            // Func que devuelve un string con múltiples parámetros
            Func<int, string, bool, double, string> func4 = (i, s, b, d) => $"Func4: {i}, {s}, {b}, {d}";
            var resFunc4 = func4(1, "test", true, 2.0);
            messages.Add($"Resultado Func4: {resFunc4}");
            results.Add("Func4", resFunc4);

            // Func que devuelve un entero
            Func<int, string, bool, int> func3 = (i, s, b) => i;
            var resFunc3 = func3(1, "test", true);
            messages.Add($"Resultado Func3: {resFunc3}");
            results.Add("Func3", resFunc3);

            // Func que devuelve un string
            Func<int, string, string> func2 = (i, s) => s;
            var resFunc2 = func2(1, "test");
            messages.Add($"Resultado Func2: {resFunc2}");
            results.Add("Func2", resFunc2);

            // Func que devuelve un string
            Func<int, string> func1 = i => $"Func1: {i}";
            var resFunc1 = func1(1);
            messages.Add($"Resultado Func1: {resFunc1}");
            results.Add("Func1", resFunc1);

            // Método local que recibe una Func y la ejecuta
            string MetodoFunc(Func<int, string> param)
            {
                var res = param(5);
                messages.Add($"MetodoFunc ejecutado con resultado: {res}");
                return res;
            }

            var resMetodoFunc = MetodoFunc(func1);
            results.Add("MetodoFuncResult", resMetodoFunc);

            logger.LogInformation("Fin de DemoFunc.");

            return new
            {
                Message = "Func ejecutada",
                Results = results,
                LogMessages = messages
            };
        }

        public static object DemoDelegate(ILogger logger)
        {
            logger.LogInformation("Inicio de DemoDelegate.");
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
            del += accion.Invoke;
            del += accionDesdeFunc.Invoke;

            // Invocar el delegado, que llamará a los tres métodos en orden
            del("Resultado");

            logger.LogInformation("Fin de DemoDelegate.");

            return new
            {
                Message = "Delegate, Func y Action ejecutados",
                LogMessages = messages
            };
        }
        public static object DemoStringFormat(ILogger logger)
        {
            string formatted = string.Format("Texto {1} {0}", "Test", 78784);

            string fecha = string.Format("Fecha: {0:yyyy.MM/dd HH:mm}", DateTime.Now);

            string numero = string.Format("Numero D2: {0:D10}", 5);

            string precio = string.Format("Precio: {0:C}", 19.95);

            string porcentaje = string.Format("Porcentaje: {0:P2}", 0.75);

            string numeroGrande = string.Format("Numero grande: {0:N0}", 123456789);

            
            logger.LogInformation(formatted);
            logger.LogInformation(fecha);
            logger.LogInformation(numero);
            logger.LogInformation(precio);
            logger.LogInformation(porcentaje);
            logger.LogInformation(numeroGrande);
      
            return new
            {
                Message = "String.Format ejecutado",
                Formatted = formatted,
                Fecha = fecha,
                Numero = numero,
                Precio = precio,
                Porcentaje = porcentaje,
                NumeroGrande = numeroGrande,
            };
        }

    }
}
