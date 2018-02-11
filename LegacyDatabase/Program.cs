using System;
using Npgsql;
using System.Data;

namespace LegacyDatabase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var connString = "Host=localhost;Username=postgres;Password=example;Database=legacy;Port=5432";

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM Items", conn);                  
                DataSet items= new DataSet();
                adapter.Fill(items);
                foreach (DataRow row in items.Tables[0].Rows)
                {
                    Console.WriteLine(row[0]);
                }
            }
        }
    }
}