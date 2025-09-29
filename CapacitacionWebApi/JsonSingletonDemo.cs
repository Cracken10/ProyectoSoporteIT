using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CapacitacionWebApi
{
    // Clase estática para contener los métodos de extensión.
    // Todos los métodos de extensión deben estar definidos en una clase 'static'.
    public static class Extensiones
    {
        // Método de extensión para la clase Singleton.
        // El primer parámetro, precedido por 'this', indica el tipo que se está extendiendo.
        public static void MostrarEstado(this Singleton singleton, ILogger logger)
        {
            logger.LogInformation($"Estado del Singleton - Nombre: {singleton.Nombre}, Valor: {singleton.Valor}");
        }

        // Método de extensión para la clase string para capitalizar la primera letra.
        public static string Capitalizar(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        // Método de extensión para la clase string para invertir la cadena.
        public static string Invertir(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        // Método de extensión para cualquier tipo de lista genérica (IList<T>).
        public static bool EsNulaOVacia<T>(this IList<T> lista)
        {
            return lista == null || lista.Count == 0;
        }
    }

    public class Singleton
    {
        private static Singleton instance;
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                    instance = new Singleton();
                return instance;
            }
        }

        public string Nombre { get; set; } = "Ejemplo Singleton";
        public int Valor { get; set; } = 100;

        private Singleton() { }
    }

    public class ActorClass
    {
        private string actor = "Privado Inicial";
    }

    public static class JsonSingletonDemo
    {
        public static object DemoJson(ILogger logger)
        {
            var singleton = Singleton.Instance;
            singleton.Nombre = "Singleton Serializado";
            singleton.Valor = 200;
            string json = JsonConvert.SerializeObject(singleton);
            logger.LogInformation($"JSON serializado: {json}");
            var deserialized = JsonConvert.DeserializeObject<Singleton>(json);
            logger.LogInformation($"JSON deserializado - Nombre: {deserialized.Nombre}, Valor: {deserialized.Valor}");
            singleton.Nombre = "Singleton Modificado";
            singleton.Valor = 300;
            logger.LogInformation($"Singleton modificado - Nombre: {singleton.Nombre}, Valor: {singleton.Valor}");
            return new
            {
                Message = "JsonSingleton ejecutado",
                JsonSerializado = json,
                Deserializado = new { deserialized.Nombre, deserialized.Valor },
                SingletonModificado = new { singleton.Nombre, singleton.Valor }
            };
        }

        public static object DemoReflection(ILogger logger)
        {
            var actorObj = new ActorClass();
            var field = typeof(ActorClass).GetField("actor", BindingFlags.NonPublic | BindingFlags.Instance);
            string initialValue = field != null ? (string)field.GetValue(actorObj) : "No se encontró el campo 'actor'";
            logger.LogInformation($"Valor inicial del campo privado 'actor': {initialValue}");
            if (field != null)
            {
                field.SetValue(actorObj, "Privado Modificado");
                string modifiedValue = (string)field.GetValue(actorObj);
                logger.LogInformation($"Valor modificado del campo privado 'actor': {modifiedValue}");
            }
            return new
            {
                Message = "Reflexión ejecutada",
                InitialActor = initialValue,
                ModifiedActor = field != null ? "Privado Modificado" : "No modificado"
            };

        }
        public static class DemostradorDeExtensiones
        {
            public static object RunDemo(ILogger logger)
            {
                logger.LogInformation("--- Demostración de Métodos de Extensión ---");

                // Ejemplo con el Singleton
                var singleton = Singleton.Instance;
                singleton.Nombre = "Singleton de Prueba";
                singleton.Valor = 99;
                singleton.MostrarEstado(logger); // Uso del método de extensión

                // Ejemplo con un string
                string miCadena = "hola mundo";
                string capitalizada = miCadena.Capitalizar();
                string invertida = miCadena.Invertir();
                logger.LogInformation($"Cadena original: '{miCadena}'");
                logger.LogInformation($"Cadena capitalizada: '{capitalizada}'");
                logger.LogInformation($"Cadena invertida: '{invertida}'");

                // Ejemplo con una colección genérica
                List<int> numeros = new List<int>();
                logger.LogInformation($"La lista de números es nula o vacía (inicial): {numeros.EsNulaOVacia()}");
                numeros.Add(1);
                logger.LogInformation($"La lista de números es nula o vacía (después de añadir): {numeros.EsNulaOVacia()}");

                return new
                {
                    Message = "Métodos de extensión ejecutados"
                };

            }
            //Ejemplo NullAble Type 
            public static class NullableTypeD
            {
                public static object NullTDemo(ILogger logger)
                {
                    logger.LogInformation("--- Demostración Simple de NullableTypes ---");

            
                    int? edad = null;
                    int? edadConValor = 30;

                    logger.LogInformation($"Edad (nula): {edad?.ToString() ?? "null"}");
                    logger.LogInformation($"Edad con valor: {edadConValor}");

        
                    if (edad.HasValue)
                    {
                        logger.LogInformation($"La edad es: {edad.Value}");
                    }
                    else
                    {
                        logger.LogInformation("La edad no tiene un valor definido.");
                    }


                    int edadFinal = edadConValor ?? 0; 
                    logger.LogInformation($"Edad con valor por defecto: {edadFinal}");

                    string? nombre = null;

                    if (nombre != null)
                    {
                        logger.LogInformation($"Longitud del nombre: {nombre.Length}");
                    }
                    else
                    {
                        logger.LogInformation("El nombre es nulo, no se puede obtener la longitud.");
                    }

    
                    nombre = "Carlos";
                    logger.LogInformation($"Nombre asignado: {nombre}");

                    return new
                    {
                        Message = "Método NullableType ejecutado de forma simple",
                        EdadAnulable = edad?.ToString() ?? "null",
                        EdadConValor = edadConValor,
                        EdadConValorDefecto = edadFinal,
                        NombreAnulable = nombre
                    };
                }


            }
        }
    }
}
