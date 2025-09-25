using System;
using System.Windows.Forms;
namespace ProyectoSoporteIT
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            LambdasDemo.DemoLambdas();
            LambdasDemo.DemoAction();
            LambdasDemo.DemoFunc();
            LambdasDemo.DemoDelegate();
        }
        }
    }
