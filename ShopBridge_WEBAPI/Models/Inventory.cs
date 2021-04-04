using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge_WEBAPI.Models
{
    public class Inventory
    {
        [Key]
        public int item_Id { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string item_Name { get; set; }

        public string item_Desc { get; set; }

        public int item_Quantity { get; set; }
        public double item_Price { get; set; }

        public byte[] item_Image { get; set; }

        public bool itemAvailability { get; set; } 
            
        public DateTime item_AddedOn { get; set; }

        public DateTime item_UpdatedOn { get; set; }



    }
}
