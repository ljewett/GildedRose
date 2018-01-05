using System;
using System.Data;
using Npgsql;

namespace Legacy
{
    public interface IDatabaseConnector
    {
        DataSet GetItems();
    }
    
    public class DatabaseConnector : IDatabaseConnector
    {
        public DataSet GetItems()
        {
            var connString = "Host=localhost;Username=steveturley;Password=;Database=legacy";
            DataSet items = new DataSet();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM Items", conn);                  
                adapter.Fill(items);
                conn.Close();
            }
            return items;
        }
    }
    
    public class GildedRose
    {
        public static void updateQuality(IDatabaseConnector connector = null)
        {
            DataSet items = connector.GetItems();
            foreach (DataRow row in items.Tables[0].Rows)
            {
                if ((!"Aged Brie".Equals(row[0])) && !"Backstage passes to a TAFKAL80ETC concert".Equals(row[0]))
                {
                    if ((int)row[2] > 0)
                    {
                        if (!"Sulfuras, Hand of Ragnaros".Equals(row[0]))
                        {
                            row[2] = (int)row[2] - 1;
                        }
                    }
                }
            else
            {
                if ((int)row[2] < 50)
                {
                    row[2] = (int)row[2] + 1;

                    if ("Backstage passes to a TAFKAL80ETC concert".Equals(row[0]))
                    {
                        if ((int)row[1] < 11)
                        {
                            if ((int)row[0] < 50)
                            {
                                row[2] = (int)row[2] + 1;
                            }
                        }

                        if ((int)row[1] < 6)
                        {
                            if ((int)row[2] < 50)
                            {
                                row[2] = (int)row[2] + 1;
                            }
                        }
                    }
                }
            }

            if (!"Sulfuras, Hand of Ragnaros".Equals(row[0]))
            {
                row[1] = ((int)row[1] - 1);
            }

            if ((int)row[1] < 0)
            {
                if (!"Aged Brie".Equals(row[0]))
                {
                    if (!"Backstage passes to a TAFKAL80ETC concert".Equals(row[0]))
                    {
                        if ((int)row[2] > 0)
                        {
                            if (!"Sulfuras, Hand of Ragnaros".Equals(row[0]))
                            {
                                row[2] = ((int)row[2] - 1);
                            }
                        }
                    }
                    else
                    {
                        row[2] = ((int)row[2] - (int)row[2]);
                    }
                }
                else
                {
                    if ((int)row[2] < 50)
                    {
                        row[2] = ((int)row[2] + 1);
                    }
                }
            }

            }
        }
    }
}

