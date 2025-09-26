using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapacitacionWebApi
{
    // Nuevo tipo enumerado para los departamentos
    public enum Departamento
    {
        IT,
        Ventas,
        Marketing,
        RecursosHumanos,
        Desconocido
    }

    public static class AnonymousEmployeesDemo
    {
        public static object DemoAnonymousEmployees(ILogger logger)
        {
            // Ejemplo de un tipo anónimo individual de un empleado
            var empleadoAnonimo = new
            {
                Id = "EMP-005",
                Nombre = "Juan Pérez",
                Departamento = "Recursos Humanos",
                Salario = 55000.50m
            };
            logger.LogInformation("--- Ejemplo de un tipo anónimo individual de empleado ---");
            logger.LogInformation($"Empleado: {empleadoAnonimo.Nombre}, ID: {empleadoAnonimo.Id}, Departamento: {empleadoAnonimo.Departamento}, Salario: {empleadoAnonimo.Salario:C}");

            // Ejemplo de una lista de tipos anónimos de empleados
            var listaEmpleadosAnonimos = new[]
            {
                new { Id = "EMP-001", Nombre = "Ana Gómez", Departamento = "IT", Salario = 62000.00m },
                new { Id = "EMP-002", Nombre = "Luis Torres", Departamento = "Ventas", Salario = 58000.75m },
                new { Id = "EMP-003", Nombre = "Marta Ruiz", Departamento = "Marketing", Salario = 59500.25m },
                new { Id = "EMP-004", Nombre = "Pedro García", Departamento = "IT", Salario = 65000.00m }
            };
            logger.LogInformation("\n--- Ejemplo de una lista de tipos anónimos de empleados ---");
            var empleadosList = new List<object>();
            foreach (var empleado in listaEmpleadosAnonimos)
            {
                logger.LogInformation($"ID: {empleado.Id}, Nombre: {empleado.Nombre}, Depto: {empleado.Departamento}, Salario: {empleado.Salario:C}");
                empleadosList.Add(new { empleado.Id, empleado.Nombre, empleado.Departamento, empleado.Salario });
            }

            // Se utiliza el enum Departamento
            Departamento departamentoEmpleado = Departamento.IT;
            logger.LogInformation("\n--- Ejemplo de un tipo enumerado individual ---");
            logger.LogInformation($"El departamento seleccionado es: {departamentoEmpleado}");

            // Para mantener la demostración limpia, se crea una clase simple aquí.
            var empleadosConEnum = new List<object>
            {
                new { Id = "EMP-006", Nombre = "Carlos Solis", Departamento = Departamento.Marketing },
                new { Id = "EMP-007", Nombre = "Diana Ramos", Departamento = Departamento.Ventas },
                new { Id = "EMP-008", Nombre = "Eduardo Mena", Departamento = Departamento.IT }
            };

            logger.LogInformation("\n--- Ejemplo de una lista con tipo enumerado ---");
            foreach (dynamic empleado in empleadosConEnum)
            {
                // Al usar 'dynamic', se accede a las propiedades del tipo anónimo
                logger.LogInformation($"ID: {empleado.Id}, Nombre: {empleado.Nombre}, Depto (enum): {empleado.Departamento}");
            }

            return new
            {
                Message = "AnonymousEmployees ejecutado con ejemplos de tipos enumerados",
                EmpleadoAnonimo = empleadoAnonimo,
                ListaEmpleados = empleadosList,
                EjemploEnum = departamentoEmpleado.ToString(),
                ListaEmpleadosConEnum = empleadosConEnum
            };
        }
    }
}
