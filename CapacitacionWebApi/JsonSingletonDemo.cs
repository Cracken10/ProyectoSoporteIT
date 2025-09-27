// Archivo: JsonSingletonDemo.cs

using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CapacitacionWebApi
{
    // Clase Singleton para la configuración de empleados
    public class JsonSingletonDemo
    {
        private static JsonSingletonDemo instance;

        // Propiedad estática para obtener la única instancia
        public static JsonSingletonDemo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JsonSingletonDemo();
                }
                return instance;
            }
        }

        // Propiedades relevantes para la configuración de empleados
        public string NombreEmpresa { get; set; } = "Mi Empresa SA";
        public string JefeActivo { get; set; } = "Sin asignar";

        // Constructor privado para prevenir la instanciación externa
        private JsonSingletonDemo() { }

        // Método de demostración para serializar y deserializar
        public static object DemoJson(ILogger logger)
        {
            // Acceder al Singleton de configuración de empleados
            var configuracion = JsonSingletonDemo.Instance;
            configuracion.NombreEmpresa = "Empresa X";
            configuracion.JefeActivo = "Juan Gerente";
            logger.LogInformation("--- Ejemplo de Singleton con datos de empleados ---");
            logger.LogInformation($"Configuración inicial - Empresa: {configuracion.NombreEmpresa}, Jefe: {configuracion.JefeActivo}");

            // Serializar el Singleton a JSON para simular guardado
            string json = JsonConvert.SerializeObject(configuracion);
            logger.LogInformation($"JSON serializado: {json}");

            // Deserializar el JSON de vuelta a un objeto
            var deserializedConfig = JsonConvert.DeserializeObject<JsonSingletonDemo>(json);
            logger.LogInformation($"JSON deserializado (nueva instancia) - Empresa: {deserializedConfig.NombreEmpresa}, Jefe: {deserializedConfig.JefeActivo}");

            // Modificar el Singleton para demostrar que es la misma instancia en memoria
            configuracion.NombreEmpresa = "Empresa Modificada";
            configuracion.JefeActivo = "Ana Supervisora";
            logger.LogInformation($"Singleton modificado en memoria - Empresa: {configuracion.NombreEmpresa}, Jefe: {configuracion.JefeActivo}");

            // Volver a acceder al Singleton para verificar la persistencia del cambio en memoria
            var mismaInstancia = JsonSingletonDemo.Instance;
            logger.LogInformation($"Acceso posterior a la misma instancia Singleton - Empresa: {mismaInstancia.NombreEmpresa}, Jefe: {mismaInstancia.JefeActivo}");

            return new
            {
                Message = "JsonSingletonEmpleadoDemo ejecutado, serializando y deserializando la configuración de empleados.",
                ConfiguracionInicial = new { configuracion.NombreEmpresa, configuracion.JefeActivo },
                JsonSerializado = json,
                Deserializado = new { deserializedConfig.NombreEmpresa, deserializedConfig.JefeActivo },
                SingletonModificado = new { mismaInstancia.NombreEmpresa, mismaInstancia.JefeActivo }
            };
        }
    }
}
