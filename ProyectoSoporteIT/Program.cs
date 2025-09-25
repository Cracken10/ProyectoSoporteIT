using System;
using System.Windows.Forms;
namespace ProyectoSoporteIT
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            LambdasDemo.DemoLambdas();
            LambdasDemo.DemoAction();
            LambdasDemo.DemoFunc();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}