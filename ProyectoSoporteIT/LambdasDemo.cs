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
    }
}
