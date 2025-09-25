using System;
using System.Reflection;
using System.Windows.Forms;

namespace ProyectoSoporteIT
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class AuthorAttribute : Attribute
    {
        private string name;
        public double Version { get; set; }

        public AuthorAttribute(string name)
        {
            this.name = name;
            Version = 1.0;
        }

        public string Name => name;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute : Attribute
    {
        private readonly int maxLength;

        public MaxLengthAttribute(int maxLength)
        {
            this.maxLength = maxLength;
        }

        public int MaxLength => maxLength;
    }


    [Author("Aldo Casas", Version = 2.0)]
    public class Documento
    {
        [MaxLength(45)]
        public string Nombre { get; set; }


    }

    public static class AttributesDemo
    {
        public static void DemoAttributes()
        {
            // Demostrar atributo Author en clase Documento
            var type = typeof(Documento);
            var authorAttr = (AuthorAttribute)Attribute.GetCustomAttribute(type, typeof(AuthorAttribute));
            if (authorAttr != null)
            {
                Console.WriteLine($"Author: {authorAttr.Name}, Version: {authorAttr.Version}");
            }

            // Demostrar atributo MaxLength en propiedad Nombre
            var property = type.GetProperty("Nombre");
            var maxLengthAttr = (MaxLengthAttribute)Attribute.GetCustomAttribute(property, typeof(MaxLengthAttribute));
            if (maxLengthAttr != null)
            {
                Console.WriteLine($"MaxLength de Nombre: {maxLengthAttr.MaxLength}");
            }


            // Mostrar mensaje en MessageBox
            MessageBox.Show("Atributos ejecutados.");
        }
    }
}