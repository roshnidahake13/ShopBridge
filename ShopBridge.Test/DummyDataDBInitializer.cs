using Microsoft.EntityFrameworkCore;
using ShopBridge_WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Test
{
    class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        [Obsolete]
        public void Seed(ShopBridgeDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            byte[] image = new byte[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            context.Inventories.AddRange(
                new Inventory() {  item_Name = "Samsung A20", item_Desc="Mobile Phone", itemAvailability=Convert.ToBoolean(1), item_AddedOn=DateTime.Now, item_Quantity=5, item_Image= image },
                new Inventory() {  item_Name = "Samsung A31", item_Desc = "Mobile Phone", itemAvailability = Convert.ToBoolean(1), item_AddedOn = DateTime.Now, item_Quantity = 10, item_Image = image },
                new Inventory() {  item_Name = "POCO X3", item_Desc = "Mobile Phone", itemAvailability = Convert.ToBoolean(1), item_AddedOn = DateTime.Now, item_Quantity = 15, item_Image = image },
                new Inventory() {  item_Name = "Macbook Air", item_Desc = "Laptop", itemAvailability = Convert.ToBoolean(1), item_AddedOn = DateTime.Now, item_Quantity = 20, item_Image = image },
                new Inventory() {  item_Name = "The Alchemist", item_Desc = "Book", itemAvailability = Convert.ToBoolean(1), item_AddedOn = DateTime.Now, item_Quantity = 50, item_Image = image }
            );
            
            context.SaveChanges();
        }
    }
}
