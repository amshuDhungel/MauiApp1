using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MauiApp1.Data;

    public static class InventoryService
    {
        private static void SaveAll(Guid userId, List<InventoryItem> items)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string itemsFilePath = Utils.GetInventoryFilePath(userId);

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(itemsFilePath, json);
        }

        public static List<InventoryItem> GetAll(Guid userId)
        {
            string itemsFilePath = Utils.GetInventoryFilePath(userId);
            if (!File.Exists(itemsFilePath))
            {
                return new List<InventoryItem>();
            }

            var json = File.ReadAllText(itemsFilePath);

            return JsonSerializer.Deserialize<List<InventoryItem>>(json);
        }

        public static List<InventoryItem> Create(Guid userId, string itemName, int quantity)
        {
            List<InventoryItem> items = GetAll(userId);
            items.Add(new InventoryItem
            {
                ItemName = itemName,
                Quantity = quantity,
            });

            SaveAll(userId, items);
            return items;
        }

        public static List<InventoryItem> Delete(Guid userId, Guid id)
        {
            List<InventoryItem> items = GetAll(userId);
            InventoryItem item = items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                throw new Exception("Item not found.");
            }

            items.Remove(item);
            SaveAll(userId, items);
            return items;
        }

        public static void DeleteByUserId(Guid userId)
        {
            string itemsFilePath = Utils.GetInventoryFilePath(userId);
            if (File.Exists(itemsFilePath))
            {
                File.Delete(itemsFilePath);
            }
        }

        public static List<InventoryItem> Update(Guid userId, Guid id, string itemName, int quantity)
        {
            List<InventoryItem> items = GetAll(userId);
            InventoryItem itemToUpdate = items.FirstOrDefault(x => x.Id == id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            itemToUpdate.ItemName = itemName;
            itemToUpdate.Quantity = quantity;
            itemToUpdate.LastOutDate = DateTime.Today;
            SaveAll(userId, items);
            return items;
        }
    }


