using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;

namespace CapacitacionWebApi
{
    // Nuevo atributo para la información del empleado
    [AttributeUsage(AttributeTargets.Class)]
    public class EmployeeAttribute : Attribute
    {
        public string EmployeeId { get; }
        public string Department { get; }
        public EmployeeAttribute(string id, string department)
        {
            EmployeeId = id;
            Department = department;
        }
    }

    // Atributo para marcar un método como de recursos humanos
    [AttributeUsage(AttributeTargets.Method)]
    public class HrMethodAttribute : Attribute
    {
    }

    // Clase de empleado con atributos modificados y nuevos
    [Employee("EMP-001", "Desarrollo")]
    public class Empleado
    {
        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; }

        [HrMethod]
        public void PerformHrReview()
        {
            Console.WriteLine("Revisión de RH realizada para el empleado.");
        }
    }

    public static class AttributesDemo
    {
        public static object DemoAttributes(ILogger logger)
        {
            var type = typeof(Empleado);

            var employeeAttr = Attribute.GetCustomAttribute(type, typeof(EmployeeAttribute)) as EmployeeAttribute;
            var employeeMessage = employeeAttr != null ? $"ID: {employeeAttr.EmployeeId}, Depto: {employeeAttr.Department}" : "Sin Employee attribute";

            var stringLengthAttr = Attribute.GetCustomAttribute(type.GetProperty("NombreCompleto"), typeof(StringLengthAttribute)) as StringLengthAttribute;
            var stringLengthMessage = stringLengthAttr != null ? $"StringLength de NombreCompleto: {stringLengthAttr.MaximumLength}" : "Sin StringLength attribute";

            var method = type.GetMethod("PerformHrReview");
            var hrMethodAttr = Attribute.GetCustomAttribute(method, typeof(HrMethodAttribute)) as HrMethodAttribute;

            if (hrMethodAttr != null) method.Invoke(Activator.CreateInstance(type), null);
            var hrMessage = hrMethodAttr != null ? "PerformHrReview marcado y ejecutado." : "PerformHrReview no marcado.";

            logger.LogInformation(employeeMessage);
            logger.LogInformation(stringLengthMessage);
            logger.LogInformation(hrMessage);

            return new { employeeMessage, stringLengthMessage, hrMessage };
        }
    }
}
