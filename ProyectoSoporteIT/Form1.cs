using System;
using System.Windows.Forms;
namespace ProyectoSoporteIT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LambdasDemo.DemoLambdas();
            MessageBox.Show("Lambdas ejecutadas. ");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LambdasDemo.DemoAction();
            MessageBox.Show("Action ejecutada.");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            LambdasDemo.DemoFunc();
            MessageBox.Show("Func ejecutada.");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            LambdasDemo.DemoDelegate();
            MessageBox.Show("Delegate ejecutado.");
        }
    }
}