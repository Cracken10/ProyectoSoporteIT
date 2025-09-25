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
    }
}