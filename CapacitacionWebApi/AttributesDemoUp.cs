using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CapacitacionWebApi;

[AttributeUsage(AttributeTargets.Class)]
public sealed class EmployeeAttribute(string id, string department) : Attribute
{
    public string EmployeeId { get; } = id;
    public string Department { get; } = department;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class HrMethodAttribute : Attribute;

[Employee("EMP-001", "Desarrollo")]
public class Empleado
{
    [Required, StringLength(100)]
    public string NombreCompleto { get; set; } = string.Empty;

    [HrMethod]
    public void PerformHrReview(ILogger logger) =>
        logger.LogInformation("Revisión de RH realizada para el empleado.");
}

public static class AttributesDemo
{
    public static string DemoAttributes(ILogger logger)
    {
        var type = typeof(Empleado);
        var empleado = new Empleado();
        var logMessages = new System.Text.StringBuilder();

        // 1. Mensaje de atributo de empleado
        var employeeAttr = type.GetCustomAttribute<EmployeeAttribute>();
        var employeeMessage = employeeAttr is not null ? $"ID: {employeeAttr.EmployeeId}, Depto: {employeeAttr.Department}" : "Sin Employee attribute";
        logMessages.AppendLine($"employeeMessage: {employeeMessage}");

        // 2. Mensaje de atributo de longitud de cadena
        var stringLengthAttr = type.GetProperty(nameof(Empleado.NombreCompleto))?.GetCustomAttribute<StringLengthAttribute>();
        var stringLengthMessage = stringLengthAttr is not null ? $"StringLength de NombreCompleto: {stringLengthAttr.MaximumLength}" : "Sin StringLength attribute";
        logMessages.AppendLine($"stringLengthMessage: {stringLengthMessage}");

        // 3. Mensaje y ejecución de método HR
        var method = type.GetMethod(nameof(Empleado.PerformHrReview));
        var hrMethodAttr = method?.GetCustomAttribute<HrMethodAttribute>();
        var hrMessage = hrMethodAttr is not null ? "PerformHrReview marcado y ejecutado." : "PerformHrReview no marcado.";

        if (hrMethodAttr is not null && method is not null)
        {
            // Solo se ejecuta si el atributo está presente
            method.Invoke(empleado, [logger]);
        }
        logMessages.AppendLine($"hrMessage: {hrMessage}");

        // Se registran todos los mensajes acumulados en un solo log
        logger.LogInformation(logMessages.ToString());

        // Se devuelve la misma cadena de mensajes como resultado
        return logMessages.ToString();
    }
}
