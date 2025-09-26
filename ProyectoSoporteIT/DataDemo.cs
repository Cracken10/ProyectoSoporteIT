using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace ProyectoSoporteIT
{
    public static class DataDemo
    {
        public static void DemoDataTable()
        {
            DataTable dt = new DataTable("Tickets");
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            DataRow row = dt.NewRow();
            row["ID"] = 1;
            row["Nombre"] = "Test";
            dt.Rows.Add(row);
            foreach (DataRow r in dt.Rows)
            {
                Console.WriteLine($"DataTable: ID={r["ID"]}, Nombre={r["Nombre"]}");
            }
        }
        public static void DemoDataSet()
        {
            DataTable dt = new DataTable("Tickets");
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Rows.Add(1, "Test");
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Console.WriteLine($"DataSet: {ds.Tables[0].TableName} tiene {ds.Tables[0].Rows.Count} filas");
        }
        public static void DemoLinq()
        {
            List<int> nums = new List<int> { 1, 2, 3, 4 };
            var pares = nums.Where(n => n % 2 == 0);
            Console.WriteLine("Where: " + string.Join(", ", pares));
            var dobles = nums.Select(n => n * 2);
            Console.WriteLine("Select: " + string.Join(", ", dobles));
            var anonList = new[] { new { A = 1, B = "Uno" }, new { A = 2, B = "Dos" } }.ToList();
            Console.WriteLine($"Anonymous: A={anonList[0].A}, B={anonList[0].B}");
            IEnumerable<int> enumerable = new List<int> { 1, 2, 3 };
            Console.WriteLine("Enumerable: " + string.Join(", ", enumerable));
        }
    }
}