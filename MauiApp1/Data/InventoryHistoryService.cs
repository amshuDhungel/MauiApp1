using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MauiApp1.Data;

public static class InventoryHistoryService
{
     private static void SaveAll(Guid userId, List<InventoryHistory> items)
     {
         string appDataDirectoryPath = Utils.GetAppDirectoryPath();
         string itemsFilePath = Utils.GetHistoryFilePath(userId);

         if (!Directory.Exists(appDataDirectoryPath))
         {
             Directory.CreateDirectory(appDataDirectoryPath);
         }

         var json = JsonSerializer.Serialize(items);
         File.WriteAllText(itemsFilePath, json);
        }

        public static List<InventoryHistory> GetAll(Guid userId)
        {
            string itemsFilePath = Utils.GetHistoryFilePath(userId);

            if (!File.Exists(itemsFilePath))
            {
                return new List<InventoryHistory>();
            }

            var json = File.ReadAllText(itemsFilePath);

            return JsonSerializer.Deserialize<List<InventoryHistory>>(json);
        }

        public static List<InventoryHistory> Create(Guid userId, string itemName, int quantity, string approvedBy, string takenBy)
        {
            List<InventoryHistory> items = GetAll(userId);
            items.Add(new InventoryHistory
            {
                ItemName = itemName,
                Quantity = quantity,
                ApprovedBy = approvedBy,
                TakenBy = takenBy
            });
            SaveAll(userId, items);
            return items;
        }

        public static void Delete(Guid userId, string itemName)
        {
            var newItems = GetAll(userId).Where(i => !i.ItemName.Equals(itemName)).ToList();

            SaveAll(userId, newItems);
        }

        public static void DeleteByUserId(Guid userId)
        {
            string itemsFilePath = Utils.GetHistoryFilePath(userId);
            if (File.Exists(itemsFilePath))
            {
                File.Delete(itemsFilePath);
            }
       }
}


