using System;
namespace ProyectoSoporteIT
{
    public static class LambdasDemo
    {
        public static void DemoLambdas()
        {
            Action voidLambda = () => Console.WriteLine("Void lambda ejecutada");
            voidLambda();
            Func<dynamic> returnLambda = () => new { Prop = "Valor" };
            var resultado = returnLambda();
            Console.WriteLine($"Lambda retorno: {resultado.Prop}");
        }
        public static void DemoAction()
        {
            Action<int, string, bool, double> accion4 = (i, s, b, d) => Console.WriteLine($"Action4: {i}, {s}, {b}, {d}");
            Action<int, string, bool> accion3 = (i, s, b) => Console.WriteLine($"Action3: {i}, {s}, {b}");
            Action<int, string> accion2 = (i, s) => Console.WriteLine($"Action2: {i}, {s}");
            Action<int> accion1 = i => Console.WriteLine($"Action1: {i}");
            accion4(1, "test", true, 2.0);
            accion3(1, "test", true);
            accion2(1, "test");
            accion1(1);
            void MetodoAccion(Action<int> param) { param(5); }
            MetodoAccion(accion1);
        }
        public static void DemoFunc()
        {
            Func<int, string, bool, double, string> func4 = (i, s, b, d) => $"Func4: {i}, {s}, {b}, {d}";
            Func<int, string, bool, int> func3 = (i, s, b) => i;
            Func<int, string, string> func2 = (i, s) => s;
            Func<int, string> func1 = i => $"Func1: {i}";
            Console.WriteLine(func4(1, "test", true, 2.0));
            Console.WriteLine(func3(1, "test", true));
            Console.WriteLine(func2(1, "test"));
            Console.WriteLine(func1(1));
            string MetodoFunc(Func<int, string> param) { return param(5); }
            Console.WriteLine(MetodoFunc(func1));
        }
        public delegate void MiDelegate4(int i, string s, bool b, double d);
        public delegate string MiFunc1(int i);
        public static void DemoDelegate()
        {
            MiDelegate4 del4 = (i, s, b, d) => Console.WriteLine($"Delegate4: {i}, {s}, {b}, {d}");
            del4(1, "test", true, 2.0);
            Action<int, string, bool, double> accion = (i, s, b, d) => Console.WriteLine($"Action en delegate: {i}, {s}, {b}, {d}");
            del4 += new MiDelegate4(accion.Invoke); 
            del4(2, "test2", false, 3.0);
            MiFunc1 funcDel = new Func<int, string>(i => $"Func en delegate: {i}").Invoke;
            Console.WriteLine(funcDel(10));
        }

    }
}