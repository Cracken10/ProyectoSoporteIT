using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace CapacitacionWebApi
{
    public enum Departamento
    {
        IT,
        Marketing,
        Ventas,
        RecursosHumanos
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

            // Ejemplo de una lista con tipo enumerado
            var empleadosConEnum = new List<object>
            {
                new { Id = "EMP-006", Nombre = "Carlos Solis", Departamento = Departamento.Marketing },
                new { Id = "EMP-007", Nombre = "Diana Ramos", Departamento = Departamento.Ventas },
                new { Id = "EMP-008", Nombre = "Eduardo Mena", Departamento = Departamento.IT }
            };
            logger.LogInformation("\n--- Ejemplo de una lista con tipo enumerado ---");
            foreach (dynamic empleado in empleadosConEnum)
            {
                logger.LogInformation($"ID: {empleado.Id}, Nombre: {empleado.Nombre}, Depto (enum): {empleado.Departamento}");
            }
            //Ejemplo Iqueryable
            var empleadosQueryable = listaEmpleadosAnonimos.AsQueryable();
            var empleadosITQueryable = empleadosQueryable
                .Where(e => e.Departamento == "IT" && e.Salario > 60000)
                .Select(e => new { e.Nombre, e.Salario })
                .ToList();
            logger.LogInformation("\n--- Ejemplo de IQueryable para empleados filtrados ---");
            var queryableList = new List<object>();
            foreach (var empleado in empleadosITQueryable)
            {
                logger.LogInformation($"Empleado de IT (IQueryable): {empleado.Nombre}, Salario: {empleado.Salario:C}");
                queryableList.Add(new { empleado.Nombre, empleado.Salario });
            }

            // Este ejemplo usa solo el filtrado.
            var empleadosFiltrados = listaEmpleadosAnonimos.Where(e => e.Salario > 60000);
            logger.LogInformation("\n--- Ejemplo de Enumerable.Where (filtrado) ---");
            var enumerableWhereList = new List<object>();
            foreach (var empleado in empleadosFiltrados)
            {
                logger.LogInformation($"Empleado con salario > 60k (Enumerable.Where): {empleado.Nombre}, Salario: {empleado.Salario:C}");
                enumerableWhereList.Add(new { empleado.Nombre, empleado.Salario });
            }

            // Este ejemplo usa solo la extracción/proyección.
            var nombresEmpleados = listaEmpleadosAnonimos.Select(e => e.Nombre);
            logger.LogInformation("\n--- Ejemplo de Enumerable.Select (extracción) ---");
            var enumerableSelectList = new List<string>();
            foreach (var nombre in nombresEmpleados)
            {
                logger.LogInformation($"Nombre de empleado (Enumerable.Select): {nombre}");
                enumerableSelectList.Add(nombre);
            }

            return new
            {
                Message = "AnonymousEmployees ejecutado con ejemplos de tipos anónimos, enumerados, IQueryable e IEnumerable",
                EmpleadoAnonimo = empleadoAnonimo,
                ListaEmpleados = empleadosList,
                EjemploEnum = departamentoEmpleado.ToString(),
                ListaEmpleadosConEnum = empleadosConEnum,
                EmpleadosITQueryable = queryableList,
                EmpleadosFiltradosEnumerable = enumerableWhereList,
                NombresEmpleadosEnumerable = enumerableSelectList
            };
        }
    }
}
