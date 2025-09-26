using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
namespace ProyectoSoporteIT
{
    public static class AdvancedDemo
    {
        public static void DemoReflection()
        {
            var obj = new ClasePrivada();
            var field = typeof(ClasePrivada).GetField("actor", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(obj, "Modificado");
            Console.WriteLine($"Reflection: {field.GetValue(obj)}");
        }
        private class ClasePrivada
        {
            private string actor = "Privado";
        }
        public static void DemoExtensionMethods()
        {
            int num = 5;
            Console.WriteLine($"Extension: {num.Doble()}");
        }
        public static void DemoNullable()
        {
            int? nullable = null;
            Console.WriteLine($"Nullable: {(nullable.HasValue ? nullable.Value.ToString() : "null")}");
            nullable = 10;
            Console.WriteLine($"Nullable: {nullable ?? 0}");
        }
        public class ClaseConstructores
        {
            public ClaseConstructores() : this(0) { }
            public ClaseConstructores(int p) { Console.WriteLine($"Constructor: {p}"); }
        }
        public static void DemoConstructors()
        {
            var c1 = new ClaseConstructores();
            var c2 = new ClaseConstructores(5);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void MetodoSincronizado(List<int> lista)
        {
            foreach (var item in lista) { Console.WriteLine($"Sincronizado: {item}"); }
            Parallel.ForEach(lista, item => Console.WriteLine($"Paralelo: {item}"));
        }
        public static void DemoMethodImpl()
        {
            var lista = new List<int> { 1, 2, 3 };
            MetodoSincronizado(lista);
        }
    }
    public static class Extensions
    {
        public static int Doble(this int i) { return i * 2; }
    }
}
