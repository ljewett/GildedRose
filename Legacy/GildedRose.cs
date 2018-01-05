using System;
using System.Data;
using System.Collections.Generic;
using Npgsql;

namespace Legacy
{
    public interface IDatabaseConnector
    {
        DataSet GetItems();
        DataSet SaveItems(DataSet items);
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

        public DataSet SaveItems(DataSet items)
        {
            // save to database
            return new DataSet();
        }
    }
    
    public class GildedRose
    {
        private static List<Item> getItems(IDatabaseConnector connector)
        {
            List<Item> items = new List<Item>();
            DataSet rawItems = connector.GetItems();
            
            foreach (DataRow row in rawItems.Tables[0].Rows)
            {
                items.Add(new Item(row));
            }
            return items;
        }

        private static DataSet createDataSet(List<Item> items)
        {
            DataSet dataSetItems = new DataSet();
            DataTable table = new DataTable("gildedrose");
            
            table.Columns.Add(new DataColumn("name", typeof(string)));
            table.Columns.Add(new DataColumn("sellin", typeof(int)));
            table.Columns.Add(new DataColumn("quality", typeof(int)));
            dataSetItems.Tables.Add(table);

            foreach (Item item in items)
            {
                dataSetItems.Tables[0].Rows.Add(item.name, item.sellin, item.quality);
            }

            return dataSetItems;
        }
        
        public static void updateQuality(IDatabaseConnector connector = null)
        {
            List<Item> items = getItems(connector);
            ItemHandlerFactory handlerFactory = new ItemHandlerFactory();

            List<Item> updatedItems = new List<Item>();
            foreach (Item item in items)
            {
                ItemHandler handler = handlerFactory.getItemHandler(item.type);
                Item newItem = handler.Update(item);
                updatedItems.Add(newItem);
            }

            DataSet itemsToSave = createDataSet(updatedItems);
            connector.SaveItems(itemsToSave);
        }
    }
}

