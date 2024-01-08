using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MauiApp1.Data.Utils;

namespace MauiApp1.Data;


    public static class OrderService
    {
        private static void SaveAll(List<Order> items)
        {
            string appDataDirectoryPath = GetAppDirectoryPath();
            string itemsFilePath = GetOrdersFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(itemsFilePath, json);
        }

        public static List<Order> GetAll()
        {
            string itemsFilePath = GetOrdersFilePath();
            if (!File.Exists(itemsFilePath))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(itemsFilePath);

            return JsonSerializer.Deserialize<List<Order>>(json);
        }

        public static List<Order> Create(string username, List<InventoryItem> items)
        {
            List<Order> reqItems = GetAll();
            reqItems.Add(new Order
            {
                Items = items,
                RequestDate = DateTime.Now,
                RequestedBy = username,
            });

            SaveAll(reqItems);
            return reqItems;
        }

        public static List<Order> Delete(Guid id)
        {
            List<Order> items = GetAll();
            Order item = items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                throw new Exception("Item not found.");
            }

            items.Remove(item);
            SaveAll(items);
            return items;
        }
    }

