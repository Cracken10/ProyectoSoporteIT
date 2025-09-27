using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CapacitacionWebApi
{
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
    }
}