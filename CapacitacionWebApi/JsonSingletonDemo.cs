using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ProyectoSoporteIT
{
    // Se habilita el soporte para tipos de referencia anulables en este archivo.
#nullable enable

    // Clase estática para contener los métodos de extensión.
    public static class Extensiones
    {
        // Método de extensión para la clase Singleton.
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
        public static bool EsNulaOVacia<T>(this IList<T>? lista)
        {
            return lista == null || lista.Count == 0;
        }
    }

    public class Singleton
    {
        private static Singleton? instance;
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

    // Clase base 'Persona' con constructores.
    public class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }

        // 1. Constructor por defecto.
        public Persona()
        {
            Nombre = "Sin Nombre";
            Edad = 0;
            Console.WriteLine("Constructor de Persona por defecto llamado.");
        }

        // 2. Constructor con un parámetro (usa encadenamiento para llamar al constructor por defecto).
        public Persona(string nombre) : this()
        {
            Nombre = nombre;
            Console.WriteLine($"Constructor de Persona con un parámetro llamado. Nombre: {Nombre}");
        }

        // 3. Constructor con dos parámetros (usa encadenamiento).
        public Persona(string nombre, int edad) : this(nombre)
        {
            Edad = edad;
            Console.WriteLine($"Constructor de Persona con dos parámetros llamado. Edad: {Edad}");
        }

        // 4. Constructor privado para demostrar la creación por método estático.
        private Persona(string nombre, int edad, bool privado) : this(nombre, edad)
        {
            Console.WriteLine($"Constructor de Persona privado llamado.");
        }

        // 5. Método estático para llamar al constructor privado.
        public static Persona CrearInstanciaPrivada(string nombre, int edad)
        {
            return new Persona(nombre, edad, true);
        }
    }

    // Clase derivada 'Empleado'.
    public class Empleado : Persona
    {
        public string Cargo { get; set; }

        // Constructor de Empleado que llama al constructor de la clase base `Persona`.
        public Empleado(string nombre, int edad, string cargo) : base(nombre, edad)
        {
            Cargo = cargo;
            Console.WriteLine($"Constructor de Empleado llamado. Cargo: {Cargo}");
        }
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
            if (deserialized != null)
            {
                logger.LogInformation($"JSON deserializado - Nombre: {deserialized.Nombre}, Valor: {deserialized.Valor}");
            }
            singleton.Nombre = "Singleton Modificado";
            singleton.Valor = 300;
            logger.LogInformation($"Singleton modificado - Nombre: {singleton.Nombre}, Valor: {singleton.Valor}");
            return new
            {
                Message = "JsonSingleton ejecutado",
                JsonSerializado = json,
                Deserializado = deserialized != null ? new { deserialized.Nombre, deserialized.Valor } : null,
                SingletonModificado = new { singleton.Nombre, singleton.Valor }
            };
        }

        public static object DemoReflection(ILogger logger)
        {
            var actorObj = new ActorClass();
            var field = typeof(ActorClass).GetField("actor", BindingFlags.NonPublic | BindingFlags.Instance);
            string? initialValue = field != null ? (string?)field.GetValue(actorObj) : "No se encontró el campo 'actor'";
            logger.LogInformation($"Valor inicial del campo privado 'actor': {initialValue}");
            if (field != null)
            {
                field.SetValue(actorObj, "Privado Modificado");
                string? modifiedValue = (string?)field.GetValue(actorObj);
                logger.LogInformation($"Valor modificado del campo privado 'actor': {modifiedValue}");
            }
            return new
            {
                Message = "Reflexión ejecutada",
                InitialActor = initialValue,
                ModifiedActor = field != null ? "Privado Modificado" : "No modificado"
            };
        }
    }

    public static class DemostradorDeExtensiones
    {
        public static object RunDemo(ILogger logger)
        {
            logger.LogInformation("--- Demostración de Métodos de Extensión ---");
            var singleton = Singleton.Instance;
            singleton.Nombre = "Singleton de Prueba";
            singleton.Valor = 99;
            singleton.MostrarEstado(logger);
            string miCadena = "hola mundo";
            string capitalizada = miCadena.Capitalizar();
            string invertida = miCadena.Invertir();
            logger.LogInformation($"Cadena original: '{miCadena}'");
            logger.LogInformation($"Cadena capitalizada: '{capitalizada}'");
            logger.LogInformation($"Cadena invertida: '{invertida}'");
            List<int> numeros = new List<int>();
            logger.LogInformation($"La lista de números es nula o vacía (inicial): {numeros.EsNulaOVacia()}");
            numeros.Add(1);
            logger.LogInformation($"La lista de números es nula o vacía (después de añadir): {numeros.EsNulaOVacia()}");
            return new
            {
                Message = "Métodos de extensión ejecutados"
            };
        }
    }

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

    public static class ConstructoresDemo
    {
        public static object RunDemo(ILogger logger)
        {
            logger.LogInformation("--- Demostración de Constructores ---");

            // Llamada al constructor con dos parámetros de Persona.
            logger.LogInformation("Creando objeto 'personaCompleta' (llamada al constructor con dos parámetros):");
            var personaCompleta = new Persona("Ana", 25);
            logger.LogInformation($"Objeto creado: Nombre='{personaCompleta.Nombre}', Edad={personaCompleta.Edad}");

            // Llamada al constructor con un parámetro de Persona.
            logger.LogInformation("Creando objeto 'personaSimple' (llamada al constructor con un parámetro):");
            var personaSimple = new Persona("Juan");
            logger.LogInformation($"Objeto creado: Nombre='{personaSimple.Nombre}', Edad={personaSimple.Edad}");

            // Llamada al constructor por defecto de Persona.
            logger.LogInformation("Creando objeto 'personaPredeterminada' (llamada al constructor por defecto):");
            var personaPredeterminada = new Persona();
            logger.LogInformation($"Objeto creado: Nombre='{personaPredeterminada.Nombre}', Edad={personaPredeterminada.Edad}");

            // Llamada al constructor de Empleado (que a su vez llama al de Persona).
            logger.LogInformation("Creando objeto 'empleado' (llamada al constructor de Empleado):");
            var empleado = new Empleado("Laura", 30, "Programadora");
            logger.LogInformation($"Objeto creado: Nombre='{empleado.Nombre}', Edad={empleado.Edad}, Cargo='{empleado.Cargo}'");

            // Llamada a un constructor privado a través de un método estático.
            logger.LogInformation("Creando objeto 'instanciaPrivada' (llamada a constructor privado):");
            var instanciaPrivada = Persona.CrearInstanciaPrivada("Secreto", 40);
            logger.LogInformation($"Objeto creado: Nombre='{instanciaPrivada.Nombre}', Edad={instanciaPrivada.Edad}");

            return new { Message = "Demostración de constructores y encadenamiento ejecutada" };
        }
    }
}
