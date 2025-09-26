using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace CapacitacionWebApi
{
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

            return new
            {
                Message = "AnonymousEmployees ejecutado",
                EmpleadoAnonimo = empleadoAnonimo,
                ListaEmpleados = empleadosList,
            };
        }
    }
}
