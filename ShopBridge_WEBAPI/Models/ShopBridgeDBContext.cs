using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge_WEBAPI.Models
{
    public class ShopBridgeDBContext:DbContext
    {
        public ShopBridgeDBContext(DbContextOptions<ShopBridgeDBContext> options):base(options)
        {
             
        }
        public DbSet<Inventory> Inventories { get; set; }
    }
}
